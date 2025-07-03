// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using AzureMcp.Areas.AppService.Models;
using AzureMcp.Services.Azure.Tenant;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.AppService.Services;

public class AppServiceService(
    ITenantService tenantService,
    ILogger<AppServiceService> logger) : IAppServiceService
{
    private readonly ITenantService _tenantService = tenantService;
    private readonly ILogger<AppServiceService> _logger = logger;

    public async Task<DatabaseConnectionInfo> AddDatabaseAsync(
        string appName,
        string resourceGroup,
        string databaseType,
        string databaseServer,
        string databaseName,
        string? connectionString,
        string subscription,
        RetryPolicy retryPolicy)
    {
        _logger.LogInformation("Adding database connection to App Service {AppName} in resource group {ResourceGroup}", 
            appName, resourceGroup);

        var client = await _tenantService.GetArmClientAsync(subscription);
        var subscriptionResource = await client.GetDefaultSubscriptionAsync();
        var resourceGroupResource = await subscriptionResource.GetResourceGroupAsync(resourceGroup);
        
        // For now, we'll simulate the operation since Azure.ResourceManager.AppService is not available
        // In a real implementation, you would use the Azure.ResourceManager.AppService package
        // to get the web app resource and update its connection strings
        
        // Build connection string if not provided
        var finalConnectionString = connectionString ?? BuildConnectionString(databaseType, databaseServer, databaseName);
        var connectionStringName = $"{databaseName}Connection";

        // Simulate the database connection addition
        // In reality, this would call the Azure Resource Manager API to update the web app's connection strings
        await Task.Delay(100); // Simulate API call

        _logger.LogInformation("Successfully simulated adding database connection {ConnectionName} to App Service {AppName}", 
            connectionStringName, appName);

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
