#!/bin/env pwsh
#Requires -Version 7

[CmdletBinding()]
param(
    [string] $ArtifactsPath,
    [string] $OutputPath,
    [string] $Version,
    [switch] $UsePaths
)

. "$PSScriptRoot/../common/scripts/common.ps1"
$RepoRoot = $RepoRoot.Path.Replace('\', '/')

$npmPackagePath = "$RepoRoot/eng/npm/wrapper"

if(!$Version) {
    $Version = & "$PSScriptRoot/Get-Version.ps1"
}

if(!$ArtifactsPath) {
    $ArtifactsPath = "$RepoRoot/.work"
}

if(!$OutputPath) {
    $OutputPath = "$RepoRoot/.dist"
}

if(!(Test-Path $ArtifactsPath)) {
    Write-Error "Artifacts path $ArtifactsPath does not exist."
    return
}

$wrapperFolder = "$RepoRoot/.work/wrapper"
Remove-Item -Path $wrapperFolder -Recurse -Force -ErrorAction SilentlyContinue -ProgressAction SilentlyContinue

Push-Location $RepoRoot
try {
    # Clear and recreate the output directory
    Remove-Item -Path $OutputPath -Recurse -Force -ErrorAction SilentlyContinue -ProgressAction SilentlyContinue
    New-Item -ItemType Directory -Force -Path "$OutputPath/platform" | Out-Null
    New-Item -ItemType Directory -Force -Path "$OutputPath/wrapper" | Out-Null
    
    $package = Get-Content "$npmPackagePath/package.json" -Raw | ConvertFrom-Json -AsHashtable
    $package.version = $Version

    # Build the project
    $platformFiles = Get-ChildItem -Path $ArtifactsPath -Filter "package.json" -Recurse
    foreach ($platformFile in $platformFiles) {
        $packageFolder = $platformFile.DirectoryName
        $platform = Get-Content $platformFile.FullName -Raw | ConvertFrom-Json -AsHashtable

        if ($platform.version -ne $version) {
           Write-Error "Version mismatch in $($platformFile.FullName). Expected $version, found $($platform.version)"
           return
        }

        $os = $platform.os[0]
        $cpu = $platform.cpu[0]

        if($package.os -notcontains $os) {
            $package.os += $os
        }
        
        if($package.cpu -notcontains $cpu) {
            $package.cpu += $cpu
        }

        if (!$IsWindows) {
            Write-Host "Setting executable permissions for $packageFolder/index.js" -ForegroundColor Yellow
            Invoke-LoggedCommand "chmod +x `"$packageFolder/index.js`""

            if ($os -ne 'win32') {
                Write-Host "Setting executable permissions for $packageFolder/dist/azmcp" -ForegroundColor Yellow
                Invoke-LoggedCommand "chmod +x `"$packageFolder/dist/azmcp`""
            }
        }
        else {
            Write-Warning "Executable permissions are not set when packing on a Windows agent."
        }

        Write-Host "Packaging $packageFolder into $OutputPath/platform"
        Invoke-LoggedCommand "npm pack $packageFolder --pack-destination '$OutputPath/platform'" -GroupOutput | Tee-Object -Variable fileName
        Write-Host "Package location: $OutputPath/platform/$fileName" -ForegroundColor Yellow

        if ($UsePaths) {
            $package.optionalDependencies[$platform.name] = "file://$((Resolve-Path "$OutputPath/platform/$fileName").Path.Replace('\', '/'))"
        } else {
            $package.optionalDependencies[$platform.name] = $version
        }
    }

    New-Item -ItemType Directory $wrapperFolder | Out-Null
    Copy-Item -Path "$npmPackagePath/*" -Destination $wrapperFolder -Recurse -Force

    if (!$IsWindows) {
        Write-Host "Setting executable permissions for $wrapperFolder/index.js" -ForegroundColor Yellow
        Invoke-LoggedCommand "chmod +x `"$wrapperFolder/index.js`""
    }

    $package | ConvertTo-Json -Depth 10 | Out-File -FilePath "$wrapperFolder/package.json" -Encoding utf8
    Write-Host "Created package.json in $wrapperFolder"

    Write-Host "Packaging $wrapperFolder into $OutputPath/wrapper"
    Invoke-LoggedCommand "npm pack $wrapperFolder --pack-destination '$OutputPath/wrapper'" -GroupOutput | Tee-Object -Variable fileName
    Write-Host "Package location: $OutputPath/wrapper/$fileName" -ForegroundColor Yellow

    Write-Host "`nPackaging completed successfully!" -ForegroundColor Green
}
finally {
    Pop-Location
}