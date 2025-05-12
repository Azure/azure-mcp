// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Client;

public class CosmosCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output),
    IClassFixture<LiveTestFixture>
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
                { "account-name", Settings.ResourceBaseName }
            });

        var databasesArray = result.AssertProperty("databases");
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
                { "account-name", Settings.ResourceBaseName },
                { "database-name", "ToDoList" }
            });

        var containersArray = result.AssertProperty("containers");
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
                { "account-name", Settings.ResourceBaseName },
                { "database-name", "ToDoList" }
            });

        var containersArray = result.AssertProperty("containers");
        Assert.Equal(JsonValueKind.Array, containersArray.ValueKind);
        Assert.NotEmpty(containersArray.EnumerateArray());
    }

    [Fact(Skip = "Cosmos needs post script to add items")]
    [Trait("Category", "Live")]
    public async Task Should_query_cosmos_database_container_items()
    {
        var result = await CallToolAsync(
            "azmcp-cosmos-database-container-item-query",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", Settings.ResourceBaseName },
                { "database-name", "ToDoList" },
                { "container-name", "Items" }
            });

        var itemsArray = result.AssertProperty("items");
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

        var accountsArray = result.AssertProperty("accounts");
        Assert.Equal(JsonValueKind.Array, accountsArray.ValueKind);
        Assert.NotEmpty(accountsArray.EnumerateArray());
    }
}
