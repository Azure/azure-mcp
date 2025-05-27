// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Client;

public class SearchCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output),
    IClassFixture<LiveTestFixture>
{
    const string IndexName = "products";

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

        var services = result.AssertProperty("services");
        Assert.Equal(JsonValueKind.Array, services.ValueKind);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_search_services_by_subscription_name()
    {
        var result = await CallToolAsync(
            "azmcp-search-service-list",
            new()
            {
                { "subscription", Settings.SubscriptionName }
            });

        var services = result.AssertProperty("services");
        Assert.Equal(JsonValueKind.Array, services.ValueKind);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_search_indexes_with_service_name()
    {
        var result = await CallToolAsync(
            "azmcp-search-index-list",
            new()
            {
                { "service-name", Settings.ResourceBaseName }
            });

        var indexes = result.AssertProperty("indexes");
        Assert.Equal(JsonValueKind.Array, indexes.ValueKind);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_get_index_details()
    {
        var result = await CallToolAsync(
            "azmcp-search-index-describe",
            new()
            {
                { "service-name", Settings.ResourceBaseName },
                { "index-name", IndexName }
            });

        var index = result.AssertProperty("index");
        Assert.Equal(JsonValueKind.Object, index.ValueKind);

        var name = index.AssertProperty("name");
        Assert.Equal(IndexName, name.GetString());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_query_search_index()
    {
        var result = await CallToolAsync(
            "azmcp-search-index-query",
            new()
            {
                { "service-name", Settings.ResourceBaseName },
                { "index-name", IndexName },
                { "query", "*" }
            });

        Assert.NotNull(result);
        Assert.Equal(JsonValueKind.Array, result.Value.ValueKind);
        Assert.True(result.Value.GetArrayLength() > 0);
    }
}
