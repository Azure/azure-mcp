using System.Text.Json.Serialization;

namespace AzureMcp.Models.Foundry;

public class ModelCatalogRequest
{
    [JsonPropertyName("filters")]
    public List<ModelCatalogFilter> Filters { get; set; } = [];

    [JsonPropertyName("continuationToken")]
    public string? ContinuationToken { get; set; }
}
