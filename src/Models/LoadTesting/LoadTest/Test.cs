using Newtonsoft.Json;

namespace AzureMcp.Models.LoadTesting.LoadTest;

public class Test
{
    [JsonProperty("environmentVariables")]
    public Dictionary<string, string> EnvironmentVariables { get; set; } = new();

    [JsonProperty("loadTestConfiguration")]
    public LoadTestConfiguration LoadTestConfiguration { get; set; } = new();

    [JsonProperty("inputArtifacts")]
    public InputArtifacts InputArtifacts { get; set; } = new();

    [JsonProperty("kind")]
    public string Kind { get; set; } = string.Empty;

    [JsonProperty("publicIPDisabled")]
    public bool PublicIPDisabled { get; set; }

    [JsonProperty("metricsReferenceIdentityType")]
    public string MetricsReferenceIdentityType { get; set; } = string.Empty;

    [JsonProperty("testId")]
    public string TestId { get; set; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("displayName")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonProperty("createdDateTime")]
    public DateTimeOffset CreatedDateTime { get; set; }

    [JsonProperty("createdBy")]
    public string CreatedBy { get; set; } = string.Empty;

    [JsonProperty("lastModifiedDateTime")]
    public DateTimeOffset LastModifiedDateTime { get; set; }

    [JsonProperty("lastModifiedBy")]
    public string LastModifiedBy { get; set; } = string.Empty;
}