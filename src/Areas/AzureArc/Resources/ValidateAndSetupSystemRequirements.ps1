<#
  Script to validate system requirements for AKS Edge Essentials
  and install Hyper-V if required.
#>

#Requires -RunAsAdministrator

Write-Host "Validating System Requirements for AKS Edge Essentials..." -ForegroundColor Cyan
Write-Host "=========================================================" -ForegroundColor Cyan

# Validate Operating System
Write-Host "Checking Operating System..." -ForegroundColor Yellow
$osVersion = (Get-CimInstance -ClassName Win32_OperatingSystem).Caption
if ($osVersion -notmatch "Windows 10|Windows 11|Windows Server 2019|Windows Server 2022") {
    Write-Error "Unsupported Operating System: $osVersion"
    exit 1
}
Write-Host "Operating System: $osVersion" -ForegroundColor Green

# Validate Processor
Write-Host "Checking Processor..." -ForegroundColor Yellow
$cpuCount = (Get-CimInstance -ClassName Win32_ComputerSystem).NumberOfLogicalProcessors
if ($cpuCount -lt 4) {
    Write-Error "Insufficient CPU cores: $cpuCount (Minimum required: 4)"
    exit 1
}
Write-Host "CPU cores: $cpuCount" -ForegroundColor Green

# Validate Memory
Write-Host "Checking Memory..." -ForegroundColor Yellow
$totalMemoryGB = (Get-CimInstance -ClassName Win32_ComputerSystem).TotalPhysicalMemory / 1GB
if ($totalMemoryGB -lt 8) {
    Write-Error "Insufficient RAM: $totalMemoryGB GB (Minimum required: 8 GB)"
    exit 1
}
Write-Host "Memory: $totalMemoryGB GB" -ForegroundColor Green

# Validate Disk Space
Write-Host "Checking Disk Space..." -ForegroundColor Yellow
$freeSpaceGB = (Get-CimInstance -ClassName Win32_LogicalDisk | Where-Object { $_.DriveType -eq 3 }).FreeSpace / 1GB | Measure-Object -Sum | Select-Object -ExpandProperty Sum
if ($freeSpaceGB -lt 50) {
    Write-Error "Insufficient Disk Space: $freeSpaceGB GB (Minimum required: 50 GB)"
    exit 1
}
Write-Host "Disk Space: $freeSpaceGB GB" -ForegroundColor Green

# Validate Hyper-V
Write-Host "Checking Hyper-V..." -ForegroundColor Yellow
$hyperVFeature = Get-WindowsOptionalFeature -FeatureName Microsoft-Hyper-V-All -Online
if ($hyperVFeature.State -ne "Enabled") {
    Write-Host "Hyper-V is not enabled. Enabling Hyper-V..." -ForegroundColor Yellow
    Enable-WindowsOptionalFeature -FeatureName Microsoft-Hyper-V -All -Online -NoRestart
    Write-Host "Hyper-V has been enabled. A system restart is required." -ForegroundColor Green
    Write-Host "Restarting system in 25 seconds..." -ForegroundColor Yellow
    Start-Sleep -Seconds 25
    Restart-Computer
    exit 0
}

Write-Host "Hyper-V is enabled." -ForegroundColor Green
Start-Sleep -Seconds 10
Write-Host "System requirements validated successfully!" -ForegroundColor Green
Start-Sleep -Seconds 10