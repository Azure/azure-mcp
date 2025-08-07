using System.Text.Json.Serialization;
using ToolSelection.Models;

namespace ToolSelection;

[JsonSourceGenerationOptions(WriteIndented = true, PropertyNameCaseInsensitive = true)]
[JsonSerializable(typeof(ListToolsResult))]
[JsonSerializable(typeof(List<Tool>))]
[JsonSerializable(typeof(Tool))]
[JsonSerializable(typeof(Dictionary<string, List<string>>), TypeInfoPropertyName = "DictionaryStringListString")]
[JsonSerializable(typeof(EmbeddingRequest))]
[JsonSerializable(typeof(EmbeddingResponse))]
[JsonSerializable(typeof(EmbeddingData))]
[JsonSerializable(typeof(Usage))]
[JsonSerializable(typeof(ApiError))]
[JsonSerializable(typeof(SuccessRateMetrics))]
public partial class SourceGenerationContext : JsonSerializerContext
{
}
