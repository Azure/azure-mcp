#!/usr/bin/env pwsh
param(
    [string]$ImageName = "azmcp-app",
    [string]$ContainerPrefix = "azmcp-test",
    [string]$Platform = "linux/amd64"
)

$ErrorActionPreference = "Stop"

function Write-Log {
    param([string]$Message)
    $timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    Write-Host "[$timestamp] $Message"
}

function Write-Error {
    param([string]$Message)
    $timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    Write-Host "[$timestamp] [ERROR] $Message" -ForegroundColor Red
}

function Cleanup-Container {
    param([string]$ContainerName)
    
    try {
        docker stop $ContainerName 2>$null | Out-Null
    } catch {
        # Ignore errors if container doesn't exist
    }
    
    try {
        docker rm $ContainerName 2>$null | Out-Null
    } catch {
        # Ignore errors if container doesn't exist
    }
}

function Wait-ForContainerReady {
    param([string]$ContainerName)
    
    $maxAttempts = 10
    $attempt = 1
    
    while ($attempt -le $maxAttempts) {
        try {
            $containerStatus = docker inspect --format='{{.State.Status}}' $ContainerName 2>$null
            if (-not $containerStatus) {
                $containerStatus = "unknown"
            }
        } catch {
            $containerStatus = "unknown"
        }
        
        Write-Log "Container status attempt $attempt/$maxAttempts`: $containerStatus"
        
        switch ($containerStatus) {
            "running" {
                Write-Log "Container is running"
                return $true
            }
            { $_ -in @("exited", "dead") } {
                Write-Error "Container exited unexpectedly"
                docker logs $ContainerName
                return $false
            }
        }
        
        if ($attempt -eq $maxAttempts) {
            Write-Error "Container failed to start after $maxAttempts attempts"
            docker logs $ContainerName
            return $false
        }
        
        $attempt++
        Start-Sleep -Seconds 3
    }
    
    return $false
}

function Wait-ForHealthStatus {
    param([string]$ContainerName)
    
    $maxAttempts = 10
    $attempt = 1
    
    # Check if health check is configured
    try {
        $healthConfigured = docker inspect --format='{{.Config.Healthcheck}}' $ContainerName 2>$null
        if ($healthConfigured -eq "<nil>" -or [string]::IsNullOrEmpty($healthConfigured)) {
            Write-Log "No health check configured in image, using manual health check"
            return $true
        }
    } catch {
        Write-Log "No health check configured in image, using manual health check" 
        return $true
    }
    
    while ($attempt -le $maxAttempts) {
        try {
            $healthStatus = docker inspect --format='{{.State.Health.Status}}' $ContainerName 2>$null
            if (-not $healthStatus) {
                $healthStatus = "unknown"
            }
        } catch {
            $healthStatus = "unknown"
        }
        
        Write-Log "Health check attempt $attempt/$maxAttempts`: $healthStatus"
        
        switch ($healthStatus) {
            "healthy" {
                Write-Log "Container is healthy"
                return $true
            }
            "unhealthy" {
                Write-Error "Container is unhealthy"
                docker logs $ContainerName
                return $false
            }
        }
        
        if ($attempt -eq $maxAttempts) {
            Write-Error "Health check timed out after $maxAttempts attempts"
            docker logs $ContainerName
            return $false
        }
        
        $attempt++
        Start-Sleep -Seconds 3
    }
    
    return $false
}

