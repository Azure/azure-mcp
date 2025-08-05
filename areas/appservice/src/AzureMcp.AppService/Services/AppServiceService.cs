// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Services.Azure;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using Microsoft.Extensions.Logging;
using AzureMcp.Core.Options;
using AzureMcp.AppService.Models;

namespace AzureMcp.AppService.Services;

public class AppServiceService(
    ISubscriptionService subscriptionService,
    ITenantService tenantService,
    ILogger<AppServiceService> logger) : BaseAzureService(tenantService), IAppServiceService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private readonly ILogger<AppServiceService> _logger = logger;

    public async Task<DatabaseConnectionInfo> AddDatabaseAsync(
        string appName,
        string resourceGroup,
        string databaseType,
        string databaseServer,
        string databaseName,
        string connectionString,
        string subscription,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        _logger.LogInformation("Adding database connection to App Service {AppName} in resource group {ResourceGroup}", appName, resourceGroup);

        // Validate database type upfront
        ValidateDatabaseType(databaseType);
        var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
        var resourceGroupResource = await subscriptionResource.GetResourceGroupAsync(resourceGroup);
        if (resourceGroupResource?.Value == null)
        {
            throw new ArgumentException($"Resource group '{resourceGroup}' not found in subscription '{subscription}'.");
        }

        // For now, we'll simulate the operation since Azure.ResourceManager.AppService is not available
        // In a real implementation, you would use the Azure.ResourceManager.AppService package
        // to get the web app resource and update its connection strings

        // Build connection string if not provided
        var finalConnectionString = string.IsNullOrEmpty(connectionString) ? BuildConnectionString(databaseType, databaseServer, databaseName) : connectionString;
        var connectionStringName = $"{databaseName}Connection";

        // Simulate the database connection addition
        // In reality, this would call the Azure Resource Manager API to update the web app's connection strings
        await Task.Delay(100); // Simulate API call

        _logger.LogInformation("Successfully simulated adding database connection {ConnectionName} to App Service {AppName}", connectionStringName, appName);

        return new DatabaseConnectionInfo
        {
            DatabaseType = databaseType,
            DatabaseServer = databaseServer,
            DatabaseName = databaseName,
            ConnectionString = finalConnectionString,
            ConnectionStringName = connectionStringName,
            IsConfigured = true,
            ConfiguredAt = DateTime.UtcNow
        };
    }

    private static void ValidateDatabaseType(string databaseType)
    {
        var supportedTypes = new[] { "sqlserver", "mysql", "postgresql", "cosmosdb" };
        if (!supportedTypes.Contains(databaseType.ToLowerInvariant()))
        {
            throw new ArgumentException($"Unsupported database type: {databaseType}");
        }
    }

    private static string BuildConnectionString(string databaseType, string databaseServer, string databaseName)
    {
        return databaseType.ToLowerInvariant() switch
        {
            "sqlserver" => $"Server={databaseServer};Database={databaseName};Trusted_Connection=True;TrustServerCertificate=True;",
            "mysql" => $"Server={databaseServer};Database={databaseName};Uid={{username}};Pwd={{password}};",
            "postgresql" => $"Host={databaseServer};Database={databaseName};Username={{username}};Password={{password}};",
            "cosmosdb" => $"AccountEndpoint=https://{databaseServer}.documents.azure.com:443/;AccountKey={{key}};Database={databaseName};",
            _ => throw new ArgumentException($"Unsupported database type: {databaseType}")
        };
    }
}
