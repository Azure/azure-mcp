// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.ResourceHealth.Options;

namespace AzureMcp.ResourceHealth.Options.ServiceHealthEvents;

public class ServiceHealthEventsListOptions : BaseResourceHealthOptions
{
    /// <summary> Filter expression to apply to the service health events. </summary>
    public string? Filter { get; set; }

    /// <summary> Type of health event to filter by. </summary>
    public string? EventType { get; set; }

    /// <summary> Status of the health event to filter by. </summary>
    public string? Status { get; set; }

    /// <summary> Tracking ID of the health event to filter by. </summary>
    public string? TrackingId { get; set; }

    /// <summary> Start time for the query. </summary>
    public string? QueryStartTime { get; set; }

    /// <summary> End time for the query. </summary>
    public string? QueryEndTime { get; set; }

    /// <summary> Maximum number of events to return. </summary>
    public int? Top { get; set; }
}
