// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Client;

public class SearchCommandTests(McpClientFixture mcpClient, LiveTestSettingsFixture liveTestSettings, ITestOutputHelper output)
    : CommandTestsBase(mcpClient, liveTestSettings, output),
    IClassFixture<McpClientFixture>, IClassFixture<LiveTestSettingsFixture>
{
    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_search_services_by_subscription_id()
    {
        Assert.NotNull(Settings.SubscriptionId);

        var result = await CallToolAsync(
            "azmcp-search-service-list",
            new()
            {
                { "subscription", Settings.SubscriptionId }
            });

        Assert.True(result.TryGetProperty("services", out var services));
        Assert.Equal(JsonValueKind.Array, services.ValueKind);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_search_services_by_subscription_name()
    {
        Assert.NotNull(Settings.SubscriptionName);

        var result = await CallToolAsync(
            "azmcp-search-service-list",
            new()
            {
                { "subscription", Settings.SubscriptionName }
            });

        Assert.True(result.TryGetProperty("services", out var services));
        Assert.Equal(JsonValueKind.Array, services.ValueKind);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_search_indexes_with_service_name()
    {
        Assert.NotNull(Settings.SearchServiceName);

        var result = await CallToolAsync(
            "azmcp-search-index-list",
            new()
            {
                { "service-name", Settings.SearchServiceName }
            });

        Assert.True(result.TryGetProperty("indexes", out var indexes));
        Assert.Equal(JsonValueKind.Array, indexes.ValueKind);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_get_index_details()
    {
        Assert.NotNull(Settings.SearchServiceName);
        Assert.NotNull(Settings.SearchIndexName);

        var result = await CallToolAsync(
            "azmcp-search-index-describe",
            new()
            {
                { "service-name", Settings.SearchServiceName },
                { "index-name", Settings.SearchIndexName }
            });

        Assert.True(result.TryGetProperty("index", out var index));
        Assert.Equal(JsonValueKind.Object, index.ValueKind);
        Assert.True(index.TryGetProperty("Name", out var name));
        Assert.Equal(Settings.SearchIndexName, name.GetString());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_query_search_index()
    {
        Assert.NotNull(Settings.SearchServiceName);
        Assert.NotNull(Settings.SearchIndexName);

        var result = await CallToolAsync(
            "azmcp-search-index-query",
            new()
            {
                { "service-name", Settings.SearchServiceName },
                { "index-name", Settings.SearchIndexName },
                { "query", "*" }
            });

        Assert.Equal(JsonValueKind.Array, result.ValueKind);
        Assert.True(result.GetArrayLength() > 0);
    }
}
