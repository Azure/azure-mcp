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
            "azmcp-quota-check",
            new() {
                { "subscription", _subscriptionId },
                { "region", "eastus" },
                { "resource-types", "Microsoft.App, Microsoft.Storage/storageAccounts" }
            });
        // assert
        var quotas = result.AssertProperty("quotaInfo");
        Assert.Equal(JsonValueKind.Object, quotas.ValueKind);
        var appQuotas = quotas.AssertProperty("Microsoft.App");
        Assert.Equal(JsonValueKind.Array, appQuotas.ValueKind);
        Assert.NotEmpty(appQuotas.EnumerateArray());
        var storageQuotas = quotas.AssertProperty("Microsoft.Storage/storageAccounts");
        Assert.Equal(JsonValueKind.Array, storageQuotas.ValueKind);
        Assert.NotEmpty(storageQuotas.EnumerateArray());
    }
}
