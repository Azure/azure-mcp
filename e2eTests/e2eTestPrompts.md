This file contains prompts used for end-to-end testing to ensure each tool is invoked properly by MCP clients. The tables are organized by Azure MCP Server areas in alphabetical order.

## App Configuration

| Tool Name | Test Prompt |
|:----------|:----------|
| azmcp-appconfig-account-list | List App Configuration stores in my subscription |
| azmcp-appconfig-kv-delete | Delete the key <key_name> in an App Configuration store <app_config_store_name> |
| azmcp-appconfig-kv-list | List all key-value settings in my App Configuration store <app_config_store_name> |
| azmcp-appconfig-kv-lock | Lock the key <key_name> in an App Configuration store <app_config_store_name> |
| azmcp-appconfig-kv-set | Set the value for key <key_name> in an App Configuration store <app_config_store_name> |
| azmcp-appconfig-kv-show | Show the content for key <key_name> in an App Configuration store <app_config_store_name> |
| azmcp-appconfig-kv-unlock | Unlock the key <key_name> in an App Configuration store <app_config_store_name> |

## Best Practices

| Tool Name | Test Prompt |
|:----------|:----------|
| | |

## Cosmos DB

| Tool Name | Test Prompt |
|:----------|:----------|
| azmcp-cosmos-account-list | List all my cosmosdb accounts in my subscription |
| azmcp-cosmos-database-container-list | List all the containers in my database <database_name> for my cosmosdb account <account_name> |
| azmcp-cosmos-database-list | List all the database in my cosmosdb account <account_name> |

## Key Vault

| Tool Name | Test Prompt |
|:----------|:----------|
| azmcp-keyvault-key-create | Create a new key called <key_name> with RSA type in <key_vault_account_name> |
| azmcp-keyvault-key-get | Get the details about the key <key_name> in <key_vault_account_name> |
| azmcp-keyvault-key-list | List all the keys in <key_vault_account_name> |

## Kusto

| Tool Name | Test Prompt |
|:----------|:----------|
| | |

## Monitor

| Tool Name | Test Prompt |
|:----------|:----------|
| azmcp-monitor-table-list | List all tables in the Log Analytics workspace <workspace_name> |
| azmcp-monitor-table-type-list | List available table types in my Log Analytics workspace <workspace_name> |
| azmcp-monitor-workspace-list | List Log Analytics workspaces in my subscription |

## PostgreSQL

| Tool Name | Test Prompt |
|:----------|:----------|
| | |

## Redis Cache

| Tool Name | Test Prompt |
|:----------|:----------|
| | |

## Resource Group

| Tool Name | Test Prompt |
|:----------|:----------|
| | |

## Role Based Access Control

| Tool Name | Test Prompt |
|:----------|:----------|
| | |

## Search

| Tool Name | Test Prompt |
|:----------|:----------|
| | |

## Service Bus

| Tool Name | Test Prompt |
|:----------|:----------|
| azmcp-servicebus-queue-details | Get the details about my service bus <service_bus_name> queue <queue_name> |
| azmcp-servicebus-topic-details | Get the details about my service bus <service_bus_name> topic <topic_name> |
| azmcp-servicebus-topic-subscription-details | Get the details about my service bus <service_bus_name> subscription <subscription_name> |

## Storage

| Tool Name | Test Prompt |
|:----------|:----------|
| azmcp-storage-account-list | List all my storage accounts in my subscription |
| azmcp-storage-blob-container-details | Get detailed properties of the storage container files in my storage account <account_name> |
| azmcp-storage-blob-container-list | List all blob containers in my storage account <account_name> |
| azmcp-storage-blob-list | List all blobs in my blob container <container_name> of my storage account <account_name> |
| azmcp-storage-table-list | List all tables in my storage account <account_name> |

## Subscription

| Tool Name | Test Prompt |
|:----------|:----------|
| | |

