#!/bin/env pwsh
#Requires -Version 7

<#
.SYNOPSIS
    Runs tool selection confidence analysis as part of CI pipeline
.DESCRIPTION
    This script runs the tool selection analysis to validate that the MCP server's
    tool selection algorithm works correctly. It's designed to be CI-friendly and
    will gracefully skip when required credentials are not available.
.PARAMETER SkipIfMissingCredentials
    Skip the test if Azure OpenAI credentials are not configured (default: true in CI)
#>

param(
    [switch]$SkipIfMissingCredentials
)

. "$PSScriptRoot/../common/scripts/common.ps1"

Push-Location $RepoRoot
try {
    $toolSelectionPath = "tests/Tools/PromptConfidenceScore"
    
    if (-not (Test-Path $toolSelectionPath)) {
        Write-Host "⏭️  Tool selection test not found at $toolSelectionPath - skipping"
        exit 0
    }
    
    Push-Location $toolSelectionPath
    try {
        # Check if we have the required test data files
        $hasToolsData = Test-Path "list-tools.json"
        $hasPromptsData = Test-Path "prompts.json"
        $hasApiKey = -not [string]::IsNullOrEmpty($env:TEXT_EMBEDDING_API_KEY) -or (Test-Path "api-key.txt")
        $hasEndpoint = -not [string]::IsNullOrEmpty($env:AOAI_ENDPOINT)
        
        # In CI mode, skip gracefully if no data or credentials are available
        if (-not $hasToolsData -and -not $hasPromptsData -and -not ($hasApiKey -and $hasEndpoint)) {
            if ($SkipIfMissingCredentials -or $env:BUILD_BUILDID -or $env:GITHUB_ACTIONS) {
                exit 0
            }
            # In non-CI mode, let Program.cs handle the error messaging with detailed help
        }
        
        # Build and run the tool
        dotnet build --configuration Release --verbosity quiet
        if ($LASTEXITCODE -ne 0) {
            Write-Host "❌ Failed to build tool selection analyzer"
            exit 1
        }
        
        # Run with CI flag to enable graceful degradation
        dotnet run --configuration Release --no-build -- --ci
        if ($LASTEXITCODE -ne 0) {
            Write-Host "❌ Tool selection analysis failed"
            exit 1
        }
        
        # Check if results were generated (but don't duplicate output from the tool itself)
        if (-not ((Test-Path "results.txt") -or (Test-Path "results.md"))) {
            Write-Host "⚠️  No results file generated"
        }
        
    } finally {
        Pop-Location
    }
    
} finally {
    Pop-Location
}
