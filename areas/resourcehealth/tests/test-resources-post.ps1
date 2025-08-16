#!/usr/bin/env pwsh

# Copyright (c) Microsoft Corporation.
# Licensed under the MIT License.

#Requires -Version 6.0
#Requires -PSEdition Core

[CmdletBinding()]
param (
    [Parameter(Mandatory)]
    [hashtable] $AdditionalParameters
)

Write-Host "Running ResourceHealth post-deployment setup..."

try {
    # Extract outputs from deployment
    $storageAccountName = $AdditionalParameters['testStorageAccountName']
    $storageAccountResourceId = $AdditionalParameters['testStorageAccountResourceId']
    $virtualMachineName = $AdditionalParameters['testVirtualMachineName']
    $virtualMachineResourceId = $AdditionalParameters['testVirtualMachineResourceId']
    $resourceGroupName = $AdditionalParameters['resourceGroupName']
    
    Write-Host "Test resources deployed successfully:"
    Write-Host "  Storage Account: $storageAccountName"
    Write-Host "  Storage Account Resource ID: $storageAccountResourceId"
    Write-Host "  Virtual Machine: $virtualMachineName"
    Write-Host "  Virtual Machine Resource ID: $virtualMachineResourceId"
    Write-Host "  Resource Group: $resourceGroupName"
    
    Write-Host "ResourceHealth post-deployment setup completed successfully."
}
catch {
    Write-Error "Failed to complete ResourceHealth post-deployment setup: $_"
    throw
}
