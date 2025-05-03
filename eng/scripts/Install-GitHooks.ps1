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

# Copy hook files
Write-Host "Copying git hooks from $gitHooksSourceDir to $gitHooksTargetDir..."
$hookFiles = Get-ChildItem -Path $gitHooksSourceDir -File

foreach ($file in $hookFiles) {
    $targetFile = Join-Path $gitHooksTargetDir $file.Name

    # compare contents of the file to see if it needs to be copied
    if (Test-Path $targetFile) {
        $sourceContent = Get-Content -Path $file.FullName -Raw
        $targetContent = Get-Content -Path $targetFile -Raw
        if ($sourceContent -eq $targetContent) {
            Write-Host "  $($file.Name) up to date"
            continue
        } else {
            Write-Host "  Updating $($file.Name) and saving backup to $targetFile.old"
            # Create a backup of the existing file
            Move-Item -Path $targetFile -Destination "$targetFile.old" -Force
        }
    } else {
        Write-Host "  Adding $($file.Name)"
    }

    Copy-Item -Path $file.FullName -Destination $targetFile -Force

    # Make hook files executable
    Write-Host "  Setting executable permission for $($file.Name)..."
    if ($IsWindows) {
        & attrib +x $targetFile
    } else {
        & chmod +x $targetFile
    }
}

# TODO: Remove after 5/9/2025
# remove existing pre-commit hook if it matches the pre-push hook
$preCommitPath = Join-Path $gitHooksTargetDir "pre-commit"
$prePushPath = Join-Path $gitHooksTargetDir "pre-push"
if(Test-Path $preCommitPath) {
    $prePushContent = Get-Content -Path $prePushPath -Raw
    $preCommitContent = Get-Content -Path $preCommitPath -Raw
    if($prePushContent -eq $preCommitContent) {
        Write-Host "Removing stale pre-commit hook"
        Remove-Item -Path $preCommitPath -Force -ErrorAction SilentlyContinue | Out-Null
    }
}
