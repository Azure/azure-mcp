#!/bin/env pwsh
#Requires -Version 7

param(
    [string] $Version
)

. "$PSScriptRoot/../common/scripts/common.ps1"
$RepoRoot = $RepoRoot.Path.Replace('\', '/')

$projectFile = "$RepoRoot/src/AzureMcp.csproj"
$project = [xml](Get-Content $projectFile)
$currentVersion = $project.Project.PropertyGroup.Version[0]

if (!$Version) {
    # get the number of commits since the last tag
    $nextVersion = [AzureEngSemanticVersion]::new($currentVersion)
    $nextVersion.IncrementAndSetToPrerelease('patch')
    $Version = $nextVersion.ToString()
}

$projectText = Get-Content $projectFile -Raw
$projectText = $projectText -replace "<Version>$([Regex]::Escape($currentVersion))</Version>", "<Version>$Version</Version>"
$projectText | Set-Content $projectFile -Force
