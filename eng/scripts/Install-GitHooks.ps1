#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Installs git hooks from eng/scripts/git-hooks to .git/hooks
.DESCRIPTION
    This script copies git hook scripts from eng/scripts/git-hooks to .git/hooks
    and ensures they are executable on all platforms.
#>

[CmdletBinding()]
param()

$repoRoot = Resolve-Path (Join-Path $PSScriptRoot "../..")
$gitHooksSourceDir = Join-Path $repoRoot "eng/scripts/git-hooks"
$gitHooksTargetDir = Join-Path $repoRoot ".git/hooks"

# Ensure the .git/hooks directory exists
if (-not (Test-Path $gitHooksTargetDir)) {
    Write-Host "Creating $gitHooksTargetDir directory..."
    New-Item -Path $gitHooksTargetDir -ItemType Directory -Force | Out-Null
}

# Remove existing pre-commit hook
Remove-Item -Path $gitHooksTargetDir/pre-commit -Force -ErrorAction SilentlyContinue | Out-Null

# Copy hook files
Write-Host "Copying git hooks from $gitHooksSourceDir to $gitHooksTargetDir..."
$hookFiles = Get-ChildItem -Path $gitHooksSourceDir -File

foreach ($file in $hookFiles) {
    $targetFile = Join-Path $gitHooksTargetDir $file.Name

    Write-Host "  Copying $($file.Name)..."
    Copy-Item -Path $file.FullName -Destination $targetFile -Force

    # Make hook files executable
    Write-Host "  Setting executable permission for $($file.Name)..."
    if ($IsWindows) {
        & attrib +x $targetFile
    } else {
        & chmod +x $targetFile
    }
}

Write-Host "Git hooks installation complete!" -ForegroundColor Green
