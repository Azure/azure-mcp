// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.Identity;
using AzureMcp.Core.Services.Http;
using AzureMcp.Kusto.Services;
using AzureMcp.Tests;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Microsoft.Extensions.Options;
using ModelContextProtocol.Client;
using Xunit;
using MsOptions = Microsoft.Extensions.Options.Options;

namespace AzureMcp.Kusto.LiveTests;

public class KustoCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output),
    IClassFixture<LiveTestFixture>, IAsyncLifetime
{
    private const string TestDatabaseName = "ToDoLists";

    public ValueTask DisposeAsync()
    {
        base.Dispose();
        return ValueTask.CompletedTask;
    }

    public async ValueTask InitializeAsync()
    {
        try
        {
            var credentials = new DefaultAzureCredential();
            await Client.PingAsync();
            var clusterInfo = await CallToolAsync(
                "azmcp_kusto_cluster_get",
                new()
                {
                { "subscription", Settings.SubscriptionId },
                { "cluster", Settings.ResourceBaseName }
                });
            var clusterUri = clusterInfo.AssertProperty("cluster").AssertProperty("clusterUri").GetString();

            // Create HttpClientService for KustoClient
            var httpClientOptions = new HttpClientOptions();
            var httpClientService = new HttpClientService(MsOptions.Create(httpClientOptions));

            var kustoClient = new KustoClient(clusterUri ?? string.Empty, credentials, "ua", httpClientService);
            var resp = await kustoClient.ExecuteControlCommandAsync(
                TestDatabaseName,
                ".set-or-replace ToDoList <| datatable (Title: string, IsCompleted: bool) [' Hello World!', false]",
                CancellationToken.None).ConfigureAwait(false);
        }
        catch
        {
            Assert.Skip("Skipping until auth fixed for Kusto");
        }
    }

    [Fact]
    public async Task Should_list_databases_in_cluster()
    {
        var result = await CallToolAsync(
            "azmcp_kusto_database_list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "cluster", Settings.ResourceBaseName }
            });

        var databasesArray = result.AssertProperty("databases");
        Assert.Equal(JsonValueKind.Array, databasesArray.ValueKind);
        Assert.NotEmpty(databasesArray.EnumerateArray());
    }

    [Fact]
    public async Task Should_list_kusto_tables()
    {
        var result = await CallToolAsync(
            "azmcp_kusto_table_list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "cluster", Settings.ResourceBaseName },
                { "database", TestDatabaseName }
            });

        var tablesArray = result.AssertProperty("tables");
        Assert.Equal(JsonValueKind.Array, tablesArray.ValueKind);
        Assert.NotEmpty(tablesArray.EnumerateArray());
    }

    [Fact]
    public async Task Should_query_kusto()
    {
        var result = await CallToolAsync(
            "azmcp_kusto_query",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "cluster", Settings.ResourceBaseName },
                { "database", TestDatabaseName },
                { "query", "ToDoList | take 1" }
            });

        var itemsArray = result.AssertProperty("items");
        Assert.Equal(JsonValueKind.Array, itemsArray.ValueKind);
        Assert.NotEmpty(itemsArray.EnumerateArray());
    }
}
