// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Client;

public class CosmosCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output),
    IClassFixture<LiveTestFixture>,
    IClassFixture<CosmosDbFixture>
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

    [Fact]
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

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_show_single_item_from_cosmos_account()
    {
        var dbResult = await CallToolAsync(
            "azmcp-cosmos-database-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", Settings.ResourceBaseName }
            }
        );
        var databases = dbResult.AssertProperty("databases");
        Assert.Equal(JsonValueKind.Array, databases.ValueKind);
        var dbEnum = databases.EnumerateArray();
        Assert.True(dbEnum.Any());

        // The agent will choose one, for this test we're going to take the first one
        var firstDatabase = dbEnum.First();
        string dbName = firstDatabase.ValueKind == JsonValueKind.Object
            ? firstDatabase.GetProperty("name").GetString()!
            : throw new InvalidOperationException($"Unexpected database element ValueKind: {firstDatabase.ValueKind}");
        Assert.False(string.IsNullOrEmpty(dbName));

        var containerResult = await CallToolAsync(
            "azmcp-cosmos-database-container-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", Settings.ResourceBaseName },
                { "database-name", dbName! }
            });
        var containers = containerResult.AssertProperty("containers");
        Assert.Equal(JsonValueKind.Array, containers.ValueKind);
        var contEnum = containers.EnumerateArray();
        Assert.True(contEnum.Any());

        // The agent will choose one, for this test we're going to take the first one
        var firstContainer = contEnum.First();
        string containerName = firstContainer.ValueKind == JsonValueKind.Object
            ? firstContainer.GetProperty("name").GetString()!
            : throw new InvalidOperationException($"Unexpected container element ValueKind: {firstContainer.ValueKind}");
        Assert.False(string.IsNullOrEmpty(containerName));

        var itemResult = await CallToolAsync(
            "azmcp-cosmos-database-container-item-query",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", Settings.ResourceBaseName },
                { "database-name", dbName! },
                { "container-name", containerName! }
            });
        var items = itemResult.AssertProperty("items");
        Assert.Equal(JsonValueKind.Array, items.ValueKind);
        Assert.True(items.EnumerateArray().Any());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_and_query_multiple_databases_and_containers()
    {
        var dbResult = await CallToolAsync(
            "azmcp-cosmos-database-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", Settings.ResourceBaseName }
            }
        );
        var databases = dbResult.AssertProperty("databases");
        Assert.Equal(JsonValueKind.Array, databases.ValueKind);
        var databasesEnum = databases.EnumerateArray();
        Assert.True(databasesEnum.Any());

        foreach (var db in databasesEnum)
        {
            string dbName = db.ValueKind == JsonValueKind.Object
                ? db.GetProperty("name").GetString()!
                : db.GetString()!;
            Assert.False(string.IsNullOrEmpty(dbName));

            var containerResult = await CallToolAsync(
                "azmcp-cosmos-database-container-list",
                new() { { "subscription", Settings.SubscriptionId }, { "account-name", Settings.ResourceBaseName! }, { "database-name", dbName! } });
            var containers = containerResult.AssertProperty("containers");
            Assert.Equal(JsonValueKind.Array, containers.ValueKind);
            var contEnum = containers.EnumerateArray();

            foreach (var container in contEnum)
            {
                string containerName = container.ValueKind == JsonValueKind.Object
                    ? container.GetProperty("name").GetString()!
                    : throw new InvalidOperationException($"Unexpected container element ValueKind: {container.ValueKind}");
                Assert.False(string.IsNullOrEmpty(containerName));

                var itemResult = await CallToolAsync(
                    "azmcp-cosmos-database-container-item-query",
                    new() { { "subscription", Settings.SubscriptionId }, { "account-name", Settings.ResourceBaseName! }, { "database-name", dbName! }, { "container-name", containerName! } });
                var items = itemResult.AssertProperty("items");
                Assert.Equal(JsonValueKind.Array, items.ValueKind);
            }
        }
    }
}
