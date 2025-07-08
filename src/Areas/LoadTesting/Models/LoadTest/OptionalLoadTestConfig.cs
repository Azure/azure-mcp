using System.Text.Json.Serialization;

namespace AzureMcp.Areas.LoadTesting.Models.LoadTest;

public class OptionalLoadTestConfig
{
    [JsonPropertyName("duration")]
    public int? Duration { get; set; }

    [JsonPropertyName("endpointUrl")]
    public string? EndpointUrl { get; set; } = string.Empty;

    [JsonPropertyName("maxResponseTimeInMs")]
    public int? MaxResponseTimeInMs { get; set; }

    [JsonPropertyName("rampUpTime")]
    public int? RampUpTime { get; set; }

    [JsonPropertyName("requestsPerSecond")]
    public int? RequestsPerSecond { get; set; }

    [JsonPropertyName("virtualUsers")]
    public int?  VirtualUsers { get; set; }
}