#!/usr/bin/env pwsh
#Requires -Version 7

[CmdletBinding()]
param(
    [string] $TestResultsPath,
    [string[]] $Areas,
    [switch] $Live,
    [switch] $CoverageSummary,
    [switch] $OpenReport
)

$ErrorActionPreference = 'Stop'
. "$PSScriptRoot/../common/scripts/common.ps1"

# Function to retry dotnet test command on macOS to handle Coverlet file locking issues
function Invoke-TestWithRetry {
    param(
        [string]$TestCommand,
        [int]$MaxAttempts = 3,
        [int]$DelaySeconds = 5
    )
    
    for ($attempt = 1; $attempt -le $MaxAttempts; $attempt++) {
        try {
            Write-Host "Running tests (attempt $attempt of $MaxAttempts)..."
            Invoke-LoggedCommand $TestCommand -AllowedExitCodes @(0, 1)
            return $LastExitCode
        }
        catch {
            $errorMessage = $_.Exception.Message
            
            # Check if this is the specific Coverlet file locking issue on macOS
            if ($IsMacOS -and $errorMessage -match "cannot access the file.*because it is being used by another process") {
                Write-Host "Detected Coverlet file locking issue on macOS: $errorMessage"
                
                if ($attempt -eq $MaxAttempts) {
                    Write-Error "Test execution failed after $MaxAttempts attempts due to file locking. Last error: $errorMessage"
                    throw
                }
                
                Write-Host "Retrying in $DelaySeconds seconds..."
                Start-Sleep -Seconds $DelaySeconds
            }
            else {
                # For other errors, don't retry
                throw
            }
        }
    }
}

$RepoRoot = $RepoRoot.Path.Replace('\', '/')

if (!$TestResultsPath) {
    $TestResultsPath = "$RepoRoot/.work/testResults"
}

# Clean previous results
Remove-Item -Recurse -Force $TestResultsPath -ErrorAction SilentlyContinue

if($env:TF_BUILD) {
    Move-Item -Path "$RepoRoot/tests/xunit.runner.ci.json" -Destination "$RepoRoot/tests/xunit.runner.json" -Force -ErrorAction Continue
    Write-Host "Replaced xunit.runner.json with xunit.runner.ci.json"
}

Write-Host "xunit.runner.json content:"
Get-Content "$RepoRoot/tests/xunit.runner.json" | Out-Host

# Run tests with coverage
$filter = $Live ? "Category~Live" : "Category!~Live"

if ($Areas) {
    $filter = "$filter & ($($Areas | ForEach-Object { "Area=$_" } | Join-String -Separator ' | '))"
}

# Use a dedicated temp directory for Coverlet on macOS to reduce file conflicts
$coverletTempDir = if ($IsMacOS) { "$TestResultsPath/coverlet-temp" } else { "" }
if ($coverletTempDir) {
    New-Item -ItemType Directory -Path $coverletTempDir -Force | Out-Null
}

$testCommand = "dotnet test '$RepoRoot/tests/AzureMcp.Tests.csproj'" +
  " --collect:'XPlat Code Coverage'" +
  " --filter '$filter'" +
  " --results-directory '$TestResultsPath'" +
  " --logger 'trx'"

# Add Coverlet temp directory configuration for macOS
if ($coverletTempDir) {
    $testCommand += " -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.TempDirectory='$coverletTempDir'"
}

$testExitCode = Invoke-TestWithRetry -TestCommand $testCommand

# Find the coverage file
$coverageFile = Get-ChildItem -Path $TestResultsPath -Recurse -Filter "coverage.cobertura.xml"
| Where-Object { $_.FullName.Replace('\','/') -notlike "*/in/*" }
| Select-Object -First 1

if (-not $coverageFile) {
    Write-Error "No coverage file found!"
    exit 1
}

# Coverage Report Generation

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
    " -classfilters:'-*Tests*;-*Program'" +
    " -filefilters:'-*JsonSourceGenerator*;-*LibraryImportGenerator*'")

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

# Command Coverage Summary

if($CoverageSummary) {
    try{
        $CommandCoverageSummaryFile = "$TestResultsPath/Coverage.md"

        $xml = [xml](Get-Content $coverageFile.FullName)

        $classes = $xml.coverage.packages.package.classes.class |
            Where-Object { $_.name -match 'AzureMcp\.(.*\.)?Commands\.' -and $_.filename -notlike '*System.Text.Json.SourceGeneration*' }

        $fileGroups = $classes |
            Group-Object { $_.filename } |
            Sort-Object Name

        $summary = $fileGroups | ForEach-Object {
            # for live tests, we only want to look at the ExecuteAsync methods
            $methods = if($Live) {
                $_.Group | ForEach-Object {
                    if($_.name -like '*<ExecuteAsync>*'){
                        # Generated code for async ExecuteAsync methods
                        return $_.methods.method
                    } else {
                        # Non async methods named ExecuteAsync
                        return $_.methods.method | Where-Object { $_.name -eq 'ExecuteAsync' }
                    }
                }
            }
            else {
                $_.Group.methods.method
            }

            $lines = $methods.lines.line
            $covered = ($lines | Where-Object { $_.hits -gt 0 }).Count
            $total = $lines.Count

            if($total) {
                return [pscustomobject]@{
                    file = $_.name
                    pct = if ($total -gt 0) { $covered * 100 / $total } else { 0 }
                    covered = $covered
                    lines = $total
                }
            }
        }

        $maxFileWidth = ($summary | Measure-Object { $_.file.Length } -Maximum).Maximum
        if ($maxFileWidth -le 0) {
            $maxFileWidth = 10
        }
        $header = $live ? "Live test code coverage for command ExecuteAsync methods" : "Unit test code coverage for command classes"

        $output = ($env:TF_BUILD ? "" : "$header`n`n") +
                "File $(' ' * ($maxFileWidth - 5)) | % Covered | Lines | Covered`n" +
                "$('-' * $maxFileWidth) | --------: | ----: | ------:`n"

        $summary | ForEach-Object {
            # Format each line with the appropriate width
            $output += ("{0,-$maxFileWidth} | {1,9:F0} | {2,5} | {3,7}`n" -f $_.file, $_.pct, $_.lines, $_.covered)
        }

        Write-Host "Writing command coverage summary to $CommandCoverageSummaryFile"
        $output | Out-File -FilePath $CommandCoverageSummaryFile -Encoding utf8

        if ($env:TF_BUILD) {
            Write-Host "##vso[task.addattachment type=Distributedtask.Core.Summary;name=$header;]$(Resolve-Path $CommandCoverageSummaryFile)"
        }
    }
    catch {
        Write-Host "Error creating coverage summary: $($_.Exception.Message)"
        Write-Host "Stack trace: $($_.Exception.StackTrace)"
        exit 1
    }
}
exit $testExitCode
