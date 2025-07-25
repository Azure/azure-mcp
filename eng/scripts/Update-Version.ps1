#!/bin/env pwsh
#Requires -Version 7
[CmdletBinding(DefaultParameterSetName='default')]
param(
    [Parameter(Mandatory=$true, ParameterSetName='Release')]
    [string] $Version,
    [Parameter(Mandatory=$true, ParameterSetName='Release')]
    [string] $ReleaseDate,
    [Parameter(ParameterSetName='Release')]
    [boolean] $ReplaceLatestEntryTitle=$true
)

. "$PSScriptRoot/../common/scripts/common.ps1"
$RepoRoot = $RepoRoot.Path.Replace('\', '/')

$projectFile = "$RepoRoot/Directory.Build.props"
$project = [xml](Get-Content $projectFile)
$currentVersion = $project.Project.PropertyGroup.Version | Select-Object -First 1

$autoVersion = $false
if (!$Version) {
    # get the number of commits since the last tag
    $nextVersion = [AzureEngSemanticVersion]::new($currentVersion)
    $nextVersion.IncrementAndSetToPrerelease('patch')
    $Version = $nextVersion.ToString()
    $autoVersion = $true
}

Write-Host "Current Version: $currentVersion"
Write-Host "New Version: $Version"
Write-Host "Updating project file $projectFile"

$projectText = Get-Content $projectFile -Raw
$projectText = $projectText -replace "<Version>$([Regex]::Escape($currentVersion))</Version>", "<Version>$Version</Version>"
$projectText | Set-Content $projectFile -Force -NoNewLine

if ($autoVersion) {
  & "$RepoRoot/eng/common/scripts/Update-ChangeLog.ps1" -Version $Version `
  -ChangelogPath "$RepoRoot/CHANGELOG.md" -Unreleased $True
}
else {
  & "$RepoRoot/eng/common/scripts/Update-ChangeLog.ps1" -Version $Version `
  -ChangelogPath "$RepoRoot/CHANGELOG.md" -Unreleased $False `
  -ReplaceLatestEntryTitle $ReplaceLatestEntryTitle -ReleaseDate $ReleaseDate
}
