#!/bin/env pwsh
#Requires -Version 7

[CmdletBinding()]
param(
    [string] $PrereleaseLabel,
    [int] $PrereleaseNumber,
    [string] $VariableName
)

. "$PSScriptRoot/../common/scripts/common.ps1"
$RepoRoot = $RepoRoot.Path.Replace('\', '/')

$projectFile = "$RepoRoot/Directory.Build.props"
$project = [xml](Get-Content $projectFile)
$version = $project.Project.PropertyGroup.Version | Select-Object -First 1
$version = [AzureEngSemanticVersion]::new($version)

if ($PrereleaseLabel) {
    $version.PrereleaseLabel = $PrereleaseLabel
    $version.PrereleaseNumber = $PrereleaseNumber
}

Write-Output $version.ToString()
if($VariableName) {
    Write-Host "##vso[task.setvariable variable=$VariableName;isOutput=true]$version"
}
