#!/bin/env pwsh
#Requires -Version 7

[CmdletBinding(DefaultParameterSetName='none')]
param(
    [string] $OutputPath,
    [string] $Version,
    [switch] $SelfContained,
    [switch] $ReadyToRun,
    [switch] $Trimmed,
    [switch] $DebugBuild,
    [Parameter(Mandatory=$true, ParameterSetName='Named')]
    [ValidateSet('windows','linux','macOS')]
    [string] $OperatingSystem,
    [Parameter(Mandatory=$true, ParameterSetName='Named')]
    [ValidateSet('x64','arm64')]
    [string] $Architecture
)

. "$PSScriptRoot/../common/scripts/common.ps1"
$RepoRoot = $RepoRoot.Path.Replace('\', '/')

$npmPackagePath = "$RepoRoot/eng/npm/platform"
$projectFile = "$RepoRoot/src/AzureMcp.csproj"

if(!$Version) {
    $Version = & "$PSScriptRoot/Get-Version.ps1"
}

if (!$OutputPath) {
    $OutputPath = "$RepoRoot/.work"
}

Push-Location $RepoRoot
try {
    $runtime = $([System.Runtime.InteropServices.RuntimeInformation]::RuntimeIdentifier)
    $parts = $runtime.Split('-')
    if($OperatingSystem) {
        switch($OperatingSystem) {
            'windows' { $os = 'win' }
            'linux' { $os = 'linux' }
            'macos' { $os = 'osx' }
            default { Write-Error "Unsupported operating system: $OperatingSystem"; return }
        }
    } else {
        $os = $parts[0]
    }

    if($Architecture) {
        switch($Architecture) {
            'x64' { $arch = 'x64' }
            'arm64' { $arch = 'arm64' }
            default { Write-Error "Unsupported architecture: $Architecture"; return }
        }
    } else {
        $arch = $parts[1]
    }
    
    switch($os) {
        'win' { $node_os = 'win32'; $extension = '.exe' }
        'osx' { $node_os = 'darwin'; $extension = '' }
        default { $node_os = $os; $extension = '' }
    }


    $outputDir = "$OutputPath/$os-$arch"
    Write-Host "Building version $Version, $os-$arch in $outputDir" -ForegroundColor Green
    
    # Clear and recreate the package output directory
    Remove-Item -Path $outputDir -Recurse -Force -ErrorAction SilentlyContinue -ProgressAction SilentlyContinue
    New-Item -Path "$outputDir/dist" -ItemType Directory -Force | Out-Null

    # Copy the platform package files to the output directory
    Copy-Item -Path "$npmPackagePath/*" -Recurse -Destination $outputDir -Force

    $configuration = if ($DebugBuild) { 'Debug' } else { 'Release' }
    $command = "dotnet publish '$projectFile' --runtime '$os-$arch' --output '$outputDir/dist' /p:Version=$Version /p:Configuration=$configuration"
    
    if($SelfContained) {
        $command += " --self-contained"
    }

    if($ReadyToRun) {
        $command += " /p:PublishReadyToRun=true"
    }

    if($Trimmed) {
        $command += " /p:PublishTrimmed=true"
    }

    Invoke-LoggedCommand $command -GroupOutput

    $package = Get-Content "$outputDir/package.json" -Raw
    $package = $package.Replace('{os}', $node_os)
    $package = $package.Replace('{cpu}', $arch)
    $package = $package.Replace('{version}', $Version)
    $package = $package.Replace('{executable}', "azmcp$extension")

    # confirm all the placeholders are replaced
    if ($package -match '\{\w+\}') {
        Write-Error "Failed to replace $($Matches[0]) in package.json"
        return
    }

    $package
    | Out-File -FilePath "$outputDir/package.json" -Encoding utf8

    Write-Host "Updated package.json in $outputDir" -ForegroundColor Yellow

    Write-Host "`nBuild completed successfully!" -ForegroundColor Green
}
finally {
    Pop-Location
}
