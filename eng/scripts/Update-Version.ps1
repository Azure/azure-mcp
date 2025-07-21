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

$projectFile = "$RepoRoot/src/AzureMcp.csproj"
$project = [xml](Get-Content $projectFile)
$currentVersion = $project.Project.PropertyGroup.Version[0]

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

# Update VSIX version in eng/vscode/package.json
$vsixPackageJsonPath = "$RepoRoot/eng/vscode/package.json"
if (Test-Path $vsixPackageJsonPath) {
    $packageJson = Get-Content $vsixPackageJsonPath -Raw | ConvertFrom-Json
    $currentVsixVersion = $packageJson.version
    if ($currentVsixVersion -ne $Version) {
        Write-Host "Current VSIX Version: $currentVsixVersion"
        Write-Host "New VSIX Version: $Version"
        $packageJson.version = $Version
        $packageJson | ConvertTo-Json -Depth 100 | Set-Content $vsixPackageJsonPath -NoNewline
        Write-Host "Updated VSIX version in $vsixPackageJsonPath from $currentVsixVersion to $Version"
    } else {
        Write-Host "VSIX version in $vsixPackageJsonPath is already $Version. No update needed."
    }
} else {
    Write-Warning "VSIX package.json not found at $vsixPackageJsonPath"
}

if ($autoVersion) {
  & "$RepoRoot/eng/common/scripts/Update-ChangeLog.ps1" -Version $Version `
  -ChangelogPath "$RepoRoot/CHANGELOG.md" -Unreleased $True
}
else {
  & "$RepoRoot/eng/common/scripts/Update-ChangeLog.ps1" -Version $Version `
  -ChangelogPath "$RepoRoot/CHANGELOG.md" -Unreleased $False `
  -ReplaceLatestEntryTitle $ReplaceLatestEntryTitle -ReleaseDate $ReleaseDate
}
