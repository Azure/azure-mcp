# 🌟 Azure MCP Server

The Azure MCP Server implements the [MCP specification](https://modelcontextprotocol.io) to create a seamless connection between AI agents and Azure services.  Azure MCP Server can be used alone or with the [GitHub Copilot for Azure extension](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azure-github-copilot) in VS Code.  This project is in Public Preview and implementation may significantly change prior to our General Availability.

Here's a short (16 seconds) video to help you get the Azure MCP Server installed in VS Code.
<video src="https://github.com/user-attachments/assets/535f393c-0ed2-479d-9b24-5ca933293c92" width="1080" height="1920" controls></video>

### ⚙️ VS Code Install Steps (Recommended)
1. Install either the stable or Insiders release of VS Code:
   * [💫 Stable release](https://code.visualstudio.com/download)
   * [🔮 Insiders release](https://code.visualstudio.com/insiders)
2. Install the [GitHub Copilot](https://marketplace.visualstudio.com/items?itemName=GitHub.copilot) and [GitHub Copilot Chat](https://marketplace.visualstudio.com/items?itemName=GitHub.copilot-chat) extensions
3. Install [Node.js](https://nodejs.org/en/download) 20 or later
   * Ensure `node` and `npm` are in your path
4. Open VS Code in an empty folder
5. Install any of the available Azure MCP Server(s) for either the stable or Insiders release of VS Code
6. Open GitHub Copilot in VS Code and [switch to Agent mode](https://code.visualstudio.com/docs/copilot/chat/chat-agent-mode)
7. Click `refresh` on the tools list.

#### 🤖 Available Azure MCP Servers

| Name         | Description                                                                                   | VS Code | VS Code Insiders |
|--------------|-----------------------------------------------------------------------------------------------|---------|------------------|
| All | All Azure tools in a single MCP server | [![Install with NPX in VS Code](https://img.shields.io/badge/VS_Code-Install_all-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Server&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%5D%7D) | [![Install with NPX in VS Code Insiders](https://img.shields.io/badge/VS_Code_Insiders-Install_all-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Server&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%5D%7D&quality=insiders) |
| App Configuration | App Configuration operations - Manage App Configuration stores. | [![Install](https://img.shields.io/badge/VS_Code-Install_appconfig-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20App%20Config&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22appconfig%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code_Insiders-Install_appconfig-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20App%20Config&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22appconfig%22%5D%7D&quality=insiders) |
| Best Practices | Returns secure, production-grade Azure SDK best practices. | [![Install](https://img.shields.io/badge/VS_Code-Install_bestpractices-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Best%20Practices&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22bestpractices%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code_Insiders-Install_bestpractices-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Best%20Practices&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22bestpractices%22%5D%7D&quality=insiders) |
| Cosmos DB    | Cosmos DB operations - Manage/query Cosmos DB resources. | [![Install](https://img.shields.io/badge/VS_Code-Install_cosmos-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Cosmos&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22cosmos%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code_Insiders-Install_cosmos-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Cosmos&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22cosmos%22%5D%7D&quality=insiders) |
| Key Vault    | Key Vault operations - Manage/access Azure Key Vault resources. | [![Install](https://img.shields.io/badge/VS_Code-Install_keyvault-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Key%20Vault&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22keyvault%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code_Insiders-Install_keyvault-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Key%20Vault&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22keyvault%22%5D%7D&quality=insiders) |
| Kusto        | Kusto operations - Manage/query Azure Data Explorer clusters. | [![Install](https://img.shields.io/badge/VS_Code-Install_kusto-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Kusto&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22kusto%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code_Insiders-Install_kusto-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Kusto&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22kusto%22%5D%7D&quality=insiders) |
| Monitor      | Azure Monitor operations - Query/analyze logs and metrics. | [![Install](https://img.shields.io/badge/VS_Code-Install_monitor-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Monitor&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22monitor%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code_Insiders-Install_monitor-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Monitor&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22monitor%22%5D%7D&quality=insiders) |
| PostgreSQL   | PostgreSQL operations - Manage Azure Database for PostgreSQL - Flexible server. | [![Install](https://img.shields.io/badge/VS_Code-Install_postgres-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20PostgreSQL&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22postgres%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code_Insiders-Install_postgres-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20PostgreSQL&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22postgres%22%5D%7D&quality=insiders) |
| Redis Cache  | Redis Cache operations - Manage/access Azure Redis Cache resources. | [![Install](https://img.shields.io/badge/VS_Code-Install_redis-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Redis%20Cache&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22redis%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code_Insiders-Install_redis-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Redis%20Cache&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22redis%22%5D%7D&quality=insiders) |
| Resource Group | Resource group operations - List/manage Azure resource groups. | [![Install](https://img.shields.io/badge/VS_Code-Install_group-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Resource%20Group&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22group%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code_Insiders-Install_group-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Resource%20Group&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22group%22%5D%7D&quality=insiders) |
| Search       | Search operations - Manage/list Azure AI Search services. | [![Install](https://img.shields.io/badge/VS_Code-Install_search-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Search&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22search%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code_Insiders-Install_search-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Search&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22search%22%5D%7D&quality=insiders) |
| Service Bus  | Service Bus operations - Manage Azure Service Bus resources. | [![Install](https://img.shields.io/badge/VS_Code-Install_servicebus-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Service%20Bus&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22servicebus%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code_Insiders-Install_servicebus-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Service%20Bus&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22servicebus%22%5D%7D&quality=insiders) |
| Storage      | Storage operations - Manage/access Azure Storage resources. | [![Install](https://img.shields.io/badge/VS_Code-Install_storage-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Storage&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22storage%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code_Insiders-Install_storage-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Storage&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22storage%22%5D%7D&quality=insiders) |
| Subscription | Azure subscription operations - List/manage Azure subscriptions. | [![Install](https://img.shields.io/badge/VS_Code-Install_subscription-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Subscription&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22subscription%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code_Insiders-Install_subscription-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Subscription&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22subscription%22%5D%7D&quality=insiders) |



###  ▶️ Getting Started
1. Open GitHub Copilot in VS Code and [switch to Agent mode](https://code.visualstudio.com/docs/copilot/chat/chat-agent-mode)
2. You should see the Azure MCP Server in the list of tools
3. Try a prompt that tells the agent to use the Azure MCP Server, such as "List my Azure Storage containers"
4. The agent should be able to use the Azure MCP Server tools to complete your query
5. For help with common issues see [Troubleshooting guide](https://github.com/Azure/azure-mcp/blob/main/TROUBLESHOOTING.md#128-tool-limit-issue)

## ✨ What can you do with the Azure MCP Server?

The Azure MCP Server supercharges your agents with Azure context. Here are some cool prompts you can try:

### 🔍 Explore Your Azure Resources

- "List my Azure storage accounts"
- "Show me all my Cosmos DB databases"
- "What indexes do I have in my Azure AI Search service 'mysvc'?"
- "List my resource groups"
- "Show me the tables in my Storage account"
- "List containers in my Cosmos DB database"
- "Get details about my Storage container"
- "Get Kusto databases in cluster 'mycluster'"
- "Sample 10 rows from table 'StormEvents' in Kusto database 'db1'"

### 📊 Query & Analyze
- "Query my Log Analytics workspace"
- "Let's search this index for 'my search query'"

### ⚙️ Manage Configuration

- "List my App Configuration stores"
- "Show my key-value pairs in App Config"

### 🔧 Advanced Azure Operations

- "List my Azure CDN endpoints"
- "Help me build an Azure application using Node.js"


## 🛠️ Currently Supported Tools
<details>
<summary>The Azure MCP Server provides tools for interacting with the following Azure services</summary>

### 🔎 Azure AI Search (search engine/vector database)
- List Azure AI Search services
- List indexes and look at their schema and configuration
- Query search indexes

### 📊 Azure Cosmos DB (NoSQL Databases)
- List Cosmos DB accounts
- List and query databases
- Manage containers and items
- Execute SQL queries against containers

### 🐘 Azure Database for PostgreSQL - Flexible Server
- List and query databases.
- List and get schema for tables.
- List, get configuration and get parameters for servers.

### 🧮 Azure Data Explorer
- List Kusto clusters
- List databases in a Kusto cluster
- List tables in a Kusto database
- Get schema for a Kusto table
- Sample rows from a Kusto table
- Query Kusto databases using KQL

### 💾 Azure Storage
- List Storage accounts
- Manage blob containers and blobs
- List and query Storage tables
- Get container properties and metadata

### 📈 Azure Monitor
#### Log Analytics
- List Log Analytics workspaces
- Query logs using KQL
- List available tables

#### Health Models
- Get health of an entity

### ⚙️ Azure App Configuration
- List App Configuration stores
- Manage key-value pairs
- Handle labeled configurations
- Lock/unlock configuration settings

### 🔑 Azure Key Vault
- List, create, and get keys

### 🎭 Azure Role-Based Access Control (RBAC)
- List role assignments

### 📦 Azure Resource Groups
- List resource groups

### 🚌 Azure Service Bus
- Examine properties and runtime information about queues, topics, and subscriptions

### ⚙️ Azure Native ISV Services
- List Monitored Resources in a Datadog Monitor

### 🔧 Azure CLI Extension
- Execute Azure CLI commands directly
- Support for all Azure CLI functionality
- JSON output formatting
- Cross-platform compatibility

### 🚀 Azure Developer CLI (azd) Extension
- Execute Azure Developer CLI commands directly
- Support for template discovery, template initialization, provisioning and deployment
- Cross-platform compatibility

Agents and models can discover and learn best practices and usage guidelines for the `azd` MCP tool. For more information, see [AZD Best Practices](https://github.com/Azure/azure-mcp/tree/main/src/Resources/azd-best-practices.txt).

### 🛡️ Azure Best Practices
- Get secure, production-grade Azure SDK best practices for effective code generation.
</details>

For detailed command documentation and examples, see [Azure MCP Commands](https://github.com/Azure/azure-mcp/blob/main/docs/azmcp-commands.md).

## 🔄️ Upgrading Existing Installs to the Latest Version

<details>
<summary>How to stay current with releases of Azure MCP Server</summary>

#### NPX

If you use the default package spec of `@azure/mcp@latest`, npx will look for a new version on each server start. If you use just `@azure/mcp`, npx will continue to use its cached version until its cache is cleared.

#### NPM

If you globally install the cli via `npm install -g @azure/mcp` it will use the installed version until you manually update it with `npm update -g @azure/mcp`.

#### Docker

There is no version update built into the docker image.  To update, just pull the latest from the repo and repeat the [docker installation instructions](#docker-install).

#### VS Code

Installation in VS Code should be in one of the previous forms and the update instructions are the same. If you installed the mcp server with the `npx` command and  `-y @azure/mcp@latest` args, npx will check for package updates each time VS Code starts the server. Using a docker container in VS Code has the same no-update limitation described above.
</details>

## ⚙️ Advanced Install Scenarios (Optional)
<details>
<summary>Docker containers, custom MCP clients, and manual install options</summary>

#### 🐋 Docker Install Steps (Optional)
For a step-by-step Docker installation, follow these instructions:

1. Clone repository
2. From repository root, build Docker image: `docker build -t azure/azuremcp .`
3. Create an `.env` file with environment variables that [match one of the `EnvironmentCredential`](https://learn.microsoft.com/dotnet/api/azure.identity.environmentcredential) sets.  For example, a `.env` file using a service principal could look like:
```json
AZURE_TENANT_ID={YOUR_AZURE_TENANT_ID}
AZURE_CLIENT_ID={YOUR_AZURE_CLIENT_ID}
AZURE_CLIENT_SECRET={YOUR_AZURE_CLIENT_SECRET}
```
4. Add `.vscode/mcp.json` or update existing MCP configuration. Replace `/full/path/to/.env` with a path to your `.env` file.
```json
{
  "servers": {
    "Azure MCP Server": {
      "command": "docker",
      "args": [
        "run",
        "-i",
        "--rm",
        "--env-file",
        "/full/path/to/.env"
        "azure/azuremcp",
      ]
    }
  }
}
```

Optionally, customers can use `--env` or `--volume` to pass authentication values.

#### 🤖 Custom MCP Client Install Steps (Optional)
You can easily configure your MCP client to use the Azure MCP Server. Have your client run the following command and access it via standard IO or SSE.

#### 🔧 Manual Install Steps (Optional)
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

You can optionally set the `--service <service>` flag to install tools for the specified Azure product or service.

1. Add `.vscode/mcp.json`:
```json
{
  "servers": {
    "Azure Best Practices": {
      "command": "npx",
      "args": [
        "-y",
        "@azure/mcp@latest",
        "server",
        "start",
        "--service",
        "bestpractices" // Any of the available MCP servers can be referenced here.
      ]
    }
  }
}
```

### Using standard IO

Configure the MCP client to execute: `npx -y @azure/mcp@latest server start`. For instructions on using , follow instructions in [Quick install with VS Code](#-quick-install-with-vs-code) or [Manual Install](#-manual-install).

### Using SSE

1. Open a terminal window and execute: `npx -y @azure/mcp@latest server start --transport sse`
2. The server starts up and is hosted at: http://localhost:5008.  To use another port, append `--port {YOUR-PORT-NUMBER}`.
3. Open your MCP client and add the SSE configuration value.  This may differ between MCP clients.  In VS Code, it will look like:
   ```json
   {
      "servers": {
        "Azure MCP Server": {
          "type": "sse",
          "url": "http://localhost:5008/sse"
        }
      }
    }
   ```


More end-to-end MCP client/agent guides are coming soon!
</details>


## 📝 Troubleshooting

See [Troubleshooting guide](https://github.com/Azure/azure-mcp/blob/main/TROUBLESHOOTING.md#128-tool-limit-issue) for help with common issues and logging.

### 🔑 Authentication

<details>
<summary>Authentication options including DefaultAzureCredential flow, RBAC permissions, troubleshooting, and production credentials</summary>

The Azure MCP Server seamlessly integrates with your host operating system's authentication mechanisms, making it super easy to get started! We use Azure Identity under the hood via [`DefaultAzureCredential`](https://learn.microsoft.com/dotnet/azure/sdk/authentication/credential-chains?tabs=dac), which tries these credentials in order:

1. **Environment Variables** (`EnvironmentCredential`) - Perfect for CI/CD pipelines
2. **Shared Token Cache** (`SharedTokenCacheCredential`) - Uses cached tokens from other tools
3. **Visual Studio** (`VisualStudioCredential`) - Uses your Visual Studio credentials
4. **Azure CLI** (`AzureCliCredential`) - Uses your existing Azure CLI login
5. **Azure PowerShell** (`AzurePowerShellCredential`) - Uses your Az PowerShell login
6. **Azure Developer CLI** (`AzureDeveloperCliCredential`) - Uses your azd login
7. **Interactive Browser** (`InteractiveBrowserCredential`) - Falls back to browser-based login if needed

If you're already logged in through any of these methods, the Azure MCP Server will automatically use those credentials. Ensure that you have the correct authorization permissions in Azure (e.g. read access to your Storage account) via RBAC (Role-Based Access Control). To learn more about Azure's RBAC authorization system, visit this [link](https://learn.microsoft.com/azure/role-based-access-control/overview).

If you're running into any issues with authentication, visit our [troubleshooting guide](https://github.com/Azure/azure-mcp/blob/main/TROUBLESHOOTING.md#authentication).

For enterprise authentication scenarios including network restrictions, security policies, and protected resources, see our [Authentication and Security guide](https://github.com/Azure/azure-mcp/blob/main/docs/Authentication.md).

### Production Credentials

By default, the Azure MCP Server excludes production credentials like Managed Identity and Workload Identity. To enable these credentials, set the environment variable:

```
AZURE_MCP_INCLUDE_PRODUCTION_CREDENTIALS=true
```

This is useful when running on Azure services where you want to use managed identities.
</details>

## 🛡️ Security Note

Your credentials are always handled securely through the official [Azure Identity SDK](https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/identity/Azure.Identity/README.md) - **we never store or manage tokens directly**.

MCP as a phenomenon is very novel and cutting-edge. As with all new technology standards, consider doing a security review to ensure any systems that integrate with MCP servers follow all regulations and standards your system is expected to adhere to. This includes not only the Azure MCP Server, but any MCP client/agent that you choose to implement down to the model provider.

## 👥 Contributing
We welcome contributions to the Azure MCP Server! Whether you're fixing bugs, adding new features, or improving documentation, your contributions are welcome.

Please read our [Contributing Guide](https://github.com/Azure/azure-mcp/blob/main/CONTRIBUTING.md) for guidelines on:

- 🛠️ Setting up your development environment
- ✨ Adding new commands
- 📝 Code style and testing requirements
- 🔄 Making pull requests

## 💬 Feedback

We're building this in the open.  Your feedback is much appreciated, and will help us shape the future of the Azure MCP server.

👉 [Open an issue in the public repository](https://github.com/Azure/azure-mcp/issues/new/choose).

## 🤝 Code of Conduct

This project has adopted the
[Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information, see the
[Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/)
or contact [open@microsoft.com](mailto:open@microsoft.com)
with any additional questions or comments.
