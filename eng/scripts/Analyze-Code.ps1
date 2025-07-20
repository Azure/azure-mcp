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
    
    # Run tool selection analysis
    try {
        & "$PSScriptRoot/Test-ToolSelection.ps1"
        if ($LASTEXITCODE -ne 0) {
            Write-Host "❌ Tool selection analysis failed"
            exit 1
        }
    } catch {
        Write-Host "⚠️  Tool selection analysis encountered an issue: $($_.Exception.Message)"
        # Don't fail the entire analyze step for tool selection issues
        Write-Host "Continuing with other analysis steps..."
    }
}
finally {
    Pop-Location
}
