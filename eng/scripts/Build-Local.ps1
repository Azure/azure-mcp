#!/bin/env pwsh
#Requires -Version 7

[CmdletBinding(DefaultParameterSetName='none')]
param(
    [switch] $Trimmed,
    [switch] $NoSelfContained,
    [switch] $NoUsePaths,
    [switch] $AllPlatforms,
    [switch] $VerifyNpx
)

. "$PSScriptRoot/../common/scripts/common.ps1"
$root = $RepoRoot.Path.Replace('\', '/')

$packagesPath = "$root/.work/platform"
$distPath = "$root/.dist"

$version = [AzureEngSemanticVersion]::ParseVersionString((& "$PSScriptRoot/Get-Version.ps1"))
$version.PrereleaseLabel = 'alpha'
$version.PrereleaseNumber = [int]::Parse((Get-Date -UFormat %s))

function Build($os, $arch) {
    & "$root/eng/scripts/Build-Module.ps1" `
        -Version $version `
        -OperatingSystem $os `
        -Architecture $arch `
        -SelfContained:(!$NoSelfContained) `
        -Trimmed:$Trimmed `
        -OutputPath $packagesPath
}

Remove-Item -Path $packagesPath -Recurse -Force -ErrorAction SilentlyContinue -ProgressAction SilentlyContinue
Remove-Item -Path $distPath -Recurse -Force -ErrorAction SilentlyContinue -ProgressAction SilentlyContinue

if($AllPlatforms) {
    Build -os linux -arch x64
    Build -os windows -arch x64
    Build -os windows -arch arm64
    Build -os macos -arch x64
    Build -os macos -arch arm64
}
else {
    $runtime = $([System.Runtime.InteropServices.RuntimeInformation]::RuntimeIdentifier)
    $parts = $runtime.Split('-')
    $os = $parts[0]
    $arch = $parts[1]

    if($os -eq 'win') {
        $os = 'windows'
    } elseif($os -eq 'osx') {
        $os = 'macos'
    }

    Build -os $os -arch $arch
}

& "$root/eng/scripts/Pack-Modules.ps1" `
    -Version $version `
    -ArtifactsPath $packagesPath `
    -UsePaths:(!$NoUsePaths) `
    -OutputPath $distPath

$tgzFile = Get-ChildItem -Path "$distPath/wrapper" -Filter '*.tgz'
| Select-Object -First 1

$testSettingsPath = "$root/.testsettings.json"
if($tgzFile -and (Test-Path -Path $testSettingsPath)) {
    $testSettings = Get-Content -Path $testSettingsPath -Raw | ConvertFrom-Json -AsHashtable
    $testSettings.TestPackage = "file://$tgzFile"
    $testSettings | ConvertTo-Json -Depth 10 | Set-Content -Path $testSettingsPath -NoNewline
}

if ($VerifyNpx) {
    Push-Location -Path $root
    try {
        Invoke-LoggedCommand "npx -y clear-npx-cache"
        Invoke-LoggedCommand "npx -y `"file://$tgzFile`" tools list"
    }
    finally {
        Pop-Location
    }
}
