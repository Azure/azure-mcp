<#
  Connect AKS Edge to Azure Arc - Fixed version without YAML dependency
#>
param(
    [String] $SubscriptionId,
    [String] $TenantId,
    [String] $Location,
    [String] $ResourceGroupName,
    [String] $ClusterName,
    [String] $KubeConfigPath
)
#Requires -RunAsAdministrator

Write-Host "Connect AKS Edge to Azure Arc" -ForegroundColor Cyan
Write-Host "=============================" -ForegroundColor Cyan

# Step 1: Check kubeconfig and context
Write-Host "`nStep 1: Checking kubeconfig" -ForegroundColor Yellow

# Ensure kubeconfig exists
if (-not (Test-Path $KubeConfigPath)) {
    $KubeConfigPath = "$env:USERPROFILE\.kube\config"
    if (-not (Test-Path $kubeConfigPath)) {
        Write-Host "Kubeconfig not found at $KubeConfigPath. Getting from AKS Edge..." -ForegroundColor Yellow
        $kubeConfigPath = $null
    }
    
    # like this : Import-Module "C:\Program Files\WindowsPowerShell\Modules\AksEdge\1.10.868.0\AksEdge.psd1" -Force
    # Dynamically find the installed version of the AksEdge module
    $aksEdgeModulePath = "C:\Program Files\WindowsPowerShell\Modules\AksEdge"
    $aksEdgeVersion = (Get-ChildItem -Path $aksEdgeModulePath -Directory | Sort-Object Name -Descending | Select-Object -First 1).Name
    
    if (-not $aksEdgeVersion) {
        Write-Error "AksEdge module not found. Please ensure it is installed."
        exit 1
    }
    
    $aksEdgePsModulePath = "$aksEdgeModulePath\$aksEdgeVersion\AksEdge.psd1"
    Import-Module $aksEdgePsModulePath -Force
    
    # Create directory if needed
    $kubeDir = Split-Path $KubeConfigPath -Parent
    if (-not (Test-Path $kubeDir)) {
        New-Item -ItemType Directory -Path $kubeDir -Force | Out-Null
    }
    
    Get-AksEdgeKubeConfig -KubeConfigPath $KubeConfigPath
}
Start-Sleep -Seconds 10

# Check current context using kubectl
Write-Host "`nChecking current context..." -ForegroundColor Yellow
$currentContext = kubectl config current-context --kubeconfig $KubeConfigPath 2>$null

if ($currentContext) {
    Write-Host "Current context: $currentContext" -ForegroundColor Green
    
    # List all contexts
    Write-Host "`nAvailable contexts:" -ForegroundColor Gray
    kubectl config get-contexts --kubeconfig $KubeConfigPath
}
else {
    Write-Warning "No current context set in kubeconfig"
}
Start-Sleep -Seconds 10
# Step 2: Verify cluster access
Write-Host "`nStep 2: Verifying cluster access" -ForegroundColor Yellow
$nodes = kubectl get nodes --kubeconfig $KubeConfigPath 2>&1

if ($LASTEXITCODE -eq 0) {
    Write-Host "Cluster access verified:" -ForegroundColor Green
    Write-Host $nodes -ForegroundColor Gray
}
else {
    Write-Error "Cannot access cluster: $nodes"
    Write-Host "`nTrying to fix kubeconfig..." -ForegroundColor Yellow
    
    # Try to get a fresh kubeconfig
    # if you know the kubeconfig path, you can set it directly: 
    # like this : Import-Module "C:\Program Files\WindowsPowerShell\Modules\AksEdge\1.10.868.0\AksEdge.psd1" -Force
    # Dynamically find the installed version of the AksEdge module
    $aksEdgeModulePath = "C:\Program Files\WindowsPowerShell\Modules\AksEdge"
    $aksEdgeVersion = (Get-ChildItem -Path $aksEdgeModulePath -Directory | Sort-Object Name -Descending | Select-Object -First 1).Name
    
    if (-not $aksEdgeVersion) {
        Write-Error "AksEdge module not found. Please ensure it is installed."
        exit 1
    }
    
    $aksEdgePsModulePath = "$aksEdgeModulePath\$aksEdgeVersion\AksEdge.psd1"
    Import-Module $aksEdgePsModulePath -Force
    Remove-Item $KubeConfigPath -Force -ErrorAction SilentlyContinue
    Get-AksEdgeKubeConfig -KubeConfigPath $KubeConfigPath
    
    # Test again
    $nodes = kubectl get nodes --kubeconfig $KubeConfigPath 2>&1
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Still cannot access cluster. Please check your AKS Edge deployment."
        exit 1
    }
}
Start-Sleep -Seconds 10
# Step 3: Azure CLI setup
Write-Host "`nStep 3: Setting up Azure CLI" -ForegroundColor Yellow

