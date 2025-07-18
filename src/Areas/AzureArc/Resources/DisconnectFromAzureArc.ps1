<#
  Script to disconnect AKS Edge cluster from Azure Arc
  This removes the Arc connection but keeps the cluster running
  Basic disconnection
.\DisconnectFromAzureArc.ps1

Force disconnection without confirmation
.\DisconnectFromAzureArc.ps1 -Force

Specify cluster name if multiple clusters exist
.\DisconnectFromAzureArc.ps1 -ClusterName "aksedge-cluster-name" -Force
#>
param(
    [String] $ResourceGroupName,
    [String] $ClusterName
)
#Requires -RunAsAdministrator

Write-Host "Azure Arc Disconnection Script" -ForegroundColor Cyan
Write-Host "==============================" -ForegroundColor Cyan

# Step 1: Check Azure CLI login
Write-Host "`nStep 1: Checking Azure CLI login" -ForegroundColor Yellow
$azAccount = az account show 2>$null | ConvertFrom-Json
if (-not $azAccount) {
    Write-Host "Please login to Azure:" -ForegroundColor Red
    az login
    $azAccount = az account show 2>$null | ConvertFrom-Json
}
Write-Host "Logged in as: $($azAccount.user.name)" -ForegroundColor Green

# Step 2: List Arc-connected clusters
Write-Host "`nStep 2: Listing Arc-connected clusters in resource group '$ResourceGroupName'" -ForegroundColor Yellow
$arcClusters = az connectedk8s list --resource-group $ResourceGroupName 2>$null | ConvertFrom-Json

if (-not $arcClusters -or $arcClusters.Count -eq 0) {
    Write-Host "No Arc-connected clusters found in resource group '$ResourceGroupName'" -ForegroundColor Yellow
    exit 0
}

Write-Host "Found Arc-connected clusters:" -ForegroundColor Green
foreach ($cluster in $arcClusters) {
    Write-Host "  - $($cluster.name) (Status: $($cluster.connectivityStatus), Location: $($cluster.location))" -ForegroundColor Gray
}

# Step 3: Select cluster to disconnect
if (-not $ClusterName) {
    if ($arcClusters.Count -eq 1) {
        $ClusterName = $arcClusters[0].name
        Write-Host "`nAutomatically selecting the only cluster: $ClusterName" -ForegroundColor Yellow
    }
    else {
        Write-Host "`nMultiple clusters found. Please specify -ClusterName parameter." -ForegroundColor Red
        exit 1
    }
}

# Step 4: Confirm disconnection
Write-Host "`nYou are about to disconnect cluster '$ClusterName' from Azure Arc." -ForegroundColor Yellow
Write-Host "This will remove the Arc agents from your cluster but the cluster will continue running." -ForegroundColor Yellow
$confirm = Read-Host "Are you sure you want to proceed? (yes/no)"
if ($confirm -ne "yes") {
    Write-Host "Disconnection cancelled." -ForegroundColor Yellow
    exit 0
}


# Step 5: Check for Arc extensions
Write-Host "`nStep 5: Checking for Arc extensions" -ForegroundColor Yellow
$extensions = az k8s-extension list --cluster-name $ClusterName --resource-group $ResourceGroupName --cluster-type connectedClusters 2>$null | ConvertFrom-Json

if ($extensions -and $extensions.Count -gt 0) {
    Write-Host "Found Arc extensions:" -ForegroundColor Yellow
    foreach ($ext in $extensions) {
        Write-Host "  - $($ext.name) (Type: $($ext.extensionType))" -ForegroundColor Gray
    }
    
    Write-Host "`nRemoving Arc extensions..." -ForegroundColor Yellow
    foreach ($ext in $extensions) {
        Write-Host "  Removing extension: $($ext.name)" -ForegroundColor Gray
        az k8s-extension delete --name $ext.name --cluster-name $ClusterName --resource-group $ResourceGroupName --cluster-type connectedClusters --yes 2>&1 | Out-Null
    }
}Start-Sleep -Seconds 10

# Step 6: Disconnect from Arc
Write-Host "`nStep 6: Disconnecting cluster from Azure Arc" -ForegroundColor Yellow
Write-Host "This may take a few minutes..." -ForegroundColor Gray

$disconnectResult = az connectedk8s delete --name $ClusterName --resource-group $ResourceGroupName --yes 2>&1

if ($LASTEXITCODE -eq 0) {
    Write-Host "`nSuccessfully disconnected from Azure Arc!" -ForegroundColor Green
}
else {
    Write-Error "Failed to disconnect from Arc: $disconnectResult"
    exit 1
}
Start-Sleep -Seconds 10

# Step 8: Delete Arc resource from Azure
Write-Host "`nStep 8: Deleting Azure Arc resource from Azure" -ForegroundColor Yellow
Write-Host "Deleting Arc resource '$ClusterName' in resource group '$ResourceGroupName'..." -ForegroundColor Gray

$deleteResult = az resource delete --name $ClusterName --resource-group $ResourceGroupName --resource-type "Microsoft.Kubernetes/connectedClusters" --yes 2>&1

if ($LASTEXITCODE -eq 0) {
    Write-Host "Successfully deleted Azure Arc resource '$ClusterName' from Azure." -ForegroundColor Green
}
else {
    Write-Error "Failed to delete Azure Arc resource: $deleteResult"
    exit 1
}

Write-Host "`nArc disconnection completed!" -ForegroundColor Green
Write-Host "Your AKS Edge cluster is still running locally but is no longer connected to Azure Arc." -ForegroundColor Cyan
Start-Sleep -Seconds 15