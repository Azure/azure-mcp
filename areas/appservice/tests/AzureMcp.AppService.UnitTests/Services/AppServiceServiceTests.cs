// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using AzureMcp.AppService.Models;
using AzureMcp.AppService.Services;
using AzureMcp.Core.Options;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.AppService.UnitTests.Services;

[Trait("Area", "AppService")]
[Trait("Service", "AppServiceService")]
public class AppServiceServiceTests
{
    private readonly ISubscriptionService _subscriptionService;
    private readonly ITenantService _tenantService;
    private readonly ILogger<AppServiceService> _logger;
    private readonly AppServiceService _service;

    public AppServiceServiceTests()
    {
        _subscriptionService = Substitute.For<ISubscriptionService>();
        _tenantService = Substitute.For<ITenantService>();
        _logger = Substitute.For<ILogger<AppServiceService>>();
        _service = new AppServiceService(_subscriptionService, _tenantService, _logger);
    }

    [Fact]
    public async Task AddDatabaseAsync_WithValidParameters_ReturnsExpectedResult()
    {
        // Arrange
        var appName = "test-app";
        var resourceGroup = "test-rg";
        var databaseType = "SqlServer";
        var databaseServer = "test-server.database.windows.net";
        var databaseName = "test-db";
        var connectionString = "";
        var subscription = "test-subscription";

        var mockSubscription = Substitute.For<SubscriptionResource>();
        var mockResourceGroup = Substitute.For<ResourceGroupResource>();
        var response = Response.FromValue(mockResourceGroup, Substitute.For<Response>());

        _subscriptionService.GetSubscription(subscription, null, null)
            .Returns(mockSubscription);
        mockSubscription.GetResourceGroupAsync(resourceGroup, TestContext.Current.CancellationToken)
            .Returns(response);

        // Act
        var result = await _service.AddDatabaseAsync(
            appName, resourceGroup, databaseType, databaseServer, databaseName,
            connectionString, subscription);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(databaseType, result.DatabaseType);
        Assert.Equal(databaseServer, result.DatabaseServer);
        Assert.Equal(databaseName, result.DatabaseName);
        Assert.Equal($"{databaseName}Connection", result.ConnectionStringName);
        Assert.True(result.IsConfigured);
        Assert.Contains("Server=test-server.database.windows.net", result.ConnectionString);
        Assert.Contains("Database=test-db", result.ConnectionString);
    }

