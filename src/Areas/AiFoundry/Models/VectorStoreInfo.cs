using System.Text.Json.Serialization;

namespace AzureMcp.Areas.AiFoundry.Models;

public record VectorStoreInfo
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;
    
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
    
    [JsonPropertyName("description")]
    public string? Description { get; init; }
    
    [JsonPropertyName("dimensions")]
    public int? Dimensions { get; init; }
    
    [JsonPropertyName("vectorCount")]
    public long? VectorCount { get; init; }
    
    [JsonPropertyName("indexType")]
    public string? IndexType { get; init; }
    
    [JsonPropertyName("embeddingModel")]
    public string? EmbeddingModel { get; init; }
    
    [JsonPropertyName("tags")]
    public Dictionary<string, string>? Tags { get; init; }
    
    [JsonPropertyName("createdDateTime")]
    public DateTimeOffset? CreatedDateTime { get; init; }
    
    [JsonPropertyName("modifiedDateTime")]
    public DateTimeOffset? ModifiedDateTime { get; init; }
    
    [JsonPropertyName("status")]
    public string? Status { get; init; }
}

public record VectorStoreListResult
{
    [JsonPropertyName("vectorStores")]
    public VectorStoreInfo[] VectorStores { get; init; } = [];
} 