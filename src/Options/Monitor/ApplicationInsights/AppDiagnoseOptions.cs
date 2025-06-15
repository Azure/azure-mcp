// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Monitor.ApplicationInsights;

public class AppDiagnoseOptions : BaseMonitorOptions, IApplicationInsightsOptions
{
    [JsonPropertyName(OptionDefinitions.Monitor.AppIdName)]
    public string? AppId { get; set; }
    
    [JsonPropertyName(OptionDefinitions.Monitor.StartTimeName)]
    public string? StartTime { get; set; }
    
    [JsonPropertyName(OptionDefinitions.Monitor.EndTimeName)]
    public string? EndTime { get; set; }

    [JsonPropertyName(OptionDefinitions.Monitor.SymptomsName)]
    public string? Symptoms { get; set; }
}
