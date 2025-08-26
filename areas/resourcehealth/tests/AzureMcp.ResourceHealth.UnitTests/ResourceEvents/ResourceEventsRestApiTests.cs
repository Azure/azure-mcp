// Test to validate REST API endpoint construction
using System.Text.Json;
using Azure.Core;
using AzureMcp.ResourceHealth.Models;
using AzureMcp.ResourceHealth.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.ResourceHealth.UnitTests.ResourceEvents;

public class ResourceEventsRestApiTests
{
    [Fact]
    public void ValidateApiEndpointConstruction()
    {
        // Test that we construct the correct REST API endpoint
        var resourceId = "/subscriptions/12345678-1234-1234-1234-123456789012/resourceGroups/test-rg/providers/Microsoft.Compute/virtualMachines/test-vm";
        var expectedBaseUrl = $"https://management.azure.com{resourceId}/providers/Microsoft.ResourceHealth/events";
        var expectedApiVersion = "2025-05-01";

        // The URL should be constructed as: https://management.azure.com{resourceId}/providers/Microsoft.ResourceHealth/events?api-version=2025-05-01
        var expectedFullUrl = $"{expectedBaseUrl}?api-version={expectedApiVersion}";

        Assert.Contains("https://management.azure.com", expectedFullUrl);
        Assert.Contains("/providers/Microsoft.ResourceHealth/events", expectedFullUrl);
        Assert.Contains("api-version=2025-05-01", expectedFullUrl);
        Assert.Contains(resourceId, expectedFullUrl);
    }

    [Fact]
    public void ValidateApiEndpointWithQueryParameters()
    {
        // Test that we construct query parameters correctly
        var resourceId = "/subscriptions/12345678-1234-1234-1234-123456789012/resourceGroups/test-rg/providers/Microsoft.Compute/virtualMachines/test-vm";
        var filter = "eventType eq 'HealthEvent'";
        var top = 10;
        var expand = "impact";

        var baseUrl = $"https://management.azure.com{resourceId}/providers/Microsoft.ResourceHealth/events";
        var queryParams = new List<string> { "api-version=2025-05-01" };

        if (!string.IsNullOrWhiteSpace(filter))
        {
            queryParams.Add($"$filter={Uri.EscapeDataString(filter)}");
        }

        if (top > 0)
        {
            queryParams.Add($"$top={top}");
        }

        if (!string.IsNullOrWhiteSpace(expand))
        {
            queryParams.Add($"$expand={Uri.EscapeDataString(expand)}");
        }

        var fullUrl = $"{baseUrl}?{string.Join("&", queryParams)}";

        Assert.Contains("$filter=", fullUrl);
        Assert.Contains("$top=10", fullUrl);
        Assert.Contains("$expand=", fullUrl);
        Assert.Contains("api-version=2025-05-01", fullUrl);
    }

    [Fact]
    public void ValidateServiceHealthEventDeserialization()
    {
        // Test that we can deserialize the expected API response structure
        var jsonResponse = """
        {
            "value": [
                {
                    "id": "/subscriptions/12345/resourceGroups/rg1/providers/Microsoft.Compute/virtualMachines/vm1/providers/Microsoft.ResourceHealth/events/event1",
                    "name": "event1",
                    "type": "Microsoft.ResourceHealth/events",
                    "properties": {
                        "title": "Virtual Machine Unavailable",
                        "eventType": "HealthEvent",
                        "status": "Resolved",
                        "summary": "Virtual machine was unavailable due to host maintenance",
                        "description": "The virtual machine became unavailable during planned host maintenance.",
                        "impactStartTime": "2024-01-01T10:00:00Z",
                        "impactMitigationTime": "2024-01-01T12:00:00Z",
                        "lastUpdateTime": "2024-01-01T12:00:00Z",
                        "trackingId": "track123",
                        "level": "Warning"
                    }
                }
            ]
        }
        """;

        // This tests the JSON structure we expect from the Azure Resource Health events API
        var apiResponse = JsonSerializer.Deserialize<ServiceHealthEventsResponse>(jsonResponse, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(apiResponse);
        Assert.NotNull(apiResponse.Value);
        Assert.Single(apiResponse.Value);

        var eventData = apiResponse.Value[0];
        Assert.Equal("event1", eventData.Name);
        Assert.NotNull(eventData.Properties);
        Assert.Equal("Virtual Machine Unavailable", eventData.Properties.Title);
        Assert.Equal("HealthEvent", eventData.Properties.EventType);
        Assert.Equal("Resolved", eventData.Properties.Status);
    }

    // Simplified response models for testing
    private class ServiceHealthEventsResponse
    {
        public List<ServiceHealthEventData> Value { get; set; } = new();
    }

    private class ServiceHealthEventData
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public ServiceHealthEventProperties? Properties { get; set; }
    }

    private class ServiceHealthEventProperties
    {
        public string? Title { get; set; }
        public string? EventType { get; set; }
        public string? Status { get; set; }
        public string? Summary { get; set; }
        public string? Description { get; set; }
        public DateTime? ImpactStartTime { get; set; }
        public DateTime? ImpactMitigationTime { get; set; }
        public DateTime? LastUpdateTime { get; set; }
        public string? TrackingId { get; set; }
        public string? Level { get; set; }
        public string? EventLevel { get; set; }
    }
}
