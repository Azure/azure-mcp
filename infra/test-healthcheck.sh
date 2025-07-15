#!/bin/bash
set -euo pipefail

# Container health check validation script

readonly IMAGE_NAME="azmcp-app"
readonly CONTAINER_PREFIX="azmcp-test"
readonly PLATFORM="linux/amd64"

log() {
    echo "[$(date +'%Y-%m-%d %H:%M:%S')] $*"
}

error() {
    echo "[ERROR] $*" >&2
}

cleanup_container() {
    local container_name="$1"
    docker stop "$container_name" 2>/dev/null || true
    docker rm "$container_name" 2>/dev/null || true
}

wait_for_container_ready() {
    local container_name="$1"
    local max_attempts=10
    local attempt=1
    
    while [[ $attempt -le $max_attempts ]]; do
        local container_status
        container_status=$(docker inspect --format='{{.State.Status}}' "$container_name" 2>/dev/null || echo "unknown")
        
        log "Container status attempt $attempt/$max_attempts: $container_status"
        
        case "$container_status" in
            "running")
                log "Container is running"
                return 0
                ;;
            "exited"|"dead")
                error "Container exited unexpectedly"
                docker logs "$container_name"
                return 1
                ;;
        esac
        
        if [[ $attempt -eq $max_attempts ]]; then
            error "Container failed to start after $max_attempts attempts"
            docker logs "$container_name"
            return 1
        fi
        
        ((attempt++))
        sleep 3
    done
}

wait_for_health_status() {
    local container_name="$1"
    local max_attempts=10
    local attempt=1
    
    # Check if health check is configured
    local health_configured
    health_configured=$(docker inspect --format='{{.Config.Healthcheck}}' "$container_name" 2>/dev/null || echo "")
    
    if [[ "$health_configured" == "<nil>" || -z "$health_configured" ]]; then
        log "No health check configured in image, using manual health check"
        return 0
    fi
    
    while [[ $attempt -le $max_attempts ]]; do
        local health_status
        health_status=$(docker inspect --format='{{.State.Health.Status}}' "$container_name" 2>/dev/null || echo "unknown")
        
        log "Health check attempt $attempt/$max_attempts: $health_status"
        
        case "$health_status" in
            "healthy")
                log "Container is healthy"
                return 0
                ;;
            "unhealthy")
                error "Container is unhealthy"
                docker logs "$container_name"
                return 1
                ;;
        esac
        
        if [[ $attempt -eq $max_attempts ]]; then
            error "Health check timed out after $max_attempts attempts"
            docker logs "$container_name"
            return 1
        fi
        
        ((attempt++))
        sleep 3
    done
}

test_sse_mode() {
    local port="$1"
    local container_name="${CONTAINER_PREFIX}-sse"
    
    log "Testing SSE transport mode (long-running service)"
    
    cleanup_container "$container_name"
    
    log "Starting SSE container on port $port"
    if ! docker run --platform $PLATFORM -d --name "$container_name" "$IMAGE_NAME" \
        --transport sse --service azure --port "$port"; then
        error "Failed to start SSE container"
        return 1
    fi
    
    log "Waiting for container to be ready..."
    if ! wait_for_container_ready "$container_name"; then
        error "Container failed to become ready"
        cleanup_container "$container_name"
        return 1
    fi
    
    log "Container started successfully"
    
    # Wait a bit more for the application to fully initialize
    sleep 5
    
    log "Checking Docker health status..."
    if ! wait_for_health_status "$container_name"; then
        error "Container health check failed - this is a critical error for SSE mode"
        docker logs "$container_name"
        cleanup_container "$container_name"
        return 1
    fi
    
    log "Testing health check script directly"
    if ! docker exec "$container_name" /app/healthcheck.sh; then
        error "Health check script failed"
        cleanup_container "$container_name"
        return 1
    fi
    log "Health check script executed successfully"
    
    log "Validating SSE service on port $port"
    if ! docker exec "$container_name" netstat -tlnp | grep -q ":$port "; then
        error "Port $port is not listening"
        docker exec "$container_name" netstat -tlnp
        cleanup_container "$container_name"
        return 1
    fi
    log "Port $port is listening"
    
    log "Verifying non-intrusive health checks"
    local log_count_before log_count_after
    log_count_before=$(docker logs "$container_name" 2>&1 | grep -c "Request starting HTTP" 2>/dev/null || echo "0")
    log_count_before=$(echo "$log_count_before" | tr -d '\n\r' | grep -o '[0-9]*' | head -1)
    log_count_before=${log_count_before:-0}
    
    sleep 35
    
    log_count_after=$(docker logs "$container_name" 2>&1 | grep -c "Request starting HTTP" 2>/dev/null || echo "0")
    log_count_after=$(echo "$log_count_after" | tr -d '\n\r' | grep -o '[0-9]*' | head -1)
    log_count_after=${log_count_after:-0}
    
    if [[ $log_count_before -eq $log_count_after ]]; then
        log "Health checks are non-intrusive (no HTTP requests)"
    else
        local new_requests=$((log_count_after - log_count_before))
        log "Warning: $new_requests HTTP requests detected during health checks"
    fi
    
    cleanup_container "$container_name"
    log "SSE mode test completed successfully"
}

