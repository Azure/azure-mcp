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
    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_monitor_tables()
    {
        var result = await CallToolAsync(
            "azmcp-monitor-table-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "workspace", $"{Settings.ResourceBaseName}-law" },
                { "resource-group", Settings.ResourceGroupName }
            });

        Assert.True(result.TryGetProperty("tables", out var tablesArray));
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

        Assert.True(result.TryGetProperty("workspaces", out var workspacesArray));
        Assert.Equal(JsonValueKind.Array, workspacesArray.ValueKind);
        Assert.NotEmpty(workspacesArray.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_query_monitor_logs()
    {
        var result = await CallToolAsync(
            "azmcp-monitor-log-query",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "workspace", $"{Settings.ResourceBaseName}-law" },
                { "query", "recent" },
                { "table-name", "TestLogs_CL" },
                { "resource-group", Settings.ResourceGroupName }
            });

        Assert.Equal(JsonValueKind.Array, result.ValueKind);
    }
}
