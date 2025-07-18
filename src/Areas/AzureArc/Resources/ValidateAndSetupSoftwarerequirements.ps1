<#
  Script to validate software requirements for AKS Edge Essentials
  and install necessary components. Also downloads and installs
  AKS Edge Essentials MSI if not present.
#>

#Requires -RunAsAdministrator

Write-Host "Validating Software Requirements for AKS Edge Essentials..." -ForegroundColor Cyan
Write-Host "==========================================================" -ForegroundColor Cyan

# Define URLs and paths
$msiDownloadUrl = "https://aka.ms/aks-edge/k8s-msi"
$msiFileName = "AKSEdgeEssentials.msi"
$msiFilePath = "$PSScriptRoot\$msiFileName"

# Validate PowerShell version
Write-Host "Checking PowerShell version..." -ForegroundColor Yellow
if ($PSVersionTable.PSVersion.Major -lt 5) {
    Write-Error "PowerShell version 5.0 or higher is required. Current version: $($PSVersionTable.PSVersion)"
    exit 1
}
Write-Host "PowerShell version: $($PSVersionTable.PSVersion)" -ForegroundColor Green

# Validate .NET Framework
Write-Host "Checking .NET Framework version..." -ForegroundColor Yellow
$dotNetVersion = (Get-ChildItem 'HKLM:\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full' -ErrorAction SilentlyContinue).GetValue("Release")
if (-not $dotNetVersion -or $dotNetVersion -lt 528040) {
    Write-Host ".NET Framework 4.8 or higher is required. Installing .NET Framework..." -ForegroundColor Yellow
    Start-Process -FilePath "https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net48-web-installer" -Wait
    Write-Host ".NET Framework installation completed. Please restart the script if required." -ForegroundColor Green
}
else {
    Write-Host ".NET Framework version is sufficient." -ForegroundColor Green
}

# Validate Windows Package Manager (winget)
Write-Host "Checking Windows Package Manager (winget)..." -ForegroundColor Yellow
if (-not (Get-Command winget -ErrorAction SilentlyContinue)) {
    Write-Host "Windows Package Manager (winget) is not installed. Installing winget..." -ForegroundColor Yellow
    Start-Process -FilePath "https://aka.ms/getwinget" -Wait
    Write-Host "winget installation completed. Please restart the script if required." -ForegroundColor Green
}
else {
    Write-Host "Windows Package Manager (winget) is installed." -ForegroundColor Green
}

# Check and download AKS Edge Essentials MSI
Write-Host "Checking for AKS Edge Essentials MSI..." -ForegroundColor Yellow
if (-not (Test-Path -Path $msiFilePath)) {
    Write-Host "AKS Edge Essentials MSI not found. Downloading..." -ForegroundColor Yellow
    try {
        Start-BitsTransfer -Source $msiDownloadUrl -Destination $msiFilePath
        Write-Host "AKS Edge Essentials MSI downloaded successfully." -ForegroundColor Green
    }
    catch {
        Write-Error "Failed to download AKS Edge Essentials MSI: $_"
        exit 1
    }
}
else {
    Write-Host "AKS Edge Essentials MSI is already present." -ForegroundColor Green
}

# Install the MSI
Write-Host "Installing AKS Edge Essentials MSI..." -ForegroundColor Yellow
try {
    Start-Process -FilePath msiexec.exe -ArgumentList "/i $msiFilePath VHDXDIR=$PSScriptRoot\vhdx" -Wait
    Write-Host "AKS Edge Essentials installation completed successfully." -ForegroundColor Green
}
catch {
    Write-Error "Failed to install AKS Edge Essentials MSI: $_"
    exit 1
}

# Validate Azure CLI
Write-Host "Checking Azure CLI installation..." -ForegroundColor Yellow
if (-not (Get-Command az -ErrorAction SilentlyContinue)) {
    Write-Host "Azure CLI is not installed. Installing Azure CLI..." -ForegroundColor Yellow
    Start-Process -FilePath "https://aka.ms/installazurecliwindows" -Wait
    Write-Host "Azure CLI installation completed. Please restart the script if required." -ForegroundColor Green
}
else {
    Write-Host "Azure CLI is installed." -ForegroundColor Green
}

# Validate Connectedk8s Extension
Write-Host "Checking Connectedk8s extension for Azure CLI..." -ForegroundColor Yellow
$connectedK8sExtension = az extension list --query "[?name=='connectedk8s']" --output tsv
if (-not $connectedK8sExtension) {
    Write-Host "Connectedk8s extension is not installed. Installing extension..." -ForegroundColor Yellow
    az extension add --name connectedk8s
    Write-Host "Connectedk8s extension installed successfully." -ForegroundColor Green
}
else {
    Write-Host "Connectedk8s extension is already installed." -ForegroundColor Green
}

Write-Host "Software requirements validated and necessary components installed successfully!" -ForegroundColor Green

Write-Host "Some changes may require a system restart to take effect." -ForegroundColor Yellow
$restartPrompt = Read-Host "Do you want to restart the system now? (yes/no)"
if ($restartPrompt -eq "yes") {
    Write-Host "Restarting the system..." -ForegroundColor Yellow
    Restart-Computer
}
else {
    Write-Host "Please restart the system manually if required." -ForegroundColor Green
}
