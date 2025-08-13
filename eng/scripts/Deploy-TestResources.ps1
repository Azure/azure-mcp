param(
    [Parameter(Mandatory=$true, ParameterSetName='SingleArea')]
    [string]$Area,  # Deprecated - use -Areas instead
    
    [Parameter(Mandatory=$true, ParameterSetName='MultipleAreas')]
    [string[]]$Areas,
    
    [Parameter(Mandatory=$true, ParameterSetName='AllAreas')]
    [switch]$All,
    
    [string]$SubscriptionId,
    [string]$ResourceGroupName,
    [string]$BaseName,
    [int]$DeleteAfterHours = 12,
    [switch]$Unique
)

$ErrorActionPreference = 'Stop'
. "$PSScriptRoot/../common/scripts/common.ps1"

function Get-AvailableAreas {
    <#
    .SYNOPSIS
    Discovers all available areas that have test resources.
    #>
    $areasWithTestResources = @()
    $areasDir = "$RepoRoot/areas"
    
    if (Test-Path $areasDir) {
        Get-ChildItem -Path $areasDir -Directory | ForEach-Object {
            $areaName = $_.Name.ToLower()
            $testResourcesPath = "$($_.FullName)/tests/test-resources.bicep"
            if (Test-Path $testResourcesPath) {
                $areasWithTestResources += $areaName
            }
        }
    }
    
    return $areasWithTestResources | Sort-Object
}

function Test-AreaExists {
    <#
    .SYNOPSIS
    Validates that the specified area exists and has test resources.
    #>
    param(
        [Parameter(Mandatory=$true)]
        [string]$AreaName
    )
    
    $areaPath = "$RepoRoot/areas/$($AreaName.ToLower())"
    $testResourcesPath = "$areaPath/tests/test-resources.bicep"
    
    if (!(Test-Path $areaPath)) {
        Write-Error "Area '$AreaName' does not exist at path '$areaPath'."
        return $false
    }
    
    if (!(Test-Path $testResourcesPath)) {
        Write-Error "Area '$AreaName' does not have test resources. Expected bicep template at '$testResourcesPath'."
        return $false
    }
    
    return $true
}

function Get-AreasToProcess {
    <#
    .SYNOPSIS
    Determines which areas to process based on the provided parameters.
    #>
    param(
        [string]$Area,
        [string[]]$Areas,
        [switch]$All
    )
    
    if ($Area) {
        # Backward compatibility - show deprecation warning
        Write-Warning "The -Area parameter is deprecated. Please use -Areas @('$Area') or -All instead."
        return @($Area.ToLower())
    }
    elseif ($All) {
        $availableAreas = Get-AvailableAreas
        if ($availableAreas.Count -eq 0) {
            Write-Error "No areas with test resources found."
            return @()
        }
        Write-Host "Deploying test resources for all available areas: $($availableAreas -join ', ')" -ForegroundColor Cyan
        return $availableAreas
    }
    else {
        # Multiple areas specified
        $processAreas = @()
        foreach ($areaName in $Areas) {
            $normalizedArea = $areaName.ToLower()
            if (Test-AreaExists -AreaName $normalizedArea) {
                $processAreas += $normalizedArea
            } else {
                throw "Area validation failed for '$areaName'"
            }
        }
        return $processAreas
    }
}

