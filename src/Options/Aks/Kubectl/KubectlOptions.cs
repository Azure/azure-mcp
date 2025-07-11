using System.Text.Json.Serialization;
using AzureMcp.Models.Option;
using AzureMcp.Options;

namespace AzureMcp.Options.Aks.Kubectl;

public class KubectlOptions : GlobalOptions
{
    [JsonPropertyName(OptionDefinitions.Aks.KubeConfigName)]
    public string? KubeConfig { get; set; }

    [JsonPropertyName(OptionDefinitions.Aks.CommandName)]
    public string? Command { get; set; }

    [JsonPropertyName(OptionDefinitions.Aks.AutoInstallKubectlName)]
    public bool AutoInstallKubectl { get; set; } = true;
}
