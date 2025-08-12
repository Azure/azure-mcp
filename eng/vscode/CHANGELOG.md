
# Release History

## 0.5.5 - 2025-08-12

### Added

- Added support for listing Azure Container Registry (ACR) registries in a subscription via the `azmcp-acr-registry-list` command. [[#915](https://github.com/Azure/azure-mcp/issues/915)]
- Added new Azure Storage commands:
  - `azmcp-storage-account-create`: Create a new Storage account. [[#927](https://github.com/Azure/azure-mcp/issues/927)]
  - `azmcp-storage-queue-message-send`: Send a message to a Storage queue. [[#794](https://github.com/Azure/azure-mcp/pull/794)]
  - `azmcp-storage-blob-details`: Get details about a Storage blob. [[#930](https://github.com/Azure/azure-mcp/issues/930)]
  - `azmcp-storage-blob-container-create`: Create a new Storage blob container. [[#937](https://github.com/Azure/azure-mcp/issues/937)]
- Bundled the **GitHub Copilot for Azure** extension as part of the Azure MCP Server extension pack.

### Changed

- The `azmcp-storage-account-list` command now returns account metadata objects instead of plain strings. Each item includes: `name`, `location`, `kind`, `skuName`, `skuTier`, `hnsEnabled`, `allowBlobPublicAccess`, `enableHttpsTrafficOnly`. Update scripts to read the `name` property. The underlying `IStorageService.GetStorageAccounts()` signature changed from `Task<List<string>>` to `Task<List<StorageAccountInfo>>`. [[#904](https://github.com/Azure/azure-mcp/issues/904)]
- Consolidated "AzSubscriptionGuid" telemetry logic into `McpRuntime`. [[#935](https://github.com/Azure/azure-mcp/pull/935)]

### Fixed

- Fixed best practices tool invocation failure when passing "all" action with "general" or "azurefunctions" resources. [[#757](https://github.com/Azure/azure-mcp/issues/757)]
- Updated metadata for CREATE and SET tools to `destructive = true`. [[#773](https://github.com/Azure/azure-mcp/pull/773)]

## 0.5.4 - 2025-08-07

### Changed

- Improved Azure MCP display name in VS Code from 'azure-mcp-server-ext' to 'Azure MCP' for better user experience in the Configure Tools interface. [[#871](https://github.com/Azure/azure-mcp/issues/871), [#876](https://github.com/Azure/azure-mcp/pull/876)]
- Updated the description of the following `CommandGroup`s to improve their tool usage by Agents:
  - Azure AI Search [[#874](https://github.com/Azure/azure-mcp/pull/874)]
  - Storage [#879](https://github.com/Azure/azure-mcp/pull/879)

### Fixed

- Fixed subscription parameter handling across all Azure MCP service methods to consistently use `subscription` instead of `subscriptionId`, enabling proper support for both subscription IDs and subscription names. [[#877](https://github.com/Azure/azure-mcp/issues/877)]
- Fixed `ToolExecuted` telemetry activity being created twice. [[#741](https://github.com/Azure/azure-mcp/pull/741)]

## 0.5.3 - 2025-08-05

### Added

- Added support for providing the `--content-type` and `--tags` properties to the `azmcp-appconfig-kv-set` command. [[#459](https://github.com/Azure/azure-mcp/pull/459)]
- Added `filter-path` and `recursive` capabilities to `azmcp-storage-datalake-file-system-list-paths`. [[#770](https://github.com/Azure/azure-mcp/issues/770)]
- Added support for listing files and directories in Azure File Shares via the `azmcp-storage-share-file-list` command. This command recursively lists all items in a specified file share directory with metadata including size, last modified date, and content type. [[#793](https://github.com/Azure/azure-mcp/pull/793)]
- Added support for Azure Virtual Desktop with new commands: [[#653](https://github.com/Azure/azure-mcp/pull/653)]
  - `azmcp-virtualdesktop-hostpool-list` - List all host pools in a subscription
  - `azmcp-virtualdesktop-sessionhost-list` - List all session hosts in a host pool
  - `azmcp-virtualdesktop-sessionhost-usersession-list` - List all user sessions on a specific session host
- Added support for creating and publishing DevDeviceId in telemetry. [[#810](https://github.com/Azure/azure-mcp/pull/810/)]
- Added caching for Cosmos DB databases and containers. [[813](https://github.com/Azure/azure-mcp/pull/813)]

### Changed

- **Parameter Name Changes**: Removed unnecessary "-name" suffixes from command parameters across 25+ parameters in 12+ Azure service areas to improve consistency and usability. Users will need to update their command-line usage and scripts. [[#853](https://github.com/Azure/azure-mcp/pull/853)]
  - **AppConfig**: `--account-name` → `--account`
  - **Search**: `--service-name` → `--service`, `--index-name` → `--index`
  - **Cosmos**: `--account-name` → `--account`, `--database-name` → `--database`, `--container-name` → `--container`
  - **Kusto**: `--cluster-name` → `--cluster`, `--database-name` → `--database`, `--table-name` → `--table`
  - **AKS**: `--cluster-name` → `--cluster`
  - **Postgres**: `--user-name` → `--user`
  - **ServiceBus**: `--queue-name` → `--queue`, `--topic-name` → `--topic`
  - **Storage**: `--account-name` → `--account`, `--container-name` → `--container`, `--table-name` → `--table`, `--file-system-name` → `--file-system`, `--tier-name` → `--tier`
  - **Monitor**: `--table-name` → `--table`, `--model` → `--health-model`, `--resource-name` → `--resource`
  - **Foundry**: `--deployment-name` → `--deployment`, `--publisher-name` → `--publisher`, `--license-name` → `--license`, `--sku-name` → `--sku`, `--azure-ai-services-name` → `--azure-ai-services`

### Fixed

- Fixed an issue where the `azmcp-storage-blob-batch-set-tier` command did not correctly handle the `--tier` parameter when setting the access tier for multiple blobs. [[#808](https://github.com/Azure/azure-mcp/pull/808)]

## 0.5.2 - 2025-07-31

### Added

- Added support for batch setting access tier for multiple Azure Storage blobs via the `azmcp-storage-blob-batch-set-tier` command. This command efficiently changes the storage tier (Hot, Cool, Archive, etc) for multiple blobs simultaneously in a single operation. [[#735](https://github.com/Azure/azure-mcp/issues/735)]
- Added descriptions to all Azure MCP command groups to improve discoverability and usability when running the server with `--mode single` or `--mode namespace`. [[#791](https://github.com/Azure/azure-mcp/pull/791)]

### Changed

- Removed toast notifications related to Azure MCP server registration and startup instructions.[[#785](https://github.com/Azure/azure-mcp/pull/785)]
- Removed `--partner-tenant-id` option from `azmcp-marketplace-product-get` command. [[#656](https://github.com/Azure/azure-mcp/pull/656)]

## 0.5.1 - 2025-07-29

### Added

- Added support for listing SQL databases via the command: `azmcp-sql-db-list`. [[#746](https://github.com/Azure/azure-mcp/pull/746)]
- Added support for reading `AZURE_SUBSCRIPTION_ID` from the environment variables if a subscription is not provided. [[#533](https://github.com/Azure/azure-mcp/pull/533)]

## 0.5.0 - 2025-07-24

### Added
- Initial Release
