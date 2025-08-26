// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.ResourceHealth.Options;

namespace AzureMcp.ResourceHealth.Options.ResourceEvents;

public class ResourceEventsGetOptions : BaseResourceHealthOptions
{
    /// <summary>
    /// The Azure resource ID to get health events for.
    /// </summary>
    public string ResourceId { get; set; } = string.Empty;

    /// <summary>
    /// Filter expression to apply to the resource events.
    /// </summary>
    public string? Filter { get; set; }

    /// <summary>
    /// Start time for the query in ISO 8601 format.
    /// </summary>
    public string? QueryStartTime { get; set; }

    /// <summary>
    /// End time for the query in ISO 8601 format.
    /// </summary>
    public string? QueryEndTime { get; set; }

    /// <summary>
    /// Maximum number of events to return.
    /// </summary>
    public int? Top { get; set; }

    /// <summary>
    /// Comma-separated list of properties to expand in the response.
    /// </summary>
    public string? Expand { get; set; }
}
