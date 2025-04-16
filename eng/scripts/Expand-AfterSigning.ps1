#!/bin/env pwsh
#Requires -Version 7

[CmdletBinding(DefaultParameterSetName='none')]
param(
    [string] $Path
)

. "$PSScriptRoot/../common/scripts/common.ps1"
$RepoRoot = $RepoRoot.Path.Replace('\', '/')

Write-Host "##[group] $Path Contents before:"
Get-ChildItem -Path $Path -File -Recurse | Select-Object -ExpandProperty FullName | Out-Host
Write-Host "##[endgroup]"

$archiveFiles = Get-ChildItem -Path $Path -Filter '*.zip' -Recurse

foreach ($archiveFile in $archiveFiles) {
    if ($archiveFile.Extension -eq '.zip') {
        Write-Host "Unpacking $archiveFile..." -ForegroundColor Yellow
        Expand-Archive -Path $archiveFile -DestinationPath $archiveFile.DirectoryName -Force
    }

    Remove-Item -Path $archiveFile -Force -ProgressAction SilentlyContinue
}

Write-Host "##[group] $Path Contents after:"
Get-ChildItem -Path $Path -File -Recurse | Select-Object -ExpandProperty FullName | Out-Host
Write-Host "##[endgroup]"