// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.ResourceHealth.LiveTests;

[Trait("Area", "ResourceHealth")]
[Trait("Category", "Live")]
public class ResourceHealthCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output) 
    : CommandTestsBase(liveTestFixture, output), IClassFixture<LiveTestFixture>
{
    [Fact]
    public async Task Should_get_availability_status_with_storage_account_resource_id()
    {
        // Use a storage account resource ID for testing since it's commonly available
        var resourceId = $"/subscriptions/{Settings.SubscriptionId}/resourceGroups/{Settings.ResourceGroupName}/providers/Microsoft.Storage/storageAccounts/{Settings.ResourceBaseName}";
        
        var result = await CallToolAsync(
            "azmcp_resourcehealth_availability-status_get",
            new()
            {
                { "resourceId", resourceId }
            });

        var status = result.AssertProperty("status");
        Assert.Equal(JsonValueKind.Object, status.ValueKind);
        
        // Verify key properties are present
        var resourceIdProperty = status.GetProperty("resourceId");
        Assert.Equal(resourceId, resourceIdProperty.GetString());
        
        var availabilityState = status.GetProperty("availabilityState");
        Assert.True(availabilityState.ValueKind == JsonValueKind.String);
    }

    [Fact]
    public async Task Should_get_availability_status_with_retry_policy()
    {
        var resourceId = $"/subscriptions/{Settings.SubscriptionId}/resourceGroups/{Settings.ResourceGroupName}/providers/Microsoft.Storage/storageAccounts/{Settings.ResourceBaseName}";
        
        var result = await CallToolAsync(
            "azmcp_resourcehealth_availability-status_get",
            new()
            {
                { "resourceId", resourceId },
                { "retry-max-retries", 3 },
                { "retry-delay-seconds", 2 }
            });

        var status = result.AssertProperty("status");
        Assert.Equal(JsonValueKind.Object, status.ValueKind);
        
        var resourceIdProperty = status.GetProperty("resourceId");
        Assert.Equal(resourceId, resourceIdProperty.GetString());
    }

    [Theory]
    [InlineData("invalid-resource-id")]
    [InlineData("/subscriptions/invalid/resourceGroups/invalid/providers/Microsoft.Compute/virtualMachines/invalid")]
    public async Task Should_return_error_for_invalid_resource_id(string invalidResourceId)
    {
        var result = await CallToolAsync(
            "azmcp_resourcehealth_availability-status_get",
            new()
            {
                { "resourceId", invalidResourceId }
            });

        Assert.False(result.HasValue);
    }

    [Fact]
    public async Task Should_return_validation_error_when_resource_id_missing()
    {
        var result = await CallToolAsync(
            "azmcp_resourcehealth_availability-status_get",
            new());

        // Should return null or empty result since resourceId is required
        Assert.False(result.HasValue);
    }
}
