
using System.Text.Json.Serialization;
using AzureMcp.Models.Command;

namespace AzureMcp.Models;

[JsonSerializable(typeof(List<CommandInfo>))]
[JsonSerializable(typeof(CommandResponse))]
[JsonSerializable(typeof(ETag), TypeInfoPropertyName = "McpETag")]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class ModelsJsonContext : JsonSerializerContext
{
    // This class is intentionally left empty. It is used for source generation of JSON serialization.
}
