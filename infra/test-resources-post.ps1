#Requires -Version 7

param(
    [string] $TenantId,
    [string] $TestApplicationId,
    [string] $ResourceGroupName,
    [string] $BaseName,
    [hashtable] $AdditionalParameters
)

$ErrorActionPreference = "Stop"

. "$PSScriptRoot/../eng/common/scripts/common.ps1"
$RepoRoot = $RepoRoot.Path.Replace('\', '/')

if($env:ARM_OIDC_TOKEN -and $TenantId -and $TestApplicationId) {
    Write-Host "Using OIDC token for Azure Powershell authentication"
    Connect-AzAccount -ServicePrincipal `
        -TenantId $TenantId `
        -ApplicationId $TestApplicationId `
        -FederatedToken $env:ARM_OIDC_TOKEN
}

$context = Get-AzContext

$testSettingsPath = "$RepoRoot/.testsettings.json"

# When using TME in CI, $context.Tenant.Name is empty so we use a map
# $context.Tenant.Name still works for local dev
$tenantName = switch($context.Tenant.Id) {
    '70a036f6-8e4d-4615-bad6-149c02e7720d' { 'TME Organization' }
    '72f988bf-86f1-41af-91ab-2d7cd011db47' { 'Microsoft' }
    default { $context.Tenant.Name }
}

$testSettings = [ordered]@{
    TenantId = $context.Tenant.Id
    TenantName = $tenantName
    SubscriptionId = $context.Subscription.Id
    SubscriptionName = $context.Subscription.Name
    ResourceGroupName = $ResourceGroupName
    ResourceBaseName = $BaseName
} | ConvertTo-Json

Write-Host "Creating test settings file at $testSettingsPath`:`n$testSettings"
$testSettings | Set-Content -Path $testSettingsPath -Force -NoNewLine

$areas = $AdditionalParameters.areas

if(!$areas) {
    $areas = @(Get-ChildItem "$RepoRoot/src/Areas" -Directory | Select-Object -ExpandProperty Name)
}

Write-Host "Processing post scripts for areas: $($areas -join ',')"

foreach($area in $areas)
{
    $areaPostScript = "$PSScriptRoot/services/$($area.ToLower())-post.ps1"
    if(Test-Path $areaPostScript) {
        Write-Host "Running post script: $areaPostScript"
        & $areaPostScript -ResourceGroupName $ResourceGroupName -BaseName $BaseName
    }
}
