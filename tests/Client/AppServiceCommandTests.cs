// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Tests.Client.Helpers;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace AzureMcp.Tests.Client;

public class AppServiceCommandTests(McpClientFixture mcpClient, LiveTestSettingsFixture liveTestSettings, ITestOutputHelper output)
    : CommandTestsBase(mcpClient, liveTestSettings, output),
    IClassFixture<McpClientFixture>, IClassFixture<LiveTestSettingsFixture>
{
    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_appservice_plans()
    {
        var result = await CallToolAsync(
            "azmcp-appservice-plan-list",
            new()
            {
                { "subscription", Settings.SubscriptionId }
            });

        Assert.True(result.TryGetProperty("appServicePlans", out var appServicePlans));
        Assert.Equal(JsonValueKind.Array, appServicePlans.ValueKind);
        Assert.NotEmpty(appServicePlans.EnumerateArray());
    }

}