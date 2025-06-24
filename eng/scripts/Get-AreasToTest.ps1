#!/bin/env pwsh
#Requires -Version 7

[CmdletBinding()]
param(
    [switch] $SetDevOpsVariables
)

. "$PSScriptRoot/../common/scripts/common.ps1"
$RepoRoot = $RepoRoot.Path.Replace('\', '/')

# When a change is made in a Common area, all areas should be tested
$coreAreas = @('Core', 'Tools', 'Group', 'Server')
$coreTestAreas = $coreAreas + @('Storage', 'KeyVault')

Push-Location $RepoRoot
try {
    $isPullRequestBuild = $env:BUILD_REASON -eq 'PullRequest'

    if(!$isPullRequestBuild) {
        # If we're not in a pull request, test all areas
        $allAreas = Get-ChildItem ./src/Areas -Directory | Select-Object -ExpandProperty Name
        $areasToTest = $allAreas + 'Core' | Sort-Object -Unique
    } else {
        # If we're in a pull request, use the set of changed files to narrow down the set of areas to test.
        $changedFiles = Get-ChangedFiles
        Write-Host ''

        [array]$changedAreas = $changedFiles | ForEach-Object { $_ -match '^(src|test)/Areas/(.*?)/' ? $Matches[2] : 'Core' } | Sort-Object -Unique

        if($changedAreas.Count -eq 1 -and $changedAreas[0] -eq 'Core')
        {
            # If we only changed Common markdown files, there's no need to run tests, so return an empty area set.
            $extensionGroups = $changedFiles | Group-Object { $_.Split('.')[-1] }
            if($extensionGroups.Count -eq 1 -and $extensionGroups[0].Name -eq 'md') {
                Write-Host 'Only common markdown files changed'
                $changedAreas = @()
            }
        }

        $hasCoreChanges = $false
        foreach($area in $changedAreas) {
            if($coreAreas -contains $area) {
                $hasCoreChanges = $true
                break
            }
        }

        # If there are core changes, ensure CoreTestAreas are in the list of areas to test
        $areasToTest = ($hasCoreChanges ? $changedAreas + $coreTestAreas : $changedAreas) | Sort-Object -Unique
    }

    if($SetDevOpsVariables) {
        # Set DevOps variables for changed areas
        $areaList = ConvertTo-Json $areasToTest -Compress
        Write-Host "##vso[task.setvariable variable=TestAreas;isOutput=true]$areaList"

        # Set a variable indicating if any areas changed
        Write-Host "##vso[task.setvariable variable=HasTestAreas;isOutput=true]$($areasToTest.Count -gt 0)"
        Write-Host ''
    }

    return $areasToTest
}
finally {
    Pop-Location
}
