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
$dockerFile = "$root/Dockerfile"

if(!$Version) {
    $Version = & "$PSScriptRoot/Get-Version.ps1"
}

# Will fix this when we update Dockerfile to multi-platform
$os = "linux"
$arch = "x64"
$tag = "azure/azure-mcp:$Version";

& "$root/eng/scripts/Build-Module.ps1" -SelfContained -Trimmed:$Trimmed -DebugBuild:$DebugBuild -OperatingSystem $os -Architecture $arch

[string]$publishDirectory = $([System.IO.Path]::Combine($distPath, "$os-$arch", "dist"))
$relativeDirectory = $(Resolve-Path $publishDirectory -Relative).Replace('\', '/')

Write-Host "Building Docker image ($tag). PATH: [$relativeDirectory]. Absolute: [$publishDirectory]."

if (!(Test-Path $publishDirectory)) {
    Write-Error "Build output directory does not exist: $publishDirectory"
    return
}

& docker build --build-arg PUBLISH_DIR="$relativeDirectory" --file $dockerFile --tag $tag .
