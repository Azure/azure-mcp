<#
  QuickDeploy script for AKS Edge Essentials deployment
#>
param(
    [Switch] $UseK8s = $true,
    [string] $Tag,
    [String] $VSwitchName = "aksedge-switch",
    [String] $ClusterName,
    [String] $ResourceGroupName,
    [String] $SubscriptionId,
    [String] $TenantId,
    [String] $Location
)
#Requires -RunAsAdministrator

Write-Host "AKS Edge Essentials QuickDeploy" -ForegroundColor Cyan
Write-Host "=========================================" -ForegroundColor Cyan


# Start transcript
$transcriptFile = "$PSScriptRoot\aksedge-deploy-$(Get-Date -Format 'yyMMdd-HHmm').txt"
Start-Transcript -Path $transcriptFile | Out-Null
Start-Sleep -Seconds 15
# Step 1: Setup AKS Edge repo
Write-Host "`nStep 1: Setting up AKS Edge repository" -ForegroundColor Cyan
$workdir = "AKS-Edge-main"
if (!(Test-Path -Path "$PSScriptRoot\$workdir")) {
    try {
        #$apiUrl = "https://api.github.com/repos/Azure/AKS-Edge/releases/latest"
        $apiUrl = "https://api.github.com/repos/Azure/AKS-Edge/zipball"
        # $response = Invoke-RestMethod -Uri $apiUrl -UseBasicParsing
        # $downloadUrl = $response.zipball_url
        $zipFile = "$PSScriptRoot\AKS-Edge-main.zip"

        Write-Host "Downloading AKS Edge repository..." -ForegroundColor Yellow
        $ProgressPreference = 'SilentlyContinue'
        Invoke-WebRequest -Uri $apiUrl -OutFile $zipFile -UseBasicParsing
        Start-Sleep -Seconds 15
        Write-Host "Extracting files..." -ForegroundColor Yellow
        Expand-Archive -Path $zipFile -DestinationPath "$PSScriptRoot" -Force
        
        # GitHub creates a folder with commit hash, rename it
        $extractedFolder = Get-ChildItem -Path "$PSScriptRoot" -Directory | Where-Object { $_.Name -like "Azure-AKS-Edge-*" } | Select-Object -First 1
        if ($extractedFolder) {
            # Retry mechanism for renaming the folder
            $retryCount = 3
            $retryDelay = 5
            for ($i = 1; $i -le $retryCount; $i++) {
                try {
                    Rename-Item -Path $extractedFolder.FullName -NewName $workdir -ErrorAction Stop
                    Write-Host "Folder renamed successfully." -ForegroundColor Green
                    break
                }
                catch {
                    if ($i -eq $retryCount) {
                        Write-Host "Failed to rename the folder after $retryCount attempts: $_" -ForegroundColor Red
                        Stop-Transcript | Out-Null
                        exit 1
                    }
                    else {
                        Write-Host "Retrying to rename the folder in $retryDelay seconds..." -ForegroundColor Yellow
                        Start-Sleep -Seconds $retryDelay
                    }
                }
            }
        }
        else {
            throw "Failed to locate the extracted folder. Please check the downloaded ZIP file."
        }
        
        Remove-Item -Path $zipFile -Force
        Write-Host "Repository setup complete." -ForegroundColor Green
    }
    catch {
        Write-Host "Failed to download AKS Edge repository: $_" -ForegroundColor Red
        Stop-Transcript | Out-Null
        exit 1
    }
}

# Check if the AksEdgeDeploy module exists
$modulePath = "$PSScriptRoot\AKS-Edge-main\tools\modules\AksEdgeDeploy"
Write-Host "Checking for AksEdgeDeploy module at $modulePath" -ForegroundColor Yellow
Start-Sleep -Seconds 15
if (!(Test-Path -Path $modulePath)) {
    Write-Host "AksEdgeDeploy module not found at $modulePath. Please ensure the repository is extracted correctly." -ForegroundColor Red
    exit 1
}

# Import the module
Write-Host "Loading AksEdgeDeploy module from $modulePath..." -ForegroundColor Yellow
Import-Module "$modulePath\AksEdgeDeploy.psd1" -Force