# Check Azure CLI login
$azAccount = az account show 2>$null | ConvertFrom-Json
if (-not $azAccount) {
    Write-Host "Please login to Azure:" -ForegroundColor Red
    az login
    $azAccount = az account show 2>$null | ConvertFrom-Json
}

Write-Host "Logged in as: $($azAccount.user.name)" -ForegroundColor Green

# Set subscription
if ($azAccount.id -ne $SubscriptionId) {
    Write-Host "Setting subscription..." -ForegroundColor Yellow
    az account set --subscription $SubscriptionId
}

# Install connectedk8s extension
$extensions = az extension list --query "[?name=='connectedk8s'].name" -o tsv
if (-not $extensions) {
    Write-Host "Installing Azure CLI connectedk8s extension..." -ForegroundColor Yellow
    az extension add --name connectedk8s
}

# Step 4: Clean up any existing Arc connection
Write-Host "`nStep 4: Checking for existing Arc connections" -ForegroundColor Yellow

# List all Arc clusters in the resource group
$existingClusters = az connectedk8s list --resource-group $ResourceGroupName 2>$null | ConvertFrom-Json

if ($existingClusters -and $existingClusters.Count -gt 0) {
    Write-Host "Found existing Arc clusters:" -ForegroundColor Yellow
    foreach ($cluster in $existingClusters) {
        Write-Host "  - $($cluster.name) (Status: $($cluster.connectivityStatus))" -ForegroundColor Gray
        
        # If there's a cluster with the same name, delete it first
        if ($cluster.name -eq $ClusterName) {
            Write-Host "Removing existing Arc connection for $ClusterName..." -ForegroundColor Yellow
            az connectedk8s delete --name $ClusterName --resource-group $ResourceGroupName --yes 2>$null
            Start-Sleep -Seconds 10
        }
    }
}

# Step 5: Connect to Azure Arc
Write-Host "`nStep 5: Connecting to Azure Arc" -ForegroundColor Yellow
Write-Host "Cluster Name: $ClusterName" -ForegroundColor Gray
Write-Host "Resource Group: $ResourceGroupName" -ForegroundColor Gray
Write-Host "Location: $Location" -ForegroundColor Gray

