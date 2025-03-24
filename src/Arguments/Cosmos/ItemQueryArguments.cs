using AzureMCP.Models;
using System.Text.Json.Serialization;

namespace AzureMCP.Arguments.Cosmos;

public class ItemQueryArguments : BaseCosmosArguments
{
    [JsonPropertyName(ArgumentDefinitions.Cosmos.DatabaseName)]
    public string? Database { get; set; }

    [JsonPropertyName(ArgumentDefinitions.Cosmos.ContainerName)]
    public string? Container { get; set; }

    [JsonPropertyName(ArgumentDefinitions.Cosmos.QueryText)]
    public string? Query { get; set; }
}
