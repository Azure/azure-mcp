#!/bin/env pwsh
#Requires -Version 7

# Defines shared constants used by both Analyze-AOT-Compact.ps1 and Render-AOT-Analysis-Result.ps1

. "$PSScriptRoot/../common/scripts/common.ps1"
$script:AOTConfig = @{
    # Base paths
    RootPath = $RepoRoot.Path.Replace('\', '/')
    ProjectFile = "$($RepoRoot.Path.Replace('\', '/'))/src/AzureMcp.csproj"
    
    # AOT report directories and files 
    ReportDirectory = "$($RepoRoot.Path.Replace('\', '/'))/.work/aotCompactReport"
    RawReportPath = "$($RepoRoot.Path.Replace('\', '/'))/.work/aotCompactReport/aot-compact-report.txt"
    JsonReportPath = "$($RepoRoot.Path.Replace('\', '/'))/.work/aotCompactReport/aot-compact-report.json"
    HtmlReportPath = "$($RepoRoot.Path.Replace('\', '/'))/.work/aotCompactReport/aot-compact-report.html"
}

function Get-AOTConfig {
    return $script:AOTConfig
}
