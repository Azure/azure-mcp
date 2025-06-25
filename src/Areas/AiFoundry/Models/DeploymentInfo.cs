using System.Text.Json.Serialization;

namespace AzureMcp.Areas.AiFoundry.Models;

/// <summary>
/// Represents information about a model deployment in Azure AI Foundry
/// </summary>
public class DeploymentInfo
{
    /// <summary>
    /// The name of the deployment
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// The ID of the deployed model
    /// </summary>
    [JsonPropertyName("modelId")]
    public required string ModelId { get; set; }

    /// <summary>
    /// The name of the deployed model
    /// </summary>
    [JsonPropertyName("modelName")]
    public string? ModelName { get; set; }

    /// <summary>
    /// The version of the deployed model
    /// </summary>
    [JsonPropertyName("modelVersion")]
    public string? ModelVersion { get; set; }

    /// <summary>
    /// The current status of the deployment
    /// </summary>
    [JsonPropertyName("status")]
    public required string Status { get; set; }

    /// <summary>
    /// The deployment endpoint URL (if available)
    /// </summary>
    [JsonPropertyName("endpointUrl")]
    public string? EndpointUrl { get; set; }

    /// <summary>
    /// When the deployment was created
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>
    /// When the deployment was last updated
    /// </summary>
    [JsonPropertyName("updatedAt")]
    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>
    /// The scaling type (e.g., "Standard", "Managed Compute")
    /// </summary>
    [JsonPropertyName("scalingType")]
    public string? ScalingType { get; set; }

    /// <summary>
    /// Number of instances in the deployment
    /// </summary>
    [JsonPropertyName("instanceCount")]
    public int? InstanceCount { get; set; }

    /// <summary>
    /// The SKU or instance type used for the deployment
    /// </summary>
    [JsonPropertyName("sku")]
    public string? Sku { get; set; }

    /// <summary>
    /// The deployment type (e.g., "Standard", "Serverless")
    /// </summary>
    [JsonPropertyName("deploymentType")]
    public string? DeploymentType { get; set; }

    /// <summary>
    /// Rate limits configured for the deployment
    /// </summary>
    [JsonPropertyName("rateLimits")]
    public Dictionary<string, object>? RateLimits { get; set; }

    /// <summary>
    /// Error message if the deployment is in a failed state
    /// </summary>
    [JsonPropertyName("errorMessage")]
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Additional properties or metadata
    /// </summary>
    [JsonPropertyName("properties")]
    public Dictionary<string, object>? Properties { get; set; }

    /// <summary>
    /// Tags associated with the deployment
    /// </summary>
    [JsonPropertyName("tags")]
    public Dictionary<string, string>? Tags { get; set; }

    /// <summary>
    /// Custom metadata for the deployment
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }
}

public record DeploymentListResult
{
    [JsonPropertyName("deployments")]
    public DeploymentInfo[] Deployments { get; init; } = [];
} 