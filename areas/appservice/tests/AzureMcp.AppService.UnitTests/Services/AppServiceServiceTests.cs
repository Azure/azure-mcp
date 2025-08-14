// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using Azure.ResourceManager.AppService;
using Azure.ResourceManager.AppService.Models;
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

        // Mock the resource group not found to avoid actual Azure calls
        var mockSubscription = Substitute.For<SubscriptionResource>();
        var nullResponse = Substitute.For<Response<ResourceGroupResource>>();
        nullResponse.Value.Returns((ResourceGroupResource)null!);
        
        mockSubscription.GetResourceGroupAsync(resourceGroup, TestContext.Current.CancellationToken)
            .Returns(Task.FromResult(nullResponse));

        _subscriptionService.GetSubscription(subscription, tenant, null)
            .Returns(mockSubscription);

        // Act & Assert - This will throw because resource group is not found
        // but we can verify the tenant parameter was passed correctly
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _service.AddDatabaseAsync(
                appName, resourceGroup, databaseType, databaseServer, databaseName,
                connectionString, subscription, tenant));

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

        // Mock the resource group not found to avoid actual Azure calls
        var mockSubscription = Substitute.For<SubscriptionResource>();
        var nullResponse = Substitute.For<Response<ResourceGroupResource>>();
        nullResponse.Value.Returns((ResourceGroupResource)null!);
        
        mockSubscription.GetResourceGroupAsync(resourceGroup, TestContext.Current.CancellationToken)
            .Returns(Task.FromResult(nullResponse));

        _subscriptionService.GetSubscription(subscription, null, retryPolicy)
            .Returns(mockSubscription);

        // Act & Assert - This will throw because resource group is not found
        // but we can verify the retry policy parameter was passed correctly
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _service.AddDatabaseAsync(
                appName, resourceGroup, databaseType, databaseServer, databaseName,
                connectionString, subscription, null, retryPolicy));

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
        var nullResponse = Substitute.For<Response<ResourceGroupResource>>();
        nullResponse.Value.Returns((ResourceGroupResource)null!);
        
        mockSubscription.GetResourceGroupAsync(Arg.Is(resourceGroup), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(nullResponse));

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
    [InlineData("SqlServer")]
    [InlineData("MySQL")]
    [InlineData("PostgreSQL")]
    [InlineData("CosmosDB")]
    public async Task AddDatabaseAsync_WithValidDatabaseTypes_DoesNotThrowDatabaseTypeError(string databaseType)
    {
        // This test verifies that valid database types don't cause database type validation errors.
        // We expect the service to fail due to resource group not found, but NOT due to invalid database type.
        
        // Arrange
        var mockSubscription = Substitute.For<SubscriptionResource>();
        var nullResponse = Substitute.For<Response<ResourceGroupResource>>();
        nullResponse.Value.Returns((ResourceGroupResource)null!);
        
        mockSubscription.GetResourceGroupAsync("test-rg", TestContext.Current.CancellationToken)
            .Returns(Task.FromResult(nullResponse));

        _subscriptionService.GetSubscription("test-subscription", null, null)
            .Returns(mockSubscription);
        
        // Act & Assert - Should throw ArgumentException about resource group, not database type
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => 
            _service.AddDatabaseAsync(
                "test-app", "test-rg", databaseType, "test-server", "test-db", 
                "", "test-subscription"));

        // The exception should be about configuration (since we're getting further), not about unsupported database type
        Assert.DoesNotContain("Unsupported database type", exception.Message);
        // We should reach past the database type validation and fail at a later stage
        Assert.True(exception.Message.Contains("Resource group") || exception.Message.Contains("Web app") || exception.Message.Contains("configuration"),
            $"Expected exception about resource/app/config access, but got: {exception.Message}");
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
        var resourceGroupResponse = Response.FromValue(mockResourceGroup, Substitute.For<Response>());
        
        _subscriptionService.GetSubscription(subscription, null, null)
            .Returns(mockSubscription);
        mockSubscription.GetResourceGroupAsync(resourceGroup, TestContext.Current.CancellationToken)
            .Returns(resourceGroupResponse);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            _service.AddDatabaseAsync(
                appName, resourceGroup, databaseType, databaseServer, databaseName,
                connectionString, subscription));

        Assert.Contains("Unsupported database type", exception.Message);
    }
}
