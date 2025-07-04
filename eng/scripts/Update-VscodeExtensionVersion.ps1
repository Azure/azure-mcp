<#
.SYNOPSIS
Sets the version for the VSCode Extension

.DESCRIPTION
Sets the version for the VSCode Extension based on provided `newVersion` or
updates after release following these rules:

* VSCode does not support prerelease qualifiers for extensions (e.g. -beta.1)
  source: https://code.visualstudio.com/api/working-with-extensions/publishing-extension#prerelease-extensions
* Prevent unintended release by ensuring a prerelease qualifier is present in
  package.json when incrementing version (in this case 'alpha' has been used)
* Upon release increment the minor version (e.g. y in x.y.z)

0.1.0 -> 0.2.0-alpha.1
0.1.0-alpha.1 -> 0.2.0-alpha.1 (this scenario is unlikely as versions with prerelease qualifiers are not supported)

#>

param(
    [string] $NewVersion
)

$PACKAGE_JSON = "$PSScriptRoot/../../../azure-mcp-extension/package.json"
. "$PSScriptRoot/../../common/scripts/common.ps1"

Set-StrictMode -Version 4

function getPackageJson {
    $packageJsonContent = Get-Content $PACKAGE_JSON -Raw
    $packageJson = ConvertFrom-Json $packageJsonContent
    return $packageJson
}

function getVersion {
    $versionString = (getPackageJson).version
    return [AzureEngSemanticVersion]::new($versionString)
}

function setVersion([string] $version) {
    $currentLocation = Get-Location
    try {
        Set-Location (Resolve-Path (Split-Path $PACKAGE_JSON -Parent))
        npm version $version
    } finally {
        Set-Location $currentLocation
    }

}

$version = $NewVersion
$unreleased = $false
$replaceLatestEntryTitle = $true

if (!$version) {
    # Increment after release
    $version = getVersion

    # This implementation differs from the implementation of
    # IncrementAndSetToPrerelease in that we always increment minor version
    # (incrementing only the prerelease version would require the releasing dev
    # to  update the minor version manually before releasing) and that we use
    # the "alpha" prerelease label.

    # 0.1.0 -> 0.2.0-alpha.1
    # 0.1.0-alpha.1 -> 0.2.0-alpha.1 (this scenario is unlikely as versions with prerelease qualifiers are not supported)
    $version.Minor++
    $version.Patch = 0
    $version.PrereleaseLabel = 'alpha'
    $version.PrereleaseNumber = 1
    $version.IsPrerelease = $true

    $unreleased = $true
    $replaceLatestEntryTitle = $false
}

setVersion $version

. "$PSScriptRoot/../common/scripts/Update-ChangeLog.ps1" `
    -Version $version `
    -ChangeLogPath "$PSScriptRoot/../../azure-mcp-extension/CHANGELOG.md" `
    -Unreleased $unreleased `
    -ReplaceLatestEntryTitle $replaceLatestEntryTitle
