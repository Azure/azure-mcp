// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Client;

public class CommandTests(McpClientFixture mcpClient, LiveTestSettingsFixture liveTestSettings, ITestOutputHelper output)
    : CommandTestsBase(mcpClient, liveTestSettings, output),
    IClassFixture<McpClientFixture>, IClassFixture<LiveTestSettingsFixture>
{
    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_groups_by_subscription()
    {
        var result = await CallToolAsync(
            "azmcp-group-list",
            new()
            {
                { "subscription", Settings.SubscriptionId }
            });

        var groupsArray = result.AssertProperty("groups");
        Assert.Equal(JsonValueKind.Array, groupsArray.ValueKind);
        Assert.NotEmpty(groupsArray.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_get_best_practices()
    {
        // Act
        JsonElement? result = await CallToolAsync("azmcp-best-practices-get", new Dictionary<string, object?>());

        Assert.True(result.HasValue, "Tool call did not return a value.");


        Assert.Equal(JsonValueKind.Array, result.Value.ValueKind);
        var entries = result.Value.EnumerateArray().ToList();
        Assert.NotEmpty(entries);

        // Combine all entries into a single normalized string for content assertion
        var combinedText = string.Join("\n", entries
            .Where(e => e.ValueKind == JsonValueKind.String)
            .Select(e => e.GetString())
            .Where(s => !string.IsNullOrWhiteSpace(s)));

        // Assert specific practices are mentioned
        Assert.Contains("Implement credential rotation", combinedText, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("retry logic", combinedText, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("logging and monitoring", combinedText, StringComparison.OrdinalIgnoreCase);
    }



    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_subscriptions()
    {
        var result = await CallToolAsync(
            "azmcp-subscription-list",
            new Dictionary<string, object?>());

        var subscriptionsArray = result.AssertProperty("subscriptions");
        Assert.Equal(JsonValueKind.Array, subscriptionsArray.ValueKind);
        Assert.NotEmpty(subscriptionsArray.EnumerateArray());
    }
}
