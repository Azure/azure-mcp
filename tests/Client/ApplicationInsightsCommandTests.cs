// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Client;

public class ApplicationInsightsCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output) 
    : CommandTestsBase(liveTestFixture, output),
      IClassFixture<LiveTestFixture>
{    [Fact]
    [Trait("Category", "Live")]
    [Trait("Category", "Skip")] // Skip this test as it requires actual Azure resources
    public async Task DiagnoseApplication_ReturnsResults()
    {
        // Arrange
        var appId = $"/subscriptions/{Settings.SubscriptionId}/resourceGroups/test-rg/providers/microsoft.insights/components/test-app";
        
        // Act
        var result = await CallToolAsync(
            "azmcp-monitor-app-diagnose",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "app-id", appId },
                { "symptoms", "Application is experiencing slow response times" }
            });
        
        // Assert
        var diagnosticResults = result.AssertProperty("results");
        Assert.Equal(JsonValueKind.Array, diagnosticResults.ValueKind);
    }
}
