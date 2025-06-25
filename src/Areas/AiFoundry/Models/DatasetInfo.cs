using System.Text.Json.Serialization;

namespace AzureMcp.Areas.AiFoundry.Models;

public record DatasetInfo
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;
    
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
    
    [JsonPropertyName("description")]
    public string? Description { get; init; }
    
    [JsonPropertyName("dataType")]
    public string? DataType { get; init; }
    
    [JsonPropertyName("version")]
    public string? Version { get; init; }
    
    [JsonPropertyName("tags")]
    public Dictionary<string, string>? Tags { get; init; }
    
    [JsonPropertyName("createdDateTime")]
    public DateTimeOffset? CreatedDateTime { get; init; }
    
    [JsonPropertyName("modifiedDateTime")]
    public DateTimeOffset? ModifiedDateTime { get; init; }
    
    [JsonPropertyName("size")]
    public long? Size { get; init; }
    
    [JsonPropertyName("itemCount")]
    public int? ItemCount { get; init; }
}

public record DatasetListResult
{
    [JsonPropertyName("datasets")]
    public DatasetInfo[] Datasets { get; init; } = [];
} 