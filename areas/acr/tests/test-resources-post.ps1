param(
    [string] $TenantId,
    [string] $TestApplicationId,
    [string] $ResourceGroupName,
    [string] $BaseName,
    [hashtable] $DeploymentOutputs
)

$ErrorActionPreference = "Stop"
$ProgressPreference = "SilentlyContinue"

. "$PSScriptRoot/../../../eng/common/scripts/common.ps1"
. "$PSScriptRoot/../../../eng/scripts/helpers/TestResourcesHelpers.ps1"

$testSettings = New-TestSettings @PSBoundParameters -OutputPath $PSScriptRoot

# $testSettings contains:
# - TenantId
# - TenantName
# - SubscriptionId
# - SubscriptionName
# - ResourceGroupName
# - ResourceBaseName

# $DeploymentOutputs keys are all UPPERCASE

# Add your post deployment steps here
# Seed a deterministic repository in the test ACR so live tests can validate existence

function Ensure-AzModules {
    # Make sure Az.Accounts and Az.ContainerRegistry are available; try to install if missing
    $hasAccounts = [bool](Get-Module -ListAvailable Az.Accounts)
    $hasAcr = [bool](Get-Module -ListAvailable Az.ContainerRegistry)
    if ($hasAccounts -and $hasAcr) { return $true }

    Write-Host "Required Az modules missing. Attempting installation (CurrentUser scope)..."
    try {
        if (-not $hasAccounts) { Install-Module -Name Az.Accounts -Scope CurrentUser -Force -AllowClobber -ErrorAction Stop }
        if (-not $hasAcr) { Install-Module -Name Az.ContainerRegistry -Scope CurrentUser -Force -AllowClobber -ErrorAction Stop }
    }
    catch {
        Write-Warning "Failed to install specific Az modules: $($_.Exception.Message). Attempting to install Az meta-module..."
        try { Install-Module -Name Az -Scope CurrentUser -Force -AllowClobber -ErrorAction Stop }
        catch { Write-Warning "Failed to install Az modules: $($_.Exception.Message)"; return $false }
    }

    return ([bool](Get-Module -ListAvailable Az.Accounts) -and [bool](Get-Module -ListAvailable Az.ContainerRegistry))
}

function Test-IsCi {
    return ($env:TF_BUILD -eq 'True' -or $env:GITHUB_ACTIONS -eq 'true' -or $env:CI -eq 'true')
}

function Connect-AzPowerShell {
    param(
        [Parameter(Mandatory = $false)] [string] $SubscriptionId,
        [Parameter(Mandatory = $false)] [string] $TenantIdParam
    )

    # Ensure required Az modules are available
    $azAccounts = Get-Module -ListAvailable Az.Accounts
    $azAcr = Get-Module -ListAvailable Az.ContainerRegistry
    if (-not $azAccounts -or -not $azAcr) {
        Write-Host "Az PowerShell modules not available (Az.Accounts/Az.ContainerRegistry)."
        return $false
    }

    $isCi = Test-IsCi
    $connected = $false

    try {
        # Reuse session if already connected
        Get-AzContext -ErrorAction Stop | Out-Null
        $connected = $true
    }
    catch {
        $connected = $false
    }

    if (-not $connected) {
        # 1) GitHub OIDC (federated token)
        if (-not $connected -and $env:AZURE_FEDERATED_TOKEN_FILE -and $env:AZURE_CLIENT_ID -and $env:AZURE_TENANT_ID) {
            try {
                $token = Get-Content -Raw -ErrorAction Stop $env:AZURE_FEDERATED_TOKEN_FILE
                if ($token) {
                    Write-Host "Connecting to Azure with Az PowerShell using federated credentials (OIDC)..."
                    Connect-AzAccount -ServicePrincipal -Tenant $env:AZURE_TENANT_ID -ClientId $env:AZURE_CLIENT_ID -FederatedToken $token -ErrorAction Stop | Out-Null
                    $connected = $true
                }
            }
            catch { $connected = $false }
        }

        # 2) Service principal (client secret)
        if (-not $connected -and $env:AZURE_CLIENT_ID -and $env:AZURE_CLIENT_SECRET -and $env:AZURE_TENANT_ID) {
            try {
                Write-Host "Connecting to Azure with Az PowerShell using service principal (client secret)..."
                $secure = ConvertTo-SecureString $env:AZURE_CLIENT_SECRET -AsPlainText -Force
                $cred = New-Object System.Management.Automation.PSCredential($env:AZURE_CLIENT_ID, $secure)
                Connect-AzAccount -ServicePrincipal -Tenant $env:AZURE_TENANT_ID -Credential $cred -ErrorAction Stop | Out-Null
                $connected = $true
            }
            catch { $connected = $false }
        }

        # 3) Managed identity
        if (-not $connected) {
            try {
                Write-Host "Connecting to Azure with Az PowerShell using managed identity..."
                Connect-AzAccount -Identity -ErrorAction Stop | Out-Null
                $connected = $true
            }
            catch { $connected = $false }
        }

        # 4) Interactive (not in CI)
        if (-not $connected -and -not $isCi) {
            try {
                $tenantArgs = if ([string]::IsNullOrWhiteSpace($TenantIdParam)) { @() } else { @('-Tenant', $TenantIdParam) }
                Write-Host "Starting interactive Azure login via Az PowerShell..."
                Connect-AzAccount @tenantArgs -ErrorAction Stop | Out-Null
                $connected = $true
            }
            catch { $connected = $false }
        }
    }

    if ($connected -and $SubscriptionId) {
        try { Set-AzContext -Subscription $SubscriptionId -ErrorAction Stop | Out-Null } catch { $connected = $false }
    }

    if (-not $connected) { Write-Host "Az PowerShell authentication was not established." }
    return $connected
}

