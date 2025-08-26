// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.ResourceHealth.Models;

public class ServiceHealthEvent
{
    /// <summary> The unique ID of the service health event. </summary>
    public string? Id { get; set; }

    /// <summary> The name of the service health event. </summary>
    public string? Name { get; set; }

    /// <summary> The title of the service health event. </summary>
    public string? Title { get; set; }

    /// <summary> The type of health event (ServiceIssue, PlannedMaintenance, HealthAdvisory, SecurityAdvisory). </summary>
    public string? EventType { get; set; }

    /// <summary> The current status of the health event (Active, Resolved). </summary>
    public string? Status { get; set; }

    /// <summary> The level of the health event (Critical, Error, Warning, Informational). </summary>
    public string? Level { get; set; }

    /// <summary> A summary description of the health event. </summary>
    public string? Summary { get; set; }

    /// <summary> Detailed description of the health event. </summary>
    public string? Description { get; set; }

    /// <summary> The header text of the health event. </summary>
    public string? Header { get; set; }

    /// <summary> The impact statement describing the scope and severity. </summary>
    public string? ImpactStatement { get; set; }

    /// <summary> The platform where the event occurred (e.g., Azure, Office 365). </summary>
    public string? Platform { get; set; }

    /// <summary> The stage of the health event lifecycle. </summary>
    public string? Stage { get; set; }

    /// <summary> The tracking ID for the health event. </summary>
    public string? TrackingId { get; set; }

    /// <summary> The communication ID for the health event. </summary>
    public string? CommunicationId { get; set; }

    /// <summary> The time when the health event started. </summary>
    public DateTimeOffset? StartTime { get; set; }

    /// <summary> The time when the health event ended. </summary>
    public DateTimeOffset? EndTime { get; set; }

    /// <summary> The time of the last update to the health event. </summary>
    public DateTimeOffset? LastUpdateTime { get; set; }

    /// <summary> The time when the health event was first reported. </summary>
    public DateTimeOffset? EventCreationTime { get; set; }

    /// <summary> List of Azure services affected by this health event. </summary>
    public List<string>? AffectedServices { get; set; }

    /// <summary> List of Azure regions affected by this health event. </summary>
    public List<string>? AffectedRegions { get; set; }

    /// <summary> List of Azure resource types affected by this health event. </summary>
    public List<string>? AffectedResourceTypes { get; set; }

    /// <summary> The subscription ID where this event is reported. </summary>
    public string? SubscriptionId { get; set; }

    /// <summary> Additional properties of the health event. </summary>
    public Dictionary<string, object>? AdditionalProperties { get; set; }
}
