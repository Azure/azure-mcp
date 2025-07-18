param(
    [string] $ResourceGroupName,
    [string] $BaseName
)

$ErrorActionPreference = "Stop"

. "$PSScriptRoot/../../eng/common/scripts/common.ps1"

$storageAccountName = "$($BaseName)mon"
$containerName = 'foo'
$context = New-AzStorageContext -StorageAccountName $storageAccountName -UseConnectedAccount
Write-Host "Uploading sample files to blob storage: $storageAccountName/$containerName" -ForegroundColor Yellow
$files = Get-ChildItem -Path "$PSScriptRoot/../samples" -Filter '*.md'
foreach ($file in $files) {
    Set-AzStorageBlobContent -File $file.FullName -Container $containerName -Blob $file.Name -Context $context -Force -ProgressAction SilentlyContinue | Out-Null
}
