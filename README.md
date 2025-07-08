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

| Name         | Description                                                                                   | All Tools | Read Only |
|--------------|-----------------------------------------------------------------------------------------------|-----------|-----------|
| All | All Azure tools in a single MCP server | [![Install with NPX in VS Code](https://img.shields.io/badge/VS_Code-Install_all-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Server&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%5D%7D) | [![Install with NPX in VS Code](https://img.shields.io/badge/VS_Code-Install_all-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Server%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--read-only%22%5D%7D) |
| App Configuration | App Configuration operations - Manage App Configuration stores. | [![Install](https://img.shields.io/badge/VS_Code-Install_appconfig-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20App%20Config&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22appconfig%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_appconfig-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20App%20Config%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22appconfig%22%2C%22--read-only%22%5D%7D) |
| Best Practices | Returns secure, production-grade Azure SDK best practices. | [![Install](https://img.shields.io/badge/VS_Code-Install_bestpractices-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Best%20Practices&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22bestpractices%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_bestpractices-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Best%20Practices%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22bestpractices%22%2C%22--read-only%22%5D%7D) |
| Cosmos DB    | Cosmos DB operations - Manage/query Cosmos DB resources. | [![Install](https://img.shields.io/badge/VS_Code-Install_cosmos-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Cosmos&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22cosmos%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_cosmos-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Cosmos%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22cosmos%22%2C%22--read-only%22%5D%7D) |
| Foundry       | AI Foundry operations - List models, deploy models and list model deployments. | [![Install](https://img.shields.io/badge/VS_Code-Install_foundry-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%Foundry&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22foundry%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_foundry-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Foundry%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22foundry%22%2C%22--read-only%22%5D%7D) |
| Key Vault    | Key Vault operations - Manage/access Azure Key Vault resources. | [![Install](https://img.shields.io/badge/VS_Code-Install_keyvault-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Key%20Vault&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22keyvault%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_keyvault-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Key%20Vault%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22keyvault%22%2C%22--read-only%22%5D%7D) |
| Azure Data Explorer        | Azure Data Explorer operations - Manage/query Azure Data Explorer clusters. | [![Install](https://img.shields.io/badge/VS_Code-Install_kusto-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Kusto&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22kusto%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_kusto-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Kusto%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22kusto%22%2C%22--read-only%22%5D%7D) |
| Monitor      | Azure Monitor operations - Query/analyze logs and metrics. | [![Install](https://img.shields.io/badge/VS_Code-Install_monitor-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Monitor&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22monitor%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_monitor-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Monitor%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22monitor%22%2C%22--read-only%22%5D%7D) |
| PostgreSQL   | PostgreSQL operations - Manage Azure Database for PostgreSQL - Flexible server. | [![Install](https://img.shields.io/badge/VS_Code-Install_postgres-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20PostgreSQL&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22postgres%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_postgres-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20PostgreSQL%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22postgres%22%2C%22--read-only%22%5D%7D) |
| Role Based Access Control  | Azure RBAC Operations - List Azure RBAC role assignments. | [![Install](https://img.shields.io/badge/VS_Code-Install_rbac-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20RBAC&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22role%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_rbac-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20RBAC%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22role%22%2C%22--read-only%22%5D%7D) |
| Redis Cache  | Redis Cache operations - Manage/access Azure Redis Cache resources. | [![Install](https://img.shields.io/badge/VS_Code-Install_redis-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Redis%20Cache&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22redis%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_redis-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Redis%20Cache%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22redis%22%2C%22--read-only%22%5D%7D) |
| Resource Group | Resource group operations - List/manage Azure resource groups. | [![Install](https://img.shields.io/badge/VS_Code-Install_group-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Resource%20Group&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22group%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_group-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Resource%20Group%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22group%22%2C%22--read-only%22%5D%7D) |
| Search       | Search operations - List/query Azure AI Search services. | [![Install](https://img.shields.io/badge/VS_Code-Install_search-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Search&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22search%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_search-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Search%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22search%22%2C%22--read-only%22%5D%7D) |
| Service Bus  | Service Bus operations - Manage Azure Service Bus resources. | [![Install](https://img.shields.io/badge/VS_Code-Install_servicebus-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Service%20Bus&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22servicebus%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_servicebus-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Service%20Bus%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22servicebus%22%2C%22--read-only%22%5D%7D) |
| SQL Database | Azure SQL Database operations - Manage Azure SQL Database servers and databases. | [![Install](https://img.shields.io/badge/VS_Code-Install_sql-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20SQL%20Database&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22sql%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_sql-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20SQL%20Database%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22sql%22%2C%22--read-only%22%5D%7D) |
| Storage      | Storage operations - Manage/access Azure Storage resources. | [![Install](https://img.shields.io/badge/VS_Code-Install_storage-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Storage&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22storage%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_storage-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Storage%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22storage%22%2C%22--read-only%22%5D%7D) |
| Subscription | Azure subscription operations - List/manage Azure subscriptions. | [![Install](https://img.shields.io/badge/VS_Code-Install_subscription-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Subscription&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22subscription%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_subscription-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Subscription%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22subscription%22%2C%22--read-only%22%5D%7D) |

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
- "Get Azure Data Explorer databases in cluster 'mycluster'"
- "Sample 10 rows from table 'StormEvents' in Azure Data Explorer database 'db1'"
- "Show me details about my Azure SQL database 'mydb'"

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

### 🗄️ Azure SQL Database
- Show database details and properties

### 🧮 Azure Data Explorer
- List Azure Data Explorer clusters
- List databases
- List tables
- Get schema for a table
- Sample rows from a table
- Query using KQL

- ### 🧮 Azure Foundry
- List Azure Foundry models
- Deploy foundry models
- List foundry model deployments

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

#### Metrics
- Query Azure Monitor metrics for resources with time series data
- List available metric definitions for resources

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

Agents and models can discover and learn best practices and usage guidelines for the `azd` MCP tool. For more information, see [AZD Best Practices](https://github.com/Azure/azure-mcp/tree/main/src/Areas/Extension/Resources/azd-best-practices.txt).

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

You can optionally set the `--namespace <service>` flag to install tools for the specified Azure product or service.

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
        "--namespace",
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

## Data Collection

The software may collect information about you and your use of the software and send it to Microsoft. Microsoft may use this information to provide services and improve our products and services. You may turn off the telemetry as described in the repository. There are also some features in the software that may enable you and Microsoft to collect data from users of your applications. If you use these features, you must comply with applicable law, including providing appropriate notices to users of your applications together with a copy of Microsoft's privacy statement. Our privacy statement is located at https://go.microsoft.com/fwlink/?LinkId=521839. You can learn more about data collection and use in the help documentation and our privacy statement. Your use of the software operates as your consent to these practices.

### Telemetry Configuration

Telemetry collection is on by default.

To opt out, set the environment variable `AZURE_MCP_COLLECT_TELEMETRY` to `false` in your environment.

## 📝 Troubleshooting

See [Troubleshooting guide](https://github.com/Azure/azure-mcp/blob/main/TROUBLESHOOTING.md#128-tool-limit-issue) for help with common issues and logging.

### 🔑 Authentication

<details>
<summary>Authentication options including DefaultAzureCredential flow, RBAC permissions, troubleshooting, and production credentials</summary>

The Azure MCP Server uses the Azure Identity library for .NET to authenticate to Microsoft Entra ID. For detailed information, see [Authentication Fundamentals](https://github.com/Azure/azure-mcp/blob/main/docs/Authentication.md#authentication-fundamentals).

If you're running into any issues with authentication, visit our [troubleshooting guide](https://github.com/Azure/azure-mcp/blob/main/TROUBLESHOOTING.md#authentication).

For enterprise authentication scenarios, including network restrictions, security policies, and protected resources, see [Authentication Scenarios in Enterprise Environments](https://github.com/Azure/azure-mcp/blob/main/docs/Authentication.md#authentication-scenarios-in-enterprise-environments).
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
