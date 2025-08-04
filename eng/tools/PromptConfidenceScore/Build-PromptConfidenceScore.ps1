#!/bin/env pwsh
#Requires -Version 7

<#
.SYNOPSIS
    Build script for tool selection confidence score calculation

.DESCRIPTION
    This script builds the tool selection confidence score calculation application.
    It restores dependencies, builds the application in Release configuration,
    and optionally runs tests if they exist.

.EXAMPLE
    .\Build-PromptConfidenceScore.ps1
    Builds the application with default settings
#>

[CmdletBinding()]
param()

Set-StrictMode -Version 3.0
$ErrorActionPreference = 'Stop'

try {
    Write-Host "Building tool selection confidence score calculation app..." -ForegroundColor Green

    # Restore dependencies
    Write-Host "Restoring dependencies..." -ForegroundColor Yellow
    & dotnet restore
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to restore dependencies"
    }

    # Build the application
    Write-Host "Building application..." -ForegroundColor Yellow
    & dotnet build --configuration Release
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to build application"
    }

    Write-Host "Build completed successfully!" -ForegroundColor Green
    Write-Host "Run with: dotnet run" -ForegroundColor Cyan

    # Optional: Run tests if they exist
    $testFiles = Get-ChildItem -Path . -Filter "*Test*" -ErrorAction SilentlyContinue
    if ($testFiles) {
        Write-Host "Running tests..." -ForegroundColor Yellow
        & dotnet test
        if ($LASTEXITCODE -ne 0) {
            throw "Tests failed"
        }
    }
}
catch {
    Write-Error "Build failed: $_"
    exit 1
}
