#!/bin/bash
set -euo pipefail

# Health check for Azure MCP Server
# Validates service availability based on transport mode
get_process_command() {
    ps aux | grep -E "azmcp.dll.*server.*start" | grep -v grep | head -n1
}

extract_port() {
    local cmd="$1"
    echo "$cmd" | grep -oE "port [0-9]+|--port [0-9]+" | grep -oE "[0-9]+" | head -n1
}

is_sse_transport() {
    local cmd="$1"
    echo "$cmd" | grep -q "transport.*sse\|--transport sse"
}

check_sse_health() {
    local port="${1:-5008}"
    
    if netstat -tlnp 2>/dev/null | grep -q ":${port} "; then
        echo "SSE service healthy on port ${port}"
        return 0
    else
        echo "SSE service unavailable on port ${port}"
        return 1
    fi
}

check_stdio_health() {
    echo "STDIO mode healthy"
    return 0
}

main() {
    local process_cmd
    process_cmd=$(get_process_command)
    
    if [[ -z "$process_cmd" ]]; then
        echo "azmcp process not found"
        return 1
    fi
    
    if is_sse_transport "$process_cmd"; then
        local port
        port=$(extract_port "$process_cmd")
        check_sse_health "${port:-5008}"
    else
        check_stdio_health
    fi
}

main "$@" 