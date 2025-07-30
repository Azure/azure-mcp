// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Areas.AppService.LiveTests;

[Trait("Area", "AppService")]
public class AppServiceCommandTests : CommandTestsBase, IClassFixture<LiveTestFixture>
{
    private readonly LiveTestFixture _liveTestFixture;

    public AppServiceCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
        : base(liveTestFixture, output)
    {
        _liveTestFixture = liveTestFixture;
    }
    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_add_database_connection_to_appservice()
    {
        // Skip test if settings file is not available
        if (!_liveTestFixture.IsSettingsAvailable)
        {
            Assert.Skip("Test skipped because .testsettings.json file is not available.");
        }
        
        // Skip test if required resources are not available
        if (string.IsNullOrEmpty(Settings.ResourceGroupName) || 
            string.IsNullOrEmpty(Settings.SubscriptionId))
        {
            Assert.Skip("Test skipped because required Azure resources are not configured.");
        }

        var appServiceName = $"{Settings.ResourceBaseName}-app";
        var databaseServer = $"{Settings.ResourceBaseName}-db-server";
        var databaseName = $"{Settings.ResourceBaseName}-db";

        var result = await CallToolAsync(
            "azmcp-appservice-database-add",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "app-name", appServiceName },
                { "database-type", "SqlServer" },
                { "database-server", databaseServer },
                { "database-name", databaseName }
            });

        // Assert the operation completed successfully
        var status = result.AssertProperty("status");
        Assert.Equal(JsonValueKind.Number, status.ValueKind);
        
        var connectionInfo = result.AssertProperty("connectionInfo");
        Assert.Equal(JsonValueKind.Object, connectionInfo.ValueKind);
        
        var serverName = connectionInfo.AssertProperty("serverName");
        Assert.Equal(databaseServer, serverName.GetString());
        
        var dbName = connectionInfo.AssertProperty("databaseName");
        Assert.Equal(databaseName, dbName.GetString());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_add_postgresql_database_connection_with_custom_string()
    {
        // Skip test if settings file is not available
        if (!_liveTestFixture.IsSettingsAvailable)
        {
            Assert.Skip("Test skipped because .testsettings.json file is not available.");
        }
        
        // Skip test if required resources are not available
        if (string.IsNullOrEmpty(Settings.ResourceGroupName) || 
            string.IsNullOrEmpty(Settings.SubscriptionId))
        {
            Assert.Skip("Test skipped because required Azure resources are not configured.");
        }

        var appServiceName = $"{Settings.ResourceBaseName}-app";
        var postgresServer = $"{Settings.ResourceBaseName}-postgres";
        var databaseName = $"{Settings.ResourceBaseName}-pgdb";
        var customConnectionString = $"Server={postgresServer}.postgres.database.azure.com;Database={databaseName};Port=5432;";

        var result = await CallToolAsync(
            "azmcp-appservice-database-add",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "app-name", appServiceName },
                { "database-type", "PostgreSql" },
                { "database-server", postgresServer },
                { "database-name", databaseName },
                { "connection-string", customConnectionString }
            });

        // Assert the operation completed successfully
        var status = result.AssertProperty("status");
        Assert.Equal(JsonValueKind.Number, status.ValueKind);
        
        var connectionInfo = result.AssertProperty("connectionInfo");
        Assert.Equal(JsonValueKind.Object, connectionInfo.ValueKind);
        
        var connectionString = connectionInfo.AssertProperty("connectionString");
        Assert.Equal(customConnectionString, connectionString.GetString());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_add_mysql_database_connection()
    {
        // Skip test if settings file is not available
        if (!_liveTestFixture.IsSettingsAvailable)
        {
            Assert.Skip("Test skipped because .testsettings.json file is not available.");
        }
        
        // Skip test if required resources are not available
        if (string.IsNullOrEmpty(Settings.ResourceGroupName) || 
            string.IsNullOrEmpty(Settings.SubscriptionId))
        {
            Assert.Skip("Test skipped because required Azure resources are not configured.");
        }

        var appServiceName = $"{Settings.ResourceBaseName}-app";
        var mysqlServer = $"{Settings.ResourceBaseName}-mysql";
        var databaseName = $"{Settings.ResourceBaseName}-mydb";

        var result = await CallToolAsync(
            "azmcp-appservice-database-add",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "app-name", appServiceName },
                { "database-type", "MySql" },
                { "database-server", mysqlServer },
                { "database-name", databaseName }
            });

        // Assert the operation completed successfully
        var status = result.AssertProperty("status");
        Assert.Equal(JsonValueKind.Number, status.ValueKind);
        
        var connectionInfo = result.AssertProperty("connectionInfo");
        Assert.Equal(JsonValueKind.Object, connectionInfo.ValueKind);
        
        var databaseType = connectionInfo.AssertProperty("databaseType");
        Assert.Equal("MySql", databaseType.GetString());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_add_cosmosdb_database_connection()
    {
        // Skip test if settings file is not available
        if (!_liveTestFixture.IsSettingsAvailable)
        {
            Assert.Skip("Test skipped because .testsettings.json file is not available.");
        }
        
        // Skip test if required resources are not available
        if (string.IsNullOrEmpty(Settings.ResourceGroupName) || 
            string.IsNullOrEmpty(Settings.SubscriptionId))
        {
            Assert.Skip("Test skipped because required Azure resources are not configured.");
        }

        var appServiceName = $"{Settings.ResourceBaseName}-app";
        var cosmosAccount = $"{Settings.ResourceBaseName}-cosmos";
        var databaseName = $"{Settings.ResourceBaseName}-cosmosdb";

        var result = await CallToolAsync(
            "azmcp-appservice-database-add",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "app-name", appServiceName },
                { "database-type", "CosmosDb" },
                { "database-server", cosmosAccount },
                { "database-name", databaseName }
            });

        // Assert the operation completed successfully
        var status = result.AssertProperty("status");
        Assert.Equal(JsonValueKind.Number, status.ValueKind);
        
        var connectionInfo = result.AssertProperty("connectionInfo");
        Assert.Equal(JsonValueKind.Object, connectionInfo.ValueKind);
        
        var databaseType = connectionInfo.AssertProperty("databaseType");
        Assert.Equal("CosmosDb", databaseType.GetString());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_handle_nonexistent_app_service_gracefully()
    {
        // Skip test if settings file is not available
        if (!_liveTestFixture.IsSettingsAvailable)
        {
            Assert.Skip("Test skipped because .testsettings.json file is not available.");
        }
        
        // This test verifies error handling for non-existent resources
        var nonExistentApp = "non-existent-app-12345";
        var databaseServer = "test-server";
        var databaseName = "test-db";

        var result = await CallToolAsync(
            "azmcp-appservice-database-add",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "app-name", nonExistentApp },
                { "database-type", "SqlServer" },
                { "database-server", databaseServer },
                { "database-name", databaseName }
            });

        // Should return an error status
        var status = result.AssertProperty("status");
        Assert.Equal(JsonValueKind.Number, status.ValueKind);
        Assert.True(status.GetInt32() >= 400, "Should return an error status code");
        
        var message = result.AssertProperty("message");
        Assert.Equal(JsonValueKind.String, message.ValueKind);
        Assert.Contains("not found", message.GetString(), StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_validate_required_parameters()
    {
        // Skip test if settings file is not available
        if (!_liveTestFixture.IsSettingsAvailable)
        {
            Assert.Skip("Test skipped because .testsettings.json file is not available.");
        }
        
        // Test that missing required parameters are properly validated
        var result = await CallToolAsync(
            "azmcp-appservice-database-add",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName }
                // Missing app-name, database-type, database-server, database-name
            });

        // Should return a validation error
        var status = result.AssertProperty("status");
        Assert.Equal(JsonValueKind.Number, status.ValueKind);
        Assert.True(status.GetInt32() >= 400, "Should return a validation error status code");
        
        var message = result.AssertProperty("message");
        Assert.Equal(JsonValueKind.String, message.ValueKind);
        Assert.Contains("required", message.GetString(), StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_validate_database_type_parameter()
    {
        // Skip test if settings file is not available
        if (!_liveTestFixture.IsSettingsAvailable)
        {
            Assert.Skip("Test skipped because .testsettings.json file is not available.");
        }
        
        // Test that invalid database type is properly validated
        var appServiceName = $"{Settings.ResourceBaseName}-app";
        
        var result = await CallToolAsync(
            "azmcp-appservice-database-add",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "app-name", appServiceName },
                { "database-type", "InvalidDatabaseType" },
                { "database-server", "test-server" },
                { "database-name", "test-db" }
            });

        // Should return a validation error
        var status = result.AssertProperty("status");
        Assert.Equal(JsonValueKind.Number, status.ValueKind);
        Assert.True(status.GetInt32() >= 400, "Should return a validation error status code");
        
        var message = result.AssertProperty("message");
        Assert.Equal(JsonValueKind.String, message.ValueKind);
        Assert.Contains("database-type", message.GetString(), StringComparison.OrdinalIgnoreCase);
    }
}
