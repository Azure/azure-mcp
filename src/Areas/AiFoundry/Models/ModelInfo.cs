using System.Text.Json.Serialization;

namespace AzureMcp.Areas.AiFoundry.Models;

/// <summary>
/// Represents information about a model in the Azure AI Foundry model catalog
/// </summary>
public class ModelInfo
{
    /// <summary>
    /// The unique identifier of the model
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// The display name of the model
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// The model publisher/organization
    /// </summary>
    [JsonPropertyName("publisher")]
    public required string Publisher { get; set; }

    /// <summary>
    /// The model version
    /// </summary>
    [JsonPropertyName("version")]
    public required string Version { get; set; }

    /// <summary>
    /// Description of the model and its capabilities
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// The type of model (e.g., "chat", "embedding", "text-generation")
    /// </summary>
    [JsonPropertyName("modelType")]
    public required string ModelType { get; set; }



    /// <summary>
    /// The model's license information
    /// </summary>
    [JsonPropertyName("license")]
    public string? License { get; set; }

    /// <summary>
    /// Programming languages supported by the model
    /// </summary>
    [JsonPropertyName("languages")]
    public string[]? Languages { get; set; }

    /// <summary>
    /// Maximum context length supported by the model
    /// </summary>
    [JsonPropertyName("contextLength")]
    public int? ContextLength { get; set; }

    /// <summary>
    /// Pricing tier or cost category
    /// </summary>
    [JsonPropertyName("pricingTier")]
    public string? PricingTier { get; set; }

    /// <summary>
    /// Whether the model is currently available for deployment
    /// </summary>
    [JsonPropertyName("isAvailable")]
    public bool IsAvailable { get; set; }

    /// <summary>
    /// Categories or tags associated with the model
    /// </summary>
    [JsonPropertyName("categories")]
    public string[]? Categories { get; set; }

    /// <summary>
    /// Model fine-tuning capabilities
    /// </summary>
    [JsonPropertyName("fineTuningSupported")]
    public bool? FineTuningSupported { get; set; }

    /// <summary>
    /// Deployment types supported by this model
    /// </summary>
    [JsonPropertyName("supportedDeploymentTypes")]
    public string[]? SupportedDeploymentTypes { get; set; }

    /// <summary>
    /// Additional model-specific capabilities or features
    /// </summary>
    [JsonPropertyName("capabilities")]
    public Dictionary<string, object>? Capabilities { get; set; }
}

public record ModelListResult
{
    [JsonPropertyName("models")]
    public ModelInfo[] Models { get; init; } = [];
} 