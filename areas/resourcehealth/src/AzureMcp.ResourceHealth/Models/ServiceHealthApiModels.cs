// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.ResourceHealth.Models;

/// <summary>
/// Response models for Service Health API deserialization - Updated for 2025-05-01 API
/// </summary>
public record ServiceHealthEventsResponse(ServiceHealthEventData[]? Value, string? NextLink);

public record ServiceHealthEventData(string? Id, string? Name, string? Type, ServiceHealthEventProperties? Properties);

public record ServiceHealthEventProperties(
    string? Title,
    string? EventType,
    string? Status,
    string? Level,
    string? Summary,
    string? Description,
    string? TrackingId,
    [property: JsonPropertyName("impactStartTime")] DateTimeOffset? ImpactStartTime,
    [property: JsonPropertyName("impactMitigationTime")] DateTimeOffset? ImpactMitigationTime,
    [property: JsonPropertyName("lastUpdateTime")] DateTimeOffset? LastUpdateTime,
    [property: JsonPropertyName("eventSource")] string? EventSource,
    [property: JsonPropertyName("eventLevel")] string? EventLevel,
    [property: JsonPropertyName("isEventSensitive")] bool? IsEventSensitive,
    [property: JsonPropertyName("platformInitiated")] bool? PlatformInitiated,
    int? Priority,
    ServiceHealthImpact[]? Impact)
{
    // Computed properties for backward compatibility
    public DateTimeOffset? EventStartTime => ImpactStartTime;
    public DateTimeOffset? EventEndTime => ImpactMitigationTime;
}

public record ServiceHealthImpact(
    [property: JsonPropertyName("impactedService")] string? ImpactedService,
    [property: JsonPropertyName("impactedServiceGuid")] string? ImpactedServiceGuid,
    [property: JsonPropertyName("impactedRegions")] ServiceHealthImpactedRegion[]? ImpactedRegions);

public record ServiceHealthImpactedRegion(
    [property: JsonPropertyName("impactedRegion")] string? ImpactedRegion,
    string? Status,
    [property: JsonPropertyName("impactedSubscriptions")] string[]? ImpactedSubscriptions,
    [property: JsonPropertyName("impactedTenants")] string[]? ImpactedTenants,
    [property: JsonPropertyName("lastUpdateTime")] DateTimeOffset? LastUpdateTime,
    ServiceHealthUpdate[]? Updates)
{
    // For backward compatibility
    public string? RegionName => ImpactedRegion;
}

public record ServiceHealthUpdate(
    string? Summary,
    [property: JsonPropertyName("updateDateTime")] DateTimeOffset? UpdateDateTime,
    [property: JsonPropertyName("eventTags")] string[]? EventTags);

// Legacy models for backward compatibility
public record ServiceHealthImpactedService(string? ServiceName);
public record ServiceHealthImpactedResource(string? ResourceName);
