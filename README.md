# 🌟 Azure MCP Server


[![Install with NPX in VS Code](https://img.shields.io/badge/VS_Code-Install_Azure_MCP_Server-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20MCP%20Server&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22%40azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%5D%7D) [![Install with NPX in VS Code Insiders](https://img.shields.io/badge/VS_Code_Insiders-Install_Azure_MCP_Server-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20MCP%20Server&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22%40azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%5D%7D&quality=insiders)

The Azure MCP Server implements the [MCP specification](https://modelcontextprotocol.io) to create a seamless connection between AI agents and key Azure services like Azure Storage, Cosmos DB, and more. 

> Please note that this project is in Public Preview and implementation may significantly change prior to our General Availability.

## 🎯 Overview

### ✨ What can you do with the Azure MCP Server?

The Azure MCP Server supercharges your agents with Azure context. Here are some cool prompts you can try:

### 🔍 Explore Your Azure Resources

- "List my Azure storage accounts"
- "Show me all my Cosmos DB databases"
- "List my resource groups"
- "Show me the tables in my Storage account"
- "List containers in my Cosmos DB database"
- "Get details about my Storage container"

### 📊 Query & Analyze
- "Query my Log Analytics workspace"

### ⚙️ Manage Configuration

- "List my App Configuration stores"
- "Show my key-value pairs in App Config"

### 🔧 Advanced Azure Operations

- "List my Azure CDN endpoints"
- "Help me build an Azure application using Node.js"

### 🔍 How It Works

The Azure MCP Server creates a seamless integration between AI agents and Azure services through:

- 🔄 Smart JSON communication that AI agents understand
- 🏗️ Natural language commands that get translated to Azure operations
- 💡 Intelligent parameter suggestions and auto-completion
- ⚡ Consistent error handling that makes sense

## 🛠️ Currently Supported Tools

The Azure MCP Server provides tools for interacting with the following Azure services:

### 📊 Azure Cosmos DB (NoSQL Databases)
- List Cosmos DB accounts
- List and query databases
- Manage containers and items
- Execute SQL queries against containers

### 💾 Azure Storage
- List Storage accounts
- Manage blob containers and blobs
- List and query Storage tables
- Get container properties and metadata

### 📈 Azure Monitor (Log Analytics)
- List Log Analytics workspaces
- Query logs using KQL
- List available tables
- Configure monitoring options

### ⚙️ Azure App Configuration
- List App Configuration stores
- Manage key-value pairs
- Handle labeled configurations
- Lock/unlock configuration settings

### 📦 Azure Resource Groups
- List resource groups
- Resource group management operations

### 🔧 Azure CLI Extension
- Execute Azure CLI commands directly
- Support for all Azure CLI functionality
- JSON output formatting
- Cross-platform compatibility

### 🚀 Azure Developer CLI (azd) Extension
- Execute Azure Developer CLI commands directly
- Support for template discovery, template initialization, provisioning and deployment
- Cross-platform compatibility

For detailed command documentation and examples, see [Azure MCP Commands](docs/azmcp-commands.md).

## 🔌 Getting Started

### VS Code + GitHub Copilot

The Azure MCP Server provides Azure data plane tools. It can be used alone or with the [GitHub Copilot for Azure extension](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azure-github-copilot) in VS Code. If you're interested in broad developer support across a variety of Azure development scenarios not included in the Azure MCP Server, such as searching documentation on Microsoft Learn, we recommend this extension as well.

### Prerequisites
1. Install either the stable or Insiders release of VS Code:
   * [💫 Stable release](https://code.visualstudio.com/download)
   * [🔮 Insiders release](https://code.visualstudio.com/insiders)
2. Install the [GitHub Copilot](https://marketplace.visualstudio.com/items?itemName=GitHub.copilot) and [GitHub Copilot Chat](https://marketplace.visualstudio.com/items?itemName=GitHub.copilot-chat) extensions
3. Open VS Code in an empty folder

### Installation

#### ✨ One-Click Install

Click one of these buttons to install the Azure MCP Server for VS Code or VS Code Insiders.

[![Install with NPX in VS Code](https://img.shields.io/badge/VS_Code-Install_Azure_MCP_Server-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20MCP%20Server&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22%40azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%5D%7D) [![Install with NPX in VS Code Insiders](https://img.shields.io/badge/VS_Code_Insiders-Install_Azure_MCP_Server-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20MCP%20Server&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22%40azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%5D%7D&quality=insiders)

Just one click, and you're ready to go! 🎉

#### 🔧 Manual Install

For a step-by-step installation, follow these instructions:

1. Add `.vscode/mcp.json`:
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

## 🧪 Test the Azure MCP Server

1. Open GitHub Copilot in VS Code and [switch to Agent mode](https://code.visualstudio.com/docs/copilot/chat/chat-agent-mode)
2. You should see the Azure MCP Server in the list of tools
3. Try a prompt that tells the agent to use the Azure MCP Server, such as "List my Azure Storage containers"
4. The agent should be able to use the Azure MCP Server tools to complete your query

## 🤖 Custom MCP Clients

You can easily configure your MCP client to use the Azure MCP Server. Have your client run the following command and access it via `http://localhost:5008` or via `STDIO`.

`npx -y @azure/mcp@latest server start`

More end-to-end MCP client/agent guides are coming soon!

## 📝 Troubleshooting

See [Troubleshooting guide](/TROUBLESHOOTING.md) for help with common issues and logging.

## 🔑 Authentication

The Azure MCP Server seamlessly integrates with your host operating system's authentication mechanisms, making it super easy to get started! We use Azure Identity under the hood via [`DefaultAzureCredential`](https://learn.microsoft.com/dotnet/azure/sdk/authentication/credential-chains?tabs=dac), which tries these credentials in order:

1. **Environment Variables** (`EnvironmentCredential`) - Perfect for CI/CD pipelines
2. **Shared Token Cache** (`SharedTokenCacheCredential`) - Uses cached tokens from other tools
3. **Visual Studio** (`VisualStudioCredential`) - Uses your Visual Studio credentials
4. **Azure CLI** (`AzureCliCredential`) - Uses your existing Azure CLI login
5. **Azure PowerShell** (`AzurePowerShellCredential`) - Uses your Az PowerShell login
6. **Azure Developer CLI** (`AzureDeveloperCliCredential`) - Uses your azd login
7. **Interactive Browser** (`InteractiveBrowserCredential`) - Falls back to browser-based login if needed

If you're already logged in through any of these methods, the Azure MCP Server will automatically use those credentials. 

If you're running into any issues with authentication, visit our [troubleshooting guide](/TROUBLESHOOTING.md).

### Production Credentials

By default, the Azure MCP Server excludes production credentials like Managed Identity and Workload Identity. To enable these credentials, set the environment variable:

```
AZURE_MCP_INCLUDE_PRODUCTION_CREDENTIALS=true
```

This is useful when running on Azure services where you want to use managed identities.

## 🛡️ Security Note

Your credentials are always handled securely through the official [Azure Identity SDK](https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/identity/Azure.Identity/README.md) - **we never store or manage tokens directly**.

MCP as a phenomenon is very novel and cutting-edge. As with all new technology standards, consider doing a security review to ensure any systems that integrate with MCP servers follow all regulations and standards your system is expected to adhere to. This includes not only the Azure MCP Server, but any MCP client/agent that you choose to implement down to the model provider.

## 👥 Contributing

We welcome contributions to the Azure MCP Server! Whether you're fixing bugs, adding new features, or improving documentation, your contributions are welcome.

Please read our [Contributing Guide](/CONTRIBUTING.md) for guidelines on:

- 🛠️ Setting up your development environment
- ✨ Adding new commands
- 📝 Code style and testing requirements
- 🔄 Making pull requests

## 🤝 Code of Conduct

This project has adopted the
[Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information, see the
[Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/)
or contact [opencode@microsoft.com](mailto:opencode@microsoft.com)
with any additional questions or comments.