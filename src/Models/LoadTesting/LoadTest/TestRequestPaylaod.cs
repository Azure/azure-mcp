using Newtonsoft.Json;

namespace AzureMcp.Models.LoadTesting.LoadTest;

public class TestRequestPayload
{
    [JsonProperty("testId")]
    public string TestId { get; set; } = string.Empty;

    [JsonProperty("description")]
    public string? Description { get; set; } = string.Empty;

    [JsonProperty("displayName")]
    public string? DisplayName { get; set; } = string.Empty;

    [JsonProperty("loadTestConfiguration")]
    public LoadTestConfiguration? LoadTestConfiguration { get; set; } = new();

    [JsonProperty("kind")]
    public string Kind { get; set; } = "URL";

    [JsonProperty("secrets")]
    public Dictionary<string, string>? Secrets { get; set; } = new();

    [JsonProperty("certificate")]
    public string? Certificate { get; set; }

    [JsonProperty("environmentVariables")]
    public Dictionary<string, string>? EnvironmentVariables { get; set; } = new();

    [JsonProperty("passFailCriteria")]
    public PassFailCriteria? PassFailCriteria { get; set; } = new();

    [JsonProperty("autoStopCriteria")]
    public AutoStopCriteria? AutoStopCriteria { get; set; } = new();

    [JsonProperty("subnetId")]
    public string? SubnetId { get; set; }

    [JsonProperty("publicIPDisabled")]
    public bool PublicIPDisabled { get; set; } = false;

    [JsonProperty("keyvaultReferenceIdentityType")]
    public string KeyvaultReferenceIdentityType { get; set; } = "SystemAssigned";

    [JsonProperty("keyvaultReferenceIdentityId")]
    public string? KeyvaultReferenceIdentityId { get; set; }

    [JsonProperty("metricsReferenceIdentityType")]
    public string MetricsReferenceIdentityType { get; set; } = "SystemAssigned";

    [JsonProperty("metricsReferenceIdentityId")]
    public string? MetricsReferenceIdentityId { get; set; }

    [JsonProperty("engineBuiltinIdentityType")]
    public string EngineBuiltinIdentityType { get; set; } = "None";

    [JsonProperty("engineBuiltinIdentityIds")]
    public string[]? EngineBuiltinIdentityIds { get; set; }
}