using System.Text.Json.Serialization;

namespace AzureMcp.Areas.LoadTesting.Models.LoadTest;

public class AdditionalFileInfo
{
    [JsonPropertyName("url")]
    public string? Url { get; set; } = string.Empty;

    [JsonPropertyName("fileName")]
    public string? FileName { get; set; } = string.Empty;

    [JsonPropertyName("fileType")]
    public string? FileType { get; set; } = string.Empty;

    [JsonPropertyName("expireDateTime")]
    public DateTimeOffset? ExpireDateTime { get; set; }

    [JsonPropertyName("validationStatus")]
    public string? ValidationStatus { get; set; } = string.Empty;
}