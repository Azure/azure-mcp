using Newtonsoft.Json;

namespace AzureMcp.Models.LoadTesting.LoadTest;

public class LoadTestConfiguration
{
    [JsonProperty("engineInstances")]
    public int EngineInstances { get; set; }

    [JsonProperty("splitAllCSVs")]
    public bool SplitAllCSVs { get; set; }

    [JsonProperty("quickStartTest")]
    public bool QuickStartTest { get; set; }
}