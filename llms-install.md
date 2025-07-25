# Azure MCP Server Installation Guide

This guide is specifically designed to help AI agents install and configure the Azure MCP Server.

## Installation Steps

### Configuration Setup

The Azure MCP Server requires configuration based on the client type. Below are the setup instructions for each supported client:

#### For VS Code

**Recommended: Use the Azure MCP Server VS Code Extension**

1. Open VS Code, go to the Extensions view (`Ctrl+Shift+X` on Windows/Linux, `Cmd+Shift+X` on macOS), and search for "Azure MCP Server".
2. Install the [Azure MCP Server extension](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azure-mcp-server) by Microsoft.
3. Open the Command Palette (`Ctrl+Shift+P` on Windows/Linux, `Cmd+Shift+P` on macOS).
4. Search for `MCP: List Servers`.
5. Select `azure-mcp-server-ext` from the list and choose `Start` to launch the MCP server.

**Alternative: Use the classic npx route via `.vscode/mcp.json`**

> **Requires Node.js (Latest LTS version)**

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

> **Requires Node.js (Latest LTS version)**

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
