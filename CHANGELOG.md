# Release History

## 0.4.2 (Unreleased)

### Features Added

### Breaking Changes

### Bugs Fixed

### Other Changes

## 0.4.1 (2025-07-17)

### Features Added

- Added support for the following Azure Load Testing operations: [[#315](https://github.com/Azure/azure-mcp/pull/315)]
  - `azmcp-loadtesting-testresource-list` - List Azure Load testing resources.
  - `azmcp-loadtesting-testresource-create` - Create a new Azure Load testing resource.
  - `azmcp-loadtesting-test-get` - Get details of a specific load test configuration.
  - `azmcp-loadtesting-test-create` - Create a new load test configuration.
  - `azmcp-loadtesting-testrun-get` - Get details of a specific load test run.
  - `azmcp-loadtesting-testrun-list` - List all load test runs for a specific test.
  - `azmcp-loadtesting-testrun-create` - Create a new load test run.
  - `azmcp-loadtesting-testrun-delete` - Delete a specific load test run.
- Added support for scanning Azure resources for compliance recommendations using the Azure Quick Review CLI via the command: `azmcp-extension-azqr`. [[#510](https://github.com/Azure/azure-mcp/pull/510)]
- Added support for listing paths in Data Lake file systems via the command: `azmcp-storage-datalake-file-system-list-paths`. [[#608](https://github.com/Azure/azure-mcp/pull/608)]
- Added support for listing SQL elastic pools via the command: `azmcp-sql-elastic-pool-list`. [[#606](https://github.com/Azure/azure-mcp/pull/606)]
- Added support for listing SQL server firewall rules via the command: `azmcp-sql-firewall-rule-list`. [[#610](https://github.com/Azure/azure-mcp/pull/610)]
- Added new commands for obtaining Azure Functions best practices via the following commands: [[#630](https://github.com/Azure/azure-mcp/pull/630)]
  - `azmcp-bestpractices-azurefunctions-get-code-generation` - Get code generation best practices for Azure Functions.
  - `azmcp-bestpractices-azurefunctions-get-deployment` - Get deployment best practices for Azure Functions.
- Added support for get details about a product in the Azure Marketplace via the command: `azmcp-marketplace-product-get`. [[#442](https://github.com/Azure/azure-mcp/pull/442)]

### Breaking Changes

- Renamed the command `azmcp-bestpractices-get` to `azmcp-bestpractices-general-get`. [[#630](https://github.com/Azure/azure-mcp/pull/630)]

### Bugs Fixed

- Fixed an issue with Azure CLI executable path resolution on Windows. [[#611](https://github.com/Azure/azure-mcp/issues/611)]
- Fixed a tool discovery timing issue when calling tools on fresh server instances. [[#604](https://github.com/Azure/azure-mcp/issues/604)]
- Fixed issue where unrecognizable json would be sent to MCP clients in STDIO mode at startup. [[#644](https://github.com/Azure/azure-mcp/issues/644)]

### Other Changes

- Changed `engines.node` in `package.json` to require Node.js version `>=20.0.0`. [[#628](https://github.com/Azure/azure-mcp/pull/628)]

## 0.4.0 (2025-07-15)

### Features Added

- Added support for listing Azure Kubernetes Service (AKS) clusters via the command `azmcp-aks-cluster-list`. [[#560](https://github.com/Azure/azure-mcp/pull/560)]
- Made the following Ahead of Time (AOT) compilation improvements saving `6.96 MB` in size total:
  - Switched to the trimmer-friendly `CreateSlimBuilder` API from `CreateBuilder`, saving `0.63 MB` in size for the native executable. [[#564](https://github.com/Azure/azure-mcp/pull/564)]
  - Switched to the trimmer-friendly `npgsql` API, saving `2.69 MB` in size for the native executable. [[#592](https://github.com/Azure/azure-mcp/pull/592)]
  - Enabled `IlcFoldIdenticalMethodBodies` to fold identical method bodies, saving `3.64 MB` in size for the native executable. [[#598](https://github.com/Azure/azure-mcp/pull/598)]
- Added support for using the hyphen/dash ("-") character in command names. [[#531](https://github.com/Azure/azure-mcp/pull/531)]
- Added support for authenticating with the Azure account used to log into VS Code. Authentication now prioritizes the VS Code broker credential when in the context of VS Code. [[#452](https://github.com/Azure/azure-mcp/pull/452)]

### Breaking Changes

- Removed SSE (Server-Sent Events) transport support. Now, only stdio transport is supported as SSE is no longer part of the MCP specification. [[#593](https://github.com/Azure/azure-mcp/issues/593)]
- Renamed `azmcp-sql-server-entraadmin-list` to `azmcp-sql-server-entra-admin-list` for better readability. [[#602](https://github.com/Azure/azure-mcp/pull/602)]

### Bugs Fixed

- Added a post-install script to ensure platform-specific versions like `@azure/mcp-${platform}-${arch}` can be resolved. Otherwise, fail install to prevent npx caching of `@azure/mcp`. [[#597](https://github.com/Azure/azure-mcp/pull/597)]
- Improved install reliability and error handling when missing platform packages on Ubuntu. [[#394](https://github.com/Azure/azure-mcp/pull/394)]

### Other Changes
- Updated `engines.node` in `package.json` to require Node.js version `>=22.0.0`.

#### Dependency Updates

- Updated the `ModelContextProtocol.AspNetCore` version from `0.3.0-preview.1` to `0.3.0-preview.2`. [[#519](https://github.com/Azure/azure-mcp/pull/519)]

## 0.3.2 (2025-07-10)

### Features Added

- Added support for listing Azure Managed Grafana details via the command: `azmcp-grafana-list`. [[#532](https://github.com/Azure/azure-mcp/pull/532)]
- Added agent best practices for Azure Terraform commands. [[#420](https://github.com/Azure/azure-mcp/pull/420)]

### Bugs Fixed

- Fixed issue where trace logs could be collected as telemetry. [[#540](https://github.com/Azure/azure-mcp/pull/540/)]
- Fixed an issue that prevented the Azure MCP from finding the Azure CLI if it was installed on a path other than the default global one. [[#351](https://github.com/Azure/azure-mcp/issues/351)]

## 0.3.1 (2025-07-08)

### Features Added

- Added support for the following SQL operations:
  - `azmcp-sql-db-show` - Show details of a SQL Database [[#516](https://github.com/Azure/azure-mcp/pull/516)]
  - `azmcp-sql-server-entra-admin-list` - List Microsoft Entra ID administrators for a SQL server [[#529](https://github.com/Azure/azure-mcp/pull/529)]
- Updates Azure MCP tool loading configurations at launch time. [[#513](https://github.com/Azure/azure-mcp/pull/513)]

### Breaking Changes

- Deprecated the `--service` flag. Use `--namespace` and `--mode` options to specify the service and mode the server will run in. [[#513](https://github.com/Azure/azure-mcp/pull/513)]

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
