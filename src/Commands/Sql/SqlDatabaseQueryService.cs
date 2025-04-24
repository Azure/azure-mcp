using System.Threading.Tasks;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Sql;
using Azure.ResourceManager.Resources;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using AzureMcp.Services.Azure;

namespace AzureMcp.Commands.Sql;

public sealed class SqlDatabaseQueryService() : BaseAzureService, ISqlDatabaseQueryService
{
    public async Task<object> ExecuteQueryAsync(string subscription, string serverName, string databaseName, string query, IProgress<int>? progress = null)
    {
        if (string.IsNullOrWhiteSpace(subscription))
        {
            return new { Error = "Subscription ID is required." };
        }
        var credential = await GetCredential();
        var armClient = new ArmClient(credential);
        // Ensure subscription is a full resource ID
        string subscriptionResourceId = subscription.StartsWith("/subscriptions/") 
            ? subscription 
            : $"/subscriptions/{subscription}";
        // Pass only the subscription GUID to CreateResourceIdentifier
        SubscriptionResource? subscriptionResource = null;
        try
        {
            subscriptionResource = armClient.GetSubscriptionResource(SubscriptionResource.CreateResourceIdentifier(subscription));
        }
        catch (Exception ex)
        {
            return new { Error = $"Failed to get subscription resource: {ex.Message}" };
        }
        if (subscriptionResource == null)
        {
            return new { Error = "Subscription resource could not be found." };
        }
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
        int rowCount = 0;
        while (await reader.ReadAsync())
        {
            var row = new Dictionary<string, object?>();
            for (int i = 0; i < reader.FieldCount; i++)
                row[reader.GetName(i)] = reader.GetValue(i);
            results.Add(row);
            rowCount++;
            if (progress != null && rowCount % 100 == 0)
            {
                progress.Report(rowCount);
            }
        }
        if (progress != null)
        {
            progress.Report(rowCount);
        }
        return results;
    }
}
