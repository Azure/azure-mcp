using Newtonsoft.Json;

namespace AzureMcp.Models.LoadTesting.LoadTest;

public class TestScriptFileInfo
{
    [JsonProperty("url")]
    public string Url { get; set; } = string.Empty;

    [JsonProperty("fileName")]
    public string FileName { get; set; } = string.Empty;

    [JsonProperty("fileType")]
    public string FileType { get; set; } = string.Empty;

    [JsonProperty("expireDateTime")]
    public DateTimeOffset ExpireDateTime { get; set; }

    [JsonProperty("validationStatus")]
    public string ValidationStatus { get; set; } = string.Empty;
}