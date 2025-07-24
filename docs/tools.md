# Azure MCP Tools Documentation

This document provides a comprehensive reference for all tools available in the Azure MCP Server. The Azure MCP Server provides tools organized by Azure service namespaces, enabling seamless integration between AI agents and Azure services.

## Overview

The Azure MCP Server exposes **130+ tools** across **29+ Azure service namespaces**, providing comprehensive coverage of Azure services from App Configuration to Workbooks. Tools can be accessed in different modes depending on your MCP client's capabilities and requirements.

### Server Modes

- **Namespace Mode (Default)**: Groups tools by service namespace (e.g., "storage", "keyvault", "cosmos")
- **All Tools Mode**: Exposes each operation as an individual tool
- **Single Tool Mode**: Single "azure" tool that routes to all operations
- **Namespace Filtering**: Expose only specific namespaces using `--namespace` flags

## Tool Categories

### Management & Operations Tools
- [Server Operations](#server-operations)
- [Subscription Management](#subscription-management)
- [Resource Group Operations](#resource-group-operations)
- [Azure CLI Operations](#azure-cli-operations)
- [Azure Developer CLI Operations](#azure-developer-cli-operations)
- [Tools Discovery](#tools-discovery)

### Data & Storage Tools
- [Azure Storage Operations](#azure-storage-operations)
- [Azure Cosmos DB Operations](#azure-cosmos-db-operations)
- [Azure SQL Database Operations](#azure-sql-database-operations)
- [Azure SQL Server Operations](#azure-sql-server-operations)
- [Azure SQL Elastic Pool Operations](#azure-sql-elastic-pool-operations)
- [Azure Database for PostgreSQL Operations](#azure-database-for-postgresql-operations)
- [Azure Cache for Redis Operations](#azure-cache-for-redis-operations)

### AI & Analytics Tools
- [Azure AI Foundry Operations](#azure-ai-foundry-operations)
- [Azure AI Search Operations](#azure-ai-search-operations)
- [Azure Data Explorer Operations](#azure-data-explorer-operations)

### Configuration & Security Tools
- [Azure App Configuration Operations](#azure-app-configuration-operations)
- [Azure Key Vault Operations](#azure-key-vault-operations)
- [Azure RBAC Operations](#azure-rbac-operations)

### Container & Orchestration Tools
- [Azure Kubernetes Service (AKS) Operations](#azure-kubernetes-service-aks-operations)

### Messaging & Integration Tools
- [Azure Service Bus Operations](#azure-service-bus-operations)

### Monitoring & Analytics Tools
- [Azure Monitor Operations](#azure-monitor-operations)
- [Azure Managed Grafana Operations](#azure-managed-grafana-operations)
- [Azure Workbooks Operations](#azure-workbooks-operations)

### Testing & Performance Tools
- [Azure Load Testing Operations](#azure-load-testing-operations)

### Marketplace & Third-Party Tools
- [Azure Marketplace Operations](#azure-marketplace-operations)
- [Azure Native ISV Operations](#azure-native-isv-operations)

### Best Practices & Guidance Tools
- [Azure MCP Best Practices](#azure-mcp-best-practices)
- [Azure Terraform Best Practices](#azure-terraform-best-practices)

---

## Tool Details

### Server Operations

Core MCP server management and configuration tools.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-server-start` | Start the Azure MCP Server with specified configuration options | N/A | `transport`, `namespace[]`, `mode`, `read-only` |

**Key Features:**
- Multiple server modes (namespace, all, single)
- Transport protocol selection (stdio, sse)
- Namespace filtering capabilities
- Read-only mode support

---

### Subscription Management

Tools for managing Azure subscriptions and tenant access.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-subscription-list` | List all Azure subscriptions accessible to your account | ✅ | `tenant`, `auth-method` |

**Key Features:**
- Multi-tenant support
- Various authentication methods
- Subscription filtering and enumeration

---

### Resource Group Operations

Tools for managing Azure resource groups.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-group-list` | List all resource groups in a subscription | ✅ | `subscription`, `auth-method` |

**Key Features:**
- Subscription-scoped resource group listing
- Resource group metadata and properties
- Integration with RBAC and access controls

---

### Azure CLI Operations

Direct Azure CLI command execution wrapper for comprehensive Azure operations.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-extension-az` | Execute Azure CLI commands with full Azure service coverage | ❌ | `command`, `auth-method` |

**Key Features:**
- Complete Azure CLI command support
- Automatic retry logic with exponential backoff
- User confirmation for destructive operations
- Pagination handling for large result sets

**Example Commands:**
- `azmcp extension az --command "group list"`
- `azmcp extension az --command "vm list --resource-group myRG"`
- `azmcp extension az --command "storage account show --name mystorageaccount"`

---

### Azure Developer CLI Operations

Azure Developer CLI wrapper for application lifecycle management.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-extension-azd` | Execute Azure Developer CLI commands for application deployment and management | ❌ | `command`, `auth-method` |

**Key Features:**
- Application scaffolding and initialization
- Infrastructure provisioning and deployment
- Environment management
- Confirmation prompts for destructive operations

**Common Workflows:**
- `azmcp extension azd --command "init"`
- `azmcp extension azd --command "up"`
- `azmcp extension azd --command "deploy"`
- `azmcp extension azd --command "down"`

---

### Tools Discovery

Tools for discovering and understanding available MCP tools.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-tools-list` | List all available tools in the Azure MCP server with descriptions and metadata | ✅ | None |

**Key Features:**
- Complete tool enumeration
- Tool descriptions and parameter information
- Namespace organization
- Read-only vs. destructive tool identification

---

### Azure Storage Operations

Comprehensive Azure Storage account, blob, table, and Data Lake operations.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-storage-account-list` | List all Storage accounts in a subscription | ✅ | `subscription`, `auth-method` |
| `azmcp-storage-blob-container-list` | List all containers in a Storage account | ✅ | `account-name`, `subscription` |
| `azmcp-storage-blob-container-details` | Get detailed properties of a storage container | ✅ | `account-name`, `container-name`, `subscription` |
| `azmcp-storage-blob-list` | List all blobs in a Storage container | ✅ | `account-name`, `container-name`, `subscription` |
| `azmcp-storage-table-list` | List all tables in a Storage account | ✅ | `account-name`, `subscription` |
| `azmcp-storage-datalake-filesystem-list-paths` | List Data Lake file system paths | ✅ | `account-name`, `file-system`, `subscription` |
| `azmcp-storage-datalake-directory-create` | Create Data Lake directory | ❌ | `account-name`, `file-system`, `directory-path`, `subscription` |

**Key Features:**
- Full Storage account management
- Blob storage operations (containers, blobs)
- Table storage access
- Data Lake Gen2 support (file systems, directories)
- Metadata and properties retrieval
- Container access level management

---

### Azure Cosmos DB Operations

NoSQL database management and querying tools.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-cosmos-account-list` | List all Cosmos DB accounts in a subscription | ✅ | `subscription`, `auth-method` |
| `azmcp-cosmos-database-list` | List all databases in a Cosmos DB account | ✅ | `account-name`, `subscription` |
| `azmcp-cosmos-database-container-list` | List all containers in a Cosmos DB database | ✅ | `account-name`, `database-name`, `subscription` |
| `azmcp-cosmos-database-container-item-query` | Execute SQL queries against Cosmos DB containers | ✅ | `account-name`, `database-name`, `container-name`, `subscription`, `query` |

**Key Features:**
- Multi-API Cosmos DB support
- SQL query execution with full syntax support
- Database and container enumeration
- Document querying and filtering
- Cross-partition query support

---

### Azure AI Search Operations

AI-powered search service management and querying.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-search-service-list` | List all Azure AI Search services in a subscription | ✅ | `subscription`, `auth-method` |
| `azmcp-search-index-list` | List all indexes in an Azure AI Search service | ✅ | `service-name` |
| `azmcp-search-index-describe` | Get complete index definition including fields, analyzers, and scoring profiles | ✅ | `service-name`, `index-name` |
| `azmcp-search-index-query` | Execute search queries against Azure AI Search indexes | ✅ | `service-name`, `index-name`, `query` |

**Key Features:**
- Full-text search capabilities
- Index schema inspection
- Advanced query syntax support
- Faceted search and filtering
- Custom scoring and relevance tuning

---

### Azure App Configuration Operations

Centralized application configuration management.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-appconfig-account-list` | List all App Configuration stores in a subscription | ✅ | `subscription`, `auth-method` |
| `azmcp-appconfig-kv-list` | List all key-value pairs in an App Configuration store | ✅ | `account-name`, `subscription`, `key`, `label` |
| `azmcp-appconfig-kv-show` | Show a specific key-value setting with metadata | ✅ | `account-name`, `key`, `subscription`, `label` |
| `azmcp-appconfig-kv-set` | Create or update a key-value setting | ❌ | `account-name`, `key`, `value`, `subscription`, `label` |
| `azmcp-appconfig-kv-delete` | Delete a key-value pair from the store | ❌ | `account-name`, `key`, `subscription`, `label` |
| `azmcp-appconfig-kv-lock` | Lock a key-value to read-only mode | ❌ | `account-name`, `key`, `subscription`, `label` |
| `azmcp-appconfig-kv-unlock` | Unlock a key-value for editing | ❌ | `account-name`, `key`, `subscription`, `label` |

**Key Features:**
- Centralized configuration management
- Label-based configuration organization
- Key-value locking for protection
- ETag-based conflict resolution
- Content type and metadata support

---

### Azure Monitor Operations

Comprehensive monitoring, logging, and analytics platform.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-monitor-workspace-list` | List Log Analytics workspaces in a subscription | ✅ | `subscription`, `auth-method` |
| `azmcp-monitor-table-list` | List all tables in a Log Analytics workspace | ✅ | `workspace`, `resource-group`, `subscription`, `table-type` |
| `azmcp-monitor-log-query` | Execute KQL queries against Log Analytics workspaces | ✅ | `workspace`, `resource-group`, `subscription`, `table-name`, `query`, `hours`, `limit` |

**Key Features:**
- KQL (Kusto Query Language) support
- Predefined query templates ('recent', 'errors')
- Cross-workspace querying
- Time-based filtering
- Advanced analytics and aggregations

**Predefined Queries:**
- `recent`: Most recent logs ordered by TimeGenerated
- `errors`: Error-level logs ordered by TimeGenerated

---

### Azure AI Foundry Operations

AI model deployment and management platform.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-foundry-project-list` | List AI Foundry projects in a subscription | ✅ | `subscription` |
| `azmcp-foundry-models-list` | List available AI models in the marketplace | ✅ | `subscription`, `publisher-name`, `license-name`, `model-name` |
| `azmcp-foundry-models-deploy` | Deploy an AI model to Azure AI services | ❌ | `subscription`, `resource-group`, `deployment-name`, `model-name`, `model-format`, `azure-ai-services-name` |
| `azmcp-foundry-models-deployments-list` | List model deployments at an endpoint | ✅ | `endpoint` |

**Key Features:**
- AI model marketplace access
- Model deployment management
- Endpoint configuration
- SKU and scaling options
- Integration with Azure AI services

---

### Azure Data Explorer Operations

Big data analytics and real-time analytics platform.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-kusto-cluster-list` | List Azure Data Explorer clusters | ✅ | `subscription` |
| `azmcp-kusto-cluster-get` | Get details for a Data Explorer cluster | ✅ | `subscription`, `cluster-name` |
| `azmcp-kusto-database-list` | List databases in a Data Explorer cluster | ✅ | `cluster-uri` or `subscription` + `cluster-name` |
| `azmcp-kusto-table-list` | List tables in a Data Explorer database | ✅ | `cluster-uri`, `database-name` |
| `azmcp-kusto-table-schema` | Get schema of a Data Explorer table | ✅ | `cluster-uri`, `database-name`, `table-name` |
| `azmcp-kusto-query` | Execute KQL queries against Data Explorer | ✅ | `cluster-uri`, `database-name`, `query` |

**Key Features:**
- Real-time analytics on large datasets
- KQL query language support
- Time series analysis
- Cross-database querying
- Schema exploration and discovery

---

### Azure Key Vault Operations

Secure key, secret, and certificate management.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-keyvault-vault-list` | List Key Vault instances in a subscription | ✅ | `subscription` |
| `azmcp-keyvault-secret-list` | List secrets in a Key Vault | ✅ | `vault-name`, `subscription` |
| `azmcp-keyvault-secret-show` | Get a secret value from Key Vault | ✅ | `vault-name`, `secret-name`, `subscription` |
| `azmcp-keyvault-secret-set` | Create or update a secret in Key Vault | ❌ | `vault-name`, `secret-name`, `value`, `subscription` |
| `azmcp-keyvault-secret-delete` | Delete a secret from Key Vault | ❌ | `vault-name`, `secret-name`, `subscription` |
| `azmcp-keyvault-key-list` | List keys in a Key Vault | ✅ | `vault-name`, `subscription` |
| `azmcp-keyvault-certificate-list` | List certificates in a Key Vault | ✅ | `vault-name`, `subscription` |

**Key Features:**
- Hardware Security Module (HSM) support
- Secret versioning and lifecycle management
- Access policies and RBAC integration
- Certificate management and renewal
- Integration with Azure services

---

### Azure SQL Database Operations

Managed SQL database service operations.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-sql-database-list` | List SQL databases on a server | ✅ | `subscription`, `server-name` |
| `azmcp-sql-database-show` | Get details of a specific SQL database | ✅ | `subscription`, `server-name`, `database-name` |
| `azmcp-sql-database-query` | Execute SQL queries against a database | ✅ | `subscription`, `server-name`, `database-name`, `query` |
| `azmcp-sql-database-backup-list` | List database backups | ✅ | `subscription`, `server-name`, `database-name` |

**Key Features:**
- T-SQL query execution
- Database backup management
- Performance monitoring
- Scaling and tier management
- Security and compliance features

---

### Azure SQL Server Operations

SQL Server instance management and configuration.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-sql-server-list` | List SQL servers in a subscription | ✅ | `subscription` |
| `azmcp-sql-server-show` | Get details of a specific SQL server | ✅ | `subscription`, `server-name` |
| `azmcp-sql-firewall-rule-list` | List firewall rules for a SQL server | ✅ | `subscription`, `server-name` |
| `azmcp-sql-entra-admin-list` | List Entra ID administrators for a SQL server | ✅ | `subscription`, `server-name` |

**Key Features:**
- Server-level security management
- Firewall rule configuration
- Entra ID integration
- Performance and monitoring settings

---

### Azure SQL Elastic Pool Operations

Elastic database pool management for cost optimization.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-sql-elastic-pool-list` | List elastic pools on a SQL server | ✅ | `subscription`, `server-name` |
| `azmcp-sql-elastic-pool-show` | Get details of a specific elastic pool | ✅ | `subscription`, `server-name`, `pool-name` |
| `azmcp-sql-elastic-pool-database-list` | List databases in an elastic pool | ✅ | `subscription`, `server-name`, `pool-name` |

**Key Features:**
- Resource sharing across databases
- Cost optimization for multiple databases
- Performance monitoring and tuning
- Dynamic scaling capabilities

---

### Azure Database for PostgreSQL Operations

Managed PostgreSQL database service operations.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-postgres-server-list` | List PostgreSQL servers in a subscription | ✅ | `subscription` |
| `azmcp-postgres-database-list` | List databases on a PostgreSQL server | ✅ | `subscription`, `server-name` |
| `azmcp-postgres-query` | Execute SQL queries against a PostgreSQL database | ✅ | `subscription`, `server-name`, `database-name`, `query` |

**Key Features:**
- PostgreSQL compatibility
- Flexible server options
- High availability and backup
- Extension support

---

### Azure Cache for Redis Operations

In-memory data store and cache management.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-redis-cache-list` | List Redis caches in a subscription | ✅ | `subscription` |
| `azmcp-redis-cluster-list` | List Redis clusters in a subscription | ✅ | `subscription` |
| `azmcp-redis-database-list` | List databases in a Redis cluster | ✅ | `subscription`, `cluster-name` |
| `azmcp-redis-access-policy-list` | List access policy assignments for Redis cache | ✅ | `subscription`, `cache-name` |

**Key Features:**
- High-performance caching
- Redis clustering support
- Access control and security
- Data persistence options
- Performance monitoring

---

### Azure Kubernetes Service (AKS) Operations

Managed Kubernetes container orchestration service.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-aks-cluster-list` | List AKS clusters in a subscription | ✅ | `subscription` |
| `azmcp-aks-cluster-show` | Get details of a specific AKS cluster | ✅ | `subscription`, `cluster-name`, `resource-group` |
| `azmcp-aks-nodepool-list` | List node pools in an AKS cluster | ✅ | `subscription`, `cluster-name`, `resource-group` |
| `azmcp-aks-addon-list` | List enabled addons for an AKS cluster | ✅ | `subscription`, `cluster-name`, `resource-group` |

**Key Features:**
- Kubernetes cluster management
- Node pool scaling and configuration
- Azure CNI and kubenet networking
- Integration with Azure services
- Security and compliance features

---

### Azure Service Bus Operations

Enterprise messaging and event streaming platform.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-servicebus-namespace-list` | List Service Bus namespaces | ✅ | `subscription` |
| `azmcp-servicebus-queue-list` | List queues in a Service Bus namespace | ✅ | `subscription`, `namespace-name` |
| `azmcp-servicebus-queue-details` | Get Service Bus queue details and metrics | ✅ | `subscription`, `namespace-name`, `queue-name` |
| `azmcp-servicebus-queue-peek` | Peek messages from a Service Bus queue | ✅ | `subscription`, `namespace-name`, `queue-name`, `message-count` |
| `azmcp-servicebus-topic-list` | List topics in a Service Bus namespace | ✅ | `subscription`, `namespace-name` |
| `azmcp-servicebus-topic-details` | Get Service Bus topic details and metrics | ✅ | `subscription`, `namespace-name`, `topic-name` |
| `azmcp-servicebus-topic-subscription-peek` | Peek messages from a topic subscription | ✅ | `subscription`, `namespace-name`, `topic-name`, `subscription-name`, `message-count` |

**Key Features:**
- Reliable message delivery
- Dead letter queue support
- Message sessions and ordering
- Topic-based publish/subscribe
- Integration with Azure Monitor

---

### Azure Workbooks Operations

Interactive reporting and visualization platform.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-workbooks-list` | List workbooks in a subscription and resource group | ✅ | `subscription`, `resource-group`, `category` |
| `azmcp-workbooks-show` | Get details of a specific workbook | ✅ | `subscription`, `resource-group`, `workbook-name` |
| `azmcp-workbooks-create` | Create a new workbook | ❌ | `subscription`, `resource-group`, `workbook-name`, `display-name`, `serialized-data` |
| `azmcp-workbooks-update` | Update an existing workbook | ❌ | `subscription`, `resource-group`, `workbook-name`, `display-name`, `serialized-data` |
| `azmcp-workbooks-delete` | Delete a workbook | ❌ | `subscription`, `resource-group`, `workbook-name` |

**Key Features:**
- Interactive data visualization
- Custom dashboard creation
- Integration with Azure Monitor
- Sharing and collaboration features
- Template-based workbook creation

---

### Azure Load Testing Operations

Application load testing and performance analysis.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-loadtest-create-script` | Create load test scripts (Locust or JMeter) | ❌ | `url`, `script-type`, `output-path` |
| `azmcp-loadtest-run` | Execute load tests in Azure Load Testing | ❌ | `test-script-path`, `resource-id` |
| `azmcp-loadtest-results` | Get performance insights from test runs | ✅ | `test-run-id`, `resource-id` |
| `azmcp-loadtest-resource-select` | Select Azure Load Testing resource | ✅ | `subscription` |

**Key Features:**
- Multiple test script formats (Locust, JMeter)
- Scalable load generation
- Real-time performance monitoring
- Integration with CI/CD pipelines
- Detailed performance analytics

---

### Azure Managed Grafana Operations

Managed Grafana service for monitoring and observability.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-grafana-workspace-list` | List Managed Grafana workspaces | ✅ | `subscription` |
| `azmcp-grafana-dashboard-list` | List dashboards in a Grafana workspace | ✅ | `subscription`, `workspace-name` |
| `azmcp-grafana-datasource-list` | List data sources in a Grafana workspace | ✅ | `subscription`, `workspace-name` |

**Key Features:**
- Fully managed Grafana service
- Integration with Azure Monitor
- Custom dashboard creation
- Multi-tenant workspace support
- Enterprise security features

---

### Azure Marketplace Operations

Azure Marketplace service and offer management.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-marketplace-offer-list` | List marketplace offers | ✅ | `publisher-id`, `offer-type` |
| `azmcp-marketplace-plan-list` | List plans for a marketplace offer | ✅ | `publisher-id`, `offer-id` |
| `azmcp-marketplace-deployment-list` | List marketplace deployments | ✅ | `subscription` |

**Key Features:**
- Marketplace offer discovery
- SaaS application management
- Billing and subscription management
- Partner center integration

---

### Azure Native ISV Operations

Third-party ISV service integrations (e.g., Datadog, Elastic).

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-datadog-monitor-list` | List Datadog monitoring resources | ✅ | `subscription` |
| `azmcp-datadog-sso-config-show` | Show Datadog SSO configuration | ✅ | `subscription`, `monitor-name` |
| `azmcp-elastic-monitor-list` | List Elastic monitoring resources | ✅ | `subscription` |

**Key Features:**
- Native Azure integration with ISV services
- Single sign-on configuration
- Billing integration
- Resource lifecycle management

---

### Azure RBAC Operations

Role-based access control management.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-role-assignment-list` | List role assignments for a scope | ✅ | `subscription`, `scope`, `principal-id` |
| `azmcp-role-definition-list` | List available role definitions | ✅ | `subscription`, `scope` |
| `azmcp-role-assignment-create` | Create a new role assignment | ❌ | `subscription`, `scope`, `principal-id`, `role-definition-id` |
| `azmcp-role-assignment-delete` | Delete a role assignment | ❌ | `subscription`, `scope`, `principal-id`, `role-definition-id` |

**Key Features:**
- Fine-grained access control
- Custom role definition support
- Principal and group management
- Scope-based permissions
- Audit and compliance tracking

---

### Azure MCP Best Practices

Best practices guidance for Azure development and deployment.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-bestpractices-get` | Get best practices for Azure development and deployment | ✅ | `resource`, `action` |

**Resource Options:**
- `general`: General Azure best practices
- `azurefunctions`: Azure Functions specific best practices

**Action Options:**
- `all`: Best practices for both code generation and deployment
- `code-generation`: Best practices for code generation
- `deployment`: Best practices for deployment (Azure Functions only)

**Key Features:**
- Curated best practices from Azure experts
- Service-specific guidance
- Security and performance recommendations
- Cost optimization strategies

---

### Azure Terraform Best Practices

Terraform-specific guidance for Infrastructure as Code.

| Tool | Description | Read-Only | Parameters |
|------|-------------|-----------|------------|
| `azmcp-azureterraformbestpractices-get` | Get Terraform best practices for Azure infrastructure | ✅ | None |

**Key Features:**
- Terraform module recommendations
- State management best practices
- Security and compliance guidelines
- Performance optimization
- CI/CD integration patterns

---

## Common Parameters

Most tools support these common parameters for authentication and reliability:

### Authentication Parameters
- `auth-method`: Authentication method (`credential`, `key`, `connectionString`)
- `tenant`: Azure Active Directory tenant ID or name
- `subscription`: Azure subscription ID or name

### Retry Parameters
- `retry-delay`: Initial delay between retry attempts (seconds)
- `retry-max-delay`: Maximum delay between retries (seconds)
- `retry-max-retries`: Maximum number of retry attempts
- `retry-mode`: Retry strategy (`fixed`, `exponential`)
- `retry-network-timeout`: Network operation timeout (seconds)

## Usage Examples

### Basic Tool Discovery
```bash
# List all available tools
azmcp tools list

# Start server in namespace mode (default)
azmcp server start

# Start server with only storage tools
azmcp server start --namespace storage
```

### Storage Operations
```bash
# List storage accounts
azmcp storage account list --subscription "my-subscription"

# List containers in a storage account
azmcp storage blob container list --account-name "mystorageaccount" --subscription "my-subscription"
```

### Monitor Operations
```bash
# List Log Analytics workspaces
azmcp monitor workspace list --subscription "my-subscription"

# Query recent logs
azmcp monitor log query --workspace "my-workspace" --resource-group "my-rg" --subscription "my-subscription" --query "recent"
```

### AI Search Operations
```bash
# List AI Search services
azmcp search service list --subscription "my-subscription"

# Query a search index
azmcp search index query --service-name "my-search-service" --index-name "my-index" --query "search terms"
```

## Error Handling

All tools include comprehensive error handling with:

- **Retry Logic**: Automatic retry with exponential backoff
- **Validation**: Parameter validation before execution
- **Authentication**: Clear authentication error messages
- **Rate Limiting**: Respect for Azure service rate limits
- **Logging**: Detailed logging for troubleshooting

## Security Considerations

- **Authentication**: Support for managed identity, service principal, and Azure CLI authentication
- **Authorization**: Respect for Azure RBAC permissions
- **Encryption**: All communications use HTTPS/TLS
- **Secrets**: Secure handling of connection strings and keys
- **Auditing**: Integration with Azure Activity Log

## Performance Features

- **Caching**: Intelligent caching of metadata and configuration
- **Pagination**: Automatic handling of large result sets
- **Parallelization**: Concurrent operations where appropriate
- **Streaming**: Efficient handling of large data transfers
- **Compression**: Data compression for network efficiency

## Getting Started

1. **Install the Azure MCP Server**: Follow the [installation instructions](../README.md)
2. **Configure Authentication**: Set up Azure CLI or managed identity authentication
3. **Start the Server**: Use `azmcp server start` with your preferred mode
4. **Discover Tools**: Use `azmcp tools list` to explore available capabilities
5. **Integrate with MCP Client**: Configure your MCP client to connect to the server

For more information, see the [main documentation](../README.md) and [troubleshooting guide](../TROUBLESHOOTING.md).
