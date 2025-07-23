// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Areas.Quota.LiveTests;

[Trait("Area", "Quota")]
public class QuotaCommandTests : CommandTestsBase,
    IClassFixture<LiveTestFixture>
{
    private readonly string _subscriptionId;

    public QuotaCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output) : base(liveTestFixture, output)
    {
        _subscriptionId = Settings.SubscriptionId;
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_check_azure_quota()
    {
        JsonElement? result = await CallToolAsync(
            "azmcp-quota-usage-get",
            new() {
                { "subscription", _subscriptionId },
                { "region", "eastus" },
                { "resource-types", "Microsoft.App, Microsoft.Storage/storageAccounts" }
            });
        // assert
        var quotas = result.AssertProperty("usageInfo");
        Assert.Equal(JsonValueKind.Object, quotas.ValueKind);
        var appQuotas = quotas.AssertProperty("Microsoft.App");
        Assert.Equal(JsonValueKind.Array, appQuotas.ValueKind);
        Assert.NotEmpty(appQuotas.EnumerateArray());
        var storageQuotas = quotas.AssertProperty("Microsoft.Storage/storageAccounts");
        Assert.Equal(JsonValueKind.Array, storageQuotas.ValueKind);
        Assert.NotEmpty(storageQuotas.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_check_azure_regions()
    {
        // act
        var result = await CallToolAsync(
            "azmcp-quota-available-region-get",
            new()
            {
                { "subscription", _subscriptionId },
                { "resource-types", "Microsoft.Web/sites, Microsoft.Storage/storageAccounts" },
            });

        // assert
        var availableRegions = result.AssertProperty("availableRegions");
        Assert.Equal(JsonValueKind.Array, availableRegions.ValueKind);
        Assert.NotEmpty(availableRegions.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_check_regions_with_cognitive_services()
    {
        // act
        var result = await CallToolAsync(
            "azmcp-quota-available-region-get",
            new()
            {
                { "subscription", _subscriptionId },
                { "resource-types", "Microsoft.CognitiveServices/accounts" },
                { "cognitive-service-model-name", "gpt-4o" },
                { "cognitive-service-deployment-sku-name", "Standard" }
            });

        // assert
        var availableRegions = result.AssertProperty("availableRegions");
        Assert.Equal(JsonValueKind.Array, availableRegions.ValueKind);
        Assert.NotEmpty(availableRegions.EnumerateArray());
    }
}
