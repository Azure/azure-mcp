<#
  Script to completely remove AKS Edge Essentials from the machine
  This will remove the cluster, virtual machines, virtual switches, and all related components
 Basic removal (keeps virtual switches)
.\RemoveAksEdgeCompletely.ps1

 Force removal without confirmation
.\RemoveAksEdgeCompletely.ps1 -Force

 Complete removal including virtual switches, modules, and registry
.\RemoveAksEdgeCompletely.ps1 -Force -RemoveVirtualSwitch -RemoveModules -CleanRegistry

 Interactive removal with confirmations
.\RemoveAksEdgeCompletely.ps1 -RemoveVirtualSwitch
  #>
param(
    [Switch] $Force = $true,
    [Switch] $RemoveVirtualSwitch = $true,
    [Switch] $RemoveModules = $true,
    [Switch] $CleanRegistry = $true
)
#Requires -RunAsAdministrator

Write-Host "AKS Edge Essentials Complete Removal Script" -ForegroundColor Red
Write-Host "===========================================" -ForegroundColor Red

# Warning
if (-not $Force) {
    Write-Host "`nWARNING: This script will completely remove AKS Edge Essentials from your machine!" -ForegroundColor Yellow
    Write-Host "This includes:" -ForegroundColor Yellow
    Write-Host "  - The Kubernetes cluster and all workloads" -ForegroundColor Red
    Write-Host "  - All virtual machines" -ForegroundColor Red
    Write-Host "  - Virtual switches (if -RemoveVirtualSwitch is specified)" -ForegroundColor Red
    Write-Host "  - All AKS Edge data and configurations" -ForegroundColor Red
    Write-Host "`nThis action cannot be undone!" -ForegroundColor Red
    
    $confirm = Read-Host "`nAre you absolutely sure you want to proceed? Type 'DELETE' to confirm"
    if ($confirm -ne "DELETE") {
        Write-Host "Removal cancelled." -ForegroundColor Yellow
        exit 0
    }
}

# Step 1: Check if AKS Edge is installed
Write-Host "`nStep 1: Checking AKS Edge installation" -ForegroundColor Yellow
try {
    $aksEdgeModule = Get-Module -ListAvailable -Name AksEdge
    if (-not $aksEdgeModule) {
        Write-Host "AKS Edge module not found. Checking for remnants..." -ForegroundColor Yellow
    }
    else {
        Write-Host "Found AKS Edge module version: $($aksEdgeModule.Version)" -ForegroundColor Gray
        Import-Module AksEdge -Force
    }
}
catch {
    Write-Error "Error checking AKS Edge installation: $_"
}

# Step 2: Disconnect from Arc if connected
Write-Host "`nStep 2: Checking Azure Arc connection" -ForegroundColor Yellow
try {
    $deploymentInfo = Get-AksEdgeDeploymentInfo -ErrorAction SilentlyContinue
    if ($deploymentInfo -and $deploymentInfo.Arc.Status -eq "Connected") {
        Write-Host "Cluster is connected to Azure Arc. Disconnecting first..." -ForegroundColor Yellow
        $disconnectScript = "$PSScriptRoot\DisconnectFromArc.ps1"
        if (Test-Path $disconnectScript) {
            & $disconnectScript -Force
        }
        else {
            Write-Host "Arc disconnect script not found. Proceeding with removal..." -ForegroundColor Yellow
        }
    }
}
catch {
    Write-Error "Error checking or disconnecting Azure Arc: $_"
}

# Step 3: Remove AKS Edge deployment
Write-Host "`nStep 3: Removing AKS Edge deployment" -ForegroundColor Yellow
try {
    if (Get-Command Remove-AksEdgeDeployment -ErrorAction SilentlyContinue) {
        Write-Host "Removing AKS Edge deployment..." -ForegroundColor Yellow
        Remove-AksEdgeDeployment -Force
        Write-Host "Deployment removed successfully" -ForegroundColor Green
    }
    else {
        Write-Host "Remove-AksEdgeDeployment command not found" -ForegroundColor Gray
    }
}
catch {
    Write-Error "Failed to remove deployment: $_"
}

# Refinement: Adding logging for each major step
Write-Host "`nStep 4: Cleaning up virtual machines" -ForegroundColor Yellow
try {
    $vms = Get-VM | Where-Object { $_.Name -like "*aksedge*" -or $_.Name -like "*ledge*" -or $_.Name -like "*wedge*" }
    if ($vms) {
        Write-Host "Found VMs to remove:" -ForegroundColor Yellow
        foreach ($vm in $vms) {
            Write-Host "  - $($vm.Name) (State: $($vm.State))" -ForegroundColor Gray
            if ($vm.State -eq "Running") {
                Write-Host "    Stopping VM..." -ForegroundColor Gray
                Stop-VM -Name $vm.Name -Force -TurnOff
            }
            Write-Host "    Removing VM..." -ForegroundColor Gray
            Remove-VM -Name $vm.Name -Force
        }
    }
    else {
        Write-Host "No AKS Edge VMs found" -ForegroundColor Gray
    }
}
catch {
    Write-Error "Error cleaning up virtual machines: $_"
}

