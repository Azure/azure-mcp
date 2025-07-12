// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Areas.Support.LiveTests;

[Trait("Area", "Support")]
[Trait("Category", "Live")]
public class SupportCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output), IClassFixture<LiveTestFixture>
{
    [Fact]
    public async Task Should_List_Support_Tickets()
    {
        var result = await CallToolAsync(
            "azmcp-support-ticket-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "top", "10" }
            });

        Assert.NotNull(result);
    }

    [Fact] 
    public async Task Should_Handle_Filter_Parameter()
    {
        var result = await CallToolAsync(
            "azmcp-support-ticket-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "filter", "Open" },
                { "top", "5" }
            });

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_Return_400_For_Invalid_Subscription()
    {
        var result = await CallToolAsync(
            "azmcp-support-ticket-list",
            new()
            {
                { "subscription", "invalid-subscription-id" }
            });

        Assert.NotNull(result);
        
        // Verify it's an error response
        var errorMessage = result.Value.GetProperty("message").GetString();
        Assert.Contains("Could not find subscription", errorMessage);
        Assert.Contains("invalid-subscription-id", errorMessage);
        
        var errorType = result.Value.GetProperty("type").GetString();
        Assert.Equal("InvalidOperationException", errorType);
    }

    [Fact]
    public async Task Should_Return_400_For_Missing_Subscription()
    {
        var result = await CallToolAsync(
            "azmcp-support-ticket-list",
            new());

        // For validation errors (missing required parameters), the result may be null
        // as the MCP client throws exceptions that are converted to null results
        Assert.Null(result);
    }

    [Theory]
    [InlineData("subscription")]
    public async Task Should_Support_Subscription_Name_Resolution(string subscriptionParam)
    {
        var result = await CallToolAsync(
            "azmcp-support-ticket-list",
            new()
            {
                { subscriptionParam, Settings.SubscriptionName },
                { "top", "5" }
            });

        // Should succeed or return a reasonable error (403 if no permission, etc.)
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_Handle_Service_Name_Natural_Language_Filter()
    {
        var result = await CallToolAsync(
            "azmcp-support-ticket-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "filter", "serviceName eq 'Billing'" },
                { "top", "5" }
            });

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_Handle_Problem_Classification_Natural_Language_Filter()
    {
        var result = await CallToolAsync(
            "azmcp-support-ticket-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "filter", "problemClassification eq 'pricing'" },
                { "top", "5" }
            });

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_Handle_Combined_Service_And_Problem_Classification_Filter()
    {
        var result = await CallToolAsync(
            "azmcp-support-ticket-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "filter", "serviceName eq 'Billing' and problemClassification eq 'pricing'" },
                { "top", "5" }
            });

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_Handle_Service_Name_With_Partial_Match()
    {
        var result = await CallToolAsync(
            "azmcp-support-ticket-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "filter", "serviceName eq 'Virtual Machine'" },
                { "top", "5" }
            });

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_Handle_Status_Filter_With_Natural_Language()
    {
        var result = await CallToolAsync(
            "azmcp-support-ticket-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "filter", "Status eq 'Open' and serviceName eq 'Storage'" },
                { "top", "5" }
            });

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_Handle_Date_Filter_With_Natural_Language()
    {
        var result = await CallToolAsync(
            "azmcp-support-ticket-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "filter", "CreatedDate ge 2024-01-01T00:00:00Z and serviceName eq 'Billing'" },
                { "top", "5" }
            });

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_Handle_Invalid_Service_Name_Gracefully()
    {
        var result = await CallToolAsync(
            "azmcp-support-ticket-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "filter", "serviceName eq 'NonExistentService'" },
                { "top", "5" }
            });

        // Should still return a result, even if the service name doesn't resolve
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_Handle_Invalid_Problem_Classification_Gracefully()
    {
        var result = await CallToolAsync(
            "azmcp-support-ticket-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "filter", "problemClassification eq 'NonExistentClassification'" },
                { "top", "5" }
            });

        // Should still return a result, even if the problem classification doesn't resolve
        Assert.NotNull(result);
    }
}
