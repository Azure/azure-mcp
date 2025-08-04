// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text;
using System.Text.Json.Serialization;

namespace AzureMcp.ApplicationInsights.Models;

public class DistributedTraceResult
{
    [JsonPropertyName("traceId")]
    public string TraceId { get; set; } = string.Empty;

    [JsonPropertyName("startTime")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? StartTime { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("details")]
    public string? TraceDetails { get; set; }

    [JsonPropertyName("relevantSpans")]
    public List<SpanDetails> RelevantSpans { get; set; } = new List<SpanDetails>();

    public static DistributedTraceResult Create(string traceId, List<SpanSummary> spans)
    {
        if (spans.Count == 0)
        {
            return new DistributedTraceResult
            {
                Description = "No spans available for the selected trace and span. Try a different traceId, spanId, or time range filter",
                TraceDetails = null,
                StartTime = null,
                TraceId = traceId
            };
        }

        string description = $"This represents a distributed trace. Parent/child relationships are represented by indentation. Columns: ItemId, ItemType, Name, Success, ResultCode, StartToEnd (milliseconds)";

        DateTime startTime = spans.Min(s => s.StartTime);

        spans = spans.OrderBy(s => s.StartTime).ToList();

        StringBuilder results = new StringBuilder();

        HashSet<int> visited = new HashSet<int>();
        List<SpanSummary> allSpans = new List<SpanSummary>();

        foreach (var span in spans)
        {
            AddSpan("", results, span, startTime, visited, allSpans);
        }

        return new DistributedTraceResult
        {
            Description = description,
            TraceDetails = results.ToString(),
            StartTime = startTime,
            TraceId = traceId,
            RelevantSpans = allSpans.Where(t => t.ItemType == "exception")
                .Select(t => new SpanDetails
                {
                    ItemId = t.ItemId,
                    Properties = t.Properties
                }).ToList()
        };
    }

    private static void AddSpan(string indent, StringBuilder results, SpanSummary span, DateTime startTime, HashSet<int> visited, List<SpanSummary> allSpans)
    {
        if (visited.Contains(span.RowId))
        {
            // Avoid processing the same span multiple times
            return;
        }
        visited.Add(span.RowId);
        string success = span.ItemType == "exception" ? "⚠️" : span.IsSuccessful.HasValue ? span.IsSuccessful.Value ? "✅" : "❌" : "❓";
        double start = (span.StartTime - startTime).TotalMilliseconds;
        double end = (span.EndTime - startTime).TotalMilliseconds;
        string duration = $"{Math.Round(start, 2)}->{Math.Round(end, 2)}";
        results.AppendLine($"{indent}{span.ItemId}, {span.ItemType}, {span.Name}, {success}, {span.ResponseCode}, {duration}");
        allSpans.Add(span);

        var childSpans = span.ChildSpans.OrderBy(span => span.StartTime).ToList();

        foreach (var child in childSpans)
        {
            // logic to avoid infinite loops in case of circular references
            if (!visited.Contains(child.RowId))
            {
                AddSpan(indent + "    ", results, child, startTime, visited, allSpans);
            }
        }
    }
}
