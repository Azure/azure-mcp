// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Monitor.ApplicationInsights;

public class AppTraceOptions : BaseMonitorOptions, IApplicationInsightsOptions
{
    [JsonPropertyName(OptionDefinitions.Monitor.AppIdName)]
    public string? AppId { get; set; }
    
    [JsonPropertyName("traceId")]
    public string? TraceId { get; set; }
    
    [JsonPropertyName("spanId")]
    public string? SpanId { get; set; }
    
    [JsonPropertyName(OptionDefinitions.Monitor.StartTimeName)]
    public string? StartTime { get; set; }
    
    [JsonPropertyName(OptionDefinitions.Monitor.EndTimeName)]
    public string? EndTime { get; set; }
}
