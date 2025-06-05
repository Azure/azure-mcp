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
    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_search_services()
    {
        var result = await CallToolAsync(
            "azmcp-search-service-list",
            new()
            {
                { "subscription", Settings.SubscriptionId }
            });

        var servicesArray = result.AssertProperty("services");
        Assert.Equal(JsonValueKind.Array, servicesArray.ValueKind);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_search_indexes()
    {
        var result = await CallToolAsync(
            "azmcp-search-index-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "service-name", Settings.ResourceBaseName },
                { "resource-group", Settings.ResourceGroupName }
            });

        var indexesArray = result.AssertProperty("indexes");
        Assert.Equal(JsonValueKind.Array, indexesArray.ValueKind);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_describe_search_index()
    {
        var result = await CallToolAsync(
            "azmcp-search-index-describe",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "service-name", Settings.ResourceBaseName },
                { "resource-group", Settings.ResourceGroupName },
                { "index-name", Settings.ResourceBaseName }
            });

        var index = result.AssertProperty("index");
        Assert.Equal(JsonValueKind.Object, index.ValueKind);
    }

    [Fact(Skip = "Requires populated index and queryable data")]
    [Trait("Category", "Live")]
    public async Task Should_query_search_index()
    {
        var result = await CallToolAsync(
            "azmcp-search-index-query",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "service-name", Settings.ResourceBaseName },
                { "resource-group", Settings.ResourceGroupName },
                { "index-name", Settings.ResourceBaseName },
                { "query", "*" }
            });

        var docs = result.AssertProperty("documents");
        Assert.Equal(JsonValueKind.Array, docs.ValueKind);
    }
}