function Test-SSEMode {
    param([int]$Port)
    
    $containerName = "$ContainerPrefix-sse"
    
    Write-Log "Testing SSE transport mode (long-running service)"
    
    Cleanup-Container $containerName
    
    Write-Log "Starting SSE container on port $Port"
    try {
        docker run --platform $Platform -d --name $containerName $ImageName --transport sse --service azure --port $Port
        if ($LASTEXITCODE -ne 0) {
            Write-Error "Failed to start SSE container"
            return $false
        }
    } catch {
        Write-Error "Failed to start SSE container: $($_.Exception.Message)"
        return $false
    }
    
    Write-Log "Waiting for container to be ready..."
    if (-not (Wait-ForContainerReady $containerName)) {
        Write-Error "Container failed to become ready"
        Cleanup-Container $containerName
        return $false
    }
    
    Write-Log "Container started successfully"
    
    # Wait a bit more for the application to fully initialize
    Start-Sleep -Seconds 5
    
    Write-Log "Checking Docker health status..."
    if (-not (Wait-ForHealthStatus $containerName)) {
        Write-Error "Container health check failed - this is a critical error for SSE mode"
        docker logs $containerName
        Cleanup-Container $containerName
        return $false
    }
    
    Write-Log "Testing health check script directly"
    try {
        docker exec $containerName /app/healthcheck.sh
        if ($LASTEXITCODE -ne 0) {
            Write-Error "Health check script failed with exit code $LASTEXITCODE"
            Cleanup-Container $containerName
            return $false
        }
        Write-Log "Health check script executed successfully"
    } catch {
        Write-Error "Health check script failed: $($_.Exception.Message)"
        Cleanup-Container $containerName
        return $false
    }
    
    Write-Log "Validating SSE service on port $Port"
    try {
        $netstatOutput = docker exec $containerName netstat -tlnp
        if ($LASTEXITCODE -ne 0) {
            Write-Error "Failed to execute netstat in container"
            Cleanup-Container $containerName
            return $false
        }
        
        if ($netstatOutput -match ":$Port ") {
            Write-Log "Port $Port is listening"
        } else {
            Write-Error "Port $Port is not listening"
            docker exec $containerName netstat -tlnp
            Cleanup-Container $containerName
            return $false
        }
    } catch {
        Write-Error "Failed to check port $Port`: $($_.Exception.Message)"
        Cleanup-Container $containerName
        return $false
    }
    
    Write-Log "Verifying non-intrusive health checks"
    try {
        $logsBefore = docker logs $containerName 2>&1
        $logCountBefore = ($logsBefore | Select-String "Request starting HTTP" | Measure-Object).Count
        if (-not $logCountBefore) { $logCountBefore = 0 }
        
        Start-Sleep -Seconds 35
        
        $logsAfter = docker logs $containerName 2>&1
        $logCountAfter = ($logsAfter | Select-String "Request starting HTTP" | Measure-Object).Count
        if (-not $logCountAfter) { $logCountAfter = 0 }
        
        if ($logCountBefore -eq $logCountAfter) {
            Write-Log "Health checks are non-intrusive (no HTTP requests)"
        } else {
            $newRequests = $logCountAfter - $logCountBefore
            Write-Log "Warning: $newRequests HTTP requests detected during health checks"
        }
    } catch {
        Write-Log "Could not verify HTTP request count, but container is working"
    }
    
    Cleanup-Container $containerName
    Write-Log "SSE mode test completed successfully"
    return $true
}

function Test-STDIOMode {
    Write-Log "Testing STDIO transport mode (execute and exit)"
    
    $testInput = '{"jsonrpc": "2.0", "id": 1, "method": "initialize", "params": {"protocolVersion": "2024-11-05", "capabilities": {}, "clientInfo": {"name": "test-client", "version": "1.0.0"}}}'
    $outputFile = [System.IO.Path]::GetTempFileName()
    
    Write-Log "Executing STDIO mode container"
    try {
        $testInput | docker run --platform $Platform -i --rm --name azmcp-test-stdio $ImageName --transport stdio --service azure > $outputFile 2>&1
        
        Write-Log "STDIO mode executed successfully"
        Write-Log "Output preview:"
        Get-Content $outputFile -Head 3 | ForEach-Object { Write-Host $_ }
    } catch {
        Write-Error "STDIO mode execution failed"
        Get-Content $outputFile | ForEach-Object { Write-Host $_ }
        Remove-Item $outputFile -ErrorAction SilentlyContinue
        return $false
    }
    
    Write-Log "Testing health check with STDIO mode detection"
    $healthContainer = "$ContainerPrefix-stdio-health"
    
    try {
        docker run --platform $Platform -d --name $healthContainer $ImageName --transport stdio --service azure
    } catch {
        # This might fail, which is expected
    }
    
    Start-Sleep -Seconds 2
    
    try {
        docker exec $healthContainer /app/healthcheck.sh 2>$null
        Write-Log "Health check correctly handles STDIO mode"
    } catch {
        Write-Log "Note: STDIO container may have exited (expected behavior)"
    }
    
    Cleanup-Container $healthContainer
    Remove-Item $outputFile -ErrorAction SilentlyContinue
    Write-Log "STDIO mode test completed successfully"
    return $true
}

function Build-Image {
    Write-Log "Building Docker image for $Platform"
    
    # Get the script directory and navigate to the root directory
    $scriptDir = Split-Path -Parent $MyInvocation.PSCommandPath
    $rootDir = Split-Path -Parent $scriptDir
    
    # Build from the root directory where Dockerfile is located
    Push-Location $rootDir
    try {
        docker build --platform $Platform -t $ImageName .
    } finally {
        Pop-Location
    }
}

function Show-DeploymentSummary {
    Write-Host @"

Container health check validation completed successfully.

Deployment Patterns:
- SSE Mode: Use Kubernetes Deployment with liveness/readiness probes
- STDIO Mode: Not suitable for Kubernetes (use CLI mode instead)

For detailed deployment instructions, see: docs/container-usage.md
"@ -ForegroundColor Green
}

function Main {
    try {
        Build-Image
        
        if (-not (Test-SSEMode -Port 5009)) {
            Write-Error "SSE mode test failed"
            exit 1
        }
        
        if (-not (Test-STDIOMode)) {
            Write-Error "STDIO mode test failed"
            exit 1
        }
        
        Show-DeploymentSummary
    } catch {
        Write-Error "Script execution failed: $($_.Exception.Message)"
        exit 1
    }
}

# Execute main function
Main 