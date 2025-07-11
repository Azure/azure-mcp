// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.Sql;
using AzureMcp.Areas.Sql.Models;
using AzureMcp.Options;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Azure.Subscription;
using AzureMcp.Services.Azure.Tenant;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Sql.Services;

public class SqlService(ISubscriptionService subscriptionService, ITenantService tenantService, ILogger<SqlService> logger) : BaseAzureService(tenantService), ISqlService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private readonly ILogger<SqlService> _logger = logger;

    public async Task<SqlDatabase?> GetDatabaseAsync(
        string serverName,
        string databaseName,
        string resourceGroup,
        string subscription,
        RetryPolicyOptions? retryPolicy,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, null, retryPolicy);

            var resourceGroupResource = await subscriptionResource
                .GetResourceGroupAsync(resourceGroup, cancellationToken);

            var sqlServerResource = await resourceGroupResource.Value
                .GetSqlServers()
                .GetAsync(serverName);

            var databaseResource = await sqlServerResource.Value
                .GetSqlDatabases()
                .GetAsync(databaseName);

            var database = databaseResource.Value.Data;

            return new SqlDatabase(
                Name: database.Name,
                Id: database.Id.ToString(),
                Type: database.ResourceType.ToString(),
                Location: database.Location.ToString(),
                Sku: database.Sku != null ? new DatabaseSku(
                    Name: database.Sku.Name,
                    Tier: database.Sku.Tier,
                    Capacity: database.Sku.Capacity,
                    Family: database.Sku.Family,
                    Size: database.Sku.Size
                ) : null,
                Status: database.Status?.ToString(),
                Collation: database.Collation,
                CreationDate: database.CreatedOn,
                MaxSizeBytes: database.MaxSizeBytes,
                ServiceLevelObjective: database.CurrentServiceObjectiveName,
                Edition: database.CurrentSku?.Name,
                ElasticPoolName: database.ElasticPoolId?.ToString().Split('/').LastOrDefault(),
                EarliestRestoreDate: database.EarliestRestoreOn,
                ReadScale: database.ReadScale?.ToString(),
                ZoneRedundant: database.IsZoneRedundant
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error getting SQL database. Server: {Server}, Database: {Database}, ResourceGroup: {ResourceGroup}, Subscription: {Subscription}",
                serverName, databaseName, resourceGroup, subscription);
            throw;
        }
    }

    public async Task<List<SqlServerEntraAdministrator>> GetEntraAdministratorsAsync(
        string serverName,
        string resourceGroup,
        string subscription,
        RetryPolicyOptions? retryPolicy,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, null, retryPolicy);

            var resourceGroupResource = await subscriptionResource
                .GetResourceGroupAsync(resourceGroup, cancellationToken);

            var sqlServerResource = await resourceGroupResource.Value
                .GetSqlServers()
                .GetAsync(serverName);

            var entraAdministrators = new List<SqlServerEntraAdministrator>();

            await foreach (var adminResource in sqlServerResource.Value.GetSqlServerAzureADAdministrators().GetAllAsync(cancellationToken))
            {
                var admin = adminResource.Data;
                entraAdministrators.Add(new SqlServerEntraAdministrator(
                    Name: admin.Name,
                    Id: admin.Id.ToString(),
                    Type: admin.ResourceType.ToString(),
                    AdministratorType: admin.AdministratorType?.ToString(),
                    Login: admin.Login,
                    Sid: admin.Sid?.ToString(),
                    TenantId: admin.TenantId?.ToString(),
                    AzureADOnlyAuthentication: admin.IsAzureADOnlyAuthenticationEnabled
                ));
            }

            return entraAdministrators;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error getting SQL server Entra ID administrators. Server: {Server}, ResourceGroup: {ResourceGroup}, Subscription: {Subscription}",
                serverName, resourceGroup, subscription);
            throw;
        }
    }
}
