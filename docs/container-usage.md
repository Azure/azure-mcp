# Container Usage Guide

This document provides comprehensive guidance for deploying Azure MCP Server using containers in various environments.

## Overview

Azure MCP Server supports two transport modes with different deployment patterns:

- **SSE Mode**: Long-running HTTP service with Server-Sent Events for MCP client communication
- **STDIO Mode**: Execute-and-exit process for MCP JSON-RPC protocol communication

## Docker Usage

### Building the Image

```bash
docker build -t azure-mcp .
```

### SSE Mode (Long-running Service)

Start the server with SSE transport:

```bash
docker run -d \
  --name azure-mcp-sse \
  -p 5008:5008 \
  azure-mcp \
  --transport sse --service azure --port 5008
```

> **Note:** The `--service` parameter applies only to SSE mode and determines which Azure service capabilities are exposed through the MCP interface.

Verify health status:

```bash
docker exec azure-mcp-sse /app/healthcheck.sh
```

### STDIO Mode (Execute and Exit)

STDIO mode is designed for MCP JSON-RPC protocol communication. It expects properly formatted MCP messages:

```bash
echo '{"jsonrpc": "2.0", "id": 1, "method": "initialize", "params": {"protocolVersion": "2024-11-05", "capabilities": {}, "clientInfo": {"name": "client", "version": "1.0.0"}}}' | \
docker run -i --rm azure-mcp --transport stdio --service azure
```

## Kubernetes Deployment

> [!IMPORTANT]
> **Service Account Security**: When deploying in Kubernetes, it is critical to use service accounts with minimal required privileges. The Azure MCP Server can access and expose Azure resources through the MCP protocol. Improper service account configuration can lead to:
> - Unintended access to sensitive Azure resources
> - Information leakage through MCP tool responses  
> - Privilege escalation if the service account has excessive permissions
> 
> Always follow the principle of least privilege and regularly audit service account permissions.

### SSE Mode - Deployment with Health Checks

Use `Deployment` for long-running SSE services:

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: azure-mcp-sse
  labels:
    app: azure-mcp
    transport: sse
spec:
  replicas: 2
  selector:
    matchLabels:
      app: azure-mcp
      transport: sse
  template:
    metadata:
      labels:
        app: azure-mcp
        transport: sse
    spec:
      serviceAccountName: azure-mcp-service-account  # Use dedicated service account
      containers:
      - name: azure-mcp
        image: azure-mcp:latest
        args:
        - "--transport"
        - "sse"
        - "--service"
        - "azure"
        ports:
        - name: http
          containerPort: 8080
          protocol: TCP
        livenessProbe:
          exec:
            command:
            - /app/healthcheck.sh
          initialDelaySeconds: 15
          periodSeconds: 30
          timeoutSeconds: 10
          failureThreshold: 3
        readinessProbe:
          exec:
            command:
            - /app/healthcheck.sh
          initialDelaySeconds: 5
          periodSeconds: 10
          timeoutSeconds: 5
          failureThreshold: 3
        resources:
          limits:
            cpu: 500m
            memory: 512Mi
          requests:
            cpu: 250m
            memory: 256Mi
        securityContext:
          runAsNonRoot: true
          runAsUser: 1000
          allowPrivilegeEscalation: false
          readOnlyRootFilesystem: true
          capabilities:
            drop:
            - ALL
---
apiVersion: v1
kind: Service
metadata:
  name: azure-mcp-sse-service
spec:
  selector:
    app: azure-mcp
    transport: sse
  ports:
  - name: http
    port: 80
    targetPort: 8080
    protocol: TCP
  type: ClusterIP
```

## Health Check Details

### Health Check Script

The container includes `/app/healthcheck.sh` which automatically detects the transport mode:

- **SSE Mode**: Verifies the configured port is listening
- **STDIO Mode**: Always returns healthy (health checks not applicable for execute-and-exit processes)

### Manual Health Check

```bash
# Test health check directly
docker exec <container-name> /app/healthcheck.sh

# Check container health status
docker inspect <container-name> --format='{{.State.Health.Status}}'
```

### Kubernetes Probe Configuration

#### Liveness Probe
Detects if the container needs to be restarted:

```yaml
livenessProbe:
  exec:
    command: ["/app/healthcheck.sh"]
  initialDelaySeconds: 15
  periodSeconds: 30
  timeoutSeconds: 10
  failureThreshold: 3
```

#### Readiness Probe
Determines if the container is ready to serve traffic:

```yaml
readinessProbe:
  exec:
    command: ["/app/healthcheck.sh"]
  initialDelaySeconds: 5
  periodSeconds: 10
  timeoutSeconds: 5
  failureThreshold: 3
```

## Best Practices

### SSE Mode
- Use `Deployment` for high availability
- Configure appropriate resource limits
- Set up `Service` for network access (services only apply to SSE mode)
- Enable both liveness and readiness probes
- Consider horizontal pod autoscaling

### STDIO Mode
- Use for MCP JSON-RPC protocol communication only
- Not suitable for Kubernetes (use CLI mode instead)
- No health checks required (execute-and-exit pattern)
- Primarily used for direct MCP client integration

### Security
- **Service Accounts**: Use dedicated service accounts with minimal Azure permissions
- **Non-Root**: Run containers as non-root user when possible
- **Image Tags**: Use specific image tags, not `latest`
- **Resource Limits**: Configure resource limits to prevent resource exhaustion
- **Secrets Management**: Use Kubernetes secrets for sensitive configuration
- **Network Policies**: Implement network policies to restrict unnecessary traffic
- **Regular Audits**: Regularly audit and review service account permissions

### Monitoring
- Monitor container health status
- Set up logging aggregation
- Configure alerts for failed health checks
- Track resource usage and performance metrics
- Monitor Azure resource access patterns for anomalies

## Troubleshooting

### Health Check Failures

```bash
# Check health check output
docker exec <container> /app/healthcheck.sh

# View container logs
docker logs <container>

# Check if process is running
docker exec <container> ps aux | grep azmcp
```

### SSE Mode Issues

```bash
# Verify port is listening
docker exec <container> netstat -tlnp | grep :8080 # Or user-defined port

# Test endpoint connectivity
curl -I http://localhost:8080/sse
```

### STDIO Mode Issues

```bash
# Test STDIO mode with proper MCP JSON-RPC message
echo '{"jsonrpc": "2.0", "id": 1, "method": "initialize", "params": {"protocolVersion": "2024-11-05", "capabilities": {}, "clientInfo": {"name": "client", "version": "1.0.0"}}}' | \
docker run -i --rm azure-mcp --transport stdio --service azure
``` 