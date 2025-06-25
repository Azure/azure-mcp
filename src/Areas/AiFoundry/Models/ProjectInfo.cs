using System.Text.Json.Serialization;

namespace AzureMcp.Areas.AiFoundry.Models;

/// <summary>
/// Represents information about an Azure AI Foundry project
/// </summary>
public class ProjectInfo
{
    /// <summary>
    /// The name of the project
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// The Azure resource group containing the project
    /// </summary>
    [JsonPropertyName("resourceGroup")]
    public required string ResourceGroup { get; set; }

    /// <summary>
    /// The Azure subscription ID
    /// </summary>
    [JsonPropertyName("subscriptionId")]
    public required string SubscriptionId { get; set; }

    /// <summary>
    /// The Azure region where the project is located
    /// </summary>
    [JsonPropertyName("location")]
    public required string Location { get; set; }

    /// <summary>
    /// The project endpoint URL for API access
    /// </summary>
    [JsonPropertyName("endpoint")]
    public required string Endpoint { get; set; }

    /// <summary>
    /// When the project was created
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>
    /// The project description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Resource tags applied to the project
    /// </summary>
    [JsonPropertyName("tags")]
    public Dictionary<string, string> Tags { get; set; } = new();

    /// <summary>
    /// The project's current status
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// The project type (Foundry, Hub-based, etc.)
    /// </summary>
    [JsonPropertyName("projectType")]
    public string? ProjectType { get; set; }
} 