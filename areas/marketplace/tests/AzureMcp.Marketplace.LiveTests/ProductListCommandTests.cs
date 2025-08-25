// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.Core.Services.Caching;
using AzureMcp.Marketplace.Services;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Xunit;

namespace AzureMcp.Tests.Areas.Marketplace.LiveTests;

[Trait("Area", "Marketplace")]
public class ProductListCommandTests : CommandTestsBase,
    IClassFixture<LiveTestFixture>
{
    private const string ProductsKey = "products";
    private const string Language = "en";
    private readonly MarketplaceService _marketplaceService;
    private readonly string _subscriptionId;

    public ProductListCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output) : base(liveTestFixture, output)
    {
        var memoryCache = new MemoryCache(Microsoft.Extensions.Options.Options.Create(new MemoryCacheOptions()));
        var cacheService = new CacheService(memoryCache);
        var tenantService = new TenantService(cacheService);
        _marketplaceService = new MarketplaceService(tenantService);
        _subscriptionId = Settings.SubscriptionId;
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_marketplace_products()
    {
        var result = await CallToolAsync(
            "azmcp_marketplace_product_list",
            new()
            {
                { "subscription", _subscriptionId }
            });

        var products = result.AssertProperty(ProductsKey);
        Assert.Equal(JsonValueKind.Array, products.ValueKind);

        // Check that we have at least one product
        var productArray = products.EnumerateArray().ToArray();
        Assert.NotEmpty(productArray);

        // Verify each product has expected properties
        foreach (var product in productArray)
        {
            Assert.True(product.TryGetProperty("uniqueProductId", out _));
            Assert.True(product.TryGetProperty("displayName", out _));
        }
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_marketplace_products_with_language_option()
    {
        var result = await CallToolAsync(
            "azmcp_marketplace_product_list",
            new()
            {
                { "subscription", _subscriptionId },
                { "language", Language }
            });

        var products = result.AssertProperty(ProductsKey);
        Assert.Equal(JsonValueKind.Array, products.ValueKind);

        // Check that we have at least one product
        var productArray = products.EnumerateArray().ToArray();
        Assert.NotEmpty(productArray);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_marketplace_products_with_language_and_search_options()
    {
        var result = await CallToolAsync(
            "azmcp_marketplace_product_list",
            new()
            {
                { "subscription", _subscriptionId },
                { "language", Language },
                { "search", "test" }
            });

        var products = result.AssertProperty(ProductsKey);
        Assert.Equal(JsonValueKind.Array, products.ValueKind);

        // Results may be empty for search, but structure should be valid
        var productArray = products.EnumerateArray().ToArray();
        foreach (var product in productArray)
        {
            Assert.True(product.TryGetProperty("uniqueProductId", out _));
            Assert.True(product.TryGetProperty("displayName", out _));
        }
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_marketplace_products_with_search_option()
    {
        var result = await CallToolAsync(
            "azmcp_marketplace_product_list",
            new()
            {
                { "subscription", _subscriptionId },
                { "search", "test" }
            });

        var products = result.AssertProperty(ProductsKey);
        Assert.Equal(JsonValueKind.Array, products.ValueKind);

        // Results may be empty for search, but structure should be valid
        var productArray = products.EnumerateArray().ToArray();
        foreach (var product in productArray)
        {
            Assert.True(product.TryGetProperty("uniqueProductId", out _));
            Assert.True(product.TryGetProperty("displayName", out _));
        }
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_marketplace_products_with_multiple_options()
    {
        var result = await CallToolAsync(
            "azmcp_marketplace_product_list",
            new()
            {
                { "subscription", _subscriptionId },
                { "language", Language },
                { "search", "microsoft" }
            });

        var products = result.AssertProperty(ProductsKey);
        Assert.Equal(JsonValueKind.Array, products.ValueKind);

        // Results may be filtered, but structure should be valid
        var productArray = products.EnumerateArray().ToArray();
        foreach (var product in productArray)
        {
            Assert.True(product.TryGetProperty("uniqueProductId", out _));
            Assert.True(product.TryGetProperty("displayName", out _));
        }
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_marketplace_products_with_filter_option()
    {
        var result = await CallToolAsync(
            "azmcp_marketplace_product_list",
            new()
            {
                { "subscription", _subscriptionId },
                { "filter", "publisherType eq 'Microsoft'" }
            });

        var products = result.AssertProperty(ProductsKey);
        Assert.Equal(JsonValueKind.Array, products.ValueKind);

        // Results may be filtered, but structure should be valid
        var productArray = products.EnumerateArray().ToArray();
        foreach (var product in productArray)
        {
            Assert.True(product.TryGetProperty("uniqueProductId", out _));
            Assert.True(product.TryGetProperty("displayName", out _));
        }
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_marketplace_products_with_orderby_option()
    {
        var result = await CallToolAsync(
            "azmcp_marketplace_product_list",
            new()
            {
                { "subscription", _subscriptionId },
                { "orderby", "displayName asc" }
            });

        var products = result.AssertProperty(ProductsKey);
        Assert.Equal(JsonValueKind.Array, products.ValueKind);

        // Check that we have at least one product
        var productArray = products.EnumerateArray().ToArray();
        Assert.NotEmpty(productArray);

        // Verify ordering if we have multiple products
        if (productArray.Length > 1)
        {
            for (int i = 0; i < productArray.Length - 1; i++)
            {
                var current = productArray[i].GetProperty("displayName").GetString();
                var next = productArray[i + 1].GetProperty("displayName").GetString();
                Assert.True(string.Compare(current, next, StringComparison.OrdinalIgnoreCase) <= 0);
            }
        }
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_marketplace_products_with_select_option()
    {
        var result = await CallToolAsync(
            "azmcp_marketplace_product_list",
            new()
            {
                { "subscription", _subscriptionId },
                { "select", "displayName,uniqueProductId,publisherDisplayName" }
            });

        var products = result.AssertProperty(ProductsKey);
        Assert.Equal(JsonValueKind.Array, products.ValueKind);

        // Check that we have at least one product
        var productArray = products.EnumerateArray().ToArray();
        Assert.NotEmpty(productArray);

        // Verify selected properties are present
        foreach (var product in productArray)
        {
            Assert.True(product.TryGetProperty("uniqueProductId", out _));
            Assert.True(product.TryGetProperty("displayName", out _));
            // Note: publisherDisplayName might be null for some products, so we just check it exists
            Assert.True(product.TryGetProperty("publisherDisplayName", out _));
        }
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_marketplace_products_with_multiple_odata_options()
    {
        var result = await CallToolAsync(
            "azmcp_marketplace_product_list",
            new()
            {
                { "subscription", _subscriptionId },
                { "filter", "publisherType eq 'Microsoft'" },
                { "orderby", "displayName desc" },
                { "select", "displayName,uniqueProductId" }
            });

        var products = result.AssertProperty(ProductsKey);
        Assert.Equal(JsonValueKind.Array, products.ValueKind);

        // Results may be filtered, but structure should be valid
        var productArray = products.EnumerateArray().ToArray();
        foreach (var product in productArray)
        {
            Assert.True(product.TryGetProperty("uniqueProductId", out _));
            Assert.True(product.TryGetProperty("displayName", out _));
        }
    }
}
