#!/bin/env pwsh
#Requires -Version 7

<#
.SYNOPSIS
    Runs tool selection confidence analysis as part of CI pipeline
.DESCRIPTION
    This script runs the tool selection analysis to validate that the Azure MCP Server's
    tool selection algorithm works correctly. It's designed to be CI-friendly and
    will gracefully skip when required credentials are not available.
.PARAMETER SkipIfMissingCredentials
    Skip the test if Azure OpenAI credentials are not configured (default: true in CI)
.PARAMETER UseToolsFile
    Use static JSON file for tools instead of dynamic loading
.PARAMETER UsePromptsFile
    Use custom prompts file (.md or .json format)
.PARAMETER OutputMarkdown
    Generate output in markdown format instead of plain text
.PARAMETER ValidateMode
    Run in validation mode to test specific tool descriptions
.PARAMETER ToolDescription
    Tool description to test (used with -ValidateMode)
.PARAMETER TestPrompts
    Array of test prompts (used with -ValidateMode)
.EXAMPLE
    # Standard CI run (graceful skip if no credentials)
    .\Test-ToolSelection.ps1
    
.EXAMPLE
    # Use custom tools file
    .\Test-ToolSelection.ps1 -UseToolsFile "my-tools.json"
    
.EXAMPLE
    # Generate markdown output
    .\Test-ToolSelection.ps1 -OutputMarkdown
    
.EXAMPLE
    # Validation mode - test specific tool description
    .\Test-ToolSelection.ps1 -ValidateMode -ToolDescription "Lists storage accounts" -TestPrompts @("show storage accounts", "list my storage")
    
.EXAMPLE
    # Full custom analysis
    .\Test-ToolSelection.ps1 -UseToolsFile "custom-tools.json" -UsePromptsFile "custom-prompts.md" -OutputMarkdown
#>

param(
    [switch]$SkipIfMissingCredentials,
    [string]$UseToolsFile,
    [string]$UsePromptsFile,
    [switch]$OutputMarkdown,
    [switch]$ValidateMode,
    [string]$ToolDescription,
    [string[]]$TestPrompts
)

$ErrorActionPreference = 'Stop'
. "$PSScriptRoot/../common/scripts/common.ps1"

