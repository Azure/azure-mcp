# Azure MCP Server Extension for Visual Studio Code

Easily bring the power of Model Context Protocol (MCP) to your Azure projects in VS Code.

## Table of Contents
- [Overview](#overview)
- [Getting Started](#getting-started)
- [What can you do with the Azure MCP Server?](#what-can-you-do-with-the-azure-mcp-server)
- [Complete List of Supported Azure Services](#complete-list-of-supported-azure-services)
- [Documentation](#documentation)
- [Feedback & Support](#feedback--support)
- [Contributing](#contributing)
- [License](#license)

## Overview

**Azure MCP Server** adds smart, context-aware AI tools right inside VS Code to help you work more efficiently with Azure resources. The Azure MCP Server supercharges your agents with Azure context across **28 different Azure services**.

## Getting Started

Follow these simple steps to start using Azure MCP in VS Code:

1. **Install the Extension**
   - Get it from the [VS Code Marketplace](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azure-mcp-server).


2. **Start the MCP Server**
   - Open Command Palette (`Ctrl+Shift+P` / `Cmd+Shift+P`)
   - Run `MCP: List Servers`

   ![List Servers](https://raw.githubusercontent.com/Azure/azure-mcp/main/eng/vscode/resources/Walkthrough/ListServers.png)

   - Select `Azure MCP`, then click **Start Server**

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

## What can you do with the Azure MCP Server?

Here are some cool prompts you can try across our supported Azure services:

### 🔎 Azure AI Search
* "What indexes do I have in my Azure AI Search service 'mysvc'?"
* "Let's search this index for 'my search query'"

### ⚙️ Azure App Configuration
* "List my App Configuration stores"
* "Show my key-value pairs in App Config"

### ☸️ Azure Kubernetes Service (AKS)
* "List my AKS clusters in my subscription"
* "Show me all my Azure Kubernetes Service clusters"

### 📊 Azure Cosmos DB
* "Show me all my Cosmos DB databases"
* "List containers in my Cosmos DB database"

### 🗄️ Azure SQL Database
* "Show me details about my Azure SQL database 'mydb'"
* "List all databases in my Azure SQL server 'myserver'"

### 💾 Azure Storage
* "List my Azure storage accounts"
* "Show me the tables in my Storage account"
* "Get details about my Storage container"

### 🔑 Azure Key Vault
* "List my Key Vault secrets"
* "Show me the certificates in my Key Vault"

### 📊 Azure Monitor
* "Query my Log Analytics workspace"
* "Show me metrics for my Azure resources"

**And 20+ more Azure services!** For the complete list of supported services and sample prompts, see our [full documentation](https://github.com/Azure/azure-mcp/blob/main/README.md#-what-can-you-do-with-the-azure-mcp-server).

## Complete List of Supported Azure Services

The Azure MCP Server provides tools for interacting with **28 Azure service areas**:

- 🔎 **Azure AI Search** - Search engine/vector database operations
- ⚙️ **Azure App Configuration** - Configuration management
- 🛡️ **Azure Best Practices** - Secure, production-grade guidance
- 🖥️ **Azure CLI Extension** - Direct Azure CLI command execution
- 📊 **Azure Cosmos DB** - NoSQL database operations
- 🧮 **Azure Data Explorer** - Analytics queries and KQL
- 🐘 **Azure Database for PostgreSQL** - PostgreSQL database management
- 🛠️ **Azure Developer CLI (azd)** - Template and deployment management
- 🧮 **Azure Foundry** - AI model management and deployment
- 🚀 **Azure Managed Grafana** - Monitoring dashboards
- 🔑 **Azure Key Vault** - Secrets, keys, and certificates
- ☸️ **Azure Kubernetes Service (AKS)** - Container orchestration
- 📦 **Azure Load Testing** - Performance testing
- 🏪 **Azure Marketplace** - Product discovery
- 📈 **Azure Monitor** - Logging, metrics, and health monitoring
- ⚙️ **Azure Native ISV Services** - Third-party integrations
- 🛡️ **Azure Quick Review CLI** - Compliance scanning
- 🔴 **Azure Redis Cache** - In-memory data store
- 🏗️ **Azure Resource Groups** - Resource organization
- 🎭 **Azure RBAC** - Access control management
- 🚌 **Azure Service Bus** - Message queuing
- 🗄️ **Azure SQL Database** - Relational database management
- 🗄️ **Azure SQL Elastic Pool** - Database resource sharing
- 🗄️ **Azure SQL Server** - Server administration
- 💾 **Azure Storage** - Blob, table, file, and data lake storage
- 📋 **Azure Subscription** - Subscription management
- 🏗️ **Azure Terraform Best Practices** - Infrastructure as code guidance
- 🖥️ **Azure Virtual Desktop** - Virtual desktop infrastructure
- 📊 **Azure Workbooks** - Custom visualizations
- 🏗️ **Bicep** - Azure resource templates

## Documentation

- See our [official documentation on learn.microsoft.com](https://learn.microsoft.com/azure/developer/azure-mcp-server/) to learn how to use the Azure MCP Server to interact with Azure resources through natural language commands from AI agents and other types of clients.
- For additional command documentation and examples, see our [GitHub repository section on Azure MCP Commands](https://github.com/Azure/azure-mcp/blob/main/docs/azmcp-commands.md).


## Feedback & Support

- Check the [Troubleshooting guide](https://github.com/Azure/azure-mcp/blob/main/TROUBLESHOOTING.md) to diagnose and resolve common issues with the Azure MCP Server.
- We're building this in the open. Your feedback is much appreciated, and will help us shape the future of the Azure MCP server.
    - 👉 Open an issue in the public [GitHub repository](https://github.com/Azure/azure-mcp/issues) — we’d love to hear from you!

## Contributing

Want to contribute?
Check out our [contribution guide](https://github.com/Azure/azure-mcp/blob/main/eng/vscode/CONTRIBUTING.md) to get started.

## License

This project is licensed under the [MIT License](https://github.com/Azure/azure-mcp/blob/main/LICENSE).