function Deploy-AreaTestResources {
    <#
    .SYNOPSIS
    Deploys test resources for a specific area.
    #>
    param(
        [Parameter(Mandatory=$true)]
        [string]$AreaName,
        [string]$SubscriptionId,
        [string]$ResourceGroupName,
        [string]$BaseName,
        [int]$DeleteAfterHours,
        [switch]$Unique
    )
    
    $testResourcesDirectory = Resolve-Path -Path "$RepoRoot/areas/$AreaName/tests" -ErrorAction SilentlyContinue
    $bicepPath = "$testResourcesDirectory/test-resources.bicep"
    
    if(!(Test-Path -Path $bicepPath)) {
        Write-Error "Test resources bicep template '$bicepPath' does not exist."
        return $false
    }

    # Base the user hash on the user's account ID, subscription, and area being deployed to
    if($Unique) {
        $hash = [guid]::NewGuid().ToString()
    } else {
        $hash = (New-StringHash $account.Id, $SubscriptionId, $AreaName)
    }

    $suffix = $hash.ToLower().Substring(0, 8)

    $areaBaseName = if($BaseName) { $BaseName } else { "mcp$($suffix)" }
    $areaResourceGroupName = if($ResourceGroupName) { $ResourceGroupName } else { "$username-mcp$($suffix)" }

    Write-Host @"

=== Deploying Area: $($AreaName.ToUpper()) ===
    SubscriptionId: '$SubscriptionId'
    SubscriptionName: '$subscriptionName'
    ResourceGroupName: '$areaResourceGroupName'
    BaseName: '$areaBaseName'
    DeleteAfterHours: $DeleteAfterHours
    TestResourcesDirectory: '$testResourcesDirectory'
"@ -ForegroundColor Green

    try {
        ./eng/common/TestResources/New-TestResources.ps1 `
            -SubscriptionId $SubscriptionId `
            -ResourceGroupName $areaResourceGroupName `
            -BaseName $areaBaseName `
            -TestResourcesDirectory $testResourcesDirectory `
            -DeleteAfterHours $DeleteAfterHours `
            -Force
        
        Write-Host "✅ Successfully deployed test resources for area '$AreaName'" -ForegroundColor Green
        return $true
    }
    catch {
        Write-Error "❌ Failed to deploy test resources for area '$AreaName': $($_.Exception.Message)"
        return $false
    }
}

function New-StringHash([string[]]$strings) {
    $string = $strings -join ' '
    $hash = [System.Security.Cryptography.SHA1]::Create()
    $bytes = [System.Text.Encoding]::UTF8.GetBytes($string)
    $hashBytes = $hash.ComputeHash($bytes)
    return [BitConverter]::ToString($hashBytes) -replace '-', ''
}

# Determine which areas to process
$areasToProcess = Get-AreasToProcess -Area $Area -Areas $Areas -All:$All

if ($areasToProcess.Count -eq 0) {
    Write-Error "No valid areas to process."
    exit 1
}

# Set up Azure context
if($SubscriptionId) {
    Select-AzSubscription -Subscription $SubscriptionId | Out-Null
    $context = Get-AzContext
} else {
    # We don't want New-TestResources to conditionally pick a subscription for us
    # If the user didn't specify a subscription, we explicitly use the current context's subscription
    $context = Get-AzContext
    $SubscriptionId = $context.Subscription.Id
}
$subscriptionName = $context.Subscription.Name
$account = $context.Account

# Calculate username for default resource group naming
$username = $account.Id.Split('@')[0].ToLower()

Push-Location $RepoRoot
try {
    $successCount = 0
    $totalCount = $areasToProcess.Count
    
    Write-Host "`n🚀 Starting deployment of test resources for $totalCount area(s)" -ForegroundColor Cyan
    
    for ($i = 0; $i -lt $areasToProcess.Count; $i++) {
        $currentArea = $areasToProcess[$i]
        $progressPercent = [int](($i / $totalCount) * 100)
        
        Write-Progress -Activity "Deploying Test Resources" -Status "Processing area $($i + 1) of ${totalCount}: $currentArea" -PercentComplete $progressPercent
        
        $success = Deploy-AreaTestResources `
            -AreaName $currentArea `
            -SubscriptionId $SubscriptionId `
            -ResourceGroupName $ResourceGroupName `
            -BaseName $BaseName `
            -DeleteAfterHours $DeleteAfterHours `
            -Unique:$Unique
        
        if ($success) {
            $successCount++
        }
    }
    
    Write-Progress -Activity "Deploying Test Resources" -Completed
    
    Write-Host "`n📊 Deployment Summary:" -ForegroundColor Cyan
    Write-Host "   Total areas processed: $totalCount" -ForegroundColor White
    Write-Host "   Successful deployments: $successCount" -ForegroundColor Green
    Write-Host "   Failed deployments: $($totalCount - $successCount)" -ForegroundColor Red
    
    if ($successCount -eq $totalCount) {
        Write-Host "`n🎉 All deployments completed successfully!" -ForegroundColor Green
    } else {
        Write-Host "`n⚠️  Some deployments failed. Check the output above for details." -ForegroundColor Yellow
        exit 1
    }
}
finally {
    Pop-Location
}
