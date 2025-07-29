#!/bin/env pwsh
#Requires -Version 7

[CmdletBinding(DefaultParameterSetName='none')]
param(
    [string] $ArtifactsPath,
    [string] $ArtifactPrefix,
    [string] $OutputPath
)

. "$PSScriptRoot/../common/scripts/common.ps1"
$RepoRoot = $RepoRoot.Path.Replace('\', '/')

$entitlements = "$RepoRoot/eng/dotnet-executable-entitlements.plist"

$artifactDirectories = Get-ChildItem -Path $ArtifactsPath -Directory
| Where-Object { $_.Name -like "$ArtifactPrefix*" }
| Where-Object { $_.Name -notlike '*FailedAttempt*' }

New-Item -ItemType Directory -Force -Path $OutputPath | Out-Null
$OutputPath = (Resolve-Path $OutputPath).Path.Replace('\', '/')

foreach ($artifactDirectory in $artifactDirectories) {
    Write-Host "`n##[group] Artifact directory '$artifactDirectory' contents:"
    Get-ChildItem -Path $artifactDirectory -File -Recurse | Select-Object -ExpandProperty FullName | Out-Host
    Write-Host "##[endgroup]`n"

    $os_arch = $artifactDirectory.Name -replace "$ArtifactPrefix", ''
    $packageJson = $artifactDirectory | Get-ChildItem -Filter "package.json" -Recurse

    Write-Host "Processing $packageJson" -ForegroundColor Yellow

    $package = Get-Content $packageJson -Raw | ConvertFrom-Json -AsHashtable
    $packageDirectory = $packageJson.DirectoryName.Replace('\','/')

    $os = $package.os[0]

    Write-Host "`nProcessing $os package in $packageDirectory" -ForegroundColor Yellow
    if ($os -eq 'darwin') {
        # Only mac binaries need to be compressed. Linux binaries aren't signed and windows are signed uncompressed. 
        
        # Mac requires code signing the binary with an entitlements file such that the signed and notarized binary will properly invoke on
        # a mac system. However, the `codesign` command is only available on a MacOS agent. With that being the case, we simply special case
        # this function here to ensure that the script does not fail outside of a MacOS agent.
        $binaryFilePath = "$packageDirectory/dist/azmcp"

        if ($IsMacOS) {
            Invoke-LoggedCommand "chmod +x `"$binaryFilePath`""
            Invoke-LoggedCommand "codesign --deep -s - -f --options runtime --entitlements `"$entitlements`" `"$binaryFilePath`""
            Invoke-LoggedCommand "codesign -d --entitlements :- `"$binaryFilePath`""
        } else {
            Write-Warning "Mac binaries should be code signed with entitlements, but this is only possible on a mac agent."
        }
        
        $archivePath = "$binaryFilePath.zip"
        Write-Host "Creating $archivePath" -ForegroundColor Yellow
        # We only need to compress the single binary file.
        Compress-Archive -Path $binaryFilePath -DestinationPath $archivePath

        Write-Host "Deleting $binaryFilePath" -ForegroundColor Yellow
        Remove-Item -Path $binaryFilePath -Force -ProgressAction SilentlyContinue
    }

    $destinationPath = Join-Path $OutputPath $os_arch
    $sourcePath = Join-Path $packageDirectory '*'
    New-Item -ItemType Directory -Force -Path $destinationPath | Out-Null

    Write-Host "Copying $sourcePath to $destinationPath`n" -ForegroundColor Yellow
    Copy-Item -Path $sourcePath -Destination $destinationPath -Recurse -Force
}

Write-Host "`n##[group] Output Path Contents:"
Get-ChildItem -Path $OutputPath -File -Recurse | Select-Object -ExpandProperty FullName | Out-Host
Write-Host "##[endgroup]`n"