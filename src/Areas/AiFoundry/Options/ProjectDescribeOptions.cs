using System.Text.Json.Serialization;
using AzureMcp.Models.Option;
using AzureMcp.Options;

namespace AzureMcp.Areas.AiFoundry.Options;

/// <summary>
/// Options for describing a specific Azure AI Foundry project
/// </summary>
public class ProjectDescribeOptions : SubscriptionOptions
{
    /// <summary>
    /// The name of the AI Foundry project
    /// </summary>
    [JsonPropertyName(AiFoundryOptionDefinitions.ProjectNameName)]
    public string? ProjectName { get; set; }
} 