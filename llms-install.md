# Azure MCP Server Installation Guide

This guide is specifically designed to help AI agents install and configure the Azure MCP Server.

## Prerequisites

1. Node.js (Latest LTS version)

## Installation Steps

### 1. Configuration Setup

The Azure MCP Server requires configuration based on the client type. Below are the setup instructions for each supported client:

#### For VS Code

1. Create or modify the MCP configuration file, `mcp.json`, in your `.vscode` folder.

```json
{
  "servers": {
    "Azure MCP Server": {
      "command": "npx",
      "args": [
        "-y",
        "@azure/mcp@latest",
        "server",
        "start"
      ]
    }
  }
}
```

#### For Windsurf

1. Create or modify the configuration file at `~/.codeium/windsurf/mcp_config.json`:

```json
{
  "mcpServers": {
    "Azure MCP Server": {
      "command": "npx",
      "args": [
        "-y",
        "@azure/mcp@latest",
        "server",
        "start"
      ]
    }
  }
}
```