$RepoRoot = $RepoRoot.Path.Replace('\', '/')

Push-Location $RepoRoot
try {
    $toolSelectionPath = "$RepoRoot/eng/tools/ToolDescriptionEvaluator"
    
    if (-not (Test-Path $toolSelectionPath)) {
        Write-Host "⏭️  Tool selection test not found at $toolSelectionPath - skipping"
        exit 0
    }
    
    Push-Location $toolSelectionPath
    try {
        # Check if we have the required sources for dynamic loading
        $hasSourceCode = Test-Path "$RepoRoot/src"
        $hasMarkdownPrompts = Test-Path "$RepoRoot/e2eTests/e2eTestPrompts.md"
        
        # Check if we have fallback test data files
        $hasToolsData = Test-Path "tools.json"
        $hasPromptsData = Test-Path "prompts.json"
        $hasApiKey = -not [string]::IsNullOrEmpty($env:TEXT_EMBEDDING_API_KEY)
        $hasEndpoint = -not [string]::IsNullOrEmpty($env:AOAI_ENDPOINT)
        
        # In validation mode, check for required parameters
        if ($ValidateMode) {
            if ([string]::IsNullOrEmpty($ToolDescription) -or $TestPrompts.Count -eq 0) {
                Write-Host "❌ Validation mode requires -ToolDescription and at least one -TestPrompts"
                Write-Host "Example: -ValidateMode -ToolDescription 'Lists storage accounts' -TestPrompts @('show storage accounts', 'list my storage')"
                exit 1
            }
            
            # Validation mode requires credentials
            if (-not ($hasApiKey -and $hasEndpoint)) {
                if ($SkipIfMissingCredentials -or $env:BUILD_BUILDID -or $env:GITHUB_ACTIONS) {
                    Write-Host "⏭️  Skipping validation mode in CI - Azure OpenAI credentials not available"
                    exit 0
                }
                Write-Host "❌ Validation mode requires AOAI_ENDPOINT and TEXT_EMBEDDING_API_KEY environment variables"
                exit 1
            }
        }
        else {
            # In CI mode, skip gracefully if no sources or credentials are available
            if (-not ($hasSourceCode -or $hasToolsData) -and -not ($hasMarkdownPrompts -or $hasPromptsData) -and -not ($hasApiKey -and $hasEndpoint)) {
                if ($SkipIfMissingCredentials -or $env:BUILD_BUILDID -or $env:GITHUB_ACTIONS) {
                    Write-Host "⏭️  Skipping tool selection analysis in CI - required data sources or credentials not available"
                    exit 0
                }
                # In non-CI mode, let Program.cs handle the error messaging with detailed help
            }
        }
        
        # Resolve custom file paths relative to repo root if they're not absolute
        $resolvedToolsFile = $null
        $resolvedPromptsFile = $null
        
        if (-not [string]::IsNullOrEmpty($UseToolsFile)) {
            if ([System.IO.Path]::IsPathRooted($UseToolsFile)) {
                $resolvedToolsFile = $UseToolsFile
            } else {
                $resolvedToolsFile = "$RepoRoot/$UseToolsFile"
            }
            
            if (-not (Test-Path $resolvedToolsFile)) {
                Write-Host "❌ Custom tools file not found: $resolvedToolsFile"
                exit 1
            }
        }
        
        if (-not [string]::IsNullOrEmpty($UsePromptsFile)) {
            if ([System.IO.Path]::IsPathRooted($UsePromptsFile)) {
                $resolvedPromptsFile = $UsePromptsFile
            } else {
                $resolvedPromptsFile = "$RepoRoot/$UsePromptsFile"
            }
            
            if (-not (Test-Path $resolvedPromptsFile)) {
                Write-Host "❌ Custom prompts file not found: $resolvedPromptsFile"
                exit 1
            }
        }
        
        # Show configuration info
        Write-Host "🔍 Running tool selection analysis..."
        if ($ValidateMode) {
            Write-Host "📝 Mode: Validation"
            Write-Host "🔧 Tool Description: $ToolDescription"
            Write-Host "💬 Test Prompts: $($TestPrompts -join ', ')"
        } else {
            Write-Host "📝 Mode: Full Analysis"
            if ($resolvedToolsFile) {
                Write-Host "🔧 Tools File: $resolvedToolsFile"
            } else {
                Write-Host "🔧 Tools Source: Dynamic loading"
            }
            if ($resolvedPromptsFile) {
                Write-Host "💬 Prompts File: $resolvedPromptsFile"
            } else {
                Write-Host "💬 Prompts Source: $RepoRoot/e2eTests/e2eTestPrompts.md"
            }
            Write-Host "📄 Output Format: $(if ($OutputMarkdown) { 'Markdown' } else { 'Plain Text' })"
        }
        
        # Build and run the tool
        dotnet build --configuration Release --verbosity quiet
        if ($LASTEXITCODE -ne 0) {
            Write-Host "❌ Failed to build tool selection analyzer"
            exit 1
        }
        
        # Prepare run arguments
        $runArgs = @("--configuration", "Release", "--no-build", "--")
        
        # Add CI flag for graceful degradation (except in validation mode where we want explicit feedback)
        if (-not $ValidateMode) {
            $runArgs += "--ci"
        }
        
        # Add validation mode arguments
        if ($ValidateMode) {
            $runArgs += "--validate"
            $runArgs += "--tool-description"
            $runArgs += $ToolDescription
            
            foreach ($prompt in $TestPrompts) {
                $runArgs += "--prompt"
                $runArgs += $prompt
            }
        }
        else {
            # Add file override arguments for full analysis mode
            if (-not [string]::IsNullOrEmpty($resolvedToolsFile)) {
                $runArgs += "--tools-file"
                $runArgs += $resolvedToolsFile
            }
            
            if (-not [string]::IsNullOrEmpty($resolvedPromptsFile)) {
                $runArgs += "--prompts-file"
                $runArgs += $resolvedPromptsFile
            }
            
            # Add markdown output flag
            if ($OutputMarkdown) {
                $runArgs += "--markdown"
            }
        }
        
        dotnet run @runArgs
        if ($LASTEXITCODE -ne 0) {
            Write-Host "❌ Tool selection analysis failed"
            exit 1
        }
        
        # Check if results were generated (only for full analysis mode)
        if (-not $ValidateMode) {
            $expectedResultFile = if ($OutputMarkdown) { "results.md" } else { "results.txt" }
            if (-not (Test-Path $expectedResultFile)) {
                Write-Host "⚠️  Expected result file '$expectedResultFile' was not generated"
            }
        }
        
    } finally {
        Pop-Location
    }
    
} finally {
    Pop-Location
}
