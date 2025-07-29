# Azure MCP Server Extension for Visual Studio Code

Easily bring the power of Model Context Protocol (MCP) to your Azure projects in VS Code.

## Overview

**Azure MCP Server** adds smart, context-aware AI tools right inside VS Code to help you work more efficiently with Azure resources.

## Getting Started

Follow these simple steps to start using Azure MCP in VS Code:

1. **Install the Extension**
   - Get it from the [VS Code Marketplace](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azure-mcp-server).


2. **Start the MCP Server**
   - Open Command Palette (`Ctrl+Shift+P` / `Cmd+Shift+P`)
   - Run `MCP: List Servers`

   ![List Servers](https://raw.githubusercontent.com/Azure/azure-mcp/main/eng/vscode/resources/Walkthrough/ListServers.png)

   - Select `Azure MCP Server ext`, then click **Start Server**

   ![Select Server](https://raw.githubusercontent.com/Azure/azure-mcp/main/eng/vscode/resources/Walkthrough/SelectServer.png)
   ![Start Server](https://raw.githubusercontent.com/Azure/azure-mcp/main/eng/vscode/resources/Walkthrough/StartServer.png)

3. **Check That It's Running**
   - Go to the **Output** tab in VS Code.
   - Look for log messages confirming the server started successfully.

   ![Output](https://raw.githubusercontent.com/Azure/azure-mcp/main/eng/vscode/resources/Walkthrough/Output.png)

4. **(Optional) Enable Specific Azure Services**
   - To enable specific Azure services (like Storage or Key Vault), add this to your `.vscode/settings.json`:

     ```json
     "azureMcp.enabledServices": ["storage", "keyvault"]
     ```

   - Then restart the MCP Server (repeat Step 2).

You’re all set! Azure MCP Server is now ready to help you work smarter with Azure resources in VS Code.

## Feedback & Support

Have questions, feedback, or feature requests?
Open an issue on our [GitHub repository](https://github.com/Azure/azure-mcp/issues) — we’d love to hear from you!

## Contributing

Want to contribute?
Check out our [contribution guide](https://github.com/Azure/azure-mcp/blob/main/eng/vscode/CONTRIBUTING.md) to get started.

## License

This project is licensed under the [MIT License](https://github.com/Azure/azure-mcp/blob/main/LICENSE).