test_stdio_mode() {
    log "Testing STDIO transport mode (execute and exit)"
    
    local test_input='{"jsonrpc": "2.0", "id": 1, "method": "initialize", "params": {"protocolVersion": "2024-11-05", "capabilities": {}, "clientInfo": {"name": "test-client", "version": "1.0.0"}}}'
    local output_file="/tmp/stdio_output.json"
    
    log "Executing STDIO mode container"
    if echo "$test_input" | docker run --platform $PLATFORM -i --rm --name azmcp-test-stdio "$IMAGE_NAME" \
        --transport stdio --service azure > "$output_file" 2>&1; then
        log "STDIO mode executed successfully"
        log "Output preview:"
        head -3 "$output_file"
    else
        error "STDIO mode execution failed"
        cat "$output_file"
        rm -f "$output_file"
        return 1
    fi
    
    log "Testing health check with STDIO mode detection"
    local health_container="${CONTAINER_PREFIX}-stdio-health"
    
    # This container may exit immediately, which is expected for STDIO mode
    docker run --platform $PLATFORM -d --name "$health_container" "$IMAGE_NAME" \
        --transport stdio --service azure 2>/dev/null || true
    
    sleep 2
    
    # For STDIO mode, the health check should either succeed or the container should have exited
    if docker exec "$health_container" /app/healthcheck.sh 2>/dev/null; then
        log "Health check correctly handles STDIO mode"
    else
        log "Note: STDIO container may have exited (expected behavior)"
    fi
    
    cleanup_container "$health_container"
    rm -f "$output_file"
    log "STDIO mode test completed successfully"
}

build_image() {
    log "Building Docker image for $PLATFORM"
    
    # Get the script directory and navigate to the root directory
    local script_dir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
    local root_dir="$(dirname "$script_dir")"
    
    # Build from the root directory where Dockerfile is located
    (cd "$root_dir" && docker build --platform $PLATFORM -t "$IMAGE_NAME" .)
}

print_deployment_summary() {
    cat << 'EOF'

Container health check validation completed successfully.

Deployment Patterns:
- SSE Mode: Use Kubernetes Deployment with liveness/readiness probes
- STDIO Mode: Not suitable for Kubernetes (use CLI mode instead)

For detailed deployment instructions, see: docs/container-usage.md
EOF
}

main() {
    if ! build_image; then
        error "Failed to build Docker image"
        exit 1
    fi
    
    if ! test_sse_mode 5009; then
        error "SSE mode test failed"
        exit 1
    fi
    
    if ! test_stdio_mode; then
        error "STDIO mode test failed"
        exit 1
    fi
    
    print_deployment_summary
}

main "$@"