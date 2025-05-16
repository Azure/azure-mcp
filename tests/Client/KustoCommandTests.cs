// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.Identity;
using AzureMcp.Tests.Client.Helpers;
using Kusto.Cloud.Platform.Data;
using Kusto.Data;
using Kusto.Data.Net.Client;
using ModelContextProtocol.Client;
using Xunit;

namespace AzureMcp.Tests.Client;


public class KustoCommandTests(McpClientFixture mcpClient, LiveTestSettingsFixture liveTestSettings, ITestOutputHelper output)
    : CommandTestsBase(mcpClient, liveTestSettings, output),
    IClassFixture<McpClientFixture>, IClassFixture<LiveTestSettingsFixture>, IAsyncLifetime
{
    private const string TestDatabaseName = "ToDoLists";

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }

    public async ValueTask InitializeAsync()
    {
        var credentials = new DefaultAzureCredential();
        await Client.PingAsync();
        var clusterInfo = await CallToolAsync(
            "azmcp-kusto-cluster-get",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "cluster-name", Settings.ResourceBaseName }
            });
        var clusterUri = clusterInfo.AssertProperty("cluster").AssertProperty("clusterUri").GetString();
        var kcsb = new KustoConnectionStringBuilder(clusterUri)
            .WithAadAzureTokenCredentialsAuthentication(credentials);
        using var adminClient = KustoClientFactory.CreateCslAdminProvider(kcsb);
        using var resp = await adminClient.ExecuteControlCommandAsync(
            TestDatabaseName,
            ".set-or-replace ToDoList <| datatable (Title: string, IsCompleted: bool) [' Hello World!', false]");
        resp.Consume();

    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_databases_in_cluster()
    {
        var result = await CallToolAsync(
            "azmcp-kusto-database-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "cluster-name", Settings.ResourceBaseName }
            });

        var databasesArray = result.AssertProperty("databases");
        Assert.Equal(JsonValueKind.Array, databasesArray.ValueKind);
        Assert.NotEmpty(databasesArray.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_kusto_tables()
    {
        var result = await CallToolAsync(
            "azmcp-kusto-table-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "cluster-name", Settings.ResourceBaseName },
                { "database-name", TestDatabaseName }
            });

        var tablesArray = result.AssertProperty("tables");
        Assert.Equal(JsonValueKind.Array, tablesArray.ValueKind);
        Assert.NotEmpty(tablesArray.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_query_kusto()
    {
        var result = await CallToolAsync(
            "azmcp-kusto-query",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "cluster-name", Settings.ResourceBaseName },
                { "database-name", TestDatabaseName },
                { "query", "ToDoList | take 1" }
            });

        var itemsArray = result.AssertProperty("items");
        Assert.Equal(JsonValueKind.Array, itemsArray.ValueKind);
        Assert.NotEmpty(itemsArray.EnumerateArray());
    }
}
