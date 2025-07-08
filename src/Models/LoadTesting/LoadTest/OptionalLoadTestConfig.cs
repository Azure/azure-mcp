using Newtonsoft.Json;

namespace AzureMcp.Models.LoadTesting.LoadTest;

public class OptionalLoadTestConfig
{
    [JsonProperty("duration")]
    public int? Duration { get; set; }

    [JsonProperty("endpointUrl")]
    public string? EndpointUrl { get; set; } = string.Empty;

    [JsonProperty("maxResponseTimeInMs")]
    public int? MaxResponseTimeInMs { get; set; }

    [JsonProperty("rampUpTime")]
    public int? RampUpTime { get; set; }

    [JsonProperty("requestsPerSecond")]
    public int? RequestsPerSecond { get; set; }

    [JsonProperty("virtualUsers")]
    public int?  VirtualUsers { get; set; }
}