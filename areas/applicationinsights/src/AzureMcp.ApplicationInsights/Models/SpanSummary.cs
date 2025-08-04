// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.ApplicationInsights.Models;

public class SpanSummary
{
    [JsonIgnore]
    public int RowId { get; set; }

    [JsonPropertyName("spanId")]
    public string? SpanId { get; set; }

    [JsonPropertyName("itemId")]
    public string? ItemId { get; init; }

    [JsonPropertyName("parentId")]
    [JsonIgnore] // don't serialize parent ID, it's already in child spans
    public string? ParentId { get; set; }

    [JsonPropertyName("responseCode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ResponseCode { get; set; }

    [JsonPropertyName("itemType")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ItemType { get; set; }

    [JsonPropertyName("success")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsSuccessful { get; set; }

    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }

    [JsonPropertyName("startTime")]
    public DateTime StartTime { get; set; }

    [JsonPropertyName("endTime")]
    public DateTime EndTime { get; set; }

    [JsonPropertyName("duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TimeSpan? Duration { get; set; }

    [JsonPropertyName("properties")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<KeyValuePair<string, string>> Properties { get; set; } = new List<KeyValuePair<string, string>>();

    [JsonPropertyName("childSpans")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<SpanSummary> ChildSpans { get; set; } = new List<SpanSummary>();

    [JsonIgnore]
    public SpanSummary? ParentSpan { get; set; } = null;
}
