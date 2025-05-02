// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Tests.Client.Helpers;
using System.Text.Json;
using Xunit;

namespace AzureMcp.Tests.Client;

public class CosmosCommandTests(McpClientFixture mcpClient, LiveTestSettingsFixture liveTestSettings, ITestOutputHelper output)
    : CommandTestsBase(mcpClient, liveTestSettings, output),
    IClassFixture<McpClientFixture>, IClassFixture<LiveTestSettingsFixture>, IClassFixture<CosmosLocalAccessFixture>
{
    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_storage_accounts_by_subscription_id()
    {
        var result = await CallToolAsync(
            "azmcp-cosmos-database-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", Settings.CosmosAccountName }
            });

        Assert.True(result.TryGetProperty("databases", out var databasesArray));
        Assert.Equal(JsonValueKind.Array, databasesArray.ValueKind);
        Assert.NotEmpty(databasesArray.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_cosmos_database_containers()
    {
        var result = await CallToolAsync(
            "azmcp-cosmos-database-container-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", Settings.CosmosAccountName },
                { "database-name", Settings.CosmosDatabaseName }
            });

        Assert.True(result.TryGetProperty("containers", out var containersArray));
        Assert.Equal(JsonValueKind.Array, containersArray.ValueKind);
        Assert.NotEmpty(containersArray.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_cosmos_database_containers_by_database_name()
    {
        var result = await CallToolAsync(
            "azmcp-cosmos-database-container-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", Settings.CosmosAccountName },
                { "database-name", Settings.CosmosDatabaseName }
            });

        Assert.True(result.TryGetProperty("containers", out var containersArray));
        Assert.Equal(JsonValueKind.Array, containersArray.ValueKind);
        Assert.NotEmpty(containersArray.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_query_cosmos_database_container_items()
    {
        var result = await CallToolAsync(
            "azmcp-cosmos-database-container-item-query",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", Settings.CosmosAccountName },
                { "database-name", Settings.CosmosDatabaseName },
                { "container-name", Settings.CosmosContainerName }
            });

        Assert.True(result.TryGetProperty("items", out var itemsArray));
        Assert.Equal(JsonValueKind.Array, itemsArray.ValueKind);
        Assert.NotEmpty(itemsArray.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_cosmos_accounts()
    {
        var result = await CallToolAsync(
            "azmcp-cosmos-account-list",
            new()
            {
                { "subscription", Settings.SubscriptionId }
            });

        Assert.True(result.TryGetProperty("accounts", out var accountsArray));
        Assert.Equal(JsonValueKind.Array, accountsArray.ValueKind);
        Assert.NotEmpty(accountsArray.EnumerateArray());
    }
}