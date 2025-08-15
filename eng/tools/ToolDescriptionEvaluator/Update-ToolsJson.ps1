#!/usr/bin/env pwsh

<#
.SYNOPSIS
    Updates the tools.json file by calling azmcp.exe tools list

.DESCRIPTION
    This script generates a fresh tools.json file by executing the azmcp.exe tools list command.
    The generated JSON file will have the correct format expected by the ToolDescriptionEvaluator tool.

.PARAMETER Force
    Overwrite the existing tools.json file without prompting

.EXAMPLE
    ./Update-ToolsJson.ps1
    Updates the tools.json file, prompting before overwriting if it exists

.EXAMPLE
    ./Update-ToolsJson.ps1 -Force
    Updates the tools.json file, overwriting without prompting
#>

param(
    [switch]$Force
)

$ErrorActionPreference = 'Stop'

# Get the directory where this script is located
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$JsonFile = Join-Path $ScriptDir "tools.json"
$AzMcpExe = Join-Path $ScriptDir "../../../core/src/AzureMcp.Cli/bin/Debug/net9.0/azmcp.exe"

# Check if azmcp.exe exists
if (-not (Test-Path $AzMcpExe)) {
    Write-Error "azmcp.exe not found at: $AzMcpExe"
    Write-Host "Please build the solution first with: dotnet build" -ForegroundColor Yellow
    exit 1
}

# Check if JSON file exists and prompt if not using -Force
if ((Test-Path $JsonFile) -and -not $Force) {
    $response = Read-Host "tools.json already exists. Overwrite? (y/N)"
    if ($response -notmatch '^[Yy]') {
        Write-Host "Operation cancelled." -ForegroundColor Yellow
        exit 0
    }
}

Write-Host "Generating tools.json..." -ForegroundColor Green

try {
    # Execute azmcp.exe tools list and save output to JSON file
    & $AzMcpExe tools list | Out-File -FilePath $JsonFile -Encoding utf8

    # Verify the file was created and has content
    if ((Test-Path $JsonFile) -and ((Get-Item $JsonFile).Length -gt 0)) {
        $fileSize = [math]::Round((Get-Item $JsonFile).Length / 1KB, 2)
        Write-Host "Successfully generated tools.json ($fileSize KB)" -ForegroundColor Green
        
        # Try to parse the JSON to verify it's valid
        try {
            $json = Get-Content $JsonFile -Raw | ConvertFrom-Json
            $toolCount = $json.results.Count
            Write-Host "Contains $toolCount tools" -ForegroundColor Cyan
        }
        catch {
            Write-Warning "Generated JSON file may not be valid: $_"
        }
    }
    else {
        Write-Error "Failed to generate tools.json or file is empty"
    }
}
catch {
    Write-Error "Failed to execute azmcp.exe: $_"
    exit 1
}
