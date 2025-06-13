#!/bin/env pwsh
#Requires -Version 7

param(
    [string]$Configuration = "Release",
    [string]$Runtime
)

$ErrorActionPreference = "Stop"

. "$PSScriptRoot/../common/scripts/common.ps1"
$root = $RepoRoot.Path.Replace('\', '/')
$projectFile = "$root/src/AzureMcp.csproj"
$reportPath = "$root/artifacts/aot-compact-report.txt"

if (-not $Runtime) {
    $runtime = [System.Runtime.InteropServices.RuntimeInformation]::RuntimeIdentifier
} else {
    $runtime = $Runtime
}

Write-Host "Running AOT compatibility (trimming) analysis for runtime: $runtime ..."

$artifactsDir = "$root/artifacts"
if (!(Test-Path $artifactsDir)) {
    New-Item -ItemType Directory -Path $artifactsDir | Out-Null
}

$projectObjDir = Join-Path (Split-Path $projectFile) "obj"
if (Test-Path $projectObjDir) {
    Write-Host "Deleting project obj directory: $projectObjDir"
    Remove-Item -Path $projectObjDir -Recurse -Force -ErrorAction SilentlyContinue
}

$publishArgs = @(
    'publish', $projectFile,
    '--configuration', $Configuration,
    '--runtime', $runtime,
    '--self-contained', 'true',
    '/p:PublishTrimmed=true',
    '/p:TrimmerSingleWarn=false'
)

Write-Host "Executing: dotnet $($publishArgs -join ' ')"

$output = & dotnet @publishArgs 2>&1
$output | Out-File -FilePath $reportPath -Encoding utf8

function Get-DllNameSet {
    param(
        [string]$projectFile
    )
    $objDir = Join-Path (Split-Path $projectFile) "bin"
    $dllNameTable = @{}
    if (Test-Path $objDir) {
        $dlls = Get-ChildItem -Path $objDir -Recurse -Filter *.dll -File
        foreach ($dll in $dlls) {
            $dllName = $dll.BaseName
            $dllNameTable[$dllName] = $true
        }
    }
    # Sort: Returns DLL names sorted with the most specialized (most dots) first.
    return $dllNameTable.Keys |
        Sort-Object @{ Expression = { $_.Split('.').Count }; Descending = $true },
                    @{ Expression = { $_ }; Descending = $true }
}

function Extract-TypeNameFromILWarningLine {
    param(
        [string]$line
    )
    # Match e.g. 'warning IL2026: Azure.Monitor.OpenTelemetry.AspNetCore.DefaultAzureMonitorOptions.Configure(AzureMonitorOptions): ...'
    if ($line -match 'warning IL\d+: ([^:]+):') {
        $full = $Matches[1]
        # Extract type name before last dot and first parenthesis
        if ($full -match '^(.*)\.([^.]+)\(') {
            return $Matches[1]
        } else {
            return $full
        }
    }
    return $null
}

function Find-BestDllMatch {
    param(
        [string]$typeName,
        [string[]]$dllNames # List of DLL names sorted with most specialized first
    )
    
    # Only look for exact matches or prefix matches i.e., typename starts with DLLName
    # This is the most reliable heuristics for .NET assemblies.
    foreach ($dllName in $dllNames) {
        if ($typeName -eq $dllName -or $typeName -like "$dllName.*") {
            return $dllName
        }
    }
    return $null
}

$dllNameSet = Get-DllNameSet -projectFile $projectFile

$warnings = $output | Select-String 'warning IL'

if ($warnings.Count -gt 0) {
    Write-Host "AOT compatibility analysis complete. See $reportPath for detailed warnings."
    Write-Host "\nSummary of warnings (with DLL mapping):"
    
    # For JSON reporting.
    $reportArray = @()
    
    foreach ($w in $warnings) {
        $line = $w.Line
        $dllName = $null
        $typeName = Extract-TypeNameFromILWarningLine -line $line
        if ($typeName) {
            $dllName = Find-BestDllMatch -typeName $typeName -dllNames $dllNameSet
        }
        
        $reportObject = [PSCustomObject]@{
            dllName = if ($dllName) { "$dllName.dll" } else { "unknown" }
            warn = $line
        }
        $reportArray += $reportObject
        
        if ($dllName) {
            Write-Host "$line [DLL: $dllName.dll]" -ForegroundColor Yellow
        } else {
            Write-Host "$line [DLL: unknown]" -ForegroundColor Yellow
        }
    }
    
    # Write JSON report to file
    $jsonReportPath = "$root/artifacts/aot-compact-report.json"
    $reportArray | ConvertTo-Json -Depth 2 | Out-File -FilePath $jsonReportPath -Encoding utf8
    Write-Host "JSON report written to: $jsonReportPath" -ForegroundColor Green
    
    exit 1
} else {
    Write-Host "AOT compatibility analysis complete. No trimmer/AOT warnings found. See $reportPath."
    # Write empty JSON array, let's generate the file always.
    $jsonReportPath = "$root/artifacts/aot-compact-report.json"
    @() | ConvertTo-Json | Out-File -FilePath $jsonReportPath -Encoding utf8
    Write-Host "Empty JSON report written to: $jsonReportPath" -ForegroundColor Green
    exit 0
}
