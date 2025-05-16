# Release History

## 0.0.20 (Unreleased)

### Features Added

### Breaking Changes

### Bugs Fixed

- Fixes Service Bus host name parameter description. https://github.com/Azure/azure-mcp/pull/209/
- Updates the usage patterns of Azure Developer CLI (azd) when invoked from MCP. https://github.com/Azure/azure-mcp/pull/203

### Features Added

### Other Changes

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
- Support for Kusto (Azure Data Explorer).  https://github.com/Azure/azure-mcp/pull/21

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
