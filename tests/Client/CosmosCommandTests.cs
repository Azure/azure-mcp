// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Tests.Client.Helpers;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace AzureMcp.Tests.Client;

public class CosmosCommandTests(McpClientFixture mcpClient, LiveTestSettingsFixture liveTestSettings, CosmosLocalAccessFixture cosmos, ITestOutputHelper output)
    : CommandTestsBase(mcpClient, liveTestSettings, output),
    IClassFixture<McpClientFixture>, IClassFixture<LiveTestSettingsFixture>, IClassFixture<CosmosLocalAccessFixture>
{
    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_storage_accounts_by_subscription_id()
    {
        await cosmos.EnsureLocalAccess(Settings, Client, Output);

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
        await cosmos.EnsureLocalAccess(Settings, Client, Output);

        var result = await CallToolAsync(
            "azmcp-cosmos-database-container-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", Settings.CosmosAccountName }
            });

        Assert.True(result.TryGetProperty("containers", out var containersArray));
        Assert.Equal(JsonValueKind.Array, containersArray.ValueKind);
        Assert.NotEmpty(containersArray.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_cosmos_database_containers_by_database_name()
    {
        await cosmos.EnsureLocalAccess(Settings, Client, Output);

        var result = await CallToolAsync(
            "azmcp-cosmos-database-container-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", Settings.CosmosAccountName },
                { "database-name", "ToDoList" }
            });

        Assert.True(result.TryGetProperty("containers", out var containersArray));
        Assert.Equal(JsonValueKind.Array, containersArray.ValueKind);
        Assert.NotEmpty(containersArray.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_query_cosmos_database_container_items()
    {
        await cosmos.EnsureLocalAccess(Settings, Client, Output);

        var result = await CallToolAsync(
            "azmcp-cosmos-database-container-item-query",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", Settings.CosmosAccountName },
                { "database-name", "ToDoList" },
                { "container-name", "Items" }
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