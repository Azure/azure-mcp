# SQL Firewall Rule List Command

## Command: `azmcp sql server firewall-rule list`

### Description
Lists all firewall rules configured for an Azure SQL Server. This command retrieves firewall rules that control which IP addresses can access the SQL server.

### Usage
```bash
azmcp sql server firewall-rule list --subscription <subscription-id> --resource-group <resource-group> --server <server-name>
```

### Parameters
- `--subscription`: Azure subscription ID or name (required)
- `--resource-group`: Name of the resource group containing the SQL server (required)
- `--server`: Name of the Azure SQL server (required)

### Output
Returns a JSON array of firewall rule objects, each containing:
- `name`: Name of the firewall rule
- `id`: Azure resource ID of the firewall rule
- `type`: Resource type (Microsoft.Sql/servers/firewallRules)
- `startIpAddress`: Start IP address of the allowed range
- `endIpAddress`: End IP address of the allowed range

### Example Output
```json
{
  "firewallRules": [
    {
      "name": "AllowAllWindowsAzureIps",
      "id": "/subscriptions/.../resourceGroups/.../providers/Microsoft.Sql/servers/myserver/firewallRules/AllowAllWindowsAzureIps",
      "type": "Microsoft.Sql/servers/firewallRules",
      "startIpAddress": "0.0.0.0",
      "endIpAddress": "0.0.0.0"
    },
    {
      "name": "AllowMyOffice",
      "id": "/subscriptions/.../resourceGroups/.../providers/Microsoft.Sql/servers/myserver/firewallRules/AllowMyOffice",
      "type": "Microsoft.Sql/servers/firewallRules",
      "startIpAddress": "203.0.113.1",
      "endIpAddress": "203.0.113.255"
    }
  ]
}
```

### Notes
- This is a read-only operation that requires read permissions on the SQL server
- If no firewall rules are configured, the command returns an empty result
- Equivalent to `az sql server firewall-rule list` command in Azure CLI