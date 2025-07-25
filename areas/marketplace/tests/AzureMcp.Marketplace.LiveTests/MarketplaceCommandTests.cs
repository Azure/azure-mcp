// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.Core.Services.Caching;
using AzureMcp.Marketplace.Services;
using AzureMcp.Tests;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Xunit;

namespace AzureMcp.Marketplace.LiveTests;

public class MarketplaceCommandTests : CommandTestsBase,
    IClassFixture<LiveTestFixture>
{
    private const string ProductKey = "product";
    private readonly MarketplaceService _marketplaceService;
    private readonly string _subscriptionId;

    public MarketplaceCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output) : base(liveTestFixture, output)
    {
        var memoryCache = new MemoryCache(Microsoft.Extensions.Options.Options.Create(new MemoryCacheOptions()));
        var cacheService = new CacheService(memoryCache);
        var tenantService = new TenantService(cacheService);
        var subscriptionService = new SubscriptionService(cacheService, tenantService);
        _marketplaceService = new MarketplaceService(tenantService);
        _subscriptionId = Settings.SubscriptionId;
    }

    [Fact]
    public async Task Should_get_marketplace_product()
    {
        // Arrange - DZH318Z0HWCB is a well-known Azure Marketplace product
        const string productId = "DZH318Z0HWCB";

        // Act
        var result = await CallToolAsync(
            "azmcp_marketplace_product_get",
            new()
            {
                { "subscription", _subscriptionId },
                { "product-id", productId }
            });

        // Assert
        var product = result.AssertProperty(ProductKey);
        Assert.Equal(JsonValueKind.Object, product.ValueKind);

        // Verify essential product properties
        var id = product.AssertProperty("id");
        Assert.Equal(JsonValueKind.String, id.ValueKind);
        Assert.Contains(productId, id.GetString());

        var displayName = product.AssertProperty("displayName");
        Assert.Equal(JsonValueKind.String, displayName.ValueKind);
        Assert.NotEmpty(displayName.GetString()!);

        // Verify properties specific to DZH318Z0HWCB
        var properties = product.AssertProperty("properties");
        Assert.Equal(JsonValueKind.Object, properties.ValueKind);

        var publisherId = properties.AssertProperty("publisherId");
        Assert.Equal(JsonValueKind.String, publisherId.ValueKind);
        Assert.Equal("Microsoft", publisherId.GetString());
    }

    [Fact]
    public async Task Should_get_marketplace_product_with_language_option()
    {
        // Arrange
        const string productId = "DZH318Z0HWCB";
        const string language = "en";

        // Act
        var result = await CallToolAsync(
            "azmcp_marketplace_product_get",
            new()
            {
                { "subscription", _subscriptionId },
                { "product-id", productId },
                { "language", language }
            });

        // Assert
        var product = result.AssertProperty(ProductKey);
        Assert.Equal(JsonValueKind.Object, product.ValueKind);

        var id = product.AssertProperty("id");
        Assert.Contains(productId, id.GetString());
    }

    [Fact]
    public async Task Should_get_marketplace_product_with_market_option()
    {
        // Arrange
        const string productId = "DZH318Z0HWCB";
        const string market = "US";

        // Act
        var result = await CallToolAsync(
            "azmcp_marketplace_product_get",
            new()
            {
                { "subscription", _subscriptionId },
                { "product-id", productId },
                { "market", market }
            });

        // Assert
        var product = result.AssertProperty(ProductKey);
        Assert.Equal(JsonValueKind.Object, product.ValueKind);

        var id = product.AssertProperty("id");
        Assert.Contains(productId, id.GetString());
    }

    [Fact]
    public async Task Should_get_marketplace_product_with_include_hidden_plans()
    {
        // Arrange
        const string productId = "DZH318Z0HWCB";

        // Act
        var result = await CallToolAsync(
            "azmcp_marketplace_product_get",
            new()
            {
                { "subscription", _subscriptionId },
                { "product-id", productId },
                { "include-hidden-plans", true }
            });

        // Assert
        var product = result.AssertProperty(ProductKey);
        Assert.Equal(JsonValueKind.Object, product.ValueKind);

        var id = product.AssertProperty("id");
        Assert.Contains(productId, id.GetString());

        // Verify that plans are included
        var properties = product.AssertProperty("properties");
        if (properties.TryGetProperty("plans", out var plans))
        {
            Assert.Equal(JsonValueKind.Array, plans.ValueKind);
        }
    }

    [Fact]
    public async Task Should_get_marketplace_product_with_service_instruction_templates()
    {
        // Arrange
        const string productId = "DZH318Z0HWCB";

        // Act
        var result = await CallToolAsync(
            "azmcp_marketplace_product_get",
            new()
            {
                { "subscription", _subscriptionId },
                { "product-id", productId },
                { "include-service-instruction-templates", true }
            });

        // Assert
        var product = result.AssertProperty(ProductKey);
        Assert.Equal(JsonValueKind.Object, product.ValueKind);

        var id = product.AssertProperty("id");
        Assert.Contains(productId, id.GetString());
    }

    [Fact]
    public async Task Should_handle_nonexistent_product_gracefully()
    {
        // Arrange - Using a non-existent product ID
        const string productId = "NonExistent.Product.12345";

        // Act & Assert
        var exception = await Assert.ThrowsAnyAsync<Exception>(async () =>
        {
            await CallToolAsync(
                "azmcp_marketplace_product_get",
                new()
                {
                    { "subscription", _subscriptionId },
                    { "product-id", productId }
                });
        });

        // The exception should indicate the product was not found
        Assert.NotNull(exception);
    }

    [Fact]
    public async Task Should_validate_required_subscription_parameter()
    {
        // Arrange
        const string productId = "DZH318Z0HWCB";

        // Act & Assert
        var exception = await Assert.ThrowsAnyAsync<Exception>(async () =>
        {
            await CallToolAsync(
                "azmcp_marketplace_product_get",
                new()
                {
                    { "product-id", productId }
                    // Missing subscription parameter
                });
        });

        Assert.NotNull(exception);
    }

    [Fact]
    public async Task Should_validate_required_product_id_parameter()
    {
        // Act & Assert
        var exception = await Assert.ThrowsAnyAsync<Exception>(async () =>
        {
            await CallToolAsync(
                "azmcp_marketplace_product_get",
                new()
                {
                    { "subscription", _subscriptionId }
                    // Missing product-id parameter
                });
        });

        Assert.NotNull(exception);
    }

    [Fact]
    public async Task Should_get_marketplace_product_with_multiple_options()
    {
        // Arrange
        const string productId = "DZH318Z0HWCB";
        const string language = "en";
        const string market = "US";

        // Act
        var result = await CallToolAsync(
            "azmcp_marketplace_product_get",
            new()
            {
                { "subscription", _subscriptionId },
                { "product-id", productId },
                { "language", language },
                { "market", market },
                { "include-hidden-plans", true },
                { "include-service-instruction-templates", true }
            });

        // Assert
        var product = result.AssertProperty(ProductKey);
        Assert.Equal(JsonValueKind.Object, product.ValueKind);

        var id = product.AssertProperty("id");
        Assert.Contains(productId, id.GetString());

        var displayName = product.AssertProperty("displayName");
        Assert.Equal(JsonValueKind.String, displayName.ValueKind);
        Assert.NotEmpty(displayName.GetString()!);

        // Verify properties
        var properties = product.AssertProperty("properties");
        Assert.Equal(JsonValueKind.Object, properties.ValueKind);

        var publisherId = properties.AssertProperty("publisherId");
        Assert.Equal("Microsoft", publisherId.GetString());
    }
}
