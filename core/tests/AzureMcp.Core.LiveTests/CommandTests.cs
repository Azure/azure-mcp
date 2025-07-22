// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Core.UnitTests.Client;
using AzureMcp.Core.UnitTests.Client.Helpers;
using AzureMcp.Tests;
using Xunit;

namespace AzureMcp.Core.LiveTests;

public class CommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output),
    IClassFixture<LiveTestFixture>
{
    [Fact]
    public async Task Should_list_groups_by_subscription()
    {
        var result = await CallToolAsync(
            "azmcp_group_list",
            new()
            {
                { "subscription", Settings.SubscriptionId }
            });

        var groupsArray = result.AssertProperty("groups");
        Assert.Equal(JsonValueKind.Array, groupsArray.ValueKind);
        Assert.NotEmpty(groupsArray.EnumerateArray());
    }

    [Fact]
    public async Task Should_get_best_practices()
    {
        // Act
        JsonElement? result = await CallToolAsync("azmcp_bestpractices_general_get", new Dictionary<string, object?>());

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
    public async Task Should_get_azure_functions_code_generation_best_practices()
    {
        // Act
        JsonElement? result = await CallToolAsync("azmcp_bestpractices_azurefunctions_get-code-generation", new Dictionary<string, object?>());

        Assert.True(result.HasValue, "Tool call did not return a value.");

        Assert.Equal(JsonValueKind.Array, result.Value.ValueKind);
        var entries = result.Value.EnumerateArray().ToList();
        Assert.NotEmpty(entries);

        // Combine all entries into a single normalized string for content assertion
        var combinedText = string.Join("\n", entries
            .Where(e => e.ValueKind == JsonValueKind.String)
            .Select(e => e.GetString())
            .Where(s => !string.IsNullOrWhiteSpace(s)));

        // Assert specific Azure Functions code generation practices are mentioned
        Assert.Contains("Use the latest programming models", combinedText, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("Azure Functions Core Tools", combinedText, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("extension bundles", combinedText, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("isolated process model", combinedText, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Should_get_azure_functions_deployment_best_practices()
    {
        // Act
        JsonElement? result = await CallToolAsync("azmcp_bestpractices_azurefunctions_get-deployment", new Dictionary<string, object?>());

        Assert.True(result.HasValue, "Tool call did not return a value.");

        Assert.Equal(JsonValueKind.Array, result.Value.ValueKind);
        var entries = result.Value.EnumerateArray().ToList();
        Assert.NotEmpty(entries);

        // Combine all entries into a single normalized string for content assertion
        var combinedText = string.Join("\n", entries
            .Where(e => e.ValueKind == JsonValueKind.String)
            .Select(e => e.GetString())
            .Where(s => !string.IsNullOrWhiteSpace(s)));

        // Assert specific Azure Functions deployment practices are mentioned
        Assert.Contains("flex consumption plan", combinedText, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("Always use Linux OS for Python", combinedText, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("Function authentication", combinedText, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("Application Insights", combinedText, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Should_list_subscriptions()
    {
        var result = await CallToolAsync(
            "azmcp_subscription_list",
            new Dictionary<string, object?>());

        var subscriptionsArray = result.AssertProperty("subscriptions");
        Assert.Equal(JsonValueKind.Array, subscriptionsArray.ValueKind);
        Assert.NotEmpty(subscriptionsArray.EnumerateArray());
    }
}