try {
    # Get the current context again
    $currentContext = kubectl config current-context --kubeconfig $KubeConfigPath 2>$null
    
    # Build the connect command
    $connectParams = @(
        "connectedk8s", "connect",
        "--name", "`"$ClusterName`"",
        "--resource-group", "`"$ResourceGroupName`"",
        "--location", "`"$Location`"",
        "--kube-config", "`"$KubeConfigPath`""
    )
    
    # Add context if available
    if ($currentContext) {
        $connectParams += "--kube-context", "`"$currentContext`""
    }
    
    # Add tags
    $connectParams += "--tags", "CreatedBy=AKSEdge", "Environment=Development"
    
    Write-Host "`nExecuting Arc connection (this may take 5-10 minutes)..." -ForegroundColor Yellow
    $connectResult = & az @connectParams 2>&1
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "`nAzure Arc connection successful!" -ForegroundColor Green
        
        # Get cluster details
        $arcCluster = az connectedk8s show --name $ClusterName --resource-group $ResourceGroupName | ConvertFrom-Json
        
        Write-Host "`nConnected cluster details:" -ForegroundColor Cyan
        Write-Host "  Name: $($arcCluster.name)" -ForegroundColor Gray
        Write-Host "  Status: $($arcCluster.connectivityStatus)" -ForegroundColor Gray
        Write-Host "  Location: $($arcCluster.location)" -ForegroundColor Gray
        Write-Host "  Kubernetes Version: $($arcCluster.kubernetesVersion)" -ForegroundColor Gray
        Write-Host "  Agent Version: $($arcCluster.agentVersion)" -ForegroundColor Gray
        
        Write-Host "`nAzure Portal URL:" -ForegroundColor Cyan
        Write-Host "https://portal.azure.com/#resource$($arcCluster.id)/overview" -ForegroundColor Blue
        
    }
    else {
        Write-Error "Arc connection failed"
        Write-Host "`nError details:" -ForegroundColor Red
        Write-Host $connectResult -ForegroundColor Red
        
        # Try alternative approach
        Write-Host "`nTrying alternative connection method..." -ForegroundColor Yellow
        
        # Create a new kubeconfig with default context
        $tempKubeconfig = "$env:TEMP\aksedge-kubeconfig.yaml"
        Copy-Item $KubeConfigPath $tempKubeconfig -Force
        
        # Try to set a default context
        kubectl config rename-context $currentContext default --kubeconfig $tempKubeconfig 2>$null
        
        # Try again with the modified kubeconfig
        $connectResult2 = az connectedk8s connect `
            --name $ClusterName `
            --resource-group $ResourceGroupName `
            --location $Location `
            --kube-config $tempKubeconfig `
            --kube-context "default" `
            --tags CreatedBy=AKSEdge Environment=Development `
            2>&1
            
        if ($LASTEXITCODE -eq 0) {
            Write-Host "Arc connection successful with modified kubeconfig!" -ForegroundColor Green
        }
        else {
            Write-Error "Alternative connection also failed: $connectResult2"
        }
        
        Remove-Item $tempKubeconfig -Force -ErrorAction SilentlyContinue
    }
    
}
catch {
    Write-Error "Exception during Arc connection: $_"
}
Start-Sleep -Seconds 10
# Step 6: Verify Arc installation
Write-Host "`nStep 6: Verifying Arc installation in cluster" -ForegroundColor Yellow

# Check Arc namespace
$arcNamespace = kubectl get namespace azure-arc --kubeconfig $KubeConfigPath 2>$null
if ($LASTEXITCODE -eq 0) {
    Write-Host "Azure Arc namespace exists" -ForegroundColor Green
    
    # Show Arc pods
    Write-Host "`nArc pods:" -ForegroundColor Yellow
    kubectl get pods -n azure-arc --kubeconfig $KubeConfigPath
    
    # Check for any pods not running
    $notRunning = kubectl get pods -n azure-arc --kubeconfig $KubeConfigPath -o json | ConvertFrom-Json | 
    Select-Object -ExpandProperty items | 
    Where-Object { $_.status.phase -ne "Running" }
    
    if ($notRunning) {
        Write-Warning "Some Arc pods are not running. Checking logs..."
        foreach ($pod in $notRunning) {
            Write-Host "`nLogs for $($pod.metadata.name):" -ForegroundColor Yellow
            kubectl logs -n azure-arc $pod.metadata.name --tail=20 --kubeconfig $KubeConfigPath
        }
    }
}
else {
    Write-Warning "Azure Arc namespace not found"
}

# Step 7: Summary and next steps
Write-Host "`n======================================" -ForegroundColor Cyan
Write-Host "Summary" -ForegroundColor Cyan
Write-Host "======================================" -ForegroundColor Cyan

Write-Host "`nUseful commands:" -ForegroundColor Yellow
Write-Host "  # View Arc cluster in Azure:" -ForegroundColor Gray
Write-Host "  az connectedk8s show --name $ClusterName --resource-group $ResourceGroupName -o table" -ForegroundColor Gray
Write-Host "`n  # Check Arc agents:" -ForegroundColor Gray
Write-Host "  kubectl get pods -n azure-arc --kubeconfig `"$KubeConfigPath`"" -ForegroundColor Gray
Write-Host "`n  # View Arc agent logs:" -ForegroundColor Gray
Write-Host "  kubectl logs -n azure-arc -l app.kubernetes.io/component=connect-agent --tail=50 --kubeconfig `"$KubeConfigPath`"" -ForegroundColor Gray
Write-Host "`n  # Enable GitOps:" -ForegroundColor Gray
Write-Host "  az k8s-configuration flux create --name gitops-config --cluster-name $ClusterName --resource-group $ResourceGroupName --namespace gitops --cluster-type connectedClusters --scope cluster --url https://github.com/Azure/arc-k8s-demo --branch main --kustomization name=infra path=./infrastructure prune=true" -ForegroundColor Gray

Write-Host "`nScript completed!" -ForegroundColor Green
Start-Sleep -Seconds 15