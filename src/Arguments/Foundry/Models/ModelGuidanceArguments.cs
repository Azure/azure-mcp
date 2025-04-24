using AzureMcp.Models.Argument;
using System.Text.Json.Serialization;

namespace AzureMcp.Arguments.Foundry.Models;

public class ModelGuidanceArguments : GlobalArguments {
    [JsonPropertyName(ArgumentDefinitions.Foundry.InferenceModelNameText)]
    public string InferenceModelName { get; set; }
}