    [Fact]
    public async Task AddDatabaseAsync_WithCustomConnectionString_UsesProvidedConnectionString()
    {
        // Arrange
        var appName = "test-app";
        var resourceGroup = "test-rg";
        var databaseType = "SqlServer";
        var databaseServer = "test-server.database.windows.net";
        var databaseName = "test-db";
        var customConnectionString = "Server=custom-server;Database=custom-db;UserId=user;Password=pass;";
        var subscription = "test-subscription";

        var mockSubscription = Substitute.For<SubscriptionResource>();
        var mockResourceGroup = Substitute.For<ResourceGroupResource>();
        var response = Response.FromValue(mockResourceGroup, Substitute.For<Response>());

        _subscriptionService.GetSubscription(subscription, null, null)
            .Returns(mockSubscription);
        mockSubscription.GetResourceGroupAsync(resourceGroup, TestContext.Current.CancellationToken)
            .Returns(response);

        // Act
        var result = await _service.AddDatabaseAsync(
            appName, resourceGroup, databaseType, databaseServer, databaseName,
            customConnectionString, subscription);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customConnectionString, result.ConnectionString);
    }

    [Fact]
    public async Task AddDatabaseAsync_WithTenant_PassesTenantToSubscriptionService()
    {
        // Arrange
        var appName = "test-app";
        var resourceGroup = "test-rg";
        var databaseType = "SqlServer";
        var databaseServer = "test-server.database.windows.net";
        var databaseName = "test-db";
        var connectionString = "";
        var subscription = "test-subscription";
        var tenant = "test-tenant";

        var mockSubscription = Substitute.For<SubscriptionResource>();
        var mockResourceGroup = Substitute.For<ResourceGroupResource>();
        var response = Response.FromValue(mockResourceGroup, Substitute.For<Response>());

        _subscriptionService.GetSubscription(subscription, tenant, null)
            .Returns(mockSubscription);
        mockSubscription.GetResourceGroupAsync(resourceGroup, TestContext.Current.CancellationToken)
            .Returns(response);

        // Act
        var result = await _service.AddDatabaseAsync(
            appName, resourceGroup, databaseType, databaseServer, databaseName,
            connectionString, subscription, tenant);

        // Assert
        Assert.NotNull(result);
        await _subscriptionService.Received(1).GetSubscription(subscription, tenant, null);
    }

    [Fact]
    public async Task AddDatabaseAsync_WithRetryPolicy_PassesRetryPolicyToSubscriptionService()
    {
        // Arrange
        var appName = "test-app";
        var resourceGroup = "test-rg";
        var databaseType = "SqlServer";
        var databaseServer = "test-server.database.windows.net";
        var databaseName = "test-db";
        var connectionString = "";
        var subscription = "test-subscription";
        var retryPolicy = new RetryPolicyOptions { MaxRetries = 3 };

        var mockSubscription = Substitute.For<SubscriptionResource>();
        var mockResourceGroup = Substitute.For<ResourceGroupResource>();
        var response = Response.FromValue(mockResourceGroup, Substitute.For<Response>());

        _subscriptionService.GetSubscription(subscription, null, retryPolicy)
            .Returns(mockSubscription);
        mockSubscription.GetResourceGroupAsync(resourceGroup, TestContext.Current.CancellationToken)
            .Returns(response);

        // Act
        var result = await _service.AddDatabaseAsync(
            appName, resourceGroup, databaseType, databaseServer, databaseName,
            connectionString, subscription, null, retryPolicy);

        // Assert
        Assert.NotNull(result);
        await _subscriptionService.Received(1).GetSubscription(subscription, null, retryPolicy);
    }

    [Fact]
    public async Task AddDatabaseAsync_ResourceGroupNotFound_ThrowsArgumentException()
    {
        // Arrange
        var appName = "test-app";
        var resourceGroup = "nonexistent-rg";
        var databaseType = "SqlServer";
        var databaseServer = "test-server.database.windows.net";
        var databaseName = "test-db";
        var connectionString = "";
        var subscription = "test-subscription";

        var mockSubscription = Substitute.For<SubscriptionResource>();
        mockSubscription.GetResourceGroupAsync(Arg.Is(resourceGroup), Arg.Any<CancellationToken>())
            .Returns((Response<ResourceGroupResource>)null!);

        _subscriptionService.GetSubscription(subscription, null, null)
            .Returns(mockSubscription);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            _service.AddDatabaseAsync(
                appName, resourceGroup, databaseType, databaseServer, databaseName,
                connectionString, subscription));

        Assert.Contains("Resource group", exception.Message);
        Assert.Contains("not found", exception.Message);
    }

    [Theory]
    [InlineData("sqlserver", "Server=test-server;Database=test-db;Trusted_Connection=True;TrustServerCertificate=True;")]
    [InlineData("mysql", "Server=test-server;Database=test-db;Uid={username};Pwd={password};")]
    [InlineData("postgresql", "Host=test-server;Database=test-db;Username={username};Password={password};")]
    [InlineData("cosmosdb", "AccountEndpoint=https://test-server.documents.azure.com:443/;AccountKey={key};Database=test-db;")]
    public async Task AddDatabaseAsync_WithDifferentDatabaseTypes_GeneratesCorrectConnectionString(
        string databaseType, string expectedConnectionStringPattern)
    {
        // Arrange
        var appName = "test-app";
        var resourceGroup = "test-rg";
        var databaseServer = "test-server";
        var databaseName = "test-db";
        var connectionString = "";
        var subscription = "test-subscription";

        var mockSubscription = Substitute.For<SubscriptionResource>();
        var mockResourceGroup = Substitute.For<ResourceGroupResource>();
        var response = Response.FromValue(mockResourceGroup, Substitute.For<Response>());

        _subscriptionService.GetSubscription(subscription, null, null)
            .Returns(mockSubscription);
        mockSubscription.GetResourceGroupAsync(resourceGroup, TestContext.Current.CancellationToken)
            .Returns(response);

        // Act
        var result = await _service.AddDatabaseAsync(
            appName, resourceGroup, databaseType, databaseServer, databaseName,
            connectionString, subscription);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedConnectionStringPattern, result.ConnectionString);
    }

    [Fact]
    public async Task AddDatabaseAsync_WithUnsupportedDatabaseType_ThrowsArgumentException()
    {
        // Arrange
        var appName = "test-app";
        var resourceGroup = "test-rg";
        var databaseType = "UnsupportedType";
        var databaseServer = "test-server";
        var databaseName = "test-db";
        var connectionString = "";
        var subscription = "test-subscription";

        var mockSubscription = Substitute.For<SubscriptionResource>();
        var mockResourceGroup = Substitute.For<ResourceGroupResource>();
        var response = Response.FromValue(mockResourceGroup, Substitute.For<Response>());

        _subscriptionService.GetSubscription(subscription, null, null)
            .Returns(mockSubscription);
        mockSubscription.GetResourceGroupAsync(resourceGroup, TestContext.Current.CancellationToken)
            .Returns(response);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            _service.AddDatabaseAsync(
                appName, resourceGroup, databaseType, databaseServer, databaseName,
                connectionString, subscription));

        Assert.Contains("Unsupported database type", exception.Message);
    }
}
