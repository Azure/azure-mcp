// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.AppService.Models;
using AzureMcp.Core.Options;
using AzureMcp.Core.Services.Azure;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using Azure.ResourceManager.AppService;
using Azure.ResourceManager.AppService.Models;
using Microsoft.Extensions.Logging;

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

        // Get the web app resource using Azure.ResourceManager.AppService
        var webApps = resourceGroupResource.Value.GetWebSites();
        var webAppResource = await webApps.GetAsync(appName);
        if (webAppResource?.Value == null)
        {
            throw new ArgumentException($"Web app '{appName}' not found in resource group '{resourceGroup}'.");
        }

        // Build connection string if not provided
        var finalConnectionString = string.IsNullOrEmpty(connectionString) ? BuildConnectionString(databaseType, databaseServer, databaseName) : connectionString;
        var connectionStringName = $"{databaseName}Connection";

        // Get current web app configuration
        var webApp = webAppResource.Value;
        var configResource = webApp.GetWebSiteConfig();
        var config = await configResource.GetAsync();
        
        // Create or update the connection string
        var connectionStrings = config.Value.Data.ConnectionStrings?.ToList() ?? new List<ConnStringInfo>();
        
        // Remove existing connection string with the same name if it exists
        connectionStrings.RemoveAll(cs => cs.Name?.Equals(connectionStringName, StringComparison.OrdinalIgnoreCase) == true);
        
        // Add the new connection string
        var connectionStringType = GetConnectionStringType(databaseType);
        connectionStrings.Add(new ConnStringInfo
        {
            Name = connectionStringName,
            ConnectionString = finalConnectionString,
            ConnectionStringType = connectionStringType
        });

        // Update the web app configuration
        var configData = config.Value.Data;
        configData.ConnectionStrings = connectionStrings;
        
        await configResource.CreateOrUpdateAsync(Azure.WaitUntil.Completed, configData);

        _logger.LogInformation("Successfully added database connection {ConnectionName} to App Service {AppName}", connectionStringName, appName);

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

    private static ConnectionStringType GetConnectionStringType(string databaseType)
    {
        return databaseType.ToLowerInvariant() switch
        {
            "sqlserver" => ConnectionStringType.SqlServer,
            "mysql" => ConnectionStringType.MySql,
            "postgresql" => ConnectionStringType.PostgreSql,
            "cosmosdb" => ConnectionStringType.Custom,
            _ => throw new ArgumentException($"Unsupported database type: {databaseType}")
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
