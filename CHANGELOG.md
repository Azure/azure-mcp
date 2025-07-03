# Release History

## 0.3.1 (Unreleased)

### Features Added

### Breaking Changes

### Bugs Fixed

### Other Changes

## 0.3.0 (2025-07-03)

### Features Added

- Added support for Azure AI Foundry [[#274](https://github.com/Azure/azure-mcp/pull/274)]. The following tools are now available:
  - `azmcp-foundry-models-list`
  - `azmcp-foundry-models-deploy`
  - `azmcp-foundry-models-deployments-list`
- Added support for telemetry [[#386](https://github.com/Azure/azure-mcp/pull/386)]. Telemetry is enabled by default but can be disabled by setting `AZURE_MCP_COLLECT_TELEMETRY` to `false`.

### Bugs Fixed
- Fixed a bug where `CallToolResult` was always successful. [[#511](https://github.com/Azure/azure-mcp/pull/511)]

## 0.2.6 (2025-07-01)

### Other Changes
- Updated the descriptions of the following tools to improve their usage by Agents: [#492](https://github.com/Azure/azure-mcp/pull/492)
  - `azmcp-datadog-monitoredresources-list`
  - `azmcp-kusto-cluster-list`
  - `azmcp-kusto-database-list`
  - `azmcp-kusto-sample`
  - `azmcp-kusto-table-list`
  - `azmcp-kusto-table-schema`

## 0.2.5 (2025-06-26)

### Bugs Fixed
- Fixed issue where tool listing incorrectly returned resources instead of text. [#465](https://github.com/Azure/azure-mcp/issues/465)
- Fixed invalid modification to HttpClient in KustoClient. [#433](https://github.com/Azure/azure-mcp/issues/433)

## 0.2.4 (2025-06-24)

### Features Added

- Added new command for resource-centric logs query in Azure Monitor with command path `azmcp-monitor-resource-logs-query` - https://github.com/Azure/azure-mcp/pull/413
- Added support for starting the server with a subset of services using the `--service` flag - https://github.com/Azure/azure-mcp/pull/424
- Improved index schema handling in Azure AI Search (index descriptions, facetable fields, etc.) - https://github.com/Azure/azure-mcp/pull/440
- Added new commands for querying metrics with Azure Monitor with command paths `azmcp-monitor-metrics-query` and `azmcp-monitor-metrics-definitions`. - https://github.com/Azure/azure-mcp/pull/428

### Breaking Changes

- Changed the command for workspace-based logs query in Azure Monitor from `azmcp-monitor-log-query` to `azmcp-monitor-workspace-logs-query`

### Bugs Fixed

- Fixed handling of non-retrievable fields in Azure AI Search. [#416](https://github.com/Azure/azure-mcp/issues/416)

### Other Changes

- Repository structure changed to organize all of an Azure service's code into a single "area" folder. ([426](https://github.com/Azure/azure-mcp/pull/426))
- Upgraded Azure.Messaging.ServiceBus to 7.20.1 and Azure.Core to 1.46.2. ([441](https://github.com/Azure/azure-mcp/pull/441/))
- Updated to ModelContextProtocol 0.3.0-preview1, which brings support for the 06-18-2025 MCP specification. ([431](https://github.com/Azure/azure-mcp/pull/431))

## 0.2.3 (2025-06-19)

### Features Added

- Adds support to launch MCP server in readonly mode - https://github.com/Azure/azure-mcp/pull/410

### Bugs Fixed

- MCP tools now expose annotations to clients https://github.com/Azure/azure-mcp/pull/388

## 0.2.2 (2025-06-17)

### Features Added

- Support for Azure ISV Services https://github.com/Azure/azure-mcp/pull/199/
- Support for Azure RBAC https://github.com/Azure/azure-mcp/pull/266
- Support for Key Vault Secrets https://github.com/Azure/azure-mcp/pull/173


## 0.2.1 (2025-06-12)

### Bugs Fixed

- Fixed the issue where queries containing double quotes failed to execute. https://github.com/Azure/azure-mcp/pull/338
- Enables dynamic proxy mode within single "azure" tool. https://github.com/Azure/azure-mcp/pull/325

## 0.2.0 (2025-06-09)

### Features Added

- Support for launching smaller service level MCP servers. https://github.com/Azure/azure-mcp/pull/324

### Bugs Fixed

- Fixed failure starting Docker image. https://github.com/Azure/azure-mcp/pull/301

## 0.1.2 (2025-06-03)

### Bugs Fixed

- Monitor Query Logs Failing.  Fixed with https://github.com/Azure/azure-mcp/pull/280

## 0.1.1 (2025-05-30)

### Bugs Fixed

- Fixed return value of `tools/list` to use JSON object names. https://github.com/Azure/azure-mcp/pull/275

### Other Changes

- Update .NET SDK version to 9.0.300 https://github.com/Azure/azure-mcp/pull/278

## 0.1.0 (2025-05-28)

### Breaking Changes

- `azmcp tool list` "args" changes to "options"

### Other Changes

- Removed "Arguments" from code base in favor of "Options" to align with System. CommandLine semantics. https://github.com/Azure/azure-mcp/pull/232

## 0.0.21 (2025-05-22)

### Features Added

- Support for Azure Redis Caches and Clusters https://github.com/Azure/azure-mcp/pull/198
- Support for Azure Monitor Health Models https://github.com/Azure/azure-mcp/pull/208

### Bugs Fixed

- Updates the usage patterns of Azure Developer CLI (azd) when invoked from MCP. https://github.com/Azure/azure-mcp/pull/203
- Fixes server binding issue when using SSE transport in Docker by replacing `ListenLocalhost` with `ListenAnyIP`, allowing external access via port mapping. https://github.com/Azure/azure-mcp/pull/233

### Other Changes

- Updated to the latest ModelContextProtocol library. https://github.com/Azure/azure-mcp/pull/220

## 0.0.20 (2025-05-17)

### Bugs Fixed

- Improve the formatting in the ParseJsonOutput method and refactor it to utilize a ParseError record. https://github.com/Azure/azure-mcp/pull/218
- Added dummy argument for best practices tool, so the schema is properly generated for Python Open API use cases. https://github.com/Azure/azure-mcp/pull/219

## 0.0.19 (2025-05-15)

### Bugs Fixed

- Fixes Service Bus host name parameter description. https://github.com/Azure/azure-mcp/pull/209/

## 0.0.18 (2025-05-14)

### Bugs Fixed

- Include option to exclude managed keys. https://github.com/Azure/azure-mcp/pull/202

## 0.0.17 (2025-05-13)

### Bugs Fixed

- Added an opt-in timeout for browser-based authentication to handle cases where the process waits indefinitely if the user closes the browser. https://github.com/Azure/azure-mcp/pull/189

## 0.0.16 (2025-05-13)

### Bugs Fixed

- Fixed being able to pass args containing spaces through an npx call to the cli

### Other Changes

- Updated to the latest ModelContextProtocol library. https://github.com/Azure/azure-mcp/pull/161

## 0.0.15 (2025-05-09)

### Features Added

- Support for getting properties and runtime information for Azure Service Bus queues, topics, and subscriptions. https://github.com/Azure/azure-mcp/pull/150/
- Support for peeking at Azure Service Bus messages from queues or subscriptions. https://github.com/Azure/azure-mcp/pull/144
- Adds Best Practices tool that provides guidance to LLMs for effective code generation. https://github.com/Azure/azure-mcp/pull/153 https://github.com/Azure/azure-mcp/pull/156

### Other Changes

- Disabled Parallel testing in the ADO pipeline for Live Tests https://github.com/Azure/azure-mcp/pull/151

## 0.0.14 (2025-05-07)

### Features Added

- Support for Azure Key Vault keys https://github.com/Azure/azure-mcp/pull/119
- Support for Azure Data Explorer  https://github.com/Azure/azure-mcp/pull/21

## 0.0.13 (2025-05-06)

### Features Added

- Support for Azure PostgreSQL. https://github.com/Azure/azure-mcp/pull/81

## 0.0.12 (2025-05-05)

### Features Added

- Azure Search Tools https://github.com/Azure/azure-mcp/pull/83

### Other Changes

- Arguments no longer echoed in response: https://github.com/Azure/azure-mcp/pull/79
- Editorconfig and gitattributes updated: https://github.com/Azure/azure-mcp/pull/91

## 0.0.11 (2025-04-29)

### Features Added

### Breaking Changes

### Bugs Fixed
- Bug fixes to existing MCP commands
- See https://github.com/Azure/azure-mcp/releases/tag/0.0.11

### Other Changes

## 0.0.10 (2025-04-17)

### Features Added
- Support for Azure Cosmos DB (NoSQL databases).
- Support for Azure Storage.
- Support for Azure Monitor (Log Analytics).
- Support for Azure App Configuration.
- Support for Azure Resource Groups.
- Support for Azure CLI.
- Support for Azure Developer CLI (azd).

### Breaking Changes

### Bugs Fixed
- See https://github.com/Azure/azure-mcp/releases/tag/0.0.10

### Other Changes
- See Blog post for details https://devblogs.microsoft.com/azure-sdk/introducing-the-azure-mcp-server/
