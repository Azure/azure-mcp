#!/bin/env pwsh
#Requires -Version 7

. "$PSScriptRoot/../common/scripts/common.ps1"

Push-Location $RepoRoot
try {
    $solutionFile = Get-ChildItem -Path . -Filter *.sln | Select-Object -First 1
    dotnet format $solutionFile --verify-no-changes

    if ($LASTEXITCODE) {
        Write-Host "❌ dotnet format detected formatting issues."
        Write-Host "Please run 'dotnet format `"$solutionFile`"' to fix the issues and then try committing again."
        exit 1
    } else {
        Write-Host "✅ dotnet format did not detect any formatting issues."
    }
}
finally {
    Pop-Location
}
