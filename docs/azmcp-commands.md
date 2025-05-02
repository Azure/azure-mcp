# Azure MCP CLI Command Reference

## Global Args

The following args are available for all commands:

| Arg | Required | Default | Description |
|-----------|----------|---------|-------------|
| `--subscription` | Yes | - | Azure subscription ID for target resources |
| `--tenant-id` | No | - | Azure tenant ID for authentication |
| `--auth-method` | No | 'credential' | Authentication method ('credential', 'key', 'connectionString') |
| `--retry-max-retries` | No | 3 | Maximum retry attempts for failed operations |
| `--retry-delay` | No | 2 | Delay between retry attempts (seconds) |
| `--retry-max-delay` | No | 10 | Maximum delay between retries (seconds) |
| `--retry-mode` | No | 'exponential' | Retry strategy ('fixed' or 'exponential') |
| `--retry-network-timeout` | No | 100 | Network operation timeout (seconds) |

## Available Commands

### Server Operations
```bash
# Start the MCP Server
azmcp server start [--transport <transport>]
```

### Subscription Management
```bash
# List available Azure subscriptions
azmcp subscription list [--tenant-id <tenant-id>]
```

### Cosmos DB Operations
```bash
# List Cosmos DB accounts in a subscription
azmcp cosmos account list --subscription <subscription>

# List databases in a Cosmos DB account
azmcp cosmos database list --subscription <subscription> --account-name <account-name>

# List containers in a Cosmos DB database
azmcp cosmos database container list --subscription <subscription> --account-name <account-name> --database-name <database-name>

# Query items in a Cosmos DB container
azmcp cosmos database container item query --subscription <subscription> \
                       --account-name <account-name> \
                       --database-name <database-name> \
                       --container-name <container-name> \
                       [--query "SELECT * FROM c"]
```

### Storage Operations
```bash
# List Storage accounts in a subscription
azmcp storage account list --subscription <subscription>

# List tables in a Storage account
azmcp storage table list --subscription <subscription> --account-name <account-name>

# List blobs in a Storage container
azmcp storage blob list --subscription <subscription> --account-name <account-name> --container-name <container-name>

# List containers in a Storage blob service
azmcp storage blob container list --subscription <subscription> --account-name <account-name>

# Get detailed properties of a storage container
azmcp storage blob container details --subscription <subscription> --account-name <account-name> --container-name <container-name>
```

### Monitor Operations
```bash
# List Log Analytics workspaces in a subscription
azmcp monitor workspace list --subscription <subscription>

# List tables in a Log Analytics workspace
azmcp monitor table list --subscription <subscription> --workspace <workspace> --resource-group <resource-group>

# Query logs from Azure Monitor using KQL
azmcp monitor log query --subscription <subscription> \
                        --workspace <workspace> \
                        --table-name <table-name> \
                        --query "<kql-query>" \
                        [--hours <hours>] \
                        [--limit <limit>]

# Examples:
# Query logs from a specific table
azmcp monitor log query --subscription <subscription> \
                        --workspace <workspace> \
                        --table-name "AppEvents_CL" \
                        --query "| order by TimeGenerated desc"
```

### App Configuration Operations
```bash
# List App Configuration stores in a subscription
azmcp appconfig account list --subscription <subscription>

# List all key-value settings in an App Configuration store
azmcp appconfig kv list --subscription <subscription> --account-name <account-name> [--key <key>] [--label <label>]

# Show a specific key-value setting
azmcp appconfig kv show --subscription <subscription> --account-name <account-name> --key <key> [--label <label>]

# Set a key-value setting
azmcp appconfig kv set --subscription <subscription> --account-name <account-name> --key <key> --value <value> [--label <label>]

# Lock a key-value setting (make it read-only)
azmcp appconfig kv lock --subscription <subscription> --account-name <account-name> --key <key> [--label <label>]

# Unlock a key-value setting (make it editable)
azmcp appconfig kv unlock --subscription <subscription> --account-name <account-name> --key <key> [--label <label>]

# Delete a key-value setting
azmcp appconfig kv delete --subscription <subscription> --account-name <account-name> --key <key> [--label <label>]
```

### Resource Group Operations
```bash
# List resource groups in a subscription
azmcp group list --subscription <subscription>
```

### Azure CLI Extension Operations
```bash
# Execute any Azure CLI command
azmcp extension az --command "<command>"

# Examples:
# List resource groups
azmcp extension az --command "group list"

# Get storage account details
azmcp extension az --command "storage account show --name <account-name> --resource-group <resource-group>"

# List virtual machines
azmcp extension az --command "vm list --resource-group <resource-group>"
```

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

## Error Handling

The CLI returns structured JSON responses for errors, including:
- Missing required args
- Invalid arg values
- Service availability issues
- Authentication errors