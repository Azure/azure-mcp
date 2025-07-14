// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Tests.Client;
using AzureMcp.Tests.Commands;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace AzureMcp.Tests.Areas.ContainerApps.LiveTests;

[Trait("Area", "ContainerApps")]
[Trait("Category", "Live")]
public class ContainerAppsCommandTests : CommandTestsBase, IClassFixture<LiveTestFixture>
{
    public ContainerAppsCommandTests(LiveTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task Should_ListContainerApps_WithSubscriptionId()
    {
        // Arrange & Act
        var result = await CallToolAsync(
            "azmcp-containerapps-list",
            new()
            {
                { "subscription", Settings.SubscriptionId }
            });

        // Assert
        Assert.Equal(200, result.GetProperty("status").GetInt32());
        
        // The test infrastructure deploys a Container App, so we should have at least one
        if (result.TryGetProperty("results", out var resultsProperty) && resultsProperty.ValueKind != JsonValueKind.Null)
        {
            var containerApps = resultsProperty.GetProperty("containerApps");
            Assert.Equal(JsonValueKind.Array, containerApps.ValueKind);

            // Verify the test Container App is present
            var testAppFound = false;
            foreach (var app in containerApps.EnumerateArray())
            {
                var name = app.GetProperty("name").GetString();
                if (name?.Contains("testapp") == true)
                {
                    testAppFound = true;
                    Assert.True(app.TryGetProperty("location", out _));
                    Assert.True(app.TryGetProperty("status", out _));
                    break;
                }
            }
            
            Assert.True(testAppFound, "Test Container App should be found in results");
        }
    }

    [Fact]
    public async Task Should_ListContainerApps_WithSubscriptionName()
    {
        // Arrange & Act
        var result = await CallToolAsync(
            "azmcp-containerapps-list",
            new()
            {
                { "subscription", Settings.SubscriptionName }
            });

        // Assert
        Assert.Equal(200, result.GetProperty("status").GetInt32());
    }

    [Fact]
    public async Task Should_ListContainerApps_WithResourceGroupFilter()
    {
        // Test with the specific resource group where our test app is deployed
        var result = await CallToolAsync(
            "azmcp-containerapps-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName }
            });

        // Should succeed and find our test Container App
        Assert.Equal(200, result.GetProperty("status").GetInt32());
        
        if (result.TryGetProperty("results", out var resultsProperty) && resultsProperty.ValueKind != JsonValueKind.Null)
        {
            var containerApps = resultsProperty.GetProperty("containerApps");
            Assert.Equal(JsonValueKind.Array, containerApps.ValueKind);
        }
    }

    [Theory]
    [InlineData("--invalid-param")]
    [InlineData("")]
    public async Task Should_Return400_WithInvalidInput(string args)
    {
        var result = await CallToolAsync(
            $"azmcp-containerapps-list {args}");

        Assert.NotEqual(200, result.GetProperty("status").GetInt32());
    }
}