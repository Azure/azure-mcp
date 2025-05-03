using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Search.Documents.Indexes.Models;
using AzureMcp.Commands.Search.Index;
using AzureMcp.Commands.Search.Service;

[JsonSerializable(typeof(ServiceListCommand.ServiceListCommandResult))]
[JsonSerializable(typeof(IndexListCommand.IndexListCommandResult))]
[JsonSerializable(typeof(IndexDescribeCommand.IndexDescribeCommandResult))]
[JsonSerializable(typeof(List<JsonElement>))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class SearchJsonContext : JsonSerializerContext
{
    // This class is generated at runtime by the source generator.
}
