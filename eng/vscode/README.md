# Azure MCP for Visual Studio Code

A Visual Studio Code extension that brings Model Context Protocol (MCP) capabilities to Azure developers.

## Overview

**Azure MCP** enables context-aware AI and advanced tooling for Azure resources—right within VS Code.

## Getting Started

1. **Install the Extension**
   - Install from the [VS Code Marketplace](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azure-mcp-server) 

2. **Start the MCP Server**
   - Open Command Palette (`Ctrl+Shift+P` / `Cmd+Shift+P`)
   - Run `MCP: List Servers`
   - Select `Azure MCP Server ext`, then click **Start Server**

3. **Confirm the Server is Running**
   - Go to the `Output` tab and look for confirmation logs

4. **(Optional) Enable Azure Services**
   - Add the following to `.vscode/settings.json`:

     ```json
     "azureMcp.enabledServices": ["storage", "keyvault"]
     ```

   - Restart the MCP Server

You're all set to use Azure MCP features!

## Feedback & Support

- Report issues or request features on [GitHub](https://github.com/Azure/azure-mcp/issues)

## Contributing

See the [contribution guide](https://github.com/Azure/azure-mcp/blob/main/eng/vscode/CONTRIBUTING.md) to get involved.

## License

[MIT License](https://github.com/Azure/azure-mcp/blob/main/LICENSE)