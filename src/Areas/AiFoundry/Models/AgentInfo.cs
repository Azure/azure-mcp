using System.Text.Json.Serialization;

namespace AzureMcp.Areas.AiFoundry.Models;

/// <summary>
/// Represents information about an Azure AI agent
/// </summary>
public class AgentInfo
{
    /// <summary>
    /// The unique identifier of the agent
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// The display name of the agent
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Description of the agent's purpose and capabilities
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// The instructions or system prompt for the agent
    /// </summary>
    [JsonPropertyName("instructions")]
    public string? Instructions { get; set; }

    /// <summary>
    /// The model used by this agent
    /// </summary>
    [JsonPropertyName("model")]
    public string? Model { get; set; }

    /// <summary>
    /// When the agent was created
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>
    /// When the agent was last updated
    /// </summary>
    [JsonPropertyName("updatedAt")]
    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>
    /// The current status of the agent
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Tools available to the agent
    /// </summary>
    [JsonPropertyName("tools")]
    public string[]? Tools { get; set; }

    /// <summary>
    /// File attachments or knowledge sources for the agent
    /// </summary>
    [JsonPropertyName("fileIds")]
    public string[]? FileIds { get; set; }

    /// <summary>
    /// Vector store IDs associated with the agent for retrieval
    /// </summary>
    [JsonPropertyName("vectorStoreIds")]
    public string[]? VectorStoreIds { get; set; }

    /// <summary>
    /// Custom metadata for the agent
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }

    /// <summary>
    /// Maximum number of completion tokens for agent responses
    /// </summary>
    [JsonPropertyName("maxCompletionTokens")]
    public int? MaxCompletionTokens { get; set; }

    /// <summary>
    /// Maximum number of prompt tokens the agent can process
    /// </summary>
    [JsonPropertyName("maxPromptTokens")]
    public int? MaxPromptTokens { get; set; }

    /// <summary>
    /// Temperature setting for the agent's responses
    /// </summary>
    [JsonPropertyName("temperature")]
    public float? Temperature { get; set; }

    /// <summary>
    /// Top-p setting for the agent's responses
    /// </summary>
    [JsonPropertyName("topP")]
    public float? TopP { get; set; }

    /// <summary>
    /// Whether the agent supports parallel tool calls
    /// </summary>
    [JsonPropertyName("parallelToolCalls")]
    public bool? ParallelToolCalls { get; set; }

    /// <summary>
    /// Response format specification for the agent
    /// </summary>
    [JsonPropertyName("responseFormat")]
    public object? ResponseFormat { get; set; }
}

public record AgentListResult
{
    [JsonPropertyName("agents")]
    public AgentInfo[] Agents { get; init; } = [];
} 