#!/bin/env pwsh
#Requires -Version 7


<#
.SYNOPSIS
    Run script for tool selection confidence score calculation

.DESCRIPTION
    This script optionally builds the root project to ensure tools can be loaded dynamically,
    then builds the tool selection confidence score calculation application.
    It restores dependencies, builds the application in Release configuration, and runs it.

.EXAMPLE
    .\Run-ToolDescriptionConfidenceScore.ps1
    Builds and runs the application with default settings
    .\Run-ToolDescriptionConfidenceScore.ps1 -BuildAzureMcp
    Builds the root project, then runs the tool selection confidence score calculation app
#>

[CmdletBinding()]
param(
    [switch]$BuildAzureMcp
    
)

Set-StrictMode -Version 3.0
$ErrorActionPreference = 'Stop'

try {
    # Get absolute paths
    $repoRoot = Resolve-Path "$PSScriptRoot/../../../" | Select-Object -ExpandProperty Path
    $toolDir = Resolve-Path "$PSScriptRoot" | Select-Object -ExpandProperty Path

    # Build the whole Azure MCP Server project if needed
    if ($BuildAzureMcp
    ) {
        Write-Host "Building root project to enable dynamic tool loading..." -ForegroundColor Yellow
        & dotnet build "$repoRoot/AzureMcp.sln" --configuration Release
        if ($LASTEXITCODE -ne 0) {
            throw "Failed to build root project"
        }
        Write-Host "Root project build completed successfully!" -ForegroundColor Green
    }

    # Check for AzureMcp.exe before building
    $cliBinDir = Join-Path $repoRoot "core/src/AzureMcp.Cli/bin/Release"
    $exePath = Get-ChildItem -Path $cliBinDir -Filter "azmcp.exe" -Recurse -ErrorAction SilentlyContinue | Select-Object -First 1
    if (-not $exePath) {
        Write-Host "[ERROR] azmcp.exe not found in project. Please run this script again with the option -BuildAzureMcp." -ForegroundColor Red
        exit 1
    }
    
    Write-Host "Building and running tool selection confidence score calculation app..." -ForegroundColor Green
    Write-Host "Building application..." -ForegroundColor Yellow
    & dotnet build "$toolDir/ToolDescriptionConfidenceScore.csproj" --configuration Release

    if ($LASTEXITCODE -ne 0) {
        throw "Failed to build application"
    }

    Write-Host "Build completed successfully!" -ForegroundColor Green
    Write-Host "Running with: dotnet run" -ForegroundColor Cyan
    Push-Location $toolDir
    & dotnet run
    Pop-Location
}
catch {
    Write-Error "Build failed: $_"

    exit 1
}