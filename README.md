# Azure MCP Server

An MCP Server and command-line interface designed for AI agents to interact with Azure services through standardized JSON communication patterns, following the Model Context Protocol (MCP) specification.

## Overview

The Azure MCP Server implements the Model Context Protocol (MCP) specification (https://modelcontextprotocol.io) to provide a standardized interface between AI agents and Azure services. This enables seamless integration of Azure capabilities into AI workflows through:

- MCP-compliant JSON schemas for service discovery and interaction
- Structured command and response patterns for AI agent consumption
- Context-aware parameter suggestions and auto-completion
- Standardized error handling and response formats

The server acts as a bridge, translating AI agent requests into Azure service operations while maintaining consistent interaction patterns defined by the MCP specification.

## Getting Started

Build the project:

1. Execute `src/build.ps1` to build binary `.dist/azmcp.exe`.

Start the MCP server:
```bash
azmcp server start
```

## Tools

`azmcp tools list`

## Commands


`azmcp cosmos databases list --subscription-id <subscription-id> --account-name <account-name>`

```json
{
  "status": 200,
  "message": "Success",
  "args": [
    {
      "name": "subscription-id",
      "description": "Azure Subscription ID",
      "command": "azmcp cosmos databases list --subscription-id <subscription-id>",
      "value": "25fd0362-aa79-488b-b37b-d6e892009fdf"
    },
    {
      "name": "account-name",
      "description": "Cosmos DB Account Name",
      "command": "azmcp cosmos databases list --account-name <account-name>",
      "value": "jongcosmostrash"
    }
  ],
  "results": {
    "databases": [
      "ToDoList"
    ]
  },
  "duration": 1234
}
```

## Example Schema with Missing Arg and Suggested Values

`azmcp cosmos databases list --subscription-id <subscription-id>`

```json
{
  "status": 400,
  "message": "Missing required args",
  "args": [
    {
      "name": "subscription-id",
      "description": "Azure Subscription ID",
      "command": "azmcp cosmos databases list --subscription-id <subscription-id>",
      "value": "25fd0362-aa79-488b-b37b-d6e892009fdf",
      "values": []
    },
    {
      "name": "account-name",
      "description": "Cosmos DB Account Name",
      "command": "azmcp cosmos databases list --account-name <account-name>",
      "value": "",
      "values": [
        {
          "name": "jongcosmostrash",
          "id": "jongcosmostrash"
        }
      ]
    }
  ],
  "duration": 156
}
```

`azmcp cosmos databases list`

```json
{
  "status": 400,
  "message": "Missing required args",
  "args": [
    {
      "name": "subscription-id",
      "description": "Azure Subscription ID",
      "command": "azmcp cosmos databases list --subscription-id <subscription-id>",
      "value": "",
      "values": [
        {
          "name": "foo sub",
          "id": "823cb539-d44d-43ee-8dc8-023fd4f27396"
        }
      ]
    },
    {
      "name": "account-name",
      "description": "Cosmos DB Account Name",
      "command": "azmcp cosmos databases list --account-name <account-name>",
      "value": ""
    }
  ],
  "duration": 89
}
```

## Error Handling

The CLI returns structured JSON responses for errors, including:
- Missing required args
- Invalid arg values
- Service availability issues
- Authentication errors

## Response Format

All responses follow a consistent JSON format:
```json
{
  "status": "200|403|500, etc",
  "message": "",
  "args": [],
  "results": [],
  "duration": 123
}
```

## Command Reference

### Global Args

The following args are available for all commands:

| Arg | Required | Default | Description |
|-----------|----------|---------|-------------|
| `--subscription-id` | Yes | - | Azure subscription ID for target resources |
| `--tenant-id` | No | - | Azure tenant ID for authentication |
| `--auth-method` | No | 'credential' | Authentication method ('credential', 'key', 'connectionString') |
| `--retry-max-retries` | No | 3 | Maximum retry attempts for failed operations |
| `--retry-delay` | No | 2 | Delay between retry attempts (seconds) |
| `--retry-max-delay` | No | 10 | Maximum delay between retries (seconds) |
| `--retry-mode` | No | 'exponential' | Retry strategy ('fixed' or 'exponential') |
| `--retry-network-timeout` | No | 100 | Network operation timeout (seconds) |

### Available Commands

#### Server Operations
```bash
# Start the MCP server
azmcp server start
```

#### Subscription Management
```bash
# List available Azure subscriptions
azmcp subscriptions list [--tenant-id <tenant-id>]
```

#### Cosmos DB Operations
```bash
# List Cosmos DB accounts in a subscription
azmcp cosmos accounts list --subscription-id <subscription-id>

# List databases in a Cosmos DB account
azmcp cosmos databases list --subscription-id <subscription-id> --account-name <account-name>

# List containers in a Cosmos DB database
azmcp cosmos databases containers list --subscription-id <subscription-id> --account-name <account-name> --database-name <database-name>

# Query items in a Cosmos DB container
azmcp cosmos databases containers items query --subscription-id <subscription-id> \
                       --account-name <account-name> \
                       --database-name <database-name> \
                       --container-name <container-name> \
                       [--query "SELECT * FROM c"]
```

#### Storage Operations
```bash
# List Storage accounts in a subscription
azmcp storage accounts list --subscription-id <subscription-id>

# List tables in a Storage account
azmcp storage tables list --subscription-id <subscription-id> --account-name <account-name>

# List blobs in a Storage account
azmcp storage blobs list --subscription-id <subscription-id> --account-name <account-name>

# List containers in a Storage blob service
azmcp storage blobs containers list --subscription-id <subscription-id> --account-name <account-name>

# Get detailed properties of a storage container
azmcp storage blobs containers details --subscription-id <subscription-id> --account-name <account-name> --container-name <container-name>
```

#### Monitor Operations
```bash
# List Log Analytics workspaces in a subscription
azmcp monitor workspaces list --subscription-id <subscription-id> [--tenant-id <tenant-id>]

# List tables in a Log Analytics workspace
azmcp monitor tables list --workspace-id <workspace-id> [--tenant-id <tenant-id>]

# Query logs from Azure Monitor using KQL
azmcp monitor logs query --subscription-id <subscription-id> \
                        --workspace-id <workspace-id> \
                        --table <table-name> \
                        --query "<kql-query>" \
                        [--hours <hours>] \
                        [--limit <limit>]

# Examples:
# Query logs from a specific table
azmcp monitor logs query --subscription-id <subscription-id> \
                        --workspace-id <workspace-id> \
                        --table "AppEvents_CL" \
                        --query "| order by TimeGenerated desc"

# Use a predefined query type with a specific table
azmcp monitor logs query --subscription-id <subscription-id> \
                        --workspace-id <workspace-id> \
                        --table "AppEvents_CL" \
                        --query "recent"
```

#### Resource Group Operations
```bash
# List resource groups in a subscription
azmcp groups list --subscription-id <subscription-id> [--tenant-id <tenant-id>]
```

#### CLI Utilities
```bash
# List all available commands and tools
azmcp tools list
```
