[CmdletBinding()]
param(
    [string]$SubscriptionId,
    [string]$ResourceGroupName,
    [string]$BaseName,
    [string[]]$Areas,
    [int]$DeleteAfterHours = 12,
    [string]$Location,
    [switch]$Unique
)

. "$PSScriptRoot/../common/scripts/common.ps1"

$context = Get-AzContext
$account = $context.Account

function New-StringHash($string) {
    $hash = [System.Security.Cryptography.SHA1]::Create()
    $bytes = [System.Text.Encoding]::UTF8.GetBytes($string)
    $hashBytes = $hash.ComputeHash($bytes)
    return [BitConverter]::ToString($hashBytes) -replace '-', ''
}

$suffix = ($Unique ? [guid]::NewGuid().ToString() : (New-StringHash $account.Id)).ToLower().Substring(0, 8)
$suffix = $suffix.ToLower().Substring(0, 8)

if(!$BaseName) {
    $BaseName = "mcp$($suffix)"
}

if(!$ResourceGroupName) {
    $username = $account.Id.Split('@')[0]
    $ResourceGroupName = "$username-mcp$($suffix)"
}

Push-Location $RepoRoot
try {
    $armParameters = @{ areas = ($Areas ?? @()) }

    Write-Host "Deploying:`n  ResourceGroupName: `"$ResourceGroupName`"`n  BaseName: `"$BaseName`"`n  DeleteAfterHours: $DeleteAfterHours`n  Location: `"$(if ($Location) { $Location } else { 'default' })`"`n  ArmTemplateParameters: $(ConvertTo-Json $armParameters -Compress)"

    $deployParams = @{
        ResourceGroupName = $ResourceGroupName
        BaseName = $BaseName
        DeleteAfterHours = $DeleteAfterHours
        AdditionalParameters = $armParameters
    }
    
    if($SubscriptionId) {
        $deployParams.SubscriptionId = $SubscriptionId
    }
    
    if($Location) {
        $deployParams.Location = $Location
    }
    
    ./eng/common/TestResources/New-TestResources.ps1 @deployParams
}
finally {
    Pop-Location
}
