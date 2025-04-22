using System.Threading.Tasks;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Sql;
using Azure.ResourceManager.Resources;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace AzureMcp.Commands.Sql;

public sealed class SqlDatabaseQueryService() : ISqlDatabaseQueryService
{
    public async Task<object> ExecuteQueryAsync(string subscription, string serverName, string databaseName, string query)
    {
        var credential = new DefaultAzureCredential();
        var armClient = new ArmClient(credential);
        var subscriptionResource = armClient.GetSubscriptionResource(SubscriptionResource.CreateResourceIdentifier(subscription));
        string? fqdn = null;
        foreach (var sqlServer in subscriptionResource.GetSqlServers())
        {
            if (sqlServer.Data.Name.Equals(serverName, System.StringComparison.OrdinalIgnoreCase))
            {
                fqdn = sqlServer.Data.FullyQualifiedDomainName;
                break;
            }
        }
        if (string.IsNullOrEmpty(fqdn))
            return new { Error = "SQL Server not found." };

        var builder = new SqlConnectionStringBuilder
        {
            DataSource = fqdn,
            InitialCatalog = databaseName,
            Authentication = SqlAuthenticationMethod.ActiveDirectoryDefault
        };
        var results = new List<Dictionary<string, object?>>();
        using var conn = new SqlConnection(builder.ConnectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(query, conn);
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var row = new Dictionary<string, object?>();
            for (int i = 0; i < reader.FieldCount; i++)
                row[reader.GetName(i)] = reader.GetValue(i);
            results.Add(row);
        }
        return results;
    }
}
