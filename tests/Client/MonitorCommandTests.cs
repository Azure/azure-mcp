// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Client;

public class MonitorCommandTests(McpClientFixture mcpClient, LiveTestSettingsFixture liveTestSettings, ITestOutputHelper output)
    : CommandTestsBase(mcpClient, liveTestSettings, output),
    IClassFixture<McpClientFixture>, IClassFixture<LiveTestSettingsFixture>
{
    [Fact(Skip = "Custom table not in bicep template")]
    [Trait("Category", "Live")]
    public async Task Should_list_monitor_tables()
    {
        var result = await CallToolAsync(
            "azmcp-monitor-table-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "workspace", Settings.ResourceBaseName },
                { "resource-group", Settings.ResourceGroupName }
            });

        var tablesArray = result.AssertProperty("tables");
        Assert.Equal(JsonValueKind.Array, tablesArray.ValueKind);
        Assert.NotEmpty(tablesArray.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_monitor_workspaces()
    {
        var result = await CallToolAsync(
            "azmcp-monitor-workspace-list",
            new()
            {
                { "subscription", Settings.SubscriptionId }
            });

        var workspacesArray = result.AssertProperty("workspaces");
        Assert.Equal(JsonValueKind.Array, workspacesArray.ValueKind);
        Assert.NotEmpty(workspacesArray.EnumerateArray());
    }

    [Fact(Skip = "Custom table not in bicep template")]
    [Trait("Category", "Live")]
    public async Task Should_query_monitor_logs()
    {
        var result = await CallToolAsync(
            "azmcp-monitor-log-query",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "workspace", Settings.ResourceBaseName },
                { "query", "recent" },
                { "table-name", "TestLogs_CL" },
                { "resource-group", Settings.ResourceGroupName }
            });

        Assert.Equal(JsonValueKind.Array, result?.ValueKind);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_monitor_table_types()
    {
        var result = await CallToolAsync(
            "azmcp-monitor-table-type-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "workspace", Settings.ResourceBaseName },
                { "resource-group", Settings.ResourceGroupName }
            });

        var tableTypesArray = result.AssertProperty("tableTypes");
        Assert.Equal(JsonValueKind.Array, tableTypesArray.ValueKind);
        Assert.NotEmpty(tableTypesArray.EnumerateArray());
    }
}
