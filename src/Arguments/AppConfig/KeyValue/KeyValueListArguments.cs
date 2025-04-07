using AzureMCP.Models.Argument;
using System.Text.Json.Serialization;

namespace AzureMCP.Arguments.AppConfig.KeyValue;

public class KeyValueListArguments : BaseAppConfigArguments
{
    [JsonPropertyName(ArgumentDefinitions.AppConfig.KeyName)]
    public string? Key { get; set; }

    [JsonPropertyName(ArgumentDefinitions.AppConfig.LabelName)]
    public string? Label { get; set; }
}