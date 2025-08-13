// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Core.Options;
using AzureMcp.Core.Services.Azure;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.Sql.Models;
using AzureMcp.Sql.Services.Models;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Sql.Services;

public class SqlService(ISubscriptionService subscriptionService, ITenantService tenantService, ILogger<SqlService> logger) : BaseAzureResourceService(subscriptionService, tenantService), ISqlService
{
    private readonly ILogger<SqlService> _logger = logger;

    /// <summary>
    /// Retrieves a specific SQL database from an Azure SQL Server.
    /// </summary>
    /// <param name="serverName">The name of the SQL server hosting the database</param>
    /// <param name="databaseName">The name of the database to retrieve</param>
    /// <param name="resourceGroup">The name of the resource group containing the server</param>
    /// <param name="subscription">The subscription ID or name</param>
    /// <param name="retryPolicy">Optional retry policy configuration for resilient operations</param>
    /// <param name="cancellationToken">Token to observe for cancellation requests</param>
    /// <returns>The SQL database if found, otherwise throws KeyNotFoundException</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the specified database is not found</exception>
    /// <exception cref="ArgumentException">Thrown when required parameters are null or empty</exception>
    public async Task<SqlDatabase> GetDatabaseAsync(
        string serverName,
        string databaseName,
        string resourceGroup,
        string subscription,
        RetryPolicyOptions? retryPolicy,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await ExecuteSingleResourceQueryAsync(
                "Microsoft.Sql/servers/databases",
                resourceGroup,
                subscription,
                retryPolicy,
                ConvertToSqlDatabaseModel,
                $"name =~ '{EscapeKqlString(databaseName)}'",
                cancellationToken);

            if (result == null)
            {
                throw new KeyNotFoundException($"SQL database '{databaseName}' not found in resource group '{resourceGroup}' for subscription '{subscription}'.");
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error getting SQL database. Server: {Server}, Database: {Database}, ResourceGroup: {ResourceGroup}, Subscription: {Subscription}",
                serverName, databaseName, resourceGroup, subscription);
            throw;
        }
    }

    /// <summary>
    /// Retrieves a list of all SQL databases from an Azure SQL Server.
    /// </summary>
    /// <param name="serverName">The name of the SQL server to list databases from</param>
    /// <param name="resourceGroup">The name of the resource group containing the server</param>
    /// <param name="subscription">The subscription ID or name</param>
    /// <param name="retryPolicy">Optional retry policy configuration for resilient operations</param>
    /// <param name="cancellationToken">Token to observe for cancellation requests</param>
    /// <returns>A list of SQL databases on the specified server</returns>
    /// <exception cref="ArgumentException">Thrown when required parameters are null or empty</exception>
    public async Task<List<SqlDatabase>> ListDatabasesAsync(
        string serverName,
        string resourceGroup,
        string subscription,
        RetryPolicyOptions? retryPolicy,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await ExecuteResourceQueryAsync(
                "Microsoft.Sql/servers/databases",
                resourceGroup,
                subscription,
                retryPolicy,
                ConvertToSqlDatabaseModel,
                cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error listing SQL databases. Server: {Server}, ResourceGroup: {ResourceGroup}, Subscription: {Subscription}",
                serverName, resourceGroup, subscription);
            throw;
        }
    }

    /// <summary>
    /// Retrieves a list of Microsoft Entra ID (formerly Azure AD) administrators for an Azure SQL Server.
    /// These administrators can authenticate to the SQL server using their Entra ID credentials.
    /// </summary>
    /// <param name="serverName">The name of the SQL server to get administrators for</param>
    /// <param name="resourceGroup">The name of the resource group containing the server</param>
    /// <param name="subscription">The subscription ID or name</param>
    /// <param name="retryPolicy">Optional retry policy configuration for resilient operations</param>
    /// <param name="cancellationToken">Token to observe for cancellation requests</param>
    /// <returns>A list of Entra ID administrators configured for the SQL server</returns>
    /// <exception cref="ArgumentException">Thrown when required parameters are null or empty</exception>
    public async Task<List<SqlServerEntraAdministrator>> GetEntraAdministratorsAsync(
        string serverName,
        string resourceGroup,
        string subscription,
        RetryPolicyOptions? retryPolicy,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await ExecuteResourceQueryAsync(
                "Microsoft.Sql/servers/administrators",
                resourceGroup,
                subscription,
                retryPolicy,
                ConvertToSqlServerEntraAdministratorModel,
                $"id contains '/servers/{EscapeKqlString(serverName)}/'",
                50,
                cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error getting SQL server Entra ID administrators. Server: {Server}, ResourceGroup: {ResourceGroup}, Subscription: {Subscription}",
                serverName, resourceGroup, subscription);
            throw;
        }
    }

    /// <summary>
    /// Retrieves a list of elastic pools from an Azure SQL Server.
    /// Elastic pools provide a cost-effective solution for managing multiple databases with varying usage patterns.
    /// </summary>
    /// <param name="serverName">The name of the SQL server to get elastic pools from</param>
    /// <param name="resourceGroup">The name of the resource group containing the server</param>
    /// <param name="subscription">The subscription ID or name</param>
    /// <param name="retryPolicy">Optional retry policy configuration for resilient operations</param>
    /// <param name="cancellationToken">Token to observe for cancellation requests</param>
    /// <returns>A list of elastic pools configured on the SQL server</returns>
    /// <exception cref="ArgumentException">Thrown when required parameters are null or empty</exception>
    public async Task<List<SqlElasticPool>> GetElasticPoolsAsync(
        string serverName,
        string resourceGroup,
        string subscription,
        RetryPolicyOptions? retryPolicy,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await ExecuteResourceQueryAsync(
                "Microsoft.Sql/servers/elasticPools",
                resourceGroup,
                subscription,
                retryPolicy,
                ConvertToSqlElasticPoolModel,
                cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error getting SQL elastic pools. Server: {Server}, ResourceGroup: {ResourceGroup}, Subscription: {Subscription}",
                serverName, resourceGroup, subscription);
            throw;
        }
    }

    /// <summary>
    /// Retrieves a list of firewall rules configured for an Azure SQL Server.
    /// Firewall rules control which IP addresses are allowed to connect to the SQL server.
    /// </summary>
    /// <param name="serverName">The name of the SQL server to get firewall rules for</param>
    /// <param name="resourceGroup">The name of the resource group containing the server</param>
    /// <param name="subscription">The subscription ID or name</param>
    /// <param name="retryPolicy">Optional retry policy configuration for resilient operations</param>
    /// <param name="cancellationToken">Token to observe for cancellation requests</param>
    /// <returns>A list of firewall rules configured on the SQL server</returns>
    /// <exception cref="ArgumentException">Thrown when required parameters are null or empty</exception>
    public async Task<List<SqlServerFirewallRule>> ListFirewallRulesAsync(
        string serverName,
        string resourceGroup,
        string subscription,
        RetryPolicyOptions? retryPolicy,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await ExecuteResourceQueryAsync(
                "Microsoft.Sql/servers/firewallRules",
                resourceGroup,
                subscription,
                retryPolicy,
                ConvertToSqlFirewallRuleModel,
                cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error getting SQL server firewall rules. Server: {Server}, ResourceGroup: {ResourceGroup}, Subscription: {Subscription}",
                serverName, resourceGroup, subscription);
            throw;
        }
    }

    private static SqlDatabase ConvertToSqlDatabaseModel(JsonElement item)
    {
        SqlDatabaseData? sqlDatabase = SqlDatabaseData.FromJson(item);
        if (sqlDatabase == null)
            throw new InvalidOperationException("Failed to parse SQL database data");

        return new SqlDatabase(
                Name: sqlDatabase.ResourceName ?? "Unknown",
                Id: sqlDatabase.ResourceId ?? "Unknown",
                Type: sqlDatabase.ResourceType ?? "Unknown",
                Location: sqlDatabase.Location,
                Sku: sqlDatabase.Sku != null ? new DatabaseSku(
                    Name: sqlDatabase.Sku.Name,
                    Tier: sqlDatabase.Sku.Tier,
                    Capacity: sqlDatabase.Sku.Capacity,
                    Family: sqlDatabase.Sku.Family,
                    Size: sqlDatabase.Sku.Size
                ) : null,
                Status: sqlDatabase.Properties?.Status,
                Collation: sqlDatabase.Properties?.Collation,
                CreationDate: sqlDatabase.Properties?.CreatedOn,
                MaxSizeBytes: sqlDatabase.Properties?.MaxSizeBytes,
                ServiceLevelObjective: sqlDatabase.Properties?.CurrentServiceObjectiveName,
                Edition: sqlDatabase.Properties?.CurrentSku?.Name,
                ElasticPoolName: sqlDatabase.Properties?.ElasticPoolId?.ToString().Split('/').LastOrDefault(),
                EarliestRestoreDate: sqlDatabase.Properties?.EarliestRestoreOn,
                ReadScale: sqlDatabase.Properties?.ReadScale,
                ZoneRedundant: sqlDatabase.Properties?.IsZoneRedundant
            );
    }

    private static SqlServerEntraAdministrator ConvertToSqlServerEntraAdministratorModel(JsonElement item)
    {
        SqlServerAadAdministratorData? admin = SqlServerAadAdministratorData.FromJson(item);
        if (admin == null)
            throw new InvalidOperationException("Failed to parse SQL server AAD administrator data");

        return new SqlServerEntraAdministrator(
                    Name: admin.ResourceName ?? "Unknown",
                    Id: admin.ResourceId ?? "Unknown",
                    Type: admin.ResourceType ?? "Unknown",
                    AdministratorType: admin.Properties?.AdministratorType,
                    Login: admin.Properties?.Login,
                    Sid: admin.Properties?.Sid?.ToString(),
                    TenantId: admin.Properties?.TenantId?.ToString(),
                    AzureADOnlyAuthentication: admin.Properties?.IsAzureADOnlyAuthenticationEnabled
                );
    }

    private static SqlElasticPool ConvertToSqlElasticPoolModel(JsonElement item)
    {
        SqlElasticPoolData? elasticPool = SqlElasticPoolData.FromJson(item);
        if (elasticPool == null)
            throw new InvalidOperationException("Failed to parse SQL elastic pool data");

        return new SqlElasticPool(
                    Name: elasticPool.ResourceName ?? "Unknown",
                    Id: elasticPool.ResourceId ?? "Unknown",
                    Type: elasticPool.ResourceType ?? "Unknown",
                    Location: elasticPool.Location,
                    Sku: elasticPool.Sku != null ? new ElasticPoolSku(
                        Name: elasticPool.Sku.Name,
                        Tier: elasticPool.Sku.Tier,
                        Capacity: elasticPool.Sku.Capacity,
                        Family: elasticPool.Sku.Family,
                        Size: elasticPool.Sku.Size
                    ) : null,
                    State: elasticPool.Properties?.State,
                    CreationDate: elasticPool.Properties?.CreatedOn,
                    MaxSizeBytes: elasticPool.Properties?.MaxSizeBytes,
                    PerDatabaseSettings: elasticPool.Properties?.PerDatabaseSettings != null ? new ElasticPoolPerDatabaseSettings(
                        MinCapacity: elasticPool.Properties.PerDatabaseSettings.MinCapacity,
                        MaxCapacity: elasticPool.Properties.PerDatabaseSettings.MaxCapacity
                    ) : null,
                    ZoneRedundant: elasticPool.Properties?.IsZoneRedundant,
                    LicenseType: elasticPool.Properties?.LicenseType,
                    DatabaseDtuMin: null, // DTU properties not available in current SDK
                    DatabaseDtuMax: null,
                    Dtu: null,
                    StorageMB: null
                );
    }

    private static SqlServerFirewallRule ConvertToSqlFirewallRuleModel(JsonElement item)
    {
        SqlFirewallRuleData? firewallRule = SqlFirewallRuleData.FromJson(item);
        if (firewallRule == null)
            throw new InvalidOperationException("Failed to parse SQL firewall rule data");

        return new SqlServerFirewallRule(
            Name: firewallRule.ResourceName ?? "Unknown",
            Id: firewallRule.ResourceId ?? "Unknown",
            Type: firewallRule.ResourceType ?? "Unknown",
            StartIpAddress: firewallRule.Properties?.StartIPAddress,
            EndIpAddress: firewallRule.Properties?.EndIPAddress
        );
    }
}
