using Newtonsoft.Json;

namespace AzureMcp.Models.LoadTesting.LoadTest;

public class InputArtifacts
{
    [JsonProperty("testScriptFileInfo")]
    public TestScriptFileInfo? TestScriptFileInfo { get; set; }

    [JsonProperty("additionalFileInfo")]
    public List<AdditionalFileInfo>? AdditionalFileInfo { get; set; } = new();
}