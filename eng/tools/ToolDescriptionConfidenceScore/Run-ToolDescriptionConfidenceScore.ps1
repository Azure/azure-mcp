#!/bin/env pwsh
#Requires -Version 7

<#
.SYNOPSIS
    Run script for tool selection confidence score calculation

.DESCRIPTION
    This script builds the tool selection confidence score calculation application.
    It restores dependencies, builds the application in Release configuration, and runs it.

.EXAMPLE
    .\Run-ToolDescriptionConfidenceScore.ps1
    Builds and runs the application with default settings
#>

[CmdletBinding()]
param()

Set-StrictMode -Version 3.0
$ErrorActionPreference = 'Stop'

try {
    Write-Host "Building and running tool selection confidence score calculation app..." -ForegroundColor Green

    # Build the application
    Write-Host "Building application..." -ForegroundColor Yellow

    & dotnet build --configuration Release

    if ($LASTEXITCODE -ne 0) {
        throw "Failed to build application"
    }

    Write-Host "Build completed successfully!" -ForegroundColor Green

    # Run the application
    Write-Host "Running with: dotnet run" -ForegroundColor Cyan

    & dotnet run

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
