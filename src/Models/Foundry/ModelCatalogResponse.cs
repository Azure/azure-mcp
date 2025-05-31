using System.Text.Json.Serialization;

namespace AzureMcp.Models.Foundry;

public class ModelCatalogResponse
{
    [JsonPropertyName("summaries")] public List<ModelInformation> Summaries { get; set; } = [];

    [JsonPropertyName("totalCount")] public int TotalCount { get; set; }

    [JsonPropertyName("continuationToken")]
    public string? ContinuationToken { get; set; }
}