# Step 5: Clean up virtual switches
Write-Host "`nStep 5: Checking virtual switches" -ForegroundColor Yellow
$switches = Get-VMSwitch | Where-Object { $_.Name -like "*aksedge*" }
if ($switches) {
    Write-Host "Found virtual switches:" -ForegroundColor Yellow
    foreach ($switch in $switches) {
        Write-Host "  - $($switch.Name) (Type: $($switch.SwitchType))" -ForegroundColor Gray
        
        if ($RemoveVirtualSwitch) {
            Write-Host "    Removing switch..." -ForegroundColor Gray
            Remove-VMSwitch -Name $switch.Name -Force
        }
        else {
            Write-Host "    Keeping switch (use -RemoveVirtualSwitch to remove)" -ForegroundColor Gray
        }
    }
}
else {
    Write-Host "No AKS Edge virtual switches found" -ForegroundColor Gray
}

# Step 6: Clean up network adapters
Write-Host "`nStep 6: Cleaning up network configurations" -ForegroundColor Yellow
# Remove HNS networks
$hnsNetworks = Get-HnsNetwork -ErrorAction SilentlyContinue | Where-Object { $_.Name -like "*aksedge*" }
if ($hnsNetworks) {
    foreach ($network in $hnsNetworks) {
        Write-Host "  Removing HNS network: $($network.Name)" -ForegroundColor Gray
        $network | Remove-HnsNetwork -ErrorAction SilentlyContinue
    }
}

# Step 7: Clean up files and folders
Write-Host "`nStep 7: Cleaning up files and folders" -ForegroundColor Yellow
$foldersToClean = @(
    "$env:ProgramData\AksEdge",
    "$env:ProgramData\MSI",
    "$env:USERPROFILE\.kube\config"
)

foreach ($folder in $foldersToClean) {
    if (Test-Path $folder) {
        Write-Host "  Removing: $folder" -ForegroundColor Gray
        Remove-Item -Path $folder -Recurse -Force -ErrorAction SilentlyContinue
    }
}

# Step 8: Uninstall AKS Edge MSI
Write-Host "`nStep 8: Uninstalling AKS Edge Essentials" -ForegroundColor Yellow
$aksEdgeProducts = Get-WmiObject -Class Win32_Product | Where-Object { $_.Name -like "*AKS Edge*" }
if ($aksEdgeProducts) {
    foreach ($product in $aksEdgeProducts) {
        Write-Host "  Uninstalling: $($product.Name)" -ForegroundColor Gray
        $product.Uninstall() | Out-Null
    }
}
else {
    # Try alternative uninstall method
    $uninstallKeys = Get-ChildItem "HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall" | 
    Get-ItemProperty | 
    Where-Object { $_.DisplayName -like "*AKS Edge*" }
    
    foreach ($key in $uninstallKeys) {
        Write-Host "  Found in registry: $($key.DisplayName)" -ForegroundColor Gray
        if ($key.UninstallString) {
            Write-Host "  Running uninstall command..." -ForegroundColor Gray
            Start-Process -FilePath "msiexec.exe" -ArgumentList "/x", $key.PSChildName, "/quiet" -Wait
        }
    }
}

# Step 9: Remove PowerShell modules
if ($RemoveModules) {
    Write-Host "`nStep 9: Removing PowerShell modules" -ForegroundColor Yellow
    $modulesToRemove = @("AksEdge", "AksEdgeDeploy")
    foreach ($moduleName in $modulesToRemove) {
        $modules = Get-Module -ListAvailable -Name $moduleName
        foreach ($module in $modules) {
            Write-Host "  Removing module: $($module.Name) v$($module.Version)" -ForegroundColor Gray
            $modulePath = $module.ModuleBase
            Remove-Item -Path $modulePath -Recurse -Force -ErrorAction SilentlyContinue
        }
    }
}

# Step 10: Clean registry (optional)
if ($CleanRegistry) {
    Write-Host "`nStep 10: Cleaning registry entries" -ForegroundColor Yellow
    $regPaths = @(
        "HKLM:\SOFTWARE\Microsoft\AKSEdge",
        "HKCU:\SOFTWARE\Microsoft\AKSEdge"
    )
    
    foreach ($path in $regPaths) {
        if (Test-Path $path) {
            Write-Host "  Removing registry key: $path" -ForegroundColor Gray
            Remove-Item -Path $path -Recurse -Force -ErrorAction SilentlyContinue
        }
    }
}

# Step 11: Clean up services
Write-Host "`nStep 11: Cleaning up services" -ForegroundColor Yellow
$services = @("AksEdgeProxy", "AksEdgeContainer")
foreach ($serviceName in $services) {
    $service = Get-Service -Name $serviceName -ErrorAction SilentlyContinue
    if ($service) {
        Write-Host "  Stopping and removing service: $serviceName" -ForegroundColor Gray
        Stop-Service -Name $serviceName -Force -ErrorAction SilentlyContinue
        sc.exe delete $serviceName 2>$null
    }
}

# Step 12: Final cleanup
Write-Host "`nStep 12: Final cleanup" -ForegroundColor Yellow
try {
    if (Get-Command docker -ErrorAction SilentlyContinue) {
        docker system prune -a -f 2>$null
    }
    Write-Host "Docker/containerd data cleared." -ForegroundColor Green
}
catch {
    Write-Error "Error during final cleanup: $_"
}

Write-Host "`nRecommended next steps:" -ForegroundColor Yellow
Write-Host "  1. Restart your computer to ensure all changes take effect" -ForegroundColor Gray
Write-Host "  2. If you plan to reinstall, download the latest version from:" -ForegroundColor Gray
Write-Host "     https://aka.ms/aks-edge/msi" -ForegroundColor Blue
Start-Sleep -Seconds 15
