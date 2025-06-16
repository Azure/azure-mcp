// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Models.Monitor;

public class ApplicationDiagnosticResult
{
    [JsonPropertyName("operationName")]
    public string OperationName { get; set; } = string.Empty;
    
    [JsonPropertyName("resultCode")]
    public string ResultCode { get; set; } = string.Empty;
    
    [JsonPropertyName("itemType")]
    public string ItemType { get; set; } = string.Empty;
    
    [JsonPropertyName("failedRequestCount")]
    public int FailedRequestCount { get; set; }
    
    [JsonPropertyName("traces")]
    public List<TraceInfo> Traces { get; set; } = new List<TraceInfo>();
}

public class TraceInfo
{
    [JsonPropertyName("traceId")]
    public string TraceId { get; set; } = string.Empty;
    
    [JsonPropertyName("spanId")]
    public string SpanId { get; set; } = string.Empty;
}
