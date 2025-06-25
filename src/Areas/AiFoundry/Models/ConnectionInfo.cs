using System.Text.Json.Serialization;

namespace AzureMcp.Areas.AiFoundry.Models;

/// <summary>
/// Represents information about a connection to external services in Azure AI Foundry
/// </summary>
public class ConnectionInfo
{
    /// <summary>
    /// The name of the connection
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// The type of connection (e.g., "AzureOpenAI", "AzureAISearch", "AzureStorage")
    /// </summary>
    [JsonPropertyName("connectionType")]
    public required string ConnectionType { get; set; }

    /// <summary>
    /// The target service this connection points to
    /// </summary>
    [JsonPropertyName("target")]
    public string? Target { get; set; }

    /// <summary>
    /// Description of the connection
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// The current status of the connection
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// When the connection was created
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>
    /// When the connection was last updated
    /// </summary>
    [JsonPropertyName("updatedAt")]
    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>
    /// The authentication type used for this connection
    /// </summary>
    [JsonPropertyName("authenticationType")]
    public string? AuthenticationType { get; set; }

    /// <summary>
    /// Categories or capabilities provided by this connection
    /// </summary>
    [JsonPropertyName("categories")]
    public string[]? Categories { get; set; }

    /// <summary>
    /// Whether the connection is currently active and usable
    /// </summary>
    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Metadata or additional properties for the connection
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }

    /// <summary>
    /// Tags associated with the connection
    /// </summary>
    [JsonPropertyName("tags")]
    public Dictionary<string, string>? Tags { get; set; }

    /// <summary>
    /// The Azure resource ID if this connection is backed by an Azure resource
    /// </summary>
    [JsonPropertyName("azureResourceId")]
    public string? AzureResourceId { get; set; }

    /// <summary>
    /// The region or location of the connected service
    /// </summary>
    [JsonPropertyName("location")]
    public string? Location { get; set; }

    /// <summary>
    /// Additional connection-specific properties
    /// </summary>
    [JsonPropertyName("properties")]
    public Dictionary<string, object>? Properties { get; set; }
}

public record ConnectionListResult
{
    [JsonPropertyName("connections")]
    public ConnectionInfo[] Connections { get; init; } = [];
} 