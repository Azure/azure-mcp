using System.Text.Json.Serialization;

namespace AzureMcp.Areas.LoadTesting.Models.LoadTest;

public class LoadTestConfiguration
{
    [JsonPropertyName("engineInstances")]
    public int EngineInstances { get; set; } = 1;

    [JsonPropertyName("splitAllCSVs")]
    public bool SplitAllCSVs { get; set; } = false;

    [JsonPropertyName("quickStartTest")]
    public bool QuickStartTest { get; set; } = false;

    [JsonPropertyName("optionalLoadTestConfig")]
    public OptionalLoadTestConfig? OptionalLoadTestConfig { get; set; } = null;

}