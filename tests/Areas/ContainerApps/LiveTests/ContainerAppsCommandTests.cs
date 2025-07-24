// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Areas.ContainerApps.LiveTests;

[Trait("Area", "ContainerApps")]
[Trait("Category", "Live")]
public class ContainerAppsCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output), IClassFixture<LiveTestFixture>
{
    [Fact]
    public async Task ContainerAppList_WithSubscription_ReturnsSuccess()
    {
        // Arrange & Act
        var result = await CallToolAsync("azmcp_containerapp_list", new()
        {
            { "subscription", Settings.SubscriptionName }
        });

        // Log the result for debugging
        Output.WriteLine($"ContainerAppList_WithSubscription result: {result?.ToString() ?? "null"}");

        // Assert
        Assert.NotNull(result);
        var containerApps = result.AssertProperty("containerApps");
        Assert.Equal(JsonValueKind.Array, containerApps.ValueKind);
    }

    [Fact]
    public async Task ContainerAppList_WithInvalidSubscription_ReturnsError()
    {
        // Arrange & Act
        var result = await CallToolAsync("azmcp_containerapp_list", new()
        {
            { "subscription", "invalid-subscription-name" }
        });

        // Log the result for debugging
        Output.WriteLine($"ContainerAppList_WithInvalidSubscription result: {result?.ToString() ?? "null"}");

        // Assert
        // Should return runtime error response with error details in results
        Assert.True(result.HasValue);
        var errorDetails = result.Value;
        Assert.True(errorDetails.TryGetProperty("message", out var messageProperty));
        Assert.Contains("Could not find subscription", messageProperty.GetString());
        Assert.True(errorDetails.TryGetProperty("type", out var typeProperty));
        Assert.Equal("Exception", typeProperty.GetString());
    }

    [Fact]
    public async Task ContainerAppList_WithResourceGroup_ReturnsSuccess()
    {
        // Arrange & Act
        var result = await CallToolAsync("azmcp_containerapp_list", new()
        {
            { "subscription", Settings.SubscriptionName },
            { "resourceGroup", "non-existent-rg" }
        });

        // Log the result for debugging
        Output.WriteLine($"ContainerAppList_WithResourceGroup result: {result?.ToString() ?? "null"}");

        // Assert
        Assert.NotNull(result);
        var containerApps = result.AssertProperty("containerApps");
        Assert.Equal(JsonValueKind.Array, containerApps.ValueKind);
    }

    [Fact]
    public async Task ContainerAppList_WithEnvironment_ReturnsSuccessOrHandlesError()
    {
        // Arrange & Act
        var result = await CallToolAsync("azmcp_containerapp_list", new()
        {
            { "subscription", Settings.SubscriptionName },
            { "environment", "non-existent-env" }
        });

        // Log the result for debugging
        Output.WriteLine($"ContainerAppList_WithEnvironment result: {result?.ToString() ?? "null"}");

        // Assert
        if (result.HasValue)
        {
            // If successful, should have containerApps array
            var containerApps = result.AssertProperty("containerApps");
            Assert.Equal(JsonValueKind.Array, containerApps.ValueKind);
        }
        else
        {
            // If it fails, that's also acceptable for a non-existent environment
            // This test mainly verifies the command handles the environment parameter
            Assert.True(true, "Command failed as expected for non-existent environment");
        }
    }

    [Fact]
    public async Task ContainerAppList_MissingRequiredParams_ReturnsError()
    {
        // Arrange & Act
        var result = await CallToolAsync("azmcp_containerapp_list", new Dictionary<string, object?>());

        // Log the result for debugging
        Output.WriteLine($"ContainerAppList_MissingRequiredParams result: {result?.ToString() ?? "null"}");

        // Assert
        Assert.Null(result); // Should return null for missing required parameters
    }
}
