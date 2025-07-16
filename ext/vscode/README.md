
# Azure MCP Visual Studio Code Extension

This extension provides Model Context Protocol (MCP) integration and tooling for Azure in Visual Studio Code.

## What is Azure MCP?
Azure MCP enables advanced AI and model context workflows for Azure resources and applications, directly within VS Code.

## Getting Started

Follow these steps to get up and running with the Azure MCP extension:

1. **Install the extension**
	- Install from the VS Code Marketplace, or
	- Clone/download this repository and install from source.

2. **(Optional) Configure selected services**
	 - Open your workspace settings (`.vscode/settings.json`)
	 - Add or edit the following entry to enable specific Azure MCP services:

		 ```json
		 "azureMcp.enabledServices": [
			 "storage",
			 "keyvault"
		 ]
		 ```
	 - The extension in next step will start the MCP server with only the selected services enabled.

3. **Start the MCP Server**
	- Open the Command Palette (`Ctrl+Shift+P` or `Cmd+Shift+P`)
	- Type `MCP: List Servers` and select it
	- Choose `Azure MCP Server ext` from the list
	- Click `Start Server`

4. **Verify the server is running**
	- Open the `Output` tab in VS Code
	- Look for messages indicating the server started successfully

You're now ready to use Azure MCP features in VS Code!

## Feedback & Support
- File issues and feature requests on [GitHub](https://github.com/Azure/azure-mcp/issues)

## Contributing
See [the contribution guidelines] for ideas and guidance on how to improve the extension. Thank you!

## License
[MIT](https://github.com/Azure/azure-mcp/blob/main/LICENSE)

