using System.Text.Json.Serialization;

namespace AzureMcp.Models.Monitor.ApplicationInsights
{
    public class DistributedTraceResult
    {
        [JsonPropertyName("traceId")]
        public string TraceId { get; set; } = string.Empty;

        [JsonPropertyName("spans")]
        public List<SpanDetails> Spans { get; set; } = new List<SpanDetails>();
    }

    public class SpanDetails
    {
        [JsonPropertyName("spanId")]
        public string SpanId { get; set; } = string.Empty;

        [JsonPropertyName("parentId")]
        public string ParentId { get; set; } = string.Empty;

        [JsonPropertyName("operationName")]
        public string OperationName { get; set; } = string.Empty;

        [JsonPropertyName("responseCode")]
        public string ResponseCode { get; set; } = string.Empty;

        [JsonPropertyName("itemType")]
        public string ItemType { get; set; } = string.Empty;

        [JsonPropertyName("isSuccessful")]
        public bool IsSuccessful { get; set; }

        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public DateTime EndTime { get; set; }

        [JsonPropertyName("duration")]
        public TimeSpan Duration { get; set; }

        [JsonPropertyName("properties")]
        public List<KeyValuePair<string, string>> Properties { get; set; } = new List<KeyValuePair<string, string>>();

        [JsonPropertyName("childSpans")]
        public List<SpanDetails> ChildSpans { get; set; } = new List<SpanDetails>();
    }
}
