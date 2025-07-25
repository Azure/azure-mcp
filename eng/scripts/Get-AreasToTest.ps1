#!/bin/env pwsh
#Requires -Version 7

[CmdletBinding()]
param(
    [switch] $SetDevOpsVariables,
    [ValidateSet('Live', 'Unit', 'All')]
    [string] $TestType = 'All'
)

. "$PSScriptRoot/../common/scripts/common.ps1"
$RepoRoot = $RepoRoot.Path.Replace('\', '/')

# When a change is made in a Common area, all areas should be tested
$areaDirectories = Get-ChildItem "$RepoRoot/areas" -Directory

# While there is a "core" directory at the repo root, we consider the "core" area to be all of the repo outside of the
# "areas" directory.
# This lets us make simple statements like:
# - Changing any eng/ files is a "core" change
# - Changes in core/src is a "core" change
# - If you change areas/redis/* we need to tes the redis "service" area
# - If you change any "core" files, we need to test all of the "core" area as well as a few canary service areas
`
$serviceAreas = $areaDirectories
| Select-Object -ExpandProperty Name

# When "core" is modified, include storage and keyVault as the canary service areas.
$coreCanaryAreas = @('storage', 'keyvault')

Push-Location $RepoRoot
try {
    $isPullRequestBuild = $env:BUILD_REASON -eq 'PullRequest'

    if(!$isPullRequestBuild) {
        # If we're not in a pull request, test all areas
        $areasToTest = @($serviceAreas + 'core' | Sort-Object -Unique)
    } else {
        # If we're in a pull request, use the set of changed files to narrow down the set of areas to test.
        $changedFiles = Get-ChangedFiles
        # $changedFiles = [
        #   areas/storage/src/someFile.cs
        #   areas/tools/src/anotherFile.cs    <- Core area
        #   areas/monitoring/README.md
        #   core/src/commonClass.cs
        #   eng/scripts/SomeScript.ps1
        # ]
        Write-Host ''

        # Currently, we don't exclude non-code files from the changed files list.
        # Updating a markdown file in a service area will still trigger tests for that area.
        $changedAreas = @($changedFiles
        | ForEach-Object { $_ -match '^areas/(.*?)/' -and $serviceAreas -contains $Matches[1] ? $Matches[1] : 'core' }
        | Sort-Object -Unique)
        # $changedAreas = @('storage', 'monitoring', 'core')

        if($changedAreas.Count -eq 0) {
            Write-Host "No changed areas detected. Defaulting to core." -ForegroundColor Yellow
            $changedAreas = @('core')
        } else {
            Write-Host "Changed areas detected: $($changedAreas -join ', ')"
        }

        $areasToTest = @(if ($changedAreas -contains 'core') {
            Write-Host "Core changes detected. Including core canary areas: $($coreCanaryAreas -join ', ')" -ForegroundColor Cyan
            @($changedAreas + $coreCanaryAreas) | Sort-Object -Unique
        } else {
            $changedAreas
        })

        # $areasToTest = @('core', 'keyVault', 'monitoring', 'storage')
    }

    Write-Host "Forming area test matrix"
    $areaMatrix = [ordered]@{}
    foreach ($area in $areasToTest) {
        $testResourcesPath = $area -eq 'core' ? 'core/tests' : "areas/$area/tests"
        $hasTestResources = Test-Path "$RepoRoot/$testResourcesPath/test-resources.bicep"
        $hasLiveTests = (Get-ChildItem $testResourcesPath -Filter '*.LiveTests.csproj' -Recurse).Count -gt 0
        $hasUnitTests = (Get-ChildItem $testResourcesPath -Filter '*.UnitTests.csproj' -Recurse).Count -gt 0

        if ($TestType -eq 'Live' -and (!$hasLiveTests -or !$hasTestResources)) {
            Write-Host "$area has changes, but no live tests or test resources found. Skipping." -ForegroundColor Yellow
            continue
        }

        if ($TestType -eq 'Unit' -and !$hasUnitTests) {
            Write-Host "$area has changes, but no unit tests found. Skipping." -ForegroundColor Yellow
            continue
        }

        $areaMatrix[$area] = [ordered]@{
            Area = $area
            UnitTests = $hasUnitTests
            LiveTests = $hasLiveTests
            TestResources = $hasTestResources
            TestResourcesPath = $testResourcesPath
        }
    }

    $hasTestAreas = $areaMatrix.Count -gt 0

    if($SetDevOpsVariables) {
        # Set DevOps variables for changed areas
        $json = ConvertTo-Json $areaMatrix -Compress
        Write-Host "##vso[task.setvariable variable=TestMatrix;isOutput=true]$json"
        # Set a variable indicating if any areas changed
        Write-Host "##vso[task.setvariable variable=HasTestAreas;isOutput=true]$hasTestAreas"
    }

    Write-Host ""
    Write-Host "TestMatrix:"
    $areaMatrix | ConvertTo-Json | Out-Host

    Write-Host "HasTestAreas: $hasTestAreas"
}
finally {
    Pop-Location
}
