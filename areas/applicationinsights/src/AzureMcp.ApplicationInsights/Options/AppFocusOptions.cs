// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.ApplicationInsights.Options;

public class AppFocusOptions : BaseAppOptions
{
    [JsonPropertyName(ApplicationInsightsOptionDefinitions.SymptomName)]
    public string? Symptom { get; set; }

    [JsonPropertyName(ApplicationInsightsOptionDefinitions.StartTimeName)]
    public string? StartTime { get; set; }

    [JsonPropertyName(ApplicationInsightsOptionDefinitions.EndTimeName)]
    public string? EndTime { get; set; }
}
