# 🌟 Azure MCP Server

[![npm version](https://img.shields.io/npm/v/@azure/mcp.svg)](https://www.npmjs.com/package/@azure/mcp)
[![VS Code Marketplace](https://img.shields.io/visual-studio-marketplace/v/ms-azuretools.vscode-azure-github-copilot.svg)](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azure-github-copilot)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![GitHub issues](https://img.shields.io/github/issues/Azure/azure-mcp.svg)](https://github.com/Azure/azure-mcp/issues)

The Azure MCP Server implements the [MCP specification](https://modelcontextprotocol.io) to create a seamless connection between AI agents and Azure services. Azure MCP Server can be used alone or with the [GitHub Copilot for Azure extension](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azure-github-copilot) in VS Code.

> **🚀 New in v0.5.0:** Namespace mode (default), improved tool organization, and Azure Workbooks support
> **📢 Public Preview:** This project is in Public Preview - implementation may significantly change prior to General Availability.

## 📋 Table of Contents

- [🚀 Quick Start](#-quick-start)
- [⚙️ VS Code Install Steps](#️-vs-code-install-steps-recommended)
- [▶️ Getting Started](#️-getting-started)
- [✨ What can you do with the Azure MCP Server?](#-what-can-you-do-with-the-azure-mcp-server)
- [🛠️ Currently Supported Tools](#️-currently-supported-tools)
- [📖 Complete Tools Documentation](#-complete-tools-documentation)
- [🔄️ Upgrading Existing Installs](#️-upgrading-existing-installs-to-the-latest-version)
- [⚙️ Advanced Install Scenarios](#️-advanced-install-scenarios-optional)
- [📝 Troubleshooting](#-troubleshooting)
- [❓ Frequently Asked Questions](#-frequently-asked-questions)
- [👥 Contributing](#-contributing)

## 🚀 Quick Start

**Get started in under 60 seconds:**

1. **Install the GitHub Copilot for Azure extension**: [![Install from VS Code Marketplace](https://img.shields.io/badge/VS_Code-Install_GitHub_Copilot_for_Azure-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azure-github-copilot)
1. **Open GitHub Copilot Chat** in VS Code and [switch to Agent mode](https://code.visualstudio.com/docs/copilot/chat/chat-agent-mode)
1. **Try it**: Ask "List my Azure Storage accounts" and watch the magic happen! ✨

### ⚙️ VS Code Install Steps (Recommended)

1. Install either the stable or Insiders release of VS Code:
   * [💫 Stable release](https://code.visualstudio.com/download)
   * [🔮 Insiders release](https://code.visualstudio.com/insiders)
1. Install the [GitHub Copilot](https://marketplace.visualstudio.com/items?itemName=GitHub.copilot) and [GitHub Copilot Chat](https://marketplace.visualstudio.com/items?itemName=GitHub.copilot-chat) extensions

1. Install the [Azure MCP Server](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azure-mcp-server) extension

1. Install the [GitHub Copilot for Azure](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azure-github-copilot) extension from the VS Code Marketplace

### 🚀 Next Steps

**Once you've installed the Azure MCP Server:**

1. **Open GitHub Copilot Chat** in VS Code and [switch to Agent mode](https://code.visualstudio.com/docs/copilot/chat/chat-agent-mode)
2. **Verify installation**: You should see "Azure MCP Server" (or specific service names) in the tools list
3. **Click refresh** on the tools list if you don't see Azure tools immediately
4. **Test your setup** with a simple prompt like:
   - `"List my Azure Storage accounts"`
   - `"Show my resource groups"`
   - `"List my Azure subscriptions"`
5. **Explore capabilities**: The agent will use Azure MCP tools to complete your queries!

**Need help?** Check out the [official documentation](https://learn.microsoft.com/azure/developer/azure-mcp-server/) and [troubleshooting guide](https://github.com/Azure/azure-mcp/blob/main/TROUBLESHOOTING.md#128-tool-limit-issue).

## ✨ What can you do with the Azure MCP Server?

The Azure MCP Server supercharges your agents with Azure context. Here are some examples organized by common use cases:

### � **Data & Analytics**

**Azure AI Search:**
- "What indexes do I have in my Azure AI Search service 'mysvc'?"
- "Let's search this index for 'my search query'"

**Azure Cosmos DB:**
- "Show me all my Cosmos DB databases"
- "List containers in my Cosmos DB database"

**Azure Data Explorer:**
- "Get Azure Data Explorer databases in cluster 'mycluster'"
- "Sample 10 rows from table 'StormEvents' in Azure Data Explorer database 'db1'"

**Azure Monitor:**
- "Query my Log Analytics workspace"

### 🏗️ **Infrastructure & Resources**

**Azure Resource Management:**
- "List my resource groups"
- "List my Azure CDN endpoints"
- "Help me build an Azure application using Node.js"

**Azure Kubernetes Service (AKS):**
- "List my AKS clusters in my subscription"
- "Show me all my Azure Kubernetes Service clusters"

**Azure Storage:**
- "List my Azure storage accounts"
- "Show me the tables in my Storage account"
- "Get details about my Storage container"
- "List paths in my Data Lake file system"

### 🗄️ **Databases**

**Azure SQL Database:**
- "Show me details about my Azure SQL database 'mydb'"
- "List Active Directory administrators for my SQL server 'myserver'"
- "List all firewall rules for my SQL server 'myserver'"
- "List all elastic pools in my SQL server 'myserver'"

### ⚙️ **Configuration & Management**

**Azure App Configuration:**
- "List my App Configuration stores"
- "Show my key-value pairs in App Config"

> **💡 Pro Tip:** Try combining multiple operations in a single prompt like *"List my storage accounts and show me the containers in the first one"* to see the power of chained Azure operations!

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

* List and create certificates
* List and create keys
* List and create secrets

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
* List the details and properties of all databases
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

Agents and models can discover and learn best practices and usage guidelines for the `azd` MCP tool. For more information, see [AZD Best Practices](https://github.com/Azure/azure-mcp/tree/main/areas/extension/src/AzureMcp.Extension/Resources/azd-best-practices.txt).

</details>

For detailed command documentation and examples, see [Azure MCP Commands](https://github.com/Azure/azure-mcp/blob/main/docs/azmcp-commands.md).

## 📖 Complete Tools Documentation

For comprehensive documentation of all 130+ available tools across 29+ Azure service namespaces, including detailed parameters, examples, and usage patterns, see our **[Complete Tools Documentation](./docs/tools.md)**.

The tools documentation provides:
- **Detailed tool reference** with parameters and examples
- **Service-specific workflows** and common scenarios
- **Authentication and security** considerations
- **Performance optimization** tips
- **Troubleshooting guidance** for each tool category

Whether you're exploring what's possible or need specific implementation details, the tools documentation is your comprehensive reference guide.

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

Microsoft publishes an official Azure MCP Server Docker container on the [Microsoft Artifact Registry](https://mcr.microsoft.com/artifact/mar/azure-sdk/azure-mcp).

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

Optionally, use `--env` or `--volume` to pass authentication values.

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

## ❓ Frequently Asked Questions

<details>
<summary><strong>Q: What's the difference between "All Tools" and individual service installs?</strong></summary>

- **All Tools**: Installs 130+ tools across all Azure services in a single server
- **Individual Services**: Install only specific services (e.g., just Storage, just Key Vault)
- **Recommendation**: Start with "All Tools" for exploration, then switch to specific services for production
</details>

<details>
<summary><strong>Q: Do I need Azure CLI installed?</strong></summary>

No! Azure MCP Server includes its own Azure CLI tools. However, having Azure CLI installed can provide additional authentication options.
</details>

<details>
<summary><strong>Q: What authentication methods are supported?</strong></summary>

Azure MCP supports all Azure Identity authentication methods:
- **Interactive browser login** (recommended for development)
- **Managed Identity** (recommended for production)
- **Service Principal** with client secret or certificate
- **Azure CLI** authentication
- **Environment variables** (AZURE_CLIENT_ID, AZURE_CLIENT_SECRET, etc.)
</details>

<details>
<summary><strong>Q: Can I use this in production?</strong></summary>

Azure MCP Server is currently in **Public Preview**. While functional, the API may change before General Availability. For production use:
- Pin to specific versions: `@azure/mcp@0.5.0` instead of `@latest`
- Use managed identity authentication
- Review security considerations for your environment
</details>

<details>
<summary><strong>Q: How do I troubleshoot connection issues?</strong></summary>

1. Check the [Troubleshooting Guide](https://github.com/Azure/azure-mcp/blob/main/TROUBLESHOOTING.md)
2. Verify your Azure authentication: `az account show`
3. Check VS Code developer console for MCP-related errors
4. Try the "refresh" button in the tools list
</details>

## 👥 Contributing

We welcome contributions to the Azure MCP Server! Whether you're fixing bugs, adding new features, or improving documentation, your contributions are welcome.

Please read our [Contributing Guide](https://github.com/Azure/azure-mcp/blob/main/CONTRIBUTING.md) for guidelines on:

* 🛠️ Setting up your development environment
* ✨ Adding new commands
* 📝 Code style and testing requirements
* 🔄 Making pull requests

## 🤝 Code of Conduct

This project has adopted the
[Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information, see the
[Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/)
or contact [open@microsoft.com](mailto:open@microsoft.com)
with any additional questions or comments.
