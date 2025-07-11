using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Aks.Cluster;

public class BaseClusterOptions : BaseAksOptions
{
    [JsonPropertyName(OptionDefinitions.Aks.ClusterName)]
    public string? Cluster { get; set; }
}
