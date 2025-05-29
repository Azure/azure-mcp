#!/usr/bin/env pwsh
#Requires -Version 7

[CmdletBinding()]
param(
    [string] $TestResultsPath,
    [switch] $Live,
    [switch] $OpenReport = $false
)

$ErrorActionPreference = 'Stop'
. "$PSScriptRoot/../common/scripts/common.ps1"

$RepoRoot = $RepoRoot.Path.Replace('\', '/')

if (!$TestResultsPath) {
    $TestResultsPath = "$RepoRoot/.work/testResults"
}

# Clean previous results
Remove-Item -Recurse -Force $TestResultsPath -ErrorAction SilentlyContinue

Remove-Item "$RepoRoot/tests/xunit.runner.json" -Force
Write-Output "Deleted existing xunit.runner.json file"
Rename-Item "$RepoRoot/tests/xunit.runner.ci.json" -NewName "xunit.runner.json"
Write-Output "Renamed xunit.runner.ci.json to xunit.runner.json"
$xunitJson = Get-Content "$RepoRoot/tests/xunit.runner.json" | ConvertFrom-Json
Write-Output $xunitJson

# Run tests with coverage
$filter = $Live ? "Category~Live" : "Category!~Live"

Invoke-LoggedCommand ("dotnet test '$RepoRoot/tests/AzureMcp.Tests.csproj'" +
  " --collect:'XPlat Code Coverage'" +
  " --filter '$filter'" +
  " --results-directory '$TestResultsPath'" +
  " --logger 'trx'")

# Find the coverage file
$coverageFile = Get-ChildItem -Path $TestResultsPath -Recurse -Filter "coverage.cobertura.xml"
| Where-Object { $_.FullName.Replace('\','/') -notlike "*/in/*" }
| Select-Object -First 1

if (-not $coverageFile) {
    Write-Error "No coverage file found!"
    exit 1
}

if($Live) {
    exit 0
}

if ($env:TF_BUILD) {
    # Write the path to the cover file to a pipeline variable
    Write-Host "##vso[task.setvariable variable=CoverageFile]$($coverageFile.FullName)"
} else {
    # Ensure reportgenerator tool is installed
    if (-not (Get-Command reportgenerator -ErrorAction SilentlyContinue)) {
        Write-Host "Installing reportgenerator tool..."
        dotnet tool install -g dotnet-reportgenerator-globaltool
    }

    # Generate reports
    Write-Host "Generating coverage reports..."

    $reportDirectory = "$TestResultsPath/coverageReport"
    Invoke-LoggedCommand ("reportgenerator" +
    " -reports:'$coverageFile'" +
    " -targetdir:'$reportDirectory'" +
    " -reporttypes:'Html;HtmlSummary;Cobertura'" +
    " -assemblyfilters:'+azmcp'" +
    " -classfilters:'-*Tests*;-*Program'")

    Write-Host "Coverage report generated at $reportDirectory/index.html"

    # Open the report in default browser
    $reportPath = "$reportDirectory/index.html"
    if (-not (Test-Path $reportPath)) {
        Write-Error "Could not find coverage report at $reportPath"
        exit 1
    }

    if ($OpenReport) {
        # Open the report in default browser
        Write-Host "Opening coverage report in browser..."
        if ($IsMacOS) {
            # On macOS, use 'open' command
            Start-Process "open" -ArgumentList $reportPath
        } elseif ($IsLinux) {
            # On Linux, use 'xdg-open'
            Start-Process "xdg-open" -ArgumentList $reportPath
        } else {
            # On Windows, use 'Start-Process'
            Start-Process $reportPath
        }
    }
}
