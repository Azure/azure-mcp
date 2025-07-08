using System.Text.Json.Serialization;

namespace AzureMcp.Areas.LoadTesting.Models.LoadTest;

public class InputArtifacts
{
    [JsonPropertyName("testScriptFileInfo")]
    public TestScriptFileInfo? TestScriptFileInfo { get; set; }

    [JsonPropertyName("additionalFileInfo")]
    public List<AdditionalFileInfo>? AdditionalFileInfo { get; set; } = new();
}