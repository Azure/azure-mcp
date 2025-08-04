// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.ApplicationInsights.Options;

public class AppImpactOptions : BaseAppOptions
{
    /// <summary>
    /// The start time of the investigation in ISO format (optional).
    /// </summary>
    [JsonPropertyName(ApplicationInsightsOptionDefinitions.StartTimeName)]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// The end time of the investigation in ISO format (optional).
    /// </summary>
    [JsonPropertyName(ApplicationInsightsOptionDefinitions.EndTimeName)]
    public DateTime EndTime { get; set; }

    /// <summary>
    /// The table to list traces on
    /// </summary>
    [JsonPropertyName(ApplicationInsightsOptionDefinitions.TableName)]
    public string? Table { get; set; }

    /// <summary>
    /// Filters for the traces
    /// </summary>
    [JsonPropertyName(ApplicationInsightsOptionDefinitions.FiltersName)]
    public string[] Filters { get; set; } = Array.Empty<string>();
}
