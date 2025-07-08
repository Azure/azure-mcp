using Newtonsoft.Json;

namespace AzureMcp.Models.LoadTesting.LoadTest;

public class PassFailCriteria
{
    [JsonProperty("passFailMetrics")]
    public Dictionary<string, object>? PassFailMetrics { get; set; } = new();

    [JsonProperty("passFailServerMetrics")]
    public Dictionary<string, object>? PassFailServerMetrics { get; set; } = new();
}