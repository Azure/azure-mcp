<#
Script to confirm the deletion of AKS Edge Essentials and its related resources.
This script checks for the presence of virtual machines, virtual switches, HNS networks,
files, folders, registry entries, PowerShell modules, and services related to AKS Edge.
#>

Write-Host "AKS Edge Essentials Deletion Confirmation Script" -ForegroundColor Cyan
Write-Host "===========================================" -ForegroundColor Cyan

# Check Virtual Machines
Write-Host "\nChecking for AKS Edge-related virtual machines..." -ForegroundColor Yellow
$vms = Get-VM | Where-Object { $_.Name -like "*aksedge*" -or $_.Name -like "*ledge*" -or $_.Name -like "*wedge*" }
if ($vms) {
    Write-Host "Virtual machines still present:" -ForegroundColor Red
    foreach ($vm in $vms) {
        Write-Host "  - $($vm.Name) (State: $($vm.State))" -ForegroundColor Gray
    }
}
else {
    Write-Host "No AKS Edge-related virtual machines found." -ForegroundColor Green
}

# Check Virtual Switches
Write-Host "\nChecking for AKS Edge-related virtual switches..." -ForegroundColor Yellow
$switches = Get-VMSwitch | Where-Object { $_.Name -like "*aksedge*" }
if ($switches) {
    Write-Host "Virtual switches still present:" -ForegroundColor Red
    foreach ($switch in $switches) {
        Write-Host "  - $($switch.Name) (Type: $($switch.SwitchType))" -ForegroundColor Gray
    }
}
else {
    Write-Host "No AKS Edge-related virtual switches found." -ForegroundColor Green
}

# Check HNS Networks
Write-Host "\nChecking for AKS Edge-related HNS networks..." -ForegroundColor Yellow
$hnsNetworks = Get-HnsNetwork | Where-Object { $_.Name -like "*aksedge*" }
if ($hnsNetworks) {
    Write-Host "HNS networks still present:" -ForegroundColor Red
    foreach ($network in $hnsNetworks) {
        Write-Host "  - $($network.Name)" -ForegroundColor Gray
    }
}
else {
    Write-Host "No AKS Edge-related HNS networks found." -ForegroundColor Green
}

# Check Files and Folders
Write-Host "\nChecking for AKS Edge-related files and folders..." -ForegroundColor Yellow
$foldersToCheck = @(
    "$env:ProgramData\AksEdge",
    "$env:ProgramData\MSI",
    "$env:USERPROFILE\.kube\config"
)
foreach ($folder in $foldersToCheck) {
    if (Test-Path $folder) {
        Write-Host "Folder still present: $folder" -ForegroundColor Red
    }
    else {
        Write-Host "Folder not found: $folder" -ForegroundColor Green
    }
}

# Check Registry Entries
Write-Host "\nChecking for AKS Edge-related registry entries..." -ForegroundColor Yellow
$regPaths = @(
    "HKLM:\SOFTWARE\Microsoft\AKSEdge",
    "HKCU:\SOFTWARE\Microsoft\AKSEdge"
)
foreach ($path in $regPaths) {
    if (Test-Path $path) {
        Write-Host "Registry key still present: $path" -ForegroundColor Red
    }
    else {
        Write-Host "Registry key not found: $path" -ForegroundColor Green
    }
}

# Check PowerShell Modules
Write-Host "\nChecking for AKS Edge-related PowerShell modules..." -ForegroundColor Yellow
$modules = Get-Module -ListAvailable | Where-Object { $_.Name -like "AksEdge*" }
if ($modules) {
    Write-Host "PowerShell modules still present:" -ForegroundColor Red
    foreach ($module in $modules) {
        Write-Host "  - $($module.Name) v$($module.Version)" -ForegroundColor Gray
    }
}
else {
    Write-Host "No AKS Edge-related PowerShell modules found." -ForegroundColor Green
}

# Check Services
Write-Host "\nChecking for AKS Edge-related services..." -ForegroundColor Yellow
$services = @("AksEdgeProxy", "AksEdgeContainer")
foreach ($serviceName in $services) {
    $service = Get-Service -Name $serviceName -ErrorAction SilentlyContinue
    if ($service) {
        Write-Host "Service still present: $serviceName" -ForegroundColor Red
    }
    else {
        Write-Host "Service not found: $serviceName" -ForegroundColor Green
    }
}

Write-Host "\nDeletion confirmation completed." -ForegroundColor Cyan
Start-Sleep -Seconds 15