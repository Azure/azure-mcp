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

    # Locate azmcp CLI artifact (platform & build-type agnostic)
    $cliBinDir = Join-Path $repoRoot "core/src/AzureMcp.Cli/bin/Release"
    $platformIsWindows = [System.Runtime.InteropServices.RuntimeInformation]::IsOSPlatform([System.Runtime.InteropServices.OSPlatform]::Windows)

    # Acceptable artifact name candidates in precedence order
    $candidateNames = if ($platformIsWindows) { @('azmcp.exe','azmcp','azmcp.dll') } else { @('azmcp','azmcp.dll') }
    $cliArtifact = $null
    foreach ($name in $candidateNames) {
        $found = Get-ChildItem -Path $cliBinDir -Filter $name -Recurse -ErrorAction SilentlyContinue | Where-Object { -not $_.PSIsContainer } | Select-Object -First 1
        if ($found) { $cliArtifact = $found; break }
    }
    if (-not $cliArtifact) {
        # Broad fallback to help user diagnose
        $any = Get-ChildItem -Path $cliBinDir -Filter 'azmcp*' -Recurse -ErrorAction SilentlyContinue | Where-Object { -not $_.PSIsContainer }
        if ($any) {
            Write-Host "[WARNING] Located the following azmcp artifacts but none matched expected names: $($any | Select-Object -ExpandProperty Name -Join ', ')" -ForegroundColor Yellow
        }
        Write-Host "[ERROR] No azmcp CLI artifact found in Release output. Try rerunning with -BuildAzureMcp or ensure Release build completed." -ForegroundColor Red
        exit 1
    }
    Write-Host "Discovered CLI artifact: $($cliArtifact.FullName)" -ForegroundColor Green

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