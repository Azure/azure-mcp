param(
    [string] $ResourceGroupName,
    [string] $BaseName
)

$ErrorActionPreference = "Stop"

. "$PSScriptRoot/../../eng/common/scripts/common.ps1"

$context = New-AzStorageContext -StorageAccountName $BaseName -UseConnectedAccount

Write-Host "Uploading README.md to blob storage: $BaseName/bar" -ForegroundColor Yellow
Set-AzStorageBlobContent  -File "$RepoRoot/README.md" -Container "bar" -Blob "README.md" -Context $context -Force -ProgressAction SilentlyContinue
