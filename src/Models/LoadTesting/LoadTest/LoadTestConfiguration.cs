using Newtonsoft.Json;

namespace AzureMcp.Models.LoadTesting.LoadTest;

public class LoadTestConfiguration
{
    [JsonProperty("engineInstances")]
    public int EngineInstances { get; set; } = 1;

    [JsonProperty("splitAllCSVs")]
    public bool SplitAllCSVs { get; set; } = false;

    [JsonProperty("quickStartTest")]
    public bool QuickStartTest { get; set; } = false;

    [JsonProperty("optionalLoadTestConfig")]
    public OptionalLoadTestConfig? OptionalLoadTestConfig { get; set; } = null;

}