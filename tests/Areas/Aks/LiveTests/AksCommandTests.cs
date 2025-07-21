// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Areas.Aks.LiveTests;

[Trait("Area", "Aks")]
[Trait("Category", "Live")]
public sealed class AksCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output), IClassFixture<LiveTestFixture>
{

    [Fact]
    public async Task Should_list_aks_clusters_by_subscription()
    {
        var result = await CallToolAsync(
            "azmcp_aks_cluster_list",
            new()
            {
                { "subscription", Settings.SubscriptionId }
            });

        var clusters = result.AssertProperty("clusters");
        Assert.Equal(JsonValueKind.Array, clusters.ValueKind);

        // Verify we have at least one cluster in the test environment
        Assert.True(clusters.GetArrayLength() > 0, "Expected at least one AKS cluster in the test environment");

        // Check each cluster is an object with expected properties
        foreach (var cluster in clusters.EnumerateArray())
        {
            Assert.Equal(JsonValueKind.Object, cluster.ValueKind);

            // Verify required properties exist
            Assert.True(cluster.TryGetProperty("name", out var nameProperty));
            Assert.False(string.IsNullOrEmpty(nameProperty.GetString()));

            // Verify optional but commonly present properties
            if (cluster.TryGetProperty("location", out var locationProperty))
            {
                Assert.False(string.IsNullOrEmpty(locationProperty.GetString()));
            }

            if (cluster.TryGetProperty("kubernetesVersion", out var versionProperty))
            {
                Assert.False(string.IsNullOrEmpty(versionProperty.GetString()));
            }

            if (cluster.TryGetProperty("provisioningState", out var stateProperty))
            {
                Assert.False(string.IsNullOrEmpty(stateProperty.GetString()));
            }
        }
    }

    [Fact]
    public async Task Should_handle_empty_subscription_gracefully()
    {
        var result = await CallToolAsync(
            "azmcp_aks_cluster_list",
            new()
            {
                { "subscription", "" }
            });

        // Should return validation error response with no results
        Assert.False(result.HasValue);
    }

    [Fact]
    public async Task Should_handle_invalid_subscription_gracefully()
    {
        var result = await CallToolAsync(
            "azmcp_aks_cluster_list",
            new()
            {
                { "subscription", "invalid-subscription" }
            });

        // Should return runtime error response with error details in results
        Assert.True(result.HasValue);
        var errorDetails = result.Value;
        Assert.True(errorDetails.TryGetProperty("message", out _));
        Assert.True(errorDetails.TryGetProperty("type", out var typeProperty));
        Assert.Equal("Exception", typeProperty.GetString());
    }

    [Fact]
    public async Task Should_validate_required_subscription_parameter()
    {
        var result = await CallToolAsync(
            "azmcp_aks_cluster_list",
            new Dictionary<string, object?>());

        // Should return error response for missing subscription (no results)
        Assert.False(result.HasValue);
    }
}
