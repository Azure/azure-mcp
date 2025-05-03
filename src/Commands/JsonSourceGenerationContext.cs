
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using AzureMcp.Commands;
using AzureMcp.Commands.Group;

namespace AzureMcp;

[JsonSerializable(typeof(GroupListCommand.Result))]
[JsonSerializable(typeof(BaseCommand.ExceptionResult))]
[JsonSerializable(typeof(JsonElement))]
[JsonSerializable(typeof(List<string>))]
[JsonSerializable(typeof(List<JsonNode>))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class JsonSourceGenerationContext : JsonSerializerContext
{

}
