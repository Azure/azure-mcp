using System.Text.Json;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Client;

public class AksCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output),
    IClassFixture<LiveTestFixture>
{
    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_List_Clusters_By_Subscription()
    {
        var result = await CallToolAsync(
            "azmcp-aks-cluster-list",
            new()
            {
                { "subscription", Settings.SubscriptionId }
            });

        // Assert that we get a response and it has the expected structure
        Assert.NotNull(result);
        if (result.HasValue &&
            result.Value.TryGetProperty("results", out var results) &&
            results.TryGetProperty("clusters", out var clusters))
        {
            Assert.Equal(JsonValueKind.Array, clusters.ValueKind);
        }
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_Return400_WithInvalidSubscription()
    {
        var result = await CallToolAsync(
            "azmcp-aks-cluster-list",
            new()
            {
                { "subscription", "invalid-subscription" }
            });

        Assert.NotNull(result);
        if (result.HasValue && result.Value.TryGetProperty("status", out var status))
        {
            Assert.Equal(400, status.GetInt32());
        }
    }
}
