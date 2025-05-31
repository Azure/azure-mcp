// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Services.Azure.Monitor;
using AzureMcp.Services.Azure.ResourceGroup;
using AzureMcp.Services.Azure.Subscription;
using AzureMcp.Services.Azure.Tenant;
using AzureMcp.Services.Caching;
using AzureMcp.Services.Interfaces;
using AzureMcp.Tests.Client.Helpers;
using AzureMcp.Tests.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Xunit;

namespace AzureMcp.Tests.Client;

public class MonitorCommandTests(LiveTestFixture fixture, ITestOutputHelper output) : CommandTestsBase(fixture, output), IClassFixture<LiveTestFixture>, IAsyncLifetime
{
    private LogAnalyticsHelper? _logHelper;
    private const string TestLogType = "TestLogs_CL";
    private IMonitorService? _monitorService;

    ValueTask IAsyncLifetime.InitializeAsync()
    {
        _monitorService = GetMonitorService();
        _logHelper = new LogAnalyticsHelper(Settings.ResourceBaseName, Settings.SubscriptionId, _monitorService, Settings.TenantId, TestLogType);
        return ValueTask.CompletedTask;
    }

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;

    private static IMonitorService GetMonitorService()
    {
        var memoryCache = new MemoryCache(Microsoft.Extensions.Options.Options.Create(new MemoryCacheOptions()));
        var cacheService = new CacheService(memoryCache);
        var tenantService = new TenantService(cacheService);
        var subscriptionService = new SubscriptionService(cacheService, tenantService);
        var resourceGroupService = new ResourceGroupService(cacheService, subscriptionService);
        return new MonitorService(subscriptionService, tenantService, resourceGroupService);
    }

    [Fact()]
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
        var array = tablesArray.EnumerateArray();
        Assert.NotEmpty(array);
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
        var array = workspacesArray.EnumerateArray();
        Assert.NotEmpty(array);
    }

    [Fact()]
    [Trait("Category", "Live")]
    public async Task Should_query_monitor_logs()
    {
        // First try to find any existing logs from last 24 hours
        Output.WriteLine($"Checking for existing logs in the last 24 hours...");
        var queryStartTime = DateTime.UtcNow;
        var result = await CallToolAsync("azmcp-monitor-log-query",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "workspace", Settings.ResourceBaseName },
                { "query", $"{TestLogType} | where TimeGenerated > ago(24h) | limit 1 | project TimeGenerated, Message" },
                { "table-name", TestLogType },
                { "resource-group", Settings.ResourceGroupName },
                { "hours", "24" }
            });

        Assert.NotNull(result);
        Assert.Equal(JsonValueKind.Array, result.Value.ValueKind);
        var logs = result.Value.EnumerateArray();
        var queryDuration = (DateTime.UtcNow - queryStartTime).TotalSeconds;

        if (logs.Any())
        {
            Output.WriteLine($"Found existing logs from last 24 hours");
            Output.WriteLine($"Query performance: {queryDuration:F1}s to execute");
            return;
        }

        // No recent logs found, create a new one
        Output.WriteLine($"No recent logs found, sending new log...");
        var status = await _logHelper!.SendInfoLogAsync(TestContext.Current.CancellationToken);
        Output.WriteLine($"Info log sent with status code: {status}");

        // Start time for query window - use the current time
        var testStartTime = DateTime.UtcNow;
        Output.WriteLine($"Starting to query for new log (max wait: 60s)...");
        const int maxWaitTimeSeconds = 60; // Reduced from 120s since we have optimized querying
        var attemptCount = 0;

        while ((DateTime.UtcNow - testStartTime).TotalSeconds < maxWaitTimeSeconds)
        {
            // More aggressive polling at start (1s, 2s, 4s, 8s, 15s...)
            var delaySeconds = Math.Min(Math.Pow(2, attemptCount), 15);
            attemptCount++;

            var elapsed = (DateTime.UtcNow - testStartTime).TotalSeconds;
            Output.WriteLine($"Attempt {attemptCount}: Querying for logs at {elapsed:F1}s...");

            queryStartTime = DateTime.UtcNow;
            result = await CallToolAsync("azmcp-monitor-log-query",
                new()
                {
                    { "subscription", Settings.SubscriptionId },
                    { "workspace", Settings.ResourceBaseName },
                    { "query", $"{TestLogType} | where TimeGenerated > datetime({testStartTime:yyyy-MM-dd HH:mm:ss.fff}) | limit 1 | project TimeGenerated, Message" },
                    { "table-name", TestLogType },
                    { "resource-group", Settings.ResourceGroupName },
                    { "hours", "1" } // Only look at last hour for new logs
                });

            queryDuration = (DateTime.UtcNow - queryStartTime).TotalSeconds;
            Output.WriteLine($"Query completed in {queryDuration:F1} seconds");

            Assert.NotNull(result);
            Assert.Equal(JsonValueKind.Array, result.Value.ValueKind);
            logs = result.Value.EnumerateArray();
            if (logs.Any())
            {
                var totalTime = (DateTime.UtcNow - testStartTime).TotalSeconds;
                Output.WriteLine($"Success! Found new log after {totalTime:F1} seconds (attempt {attemptCount})");
                Output.WriteLine($"Query performance: {queryDuration:F1}s to execute, {totalTime:F1}s total test time");
                return;
            }

            Output.WriteLine($"No logs found yet (attempt {attemptCount}), waiting {delaySeconds:F1} seconds before retrying...");
            await Task.Delay(TimeSpan.FromSeconds(delaySeconds), TestContext.Current.CancellationToken);
        }

        Assert.Fail($"No logs found in {TestLogType} table after waiting {maxWaitTimeSeconds} seconds");
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
        var array = tableTypesArray.EnumerateArray();
        Assert.NotEmpty(array);
    }
}
