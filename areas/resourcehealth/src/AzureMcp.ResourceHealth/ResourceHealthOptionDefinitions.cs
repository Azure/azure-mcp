// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.ResourceHealth;

public static class ResourceHealthOptionDefinitions
{
    public const string ResourceIdName = "resourceId";
    public const string FilterName = "filter";
    public const string EventTypeName = "event-type";
    public const string StatusName = "status";
    public const string TrackingIdName = "tracking-id";
    public const string QueryStartTimeName = "query-start-time";
    public const string QueryEndTimeName = "query-end-time";
    public const string TopName = "top";
    public const string ExpandName = "expand";

    public static readonly Option<string> ResourceId = new(
        $"--{ResourceIdName}",
        "The Azure resource ID to get health status for (e.g., /subscriptions/{sub}/resourceGroups/{rg}/providers/Microsoft.Compute/virtualMachines/{vm})."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> Filter = new(
        $"--{FilterName}",
        "Filter expression to apply to the service health events (e.g., 'eventType eq ServiceIssue')."
    );

    public static readonly Option<string> EventType = new(
        $"--{EventTypeName}",
        "Type of health event (ServiceIssue, PlannedMaintenance, HealthAdvisory, SecurityAdvisory)."
    );

    public static readonly Option<string> Status = new(
        $"--{StatusName}",
        "Status of the health event (Active, Resolved)."
    );

    public static readonly Option<string> TrackingId = new(
        $"--{TrackingIdName}",
        "Tracking ID of the health event to filter by."
    );

    public static readonly Option<string> QueryStartTime = new(
        $"--{QueryStartTimeName}",
        "Start time for the query in ISO 8601 format (e.g., '2023-01-01T00:00:00Z')."
    );

    public static readonly Option<string> QueryEndTime = new(
        $"--{QueryEndTimeName}",
        "End time for the query in ISO 8601 format (e.g., '2023-12-31T23:59:59Z')."
    );

    public static readonly Option<int?> Top = new(
        $"--{TopName}",
        "Maximum number of events to return."
    );

    public static readonly Option<string> Expand = new(
        $"--{ExpandName}",
        "Comma-separated list of properties to expand in the response (e.g., 'impact', 'recommendedActions')."
    );
}