function Import-AcrImageAz {
    param(
        [Parameter(Mandatory = $true)] [string] $ResourceGroupName,
        [Parameter(Mandatory = $true)] [string] $RegistryName
    )

    try {
        Write-Host "Importing testrepo:latest into $RegistryName from mcr.microsoft.com via Az PowerShell..."
        Import-AzContainerRegistryImage -ResourceGroupName $ResourceGroupName -RegistryName $RegistryName -SourceImage "hello-world:latest" -SourceRegistryUri "mcr.microsoft.com" -Image "testrepo:latest" -ErrorAction Stop | Out-Null
        Write-Host "Imported testrepo:latest from mcr.microsoft.com/hello-world:latest"
        return $true
    }
    catch {
        $msg = $_.Exception.Message
        if ($msg -match 'already exists' -or $msg -match 'Conflict') {
            Write-Host "Image already exists; proceeding."
            return $true
        }
        Write-Warning "MCR import via Az PowerShell failed: $msg. Attempting Docker Hub alpine:latest..."
    }

    try {
        Import-AzContainerRegistryImage -ResourceGroupName $ResourceGroupName -RegistryName $RegistryName -SourceImage "alpine:latest" -SourceRegistryUri "docker.io" -Image "testrepo:latest" -ErrorAction Stop | Out-Null
        Write-Host "Imported testrepo:latest from docker.io/library/alpine:latest"
        return $true
    }
    catch {
        $msg = $_.Exception.Message
        if ($msg -match 'already exists' -or $msg -match 'Conflict') {
            Write-Host "Image already exists; proceeding."
            return $true
        }
        Write-Warning "Docker Hub import via Az PowerShell failed: $msg."
        return $false
    }
}

try {
    $subscriptionId = $testSettings.SubscriptionId
    $registryName = $testSettings.ResourceBaseName
    $rgName = $testSettings.ResourceGroupName

    if (-not $subscriptionId -or -not $registryName) {
        throw "Missing SubscriptionId or ResourceBaseName in test settings."
    }

    # Ensure Az modules are available and loaded
    if (-not (Ensure-AzModules)) { Write-Warning "Az modules unavailable; skipping seeding."; return }
    try { Import-Module Az.Accounts -ErrorAction Stop; Import-Module Az.ContainerRegistry -ErrorAction Stop } catch { Write-Warning "Failed to import Az modules: $($_.Exception.Message)"; return }

    # Login and import using Az PowerShell only
    $loginOk = Connect-AzPowerShell -SubscriptionId $subscriptionId -TenantIdParam $TenantId
    if (-not $loginOk) { return }

    Write-Host "Seeding ACR registry '$registryName' with repo 'testrepo:latest' via Az PowerShell image import..."
    [void](Import-AcrImageAz -ResourceGroupName $rgName -RegistryName $registryName)
}
catch {
    Write-Warning "ACR seeding step failed: $($_.Exception.Message). Live tests may skip repository assertions."
}
