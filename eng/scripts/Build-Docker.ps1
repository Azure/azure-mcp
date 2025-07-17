#!/bin/env pwsh
#Requires -Version 7

[CmdletBinding(DefaultParameterSetName='none')]
param(
    [switch] $Trimmed,
    [switch] $AllPlatforms,
    [switch] $DebugBuild
)

. "$PSScriptRoot/../common/scripts/common.ps1"
$root = $RepoRoot.Path.Replace('\', '/')
$distPath = "$root/.dist"

& "$root/eng/scripts/Build-Local.ps1" -Trimmed:$Trimmed -AllPlatforms:$AllPlatforms -DebugBuild:$DebugBuild

$runtime = $([System.Runtime.InteropServices.RuntimeInformation]::RuntimeIdentifier)
$parts = $runtime.Split('-')
$os = $parts[0]
$arch = $parts[1]

if($os -eq 'win') {
    $os = 'windows'
} elseif($os -eq 'osx') {
    $os = 'macos'
}

$publishDirectory = [System.IO.Path]::Combine($distPath, "platform", "$os-$arch")

if (!(Test-Path $publishDirectory)) {
    Write-Error "Build output directory does not exist: $publishDirectory"
    return
}

& docker build --build-arg PUBLISH_DIR=""