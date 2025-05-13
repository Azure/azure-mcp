#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Disable git hooks from in .git/hooks by renaming them
#>

[CmdletBinding()]
param()

$repoRoot = Resolve-Path (Join-Path $PSScriptRoot "../..")
$gitHooksTargetDir = Join-Path $repoRoot ".git/hooks"

if (-not (Test-Path $gitHooksTargetDir)) {
    Write-Host "Git hooks directory not found. Skipping hook removal."
    exit 0
}

# Copy hook files
Write-Host "Disabling git hooks in $gitHooksTargetDir..."

$hookFiles = Get-ChildItem -Path $gitHooksTargetDir -File
| Where-Object { $_.Extension -eq '' }

foreach ($file in $hookFiles) {
    Write-Host "  Renaming $($file.Name) to $($file.Name).old"
    # Create a backup of the existing file
    Move-Item -Path $file -Destination "$file.old" -Force
}
