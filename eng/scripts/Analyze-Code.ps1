#!/bin/env pwsh
#Requires -Version 7

. "$PSScriptRoot/../common/scripts/common.ps1"
$RepoRoot = $RepoRoot.Path.Replace('\', '/')

Push-Location $RepoRoot
try {
    # source analysis steps here
}
finally {
    Pop-Location
}