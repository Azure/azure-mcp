using System.Text.Json.Serialization;

namespace AzureMcp.Areas.LoadTesting.Models.LoadTest;

public class PassFailCriteria
{
    [JsonPropertyName("passFailMetrics")]
    public Dictionary<string, object>? PassFailMetrics { get; set; } = new();

    [JsonPropertyName("passFailServerMetrics")]
    public Dictionary<string, object>? PassFailServerMetrics { get; set; } = new();
}