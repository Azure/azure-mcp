using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Areas.Server.Models;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Areas.Server;

[JsonSerializable(typeof(RegistryRoot))]
[JsonSerializable(typeof(Dictionary<string, RegistryServerInfo>))]
[JsonSerializable(typeof(RegistryServerInfo))]
[JsonSerializable(typeof(ListToolsResult))]
[JsonSerializable(typeof(IList<McpClientTool>))]
[JsonSerializable(typeof(Dictionary<string, object?>))]
[JsonSerializable(typeof(JsonElement))]
[JsonSerializable(typeof(Tool))]
[JsonSerializable(typeof(List<Tool>))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
    WriteIndented = true
)]
internal sealed partial class ServerJsonContext : JsonSerializerContext
{
}
