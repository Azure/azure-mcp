#!/bin/env pwsh
#Requires -Version 7

param(
    [string]$Configuration = "Release",
    [string]$Runtime
)

$ErrorActionPreference = "Stop"

. "$PSScriptRoot/../common/scripts/common.ps1"
$root = $RepoRoot.Path.Replace('\', '/')
$projectFile = "$root/src/AzureMcp.csproj"
$reportPath = "$root/artifacts/aot-compact-report.txt"

if (-not $Runtime) {
    $runtime = [System.Runtime.InteropServices.RuntimeInformation]::RuntimeIdentifier
} else {
    $runtime = $Runtime
}

Write-Host "Running AOT compatibility (trimming) analysis for runtime: $runtime ..."

$artifactsDir = "$root/artifacts"
if (!(Test-Path $artifactsDir)) {
    New-Item -ItemType Directory -Path $artifactsDir | Out-Null
}

$projectObjDir = Join-Path (Split-Path $projectFile) "obj"
if (Test-Path $projectObjDir) {
    Write-Host "Deleting project obj directory: $projectObjDir"
    Remove-Item -Path $projectObjDir -Recurse -Force -ErrorAction SilentlyContinue
}

$publishArgs = @(
    'publish', $projectFile,
    '--configuration', $Configuration,
    '--runtime', $runtime,
    '--self-contained', 'true',
    '/p:PublishTrimmed=true',
    '/p:TrimmerSingleWarn=false'
)

Write-Host "Executing: dotnet $($publishArgs -join ' ')"

$output = & dotnet @publishArgs 2>&1
$output | Out-File -FilePath $reportPath -Encoding utf8

if ($output -match 'warning IL') {
    Write-Host "AOT compatibility analysis complete. See $reportPath for detailed warnings."
    Write-Host "\nSummary of warnings:"
    ($output | Select-String 'warning IL') | ForEach-Object { Write-Host $_.Line -ForegroundColor Yellow }
    exit 1
} else {
    Write-Host "AOT compatibility analysis complete. No trimmer/AOT warnings found. See $reportPath."
    exit 0
}
