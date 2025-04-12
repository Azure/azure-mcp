#!/usr/bin/env pwsh

[CmdletBinding()]
param(
    [string] $TestResultsPath
)

$ErrorActionPreference = 'Stop'
. "$PSScriptRoot/../common/scripts/common.ps1"

$RepoRoot = $RepoRoot.Path.Replace('\', '/')

if (!$TestResultsPath) {
    $TestResultsPath = "$RepoRoot/tests/TestResults"
}

# Clean previous results
Remove-Item -Recurse -Force $TestResultsPath -ErrorAction SilentlyContinue

# Run tests with coverage
Write-Host "Running tests with coverage..."
Invoke-LoggedCommand ("dotnet test '$RepoRoot/tests/AzureMcp.Tests.csproj'" +
  " --collect:'XPlat Code Coverage'" +
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

    $reportDirectory = "$RepoRoot/tests/TestResults/CoverageReport"
    Invoke-LoggedCommand ("reportgenerator" +
    " -reports:'$coverageFile'" +
    " -targetdir:'$reportDirectory'" +
    " -reporttypes:'Html;HtmlSummary;Cobertura'" +
    " -assemblyfilters:'+azmcp'" +
    " -classfilters:'-*Tests*;-*Program'")

    Write-Host "Coverage report generated at $reportDirectory/index.html"

    # Open the report in default browser
    $reportPath = "$reportDirectory/index.html"
    if (Test-Path $reportPath) {
        Write-Host "Opening coverage report in browser..."
        Start-Process $reportPath
    } else {
        Write-Error "Could not find coverage report at $reportPath"
    }
}
