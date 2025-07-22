# 🌟 Azure MCP Server

The Azure MCP Server implements the [MCP specification](https://modelcontextprotocol.io) to create a seamless connection between AI agents and Azure services.  Azure MCP Server can be used alone or with the [GitHub Copilot for Azure extension](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azure-github-copilot) in VS Code.  This project is in Public Preview and implementation may significantly change prior to our General Availability.

Here's a short (16 seconds) video to help you get the Azure MCP Server installed in VS Code.
<video src="https://github.com/user-attachments/assets/535f393c-0ed2-479d-9b24-5ca933293c92" width="1080" height="1920" controls></video>

### ⚙️ VS Code Install Steps (Recommended)

1. Install either the stable or Insiders release of VS Code:
   * [💫 Stable release](https://code.visualstudio.com/download)
   * [🔮 Insiders release](https://code.visualstudio.com/insiders)
1. Install the [GitHub Copilot](https://marketplace.visualstudio.com/items?itemName=GitHub.copilot) and [GitHub Copilot Chat](https://marketplace.visualstudio.com/items?itemName=GitHub.copilot-chat) extensions
1. Install [Node.js](https://nodejs.org/en/download) 20 or later
   * Ensure `npx` is included in your environment path.
1. Install any of the available Azure MCP Servers from the table below.
1. Open GitHub Copilot in VS Code and [switch to Agent mode](https://code.visualstudio.com/docs/copilot/chat/chat-agent-mode)
1. Click `refresh` on the tools list.

#### 🤖 Available Azure MCP Servers

| Name                      | Description                                                      | Read/Write Tools | Read Only Tools |
|---------------------------|------------------------------------------------------------------|------------------|-----------------|
| All                       | All Azure MCP tools in a single server.                          | [![Install](https://img.shields.io/badge/VS_Code-Install_all-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Server&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_all-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Server%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--read-only%22%5D%7D) |
| App Configuration         | Manage configuration stores and key-value pairs.                 | [![Install](https://img.shields.io/badge/VS_Code-Install_appconfig-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20App%20Config&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22appconfig%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_appconfig-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20App%20Config%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22appconfig%22%2C%22--read-only%22%5D%7D) |
| Best Practices            | Secure, production-grade Azure SDK and Azure Functions guidance. | [![Install](https://img.shields.io/badge/VS_Code-Install_bestpractices-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Best%20Practices&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22bestpractices%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_bestpractices-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Best%20Practices%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22bestpractices%22%2C%22--read-only%22%5D%7D) |
| Bicep                     | Get ARM template schemas and resource validation.                | [![Install](https://img.shields.io/badge/VS_Code-Install_bicep-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Bicep&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22bicepschema%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_bicep-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Bicep%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22bicepschema%22%2C%22--read-only%22%5D%7D) |
| CLI Extension             | Execute `az`, `azd`, and `azqr` CLI commands.                    | [![Install](https://img.shields.io/badge/VS_Code-Install_cliextension-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20CLI&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22extension%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_cliextension-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20CLI%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22extension%22%2C%22--read-only%22%5D%7D) |
| Cosmos DB                 | Manage NoSQL databases and containers.                           | [![Install](https://img.shields.io/badge/VS_Code-Install_cosmos-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Cosmos&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22cosmos%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_cosmos-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Cosmos%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22cosmos%22%2C%22--read-only%22%5D%7D) |
| Data Explorer             | Query and manage clusters and databases.                         | [![Install](https://img.shields.io/badge/VS_Code-Install_kusto-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Kusto&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22kusto%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_kusto-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Kusto%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22kusto%22%2C%22--read-only%22%5D%7D) |
| Foundry                   | Manage AI model deployments and foundations.                     | [![Install](https://img.shields.io/badge/VS_Code-Install_foundry-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Foundry&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22foundry%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_foundry-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Foundry%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22foundry%22%2C%22--read-only%22%5D%7D) |
| Grafana                   | Monitor dashboards and analytics visualization.                  | [![Install](https://img.shields.io/badge/VS_Code-Install_grafana-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Grafana&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22grafana%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_grafana-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Grafana%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22grafana%22%2C%22--read-only%22%5D%7D) |
| Key Vault                 | Manage secrets, keys, and certificates.                          | [![Install](https://img.shields.io/badge/VS_Code-Install_keyvault-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Key%20Vault&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22keyvault%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_keyvault-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Key%20Vault%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22keyvault%22%2C%22--read-only%22%5D%7D) |
| Kubernetes Service        | List and manage clusters.                                        | [![Install](https://img.shields.io/badge/VS_Code-Install_aks-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20AKS&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22aks%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_aks-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20AKS%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22aks%22%2C%22--read-only%22%5D%7D) |
| Load Testing              | Create, run, and manage tests.                                   | [![Install](https://img.shields.io/badge/VS_Code-Install_loadtesting-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Load%20Testing&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22loadtesting%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_loadtesting-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Load%20Testing%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22loadtesting%22%2C%22--read-only%22%5D%7D) |
| Marketplace               | Query Azure Marketplace products.                                | [![Install](https://img.shields.io/badge/VS_Code-Install_marketplace-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=Azure%20Marketplace&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22marketplace%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_marketplace-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Marketplace%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--service%22%2C%22marketplace%22%2C%22--read-only%22%5D%7D) |
| Monitor                   | Query/analyze logs and metrics.                                  | [![Install](https://img.shields.io/badge/VS_Code-Install_monitor-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Monitor&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22monitor%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_monitor-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Monitor%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22monitor%22%2C%22--read-only%22%5D%7D) |
| Native ISV Services       | List monitored resources in Datadog.                             | [![Install](https://img.shields.io/badge/VS_Code-Install_datadog-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Native%20ISV&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22datadog%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_datadog-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Native%20ISV%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22datadog%22%2C%22--read-only%22%5D%7D) |
| PostgreSQL                | Manage flexible PostgreSQL database servers.                     | [![Install](https://img.shields.io/badge/VS_Code-Install_postgres-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20PostgreSQL&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22postgres%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_postgres-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20PostgreSQL%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22postgres%22%2C%22--read-only%22%5D%7D) |
| Redis Cache               | Manage Redis caches and data operations.                         | [![Install](https://img.shields.io/badge/VS_Code-Install_redis-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Redis%20Cache&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22redis%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_redis-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Redis%20Cache%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22redis%22%2C%22--read-only%22%5D%7D) |
| Resource Group            | Manage resource groups and deployments.                          | [![Install](https://img.shields.io/badge/VS_Code-Install_resourcegroup-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Resource%20Group&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22group%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_resourcegroup-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Resource%20Group%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22group%22%2C%22--read-only%22%5D%7D) |
| Role Based Access Control | Manage role assignments and permissions.                         | [![Install](https://img.shields.io/badge/VS_Code-Install_rbac-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20RBAC&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22role%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_rbac-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20RBAC%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22role%22%2C%22--read-only%22%5D%7D) |
| Search                    | Query AI Search services and indexes.                            | [![Install](https://img.shields.io/badge/VS_Code-Install_search-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Search&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22search%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_search-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Search%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22search%22%2C%22--read-only%22%5D%7D) |
| Service Bus               | Manage queues, topics, and messaging.                            | [![Install](https://img.shields.io/badge/VS_Code-Install_servicebus-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Service%20Bus&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22servicebus%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_servicebus-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Service%20Bus%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22servicebus%22%2C%22--read-only%22%5D%7D) |
| SQL                       | Manage SQL databases and servers.                                | [![Install](https://img.shields.io/badge/VS_Code-Install_sql-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20SQL%20Database&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22sql%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_sql-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20SQL%20Database%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22sql%22%2C%22--read-only%22%5D%7D) |
| Storage                   | Manage storage accounts and blob data.                           | [![Install](https://img.shields.io/badge/VS_Code-Install_storage-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Storage&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22storage%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_storage-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Storage%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22storage%22%2C%22--read-only%22%5D%7D) |
| Subscription              | Manage Azure subscription details.                               | [![Install](https://img.shields.io/badge/VS_Code-Install_subscription-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Subscription&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22subscription%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_subscription-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Subscription%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22subscription%22%2C%22--read-only%22%5D%7D) |
| Terraform Best Practices  | Secure, production-grade Azure Terraform guidance.               | [![Install](https://img.shields.io/badge/VS_Code-Install_terraform-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Terraform%20Best%20Practices&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22azureterraformbestpractices%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_terraform-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Terraform%20Best%20Practices%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22azureterraformbestpractices%22%2C%22--read-only%22%5D%7D) |
| Workbooks                 | Manage Azure Workbooks for data visualization.                   | [![Install](https://img.shields.io/badge/VS_Code-Install_workbooks-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Workbooks&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22workbooks%22%5D%7D) | [![Install](https://img.shields.io/badge/VS_Code-Install_workbooks-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://vscode.dev/redirect/mcp/install?name=Azure%20Workbooks%20Read%20Only&config=%7B%22command%22%3A%22npx%22%2C%22args%22%3A%5B%22-y%22%2C%22@azure%2Fmcp%40latest%22%2C%22server%22%2C%22start%22%2C%22--namespace%22%2C%22workbooks%22%2C%22--read-only%22%5D%7D) |

### ▶️ Getting Started

1. Open GitHub Copilot in VS Code and [switch to Agent mode](https://code.visualstudio.com/docs/copilot/chat/chat-agent-mode)
1. You should see the Azure MCP Server in the list of tools
1. Try a prompt that tells the agent to use the Azure MCP Server, such as "List my Azure Storage containers"
1. The agent should be able to use the Azure MCP Server tools to complete your query
1. Check out the [documentation](https://learn.microsoft.com/azure/developer/azure-mcp-server/) and [troubleshooting guide](https://github.com/Azure/azure-mcp/blob/main/TROUBLESHOOTING.md#128-tool-limit-issue)

## ✨ What can you do with the Azure MCP Server?

The Azure MCP Server supercharges your agents with Azure context. Here are some cool prompts you can try:

### 🔎 Azure AI Search

* "What indexes do I have in my Azure AI Search service 'mysvc'?"
* "Let's search this index for 'my search query'"

### ⚙️ Azure App Configuration

* "List my App Configuration stores"

### ☸️ Azure Kubernetes Service (AKS)

* "List my AKS clusters in my subscription"
* "Show me all my Azure Kubernetes Service clusters"

### 📊 Azure Cosmos DB

* "Show me all my Cosmos DB databases"
* "List containers in my Cosmos DB database"

### 🧮 Azure Data Explorer

* "Get Azure Data Explorer databases in cluster 'mycluster'"
* "Sample 10 rows from table 'StormEvents' in Azure Data Explorer database 'db1'"

### 📊 Azure Monitor

* "Query my Log Analytics workspace"

### 🗄️ Azure SQL Database

* "Show me details about my Azure SQL database 'mydb'"
* "List Active Directory administrators for my SQL server 'myserver'"
* "List all firewall rules for my SQL server 'myserver'"
* "List all elastic pools in my SQL server 'myserver'"

### 🔧 Azure Resource Management

* "List my resource groups"
* "List my Azure CDN endpoints"
* "Help me build an Azure application using Node.js"

### 💾 Azure Storage

* "List my Azure storage accounts"
* "Show me the tables in my Storage account"
* "Get details about my Storage container"
* "List paths in my Data Lake file system"
* "Show my key-value pairs in App Config"

## 🛠️ Currently Supported Tools

<details>
<summary>The Azure MCP Server provides tools for interacting with the following Azure services</summary>

### 🔎 Azure AI Search (search engine/vector database)

* List Azure AI Search services
* List indexes and look at their schema and configuration
* Query search indexes

### ⚙️ Azure App Configuration

* List App Configuration stores
* Manage key-value pairs
* Handle labeled configurations
* Lock/unlock configuration settings

### 🛡️ Azure Best Practices

* Get secure, production-grade Azure SDK best practices for effective code generation.

### 🖥️ Azure CLI Extension

* Execute Azure CLI commands directly
* Support for all Azure CLI functionality
* JSON output formatting
* Cross-platform compatibility

### 📊 Azure Cosmos DB (NoSQL Databases)

* List Cosmos DB accounts
* List and query databases
* Manage containers and items
* Execute SQL queries against containers

### 🧮 Azure Data Explorer

* List Azure Data Explorer clusters
* List databases
* List tables
* Get schema for a table
* Sample rows from a table
* Query using KQL

### 🐘 Azure Database for PostgreSQL - Flexible Server

* List and query databases.
* List and get schema for tables.
* List, get configuration and get parameters for servers.

### 🛠️ Azure Developer CLI (azd) Extension

* Execute Azure Developer CLI commands directly
* Support for template discovery, template initialization, provisioning and deployment
* Cross-platform compatibility

### 🧮 Azure Foundry

* List Azure Foundry models
* Deploy foundry models
* List foundry model deployments

### 🚀 Azure Managed Grafana

* List Azure Managed Grafana

### 🔑 Azure Key Vault

* List, create, and get keys
* Get secrets

### ☸️ Azure Kubernetes Service (AKS)

* List Azure Kubernetes Service clusters

### 📦 Azure Load Testing

* List, create load test resources
* List, create load tests
* Get, list, (create) run and rerun, update load test runs

### 🏪 Azure Marketplace

* Get details about Marketplace products

### 📈 Azure Monitor

#### Log Analytics

* List Log Analytics workspaces
* Query logs using KQL
* List available tables

#### Health Models

* Get health of an entity

#### Metrics

* Query Azure Monitor metrics for resources with time series data
* List available metric definitions for resources

### ⚙️ Azure Native ISV Services

* List Monitored Resources in a Datadog Monitor

### 🛡️ Azure Quick Review CLI Extension

* Scan Azure resources for compliance related recommendations

### 🔴 Azure Redis Cache

* List Redis Cluster resources
* List databases in Redis Clusters
* List Redis Cache resources
* List access policies for Redis Caches

### 🏗️ Azure Resource Groups

* List resource groups

### 🎭 Azure Role-Based Access Control (RBAC)

* List role assignments

### 🚌 Azure Service Bus

* Examine properties and runtime information about queues, topics, and subscriptions

### 🗄️ Azure SQL Database

* Show database details and properties
* List SQL server firewall rules
* List elastic pools and their configurations

### 💾 Azure Storage

* List Storage accounts
* Manage blob containers and blobs
* List and query Storage tables
* List paths in Data Lake file systems
* Get container properties and metadata

### 📋 Azure Subscription

* List Azure subscriptions

### 🏗️ Azure Terraform Best Practices

* Get secure, production-grade Azure Terraform best practices for effective code generation and command execution

### 📊 Azure Workbooks

* List workbooks in resource groups
* Create new workbooks with custom visualizations
* Update existing workbook configurations
* Get workbook details and metadata
* Delete workbooks when no longer needed

### 🏗️ Bicep

* Get the Bicep schema for specific Azure resource types

Agents and models can discover and learn best practices and usage guidelines for the `azd` MCP tool. For more information, see [AZD Best Practices](https://github.com/Azure/azure-mcp/tree/main/src/Areas/Extension/Resources/azd-best-practices.txt).

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

### 🐋 Docker Install Steps (Optional)

For a step-by-step Docker installation, follow these instructions:

1. Create an `.env` file with environment variables that [match one of the `EnvironmentCredential`](https://learn.microsoft.com/dotnet/api/azure.identity.environmentcredential) sets.  For example, a `.env` file using a service principal could look like:

    ```bash
    AZURE_TENANT_ID={YOUR_AZURE_TENANT_ID}
    AZURE_CLIENT_ID={YOUR_AZURE_CLIENT_ID}
    AZURE_CLIENT_SECRET={YOUR_AZURE_CLIENT_SECRET}
    ```

2. Add `.vscode/mcp.json` or update existing MCP configuration. Replace `/full/path/to/.env` with a path to your `.env` file.

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
            "mcr.microsoft.com/azure-sdk/azure-mcp:latest",
          ]
        }
      }
    }
    ```

Optionally, customers can use `--env` or `--volume` to pass authentication values.

### 🤖 Custom MCP Client Install Steps (Optional)

You can easily configure your MCP client to use the Azure MCP Server. Have your client run the following command and access it via standard IO.

```bash
npx -y @azure/mcp@latest server start
```

### 🔧 Manual Install Steps (Optional)

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

    You can optionally set the `--namespace <namespace>` flag to install tools for the specified Azure product or service.

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

More end-to-end MCP client/agent guides are coming soon!
</details>

## Data Collection

The software may collect information about you and your use of the software and send it to Microsoft. Microsoft may use this information to provide services and improve our products and services. You may turn off the telemetry as described in the repository. There are also some features in the software that may enable you and Microsoft to collect data from users of your applications. If you use these features, you must comply with applicable law, including providing appropriate notices to users of your applications together with a copy of Microsoft's [privacy statement](https://www.microsoft.com/privacy/privacystatement). You can learn more about data collection and use in the help documentation and our privacy statement. Your use of the software operates as your consent to these practices.

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

* 🛠️ Setting up your development environment
* ✨ Adding new commands
* 📝 Code style and testing requirements
* 🔄 Making pull requests

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
