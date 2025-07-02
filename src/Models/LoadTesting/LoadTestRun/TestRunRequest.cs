using Newtonsoft.Json;

namespace AzureMcp.Models.LoadTesting.LoadTestRun;

public class TestRunRequest
{
    [JsonProperty("testId")]
    public string TestId { get; set; } = string.Empty;
    
    [JsonProperty("displayName")]
    public string DisplayName { get; set; } = string.Empty;
    
    [JsonProperty("secrets")]
    public IDictionary<string, string> Secrets { get; set; } = new Dictionary<string, string>();
    
    [JsonProperty("certificate")]
    public string? Certificate { get; set; } = null;
    
    [JsonProperty("environmentVariables")]
    public IDictionary<string, string> EnvironmentVariables { get; set; } = new Dictionary<string, string>();
    
    [JsonProperty("description")]
    public string? Description { get; set; } = null;
    
    [JsonProperty("loadTestConfiguration")]
    public LoadTestConfiguration LoadTestConfiguration { get; set; } = new LoadTestConfiguration();
    
    [JsonProperty("debugLogsEnabled")]
    public bool? DebugLogsEnabled { get; set; } = false;
    
    [JsonProperty("requestDataLevel")]
    public RequestDataLevel? RequestDataLevel { get; set; }
}

public class LoadTestConfiguration
{
    [JsonProperty("optionalLoadTestConfig")]
    public object? OptionalLoadTestConfig { get; set; } = null;
}
