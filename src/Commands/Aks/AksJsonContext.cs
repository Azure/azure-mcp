using System.Text.Json.Serialization;

namespace AzureMcp.Commands.Aks;

[JsonSerializable(typeof(ClusterListCommand.ClusterListCommandResult))]
[JsonSerializable(typeof(ClusterGetCommand.ClusterGetCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault)]
internal sealed partial class AksJsonContext : JsonSerializerContext
{
}
