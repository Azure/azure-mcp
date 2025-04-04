using AzureMCP.Models;
using System.Text.Json.Serialization;

namespace AzureMCP.Arguments.Cosmos;

public class ItemQueryArguments : BaseDatabaseArguments
{
    [JsonPropertyName(ArgumentDefinitions.Cosmos.ContainerName)]
    public string? Container { get; set; }

    [JsonPropertyName(ArgumentDefinitions.Cosmos.QueryText)]
    public string? Query { get; set; }
}