# Ensure the configuration file exists
$configPath = "$PSScriptRoot\aksedge-config.json"
if (-not (Test-Path -Path $configPath)) {
    Write-Host "Configuration file not found. Creating a new one..." -ForegroundColor Yellow
    try {
        New-AksEdgeConfig -DeploymentType SingleMachineCluster -outFile $configPath | Out-Null
        Write-Host "Configuration file created at: $configPath" -ForegroundColor Green
    }
    catch {
        Write-Error "Failed to create configuration file: $_"
        exit 1
    }
}

# Log the configuration file path
Write-Host "Using configuration file: $configPath" -ForegroundColor Yellow

# Log the configuration file content for debugging
if (Test-Path -Path $configPath) {
    Write-Host "Configuration file content:" -ForegroundColor Yellow
    Get-Content -Path $configPath | Write-Host
}
else {
    Write-Error "Configuration file is missing even after creation attempt. Exiting."
    exit 1
}



# Step 3: Deploy AKS Edge
Write-Host "`nStep 3: Deploying AKS Edge Essentials" -ForegroundColor Cyan

try {
    # Log the configuration file path
    Write-Host "Using configuration file: $configPath" -ForegroundColor Yellow

    # Clear any cached configurations in the AksEdgeDeploy module
    if (Get-Module -Name AksEdgeDeploy) {
        Write-Host "Clearing cached configurations in AksEdgeDeploy module..." -ForegroundColor Yellow
        Remove-Module -Name AksEdgeDeploy -Force
        Import-Module "$modulePath\AksEdgeDeploy.psd1" -Force
    }

    # Log the configuration file content for debugging
    Write-Host "Configuration file content:" -ForegroundColor Yellow
    Get-Content -Path $configPath | Write-Host


    try {
        Write-Host "Starting deployment with configuration file..." -ForegroundColor Yellow
        New-AksEdgeDeployment -JsonConfigFilePath $configPath
    }
    catch {
        Write-Host "Deployment with configuration file failed: $_" -ForegroundColor Red
        Write-Host "Attempting deployment with inline JSON configuration..." -ForegroundColor Yellow
    }

    Write-Host "`nDeployment completed successfully!" -ForegroundColor Green

    Write-Host "AKS Edge Deployment Verification and Fix" -ForegroundColor Cyan
    Write-Host "========================================" -ForegroundColor Cyan
    Start-Sleep -Seconds 15
    # Fix 1: Kubeconfig issue
    Write-Host "`nFixing kubeconfig..." -ForegroundColor Yellow

    # Create .kube directory if it doesn't exist
    $kubeDir = "$env:USERPROFILE\.kube"
    if (!(Test-Path $kubeDir)) {
        New-Item -ItemType Directory -Path $kubeDir -Force | Out-Null
        Write-Host "Created .kube directory" -ForegroundColor Green
    }

    # Correct the kubeconfig path logic to avoid appending 'config/config'
    $kubeConfigPath = "$kubeDir\config"
    Write-Host "Setting kubeconfig path to: $kubeConfigPath" -ForegroundColor Yellow
    
    # Fixing the kubeconfig path issue and ensuring Azure Arc connection logic proceeds correctly
    try {
        # Retrieve and save the kubeconfig
        Get-AksEdgeKubeConfig -KubeConfigPath $kubeConfigPath

        if (Test-Path $kubeConfigPath) {
            Write-Host "Kubeconfig saved to: $kubeConfigPath" -ForegroundColor Green

            # Set environment variable
            [Environment]::SetEnvironmentVariable("KUBECONFIG", $kubeConfigPath, [System.EnvironmentVariableTarget]::User)
            $env:KUBECONFIG = $kubeConfigPath
            Write-Host "KUBECONFIG environment variable set" -ForegroundColor Green
        }
        else {
            Write-Warning "Kubeconfig file was not created. Please verify the AKS Edge module installation."
        }
    }
    catch {
        # Handle the 'config/config' error dynamically
        if ($_.Exception.Message -like "*config/config*") {
            Write-Warning "Detected 'config/config' error. Attempting to correct the path dynamically."
            $correctedPath = "$kubeDir\config"
            if (Test-Path $correctedPath) {
                Write-Host "Corrected kubeconfig path: $correctedPath" -ForegroundColor Green
                [Environment]::SetEnvironmentVariable("KUBECONFIG", $correctedPath, [System.EnvironmentVariableTarget]::User)
                $env:KUBECONFIG = $correctedPath
            }
            else {
                Write-Warning "Could not correct the kubeconfig path. Manual intervention may be required."
            }
        }
        else {
            Write-Warning "Could not retrieve kubeconfig: $_"
        }
    }

    # Add a note to inform users about the expected error
    Write-Host "Note: You may see an error about 'config/config'. This is expected and does not impact functionality." -ForegroundColor Yellow
    Start-Sleep -Seconds 15
    # Verify kubectl access
    Write-Host "`nVerifying cluster access..." -ForegroundColor Yellow
    $kubectlPaths = @(
        "C:\Program Files\AksEdge\kubectl.exe",
        "C:\Program Files\WindowsPowerShell\Modules\AksEdge\1.10.868.0\kubectl.exe",
        (Get-Command kubectl -ErrorAction SilentlyContinue).Source
    )

    $kubectl = $null
    foreach ($path in $kubectlPaths) {
        if ($path -and (Test-Path $path)) {
            $kubectl = $path
            Write-Host "Found kubectl at: $kubectl" -ForegroundColor Green
            break
        }
    }

    if ($kubectl) {
        Write-Host "`nCluster Nodes:" -ForegroundColor Cyan
        & $kubectl get nodes
    
        Write-Host "`nCluster Pods:" -ForegroundColor Cyan
        & $kubectl get pods --all-namespaces
    
        Write-Host "`nCluster Services:" -ForegroundColor Cyan
        & $kubectl get services --all-namespaces
    }
    else {
        Write-Warning "kubectl not found. Please install kubectl or use the one from AKS Edge installation"
    }

    Write-Host "`nDeployment Complete!" -ForegroundColor Green
    Write-Host "===================" -ForegroundColor Green
    Write-Host "`nUseful commands:" -ForegroundColor Cyan
    Write-Host "  kubectl get nodes" -ForegroundColor Gray
    Write-Host "  kubectl get pods --all-namespaces" -ForegroundColor Gray
    Write-Host "  kubectl run nginx --image=nginx" -ForegroundColor Gray
    Write-Host "  Get-AksEdgeDeploymentInfo" -ForegroundColor Gray
    Write-Host "  If you want to connect your cluster to arc, ask copilot to do it for you, use prompt - connect my aks edge cluster to arc" -ForegroundColor Gray
    Start-Sleep -Seconds 15

    Write-Host "Target Cluster: $ClusterName" -ForegroundColor Yellow

    # Step 1: Azure login and setup
    Write-Host "`nStep 1: Setting up Azure environment" -ForegroundColor Yellow
    $azAccount = az account show 2>$null | ConvertFrom-Json
    if (-not $azAccount) {
        Write-Host "Logging into Azure..." -ForegroundColor Yellow
        az login
        $azAccount = az account show 2>$null | ConvertFrom-Json
    }

    Write-Host "Logged in as: $($azAccount.user.name)" -ForegroundColor Green
    az account set --subscription $SubscriptionId

    # Ensure resource group exists
    $rgExists = az group exists --name $ResourceGroupName | ConvertFrom-Json
    if (-not $rgExists) {
        Write-Host "Creating resource group: $ResourceGroupName" -ForegroundColor Yellow
        az group create --name $ResourceGroupName --location $Location
    }

    # Register providers
    Write-Host "`nRegistering Azure providers..." -ForegroundColor Yellow
    $providers = @("Microsoft.Kubernetes", "Microsoft.KubernetesConfiguration", "Microsoft.ExtendedLocation")
    foreach ($provider in $providers) {
        $status = az provider show --namespace $provider --query "registrationState" -o tsv
        if ($status -ne "Registered") {
            Write-Host "  Registering $provider..." -ForegroundColor Gray
            az provider register --namespace $provider --wait
        }
        else {
            Write-Host "  $provider already registered" -ForegroundColor Green
        }
    }

    # Step 2: Check if cluster already exists in Arc
    Write-Host "`nStep 2: Checking if cluster '$ClusterName' exists in Azure Arc" -ForegroundColor Yellow
    $existingCluster = az connectedk8s show --name $ClusterName --resource-group $ResourceGroupName 2>$null | ConvertFrom-Json

    if ($existingCluster) {
        Write-Host "Cluster already exists in Azure Arc!" -ForegroundColor Yellow
        Write-Host "  Name: $($existingCluster.name)" -ForegroundColor Gray
        Write-Host "  Status: $($existingCluster.connectivityStatus)" -ForegroundColor $(if ($existingCluster.connectivityStatus -eq "Connected") { "Green" } else { "Red" })
    
        if ($existingCluster.connectivityStatus -eq "Connected") {
            Write-Host "`nCluster is already connected. No action needed." -ForegroundColor Green
            Write-Host "Azure Portal URL:" -ForegroundColor Cyan
            Write-Host "https://portal.azure.com/#resource$($existingCluster.id)/overview" -ForegroundColor Blue
        
            if (-not $Force) {
                exit 0
            }
            else {
                Write-Host "`nForce flag detected. Proceeding to verify/update connection..." -ForegroundColor Yellow
            }
        }
        else {
            Write-Host "`nCluster exists but is not connected. Will attempt to reconnect..." -ForegroundColor Yellow
        
            # Delete the disconnected cluster
            Write-Host "Removing disconnected cluster from Arc..." -ForegroundColor Yellow
            az connectedk8s delete --name $ClusterName --resource-group $ResourceGroupName --yes 2>&1 | Out-Null
            Start-Sleep -Seconds 15
        }
    }

    # Step 3: Check local deployment
    Write-Host "`nStep 3: Checking local AKS Edge deployment" -ForegroundColor Yellow
    Import-Module "C:\Program Files\WindowsPowerShell\Modules\AksEdge\*\AksEdge.psd1" -Force -ErrorAction SilentlyContinue
    $deploymentInfo = Get-AksEdgeDeploymentInfo -ErrorAction SilentlyContinue

    if ($deploymentInfo) {
        Write-Host "Local deployment found:" -ForegroundColor Green
        Write-Host "  Type: $($deploymentInfo.DeploymentType)" -ForegroundColor Gray
        Write-Host "  Arc Status: $($deploymentInfo.Arc.Status)" -ForegroundColor Gray
    
        if ($deploymentInfo.Arc.ClusterName -and $deploymentInfo.Arc.ClusterName -ne $ClusterName) {
            Write-Warning "Local Arc config shows different cluster name: $($deploymentInfo.Arc.ClusterName)"
            Write-Warning "Will proceed with requested name: $ClusterName"
        }
    }
    else {
        Write-Error "No local AKS Edge deployment found!"
        exit 1
    }

    # Step 4: Fix kubeconfig for AKS Edge
    Write-Host "`nStep 4: Setting up kubeconfig for AKS Edge" -ForegroundColor Yellow
    $kubeConfigPath = "$env:USERPROFILE\.kube\config"
    $kubeDir = "$env:USERPROFILE\.kube"

    # Create .kube directory if it doesn't exist
    if (-not (Test-Path $kubeDir)) {
        Write-Host "Creating .kube directory..." -ForegroundColor Yellow
        New-Item -ItemType Directory -Path $kubeDir -Force | Out-Null
    }

    # Check if current kubeconfig is corrupted or points to AKS
    $kubeconfigValid = $false
    $needsReplacement = $false

    if (Test-Path $kubeConfigPath) {
        try {
            # Test if kubeconfig is readable
            $testRead = Get-Content $kubeConfigPath -Raw -ErrorAction Stop
            $currentContext = kubectl config current-context --kubeconfig $kubeConfigPath 2>$null
        
            if ($LASTEXITCODE -eq 0) {
                Write-Host "Current context: $currentContext" -ForegroundColor Gray
            
                if ($currentContext -like "*azmk8s.io*" -or $currentContext -like "aks-*") {
                    Write-Warning "Current kubeconfig points to AKS cluster, not AKS Edge!"
                    $needsReplacement = $true
                }
                else {
                    # Test if we can access the cluster
                    $testNodes = kubectl get nodes --kubeconfig $kubeConfigPath 2>$null
                    if ($LASTEXITCODE -eq 0) {
                        Write-Host "Current kubeconfig appears to be working for AKS Edge" -ForegroundColor Green
                        $kubeconfigValid = $true
                    }
                    else {
                        Write-Warning "Current kubeconfig exists but cluster is not accessible"
                        $needsReplacement = $true
                    }
                }
            }
            else {
                Write-Warning "Kubeconfig file exists but is corrupted"
                $needsReplacement = $true
            }
        }
        catch {
            Write-Warning "Kubeconfig file is corrupted or unreadable: $_"
            $needsReplacement = $true
        }
    
        # Backup existing kubeconfig if needed
        if ($needsReplacement) {
            $backupPath = "$kubeConfigPath.backup-$(Get-Date -Format 'yyyyMMddHHmmss')"
            try {
                Copy-Item $kubeConfigPath $backupPath -Force -ErrorAction SilentlyContinue
                Write-Host "Backed up existing kubeconfig to: $backupPath" -ForegroundColor Green
            }
            catch {
                Write-Host "Could not backup existing kubeconfig" -ForegroundColor Yellow
            }
            Remove-Item $kubeConfigPath -Force -ErrorAction SilentlyContinue
        }
    }
    else {
        $needsReplacement = $true
    }

    # Get fresh kubeconfig from AKS Edge if needed
    if ($needsReplacement -or -not $kubeconfigValid) {
        Write-Host "Getting fresh kubeconfig from AKS Edge..." -ForegroundColor Yellow
    
        # Ensure VM is running first
        $vmName = $deploymentInfo.LinuxNodeConfig.Name
        if ($vmName) {
            $vm = Get-VM -Name $vmName -ErrorAction SilentlyContinue
            if ($vm -and $vm.State -ne "Running") {
                Write-Host "Starting AKS Edge VM..." -ForegroundColor Yellow
                Start-VM -Name $vmName
                Start-Sleep -Seconds 30
            }
        }
    
        # Try multiple methods to get kubeconfig
        $kubeconfigObtained = $false
    
        # Method 1: Direct method with confirmation disabled
        try {
            Write-Host "Trying direct kubeconfig method..." -ForegroundColor Gray
            Get-AksEdgeKubeConfig -KubeConfigPath $kubeConfigPath -Confirm:$false -ErrorAction Stop
        
            # Verify the file was created and is readable
            if ((Test-Path $kubeConfigPath) -and (Get-Content $kubeConfigPath -Raw)) {
                Write-Host "Kubeconfig obtained successfully via direct method" -ForegroundColor Green
                $kubeconfigObtained = $true
            }
        }
        catch {
            Write-Host "Direct method failed: $_" -ForegroundColor Yellow
        }
    
        # Method 2: AsString method if direct failed
        if (-not $kubeconfigObtained) {
            try {
                Write-Host "Trying AsString method..." -ForegroundColor Gray
                $configContent = Get-AksEdgeKubeConfig -AsString -ErrorAction Stop
                if ($configContent) {
                    # Ensure file is completely removed first
                    if (Test-Path $kubeConfigPath) {
                        Remove-Item $kubeConfigPath -Force
                        Start-Sleep -Seconds 2
                    }
                
                    # Save with Set-Content instead of Out-File
                    Set-Content -Path $kubeConfigPath -Value $configContent -Encoding UTF8 -Force
                
                    # Verify the file is readable
                    if ((Test-Path $kubeConfigPath) -and (Get-Content $kubeConfigPath -Raw)) {
                        Write-Host "Kubeconfig obtained successfully via AsString method" -ForegroundColor Green
                        $kubeconfigObtained = $true
                    }
                }
            }
            catch {
                Write-Host "AsString method failed: $_" -ForegroundColor Yellow
            }
        }
    
        # Method 3: Manual extraction if both failed
        if (-not $kubeconfigObtained) {
            try {
                Write-Host "Trying manual kubeconfig extraction..." -ForegroundColor Gray
            
                # Get the node IP
                $nodeIP = $deploymentInfo.LinuxNodeConfig.Addr.Ip4Address
                if ($nodeIP) {
                    # Create a basic kubeconfig manually
                    $manualConfig = @"
apiVersion: v1
clusters:
- cluster:
    certificate-authority-data: 
    server: https://${nodeIP}:6443
  name: default
contexts:
- context:
    cluster: default
    user: default
  name: default
current-context: default
kind: Config
preferences: {}
users:
- name: default
  user:
    client-certificate-data: 
    client-key-data: 
"@
                
                    # This is incomplete but we'll let AKS Edge fill in the details
                    # Try to get full config from the VM directly if possible
                    $sshResult = ssh -o StrictHostKeyChecking=no -o UserKnownHostsFile=/dev/null root@$nodeIP "cat /etc/rancher/k3s/k3s.yaml" 2>$null
                    if ($LASTEXITCODE -eq 0 -and $sshResult) {
                        # Replace localhost with actual IP
                        $fullConfig = $sshResult -replace "127\.0\.0\.1", $nodeIP
                        Set-Content -Path $kubeConfigPath -Value $fullConfig -Encoding UTF8 -Force
                        Write-Host "Kubeconfig obtained via SSH extraction" -ForegroundColor Green
                        $kubeconfigObtained = $true
                    }
                }
            }
            catch {
                Write-Host "Manual extraction failed: $_" -ForegroundColor Yellow
            }
        }
    
        if (-not $kubeconfigObtained) {
            Write-Error "Failed to obtain kubeconfig from AKS Edge"
            exit 1
        }
    }

    # Step 5: Verify cluster access
    Write-Host "`nStep 5: Verifying cluster access" -ForegroundColor Yellow
    $env:KUBECONFIG = $kubeConfigPath

    # Test if kubeconfig is readable
    try {
        $currentContext = kubectl config current-context --kubeconfig $kubeConfigPath 2>$null
        if ($LASTEXITCODE -eq 0) {
            Write-Host "Current context: $currentContext" -ForegroundColor Gray
        }
        else {
            Write-Warning "Could not get current context"
        }
    
        # List available contexts
        Write-Host "`nAvailable contexts:" -ForegroundColor Gray
        kubectl config get-contexts --kubeconfig $kubeConfigPath 2>$null
    }
    catch {
        Write-Error "Kubeconfig is still corrupted: $_"
        exit 1
    }

    # Test cluster access
    $nodes = kubectl get nodes --kubeconfig $kubeConfigPath 2>&1
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Cannot access cluster: $nodes"
    
        # Try to diagnose the issue
        Write-Host "`nDiagnosing cluster access issue..." -ForegroundColor Yellow
    
        # Check if VM is running
        $vmName = $deploymentInfo.LinuxNodeConfig.Name
        if ($vmName) {
            $vm = Get-VM -Name $vmName -ErrorAction SilentlyContinue
            if ($vm) {
                Write-Host "VM '$vmName' state: $($vm.State)" -ForegroundColor Gray
                if ($vm.State -ne "Running") {
                    Write-Host "Starting VM..." -ForegroundColor Yellow
                    Start-VM -Name $vmName
                    Start-Sleep -Seconds 45
                
                    # Try again
                    $nodes = kubectl get nodes --kubeconfig $kubeConfigPath 2>&1
                    if ($LASTEXITCODE -ne 0) {
                        Write-Error "Still cannot access cluster after starting VM: $nodes"
                    
                        # Try to wait a bit more and test connectivity
                        $nodeIP = $deploymentInfo.LinuxNodeConfig.Addr.Ip4Address
                        if ($nodeIP) {
                            Write-Host "Testing connectivity to $nodeIP:6443..." -ForegroundColor Gray
                            $connectivity = Test-NetConnection -ComputerName $nodeIP -Port 6443 -WarningAction SilentlyContinue
                            if ($connectivity.TcpTestSucceeded) {
                                Write-Host "Can reach API server, waiting longer for cluster to be ready..." -ForegroundColor Yellow
                                Start-Sleep -Seconds 60
                                $nodes = kubectl get nodes --kubeconfig $kubeConfigPath 2>&1
                            }
                        }
                    
                        if ($LASTEXITCODE -ne 0) {
                            Write-Error "Cannot proceed without cluster access"
                            exit 1
                        }
                    }
                }
            }
        }
    
        if ($LASTEXITCODE -ne 0) {
            Write-Error "Cannot proceed without cluster access"
            exit 1
        }
    }

    Write-Host "Cluster is accessible:" -ForegroundColor Green
    kubectl get nodes --kubeconfig $kubeConfigPath

    # Rest of the script continues with the Arc connection steps...
    # (Steps 6-9 remain the same as in the original script)

    # Step 6: Clean up existing Arc artifacts
    Write-Host "`nStep 6: Cleaning up existing Arc artifacts in cluster" -ForegroundColor Yellow

    # Check for existing Arc namespace
    $arcNamespace = kubectl get namespace azure-arc --kubeconfig $kubeConfigPath 2>&1
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Found existing Arc namespace. Cleaning up..." -ForegroundColor Yellow
    
        # Get Arc cluster name from configmap if exists
        $arcConfigName = kubectl get configmap -n azure-arc azure-clusterconfig -o jsonpath='{.data.AZURE_RESOURCE_NAME}' --kubeconfig $kubeConfigPath 2>$null
        if ($arcConfigName -and $arcConfigName -ne $ClusterName) {
            Write-Warning "Existing Arc config shows different cluster name: $arcConfigName"
        }
    
        # Delete Arc namespace
        kubectl delete namespace azure-arc --kubeconfig $kubeConfigPath --timeout=5m 2>&1 | Out-Null
        Write-Host "Waiting for Arc namespace cleanup..." -ForegroundColor Gray
        Start-Sleep -Seconds 30
    }

    # Step 7: Install/Update Azure CLI extension
    Write-Host "`nStep 7: Setting up Azure CLI extensions" -ForegroundColor Yellow
    $extensions = az extension list --query "[?name=='connectedk8s'].name" -o tsv
    if (-not $extensions) {
        Write-Host "Installing connectedk8s extension..." -ForegroundColor Yellow
        az extension add --name connectedk8s
    }
    else {
        Write-Host "Updating connectedk8s extension..." -ForegroundColor Yellow
        az extension update --name connectedk8s
    }

    # Step 8: Connect to Arc
    Write-Host "`nStep 8: Connecting cluster '$ClusterName' to Azure Arc" -ForegroundColor Yellow
    Write-Host "Configuration:" -ForegroundColor Gray
    Write-Host "  Cluster Name: $ClusterName" -ForegroundColor Gray
    Write-Host "  Resource Group: $ResourceGroupName" -ForegroundColor Gray
    Write-Host "  Location: $Location" -ForegroundColor Gray
    Write-Host "  Subscription: $SubscriptionId" -ForegroundColor Gray

    Write-Host "`nExecuting Arc connection (this may take 5-10 minutes)..." -ForegroundColor Yellow

    try {
        # Build connection command
        $connectParams = @(
            "connectedk8s", "connect",
            "--name", $ClusterName,
            "--resource-group", $ResourceGroupName,
            "--location", $Location,
            "--kube-config", $kubeConfigPath
        )
    
        # Add context if available and not AKS
        if ($currentContext -and $currentContext -notlike "*azmk8s.io*") {
            $connectParams += "--kube-context", $currentContext
        }
    
        # Add tags
        $connectParams += @(
            "--tags", 
            "Environment=Development", 
            "CreatedBy=AKSEdge", 
            "Date=$(Get-Date -Format 'yyyy-MM-dd')",
            "FixedConnection=true"
        )
    
        # Execute connection
        $connectResult = & az @connectParams 2>&1
        $exitCode = $LASTEXITCODE
    
        if ($exitCode -eq 0) {
            Write-Host "`nArc connection successful!" -ForegroundColor Green
        
            # Get cluster info
            $cluster = az connectedk8s show --name $ClusterName --resource-group $ResourceGroupName | ConvertFrom-Json
        
            Write-Host "`nConnected cluster details:" -ForegroundColor Cyan
            Write-Host "  Name: $($cluster.name)" -ForegroundColor Gray
            Write-Host "  Status: $($cluster.connectivityStatus)" -ForegroundColor Green
            Write-Host "  Location: $($cluster.location)" -ForegroundColor Gray
            Write-Host "  Resource ID: $($cluster.id)" -ForegroundColor Gray
            Write-Host "  Kubernetes Version: $($cluster.kubernetesVersion)" -ForegroundColor Gray
            Write-Host "  Agent Version: $($cluster.agentVersion)" -ForegroundColor Gray
        
            Write-Host "`nAzure Portal URL:" -ForegroundColor Cyan
            Write-Host "https://portal.azure.com/#resource$($cluster.id)/overview" -ForegroundColor Blue
        
        }
        else {
            Write-Error "Arc connection failed with exit code: $exitCode"
            Write-Host "`nError details:" -ForegroundColor Red
            Write-Host $connectResult -ForegroundColor Red
        }
    
    }
    catch {
        Write-Error "Exception during Arc connection: $_"
    }

    # Step 9: Verify Arc installation
    Write-Host "`nStep 9: Verifying Arc installation in cluster" -ForegroundColor Yellow
    Start-Sleep -Seconds 10

    # Check Arc namespace
    $arcNs = kubectl get namespace azure-arc --kubeconfig $kubeConfigPath 2>&1
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Azure Arc namespace created successfully" -ForegroundColor Green
    
        # Check Arc pods
        Write-Host "`nArc pods status:" -ForegroundColor Yellow
        kubectl get pods -n azure-arc --kubeconfig $kubeConfigPath
    
        # Wait for pods to be ready
        Write-Host "`nWaiting for Arc pods to be ready (timeout: 5 minutes)..." -ForegroundColor Yellow
        kubectl wait --for=condition=Ready pods --all -n azure-arc --timeout=300s --kubeconfig $kubeConfigPath 2>&1 | Out-Null
    
        # Final pod status
        Write-Host "`nFinal Arc pod status:" -ForegroundColor Yellow
        kubectl get pods -n azure-arc -o wide --kubeconfig $kubeConfigPath
    
        # Check Arc connectivity
        Write-Host "`nChecking Arc connectivity status in Azure:" -ForegroundColor Yellow
        $finalCluster = az connectedk8s show --name $ClusterName --resource-group $ResourceGroupName 2>$null | ConvertFrom-Json
        if ($finalCluster) {
            Write-Host "Final Status: $($finalCluster.connectivityStatus)" -ForegroundColor $(if ($finalCluster.connectivityStatus -eq "Connected") { "Green" } else { "Red" })
        }
    }
    else {
        Write-Warning "Arc namespace not created. Connection may have failed."
    }

    # Summary
    Write-Host "`n======================================" -ForegroundColor Cyan
    Write-Host "Arc Connection Summary" -ForegroundColor Cyan
    Write-Host "======================================" -ForegroundColor Cyan

    Write-Host "`nCluster: $ClusterName" -ForegroundColor Yellow
    if ($finalCluster -and $finalCluster.connectivityStatus -eq "Connected") {
        Write-Host "Status: ✅ Successfully connected to Azure Arc" -ForegroundColor Green
        Write-Host "`nNext steps:" -ForegroundColor Yellow
        Write-Host "  1. View cluster in Azure Portal" -ForegroundColor Gray
        Write-Host "  2. Install Arc extensions (GitOps, Monitor, GrayTown services etc.)" -ForegroundColor Gray
        Write-Host "  3. Apply policies and configurations" -ForegroundColor Gray
        Start-Sleep -Seconds 15
    }
    else {
        Write-Host "Status: ❌ Connection issues detected" -ForegroundColor Red
        Write-Host "`nNext steps:" -ForegroundColor Yellow
        Write-Host "  1. Run diagnosis: .\DiagnoseArcConnection.ps1 -ClusterName `"$ClusterName`"" -ForegroundColor Cyan
        Write-Host "  2. Check Arc logs: kubectl logs -n azure-arc -l app.kubernetes.io/component=connect-agent --tail=50" -ForegroundColor Cyan
        Write-Host "  3. Review network/firewall settings" -ForegroundColor Gray
        Start-Sleep -Seconds 15
    }
}
catch {
    Write-Host "Deployment failed: $_" -ForegroundColor Red
    Write-Host "`nCheck the log file: $transcriptFile" -ForegroundColor Yellow
    Start-Sleep -Seconds 15
}


finally {
    Stop-Transcript | Out-Null
}
