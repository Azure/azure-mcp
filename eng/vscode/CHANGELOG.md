
# Release History

## 0.5.4 - Unreleased

### Added

### Changed
- Improved Azure MCP display name in VS Code from 'azure-mcp-server-ext' to 'Azure MCP' for better user experience in the Configure Tools interface. [[#871](https://github.com/Azure/azure-mcp/issues/871), [#876](https://github.com/Azure/azure-mcp/pull/876)]

### Fixed

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
