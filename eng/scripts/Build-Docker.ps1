#!/bin/env pwsh
#Requires -Version 7

[CmdletBinding(DefaultParameterSetName='none')]
param(
    [string] $Version,
    [switch] $Trimmed,
    [switch] $DebugBuild
)

. "$PSScriptRoot/../common/scripts/common.ps1"
$root = $RepoRoot.Path.Replace('\', '/')
$distPath = "$root/.work"

if(!$Version) {
    $Version = & "$PSScriptRoot/Get-Version.ps1"
}

dotnet publish -t:PublishContainer -p:Version=$Version `
    --self-contained `
    -p:PublishTrimmed=$Trimmed
