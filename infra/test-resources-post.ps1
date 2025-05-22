#Requires -Version 7

param(
    [string] $TenantId,
    [string] $TestApplicationId,
    [string] $ResourceGroupName,
    [string] $BaseName
)

$ErrorActionPreference = "Stop"

. "$PSScriptRoot/../eng/common/scripts/common.ps1"
$RepoRoot = $RepoRoot.Path.Replace('\', '/')

Connect-AzAccount -ServicePrincipal `
    -TenantId $TenantId `
    -ApplicationId $TestApplicationId `
    -FederatedToken $env:ARM_OIDC_TOKEN

$context = Get-AzContext

$testSettingsPath = "$RepoRoot/.testsettings.json"

# When using TME in CI, $context.Tenant.Name is empty so we use a map
# $context.Tenant.Name still works for local dev
$tenantName = switch($context.Tenant.Id) {
    '70a036f6-8e4d-4615-bad6-149c02e7720d' { 'TME01' }
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

$servicePostScripts = Get-ChildItem -Path "$PSScriptRoot/services" -Filter "*-post.ps1" -Recurse -File
foreach ($script in $servicePostScripts) {
    Write-Host "Running post script: $($script.FullName)"
    & $script.FullName -ResourceGroupName $ResourceGroupName -BaseName $BaseName
}
