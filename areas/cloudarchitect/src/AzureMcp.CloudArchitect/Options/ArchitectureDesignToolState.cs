// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.CloudArchitect.Options;

/// <summary>
/// The state object for the architecture design tool.
/// </summary>
public class ArchitectureDesignToolState
{
    [JsonPropertyName("architectureComponents")]
    public List<string> ArchitectureComponents { get; set; } = new();

    [JsonPropertyName("architectureTiers")]
    public ArchitectureDesignTiers ArchitectureTiers { get; set; } = new();

    [JsonPropertyName("thought")]
    public string Thought { get; set; } = string.Empty;

    [JsonPropertyName("suggestedHint")]
    public string SuggestedHint { get; set; } = string.Empty;

    [JsonPropertyName("requirements")]
    public ArchitectureDesignRequirements Requirements { get; set; } = new();

    [JsonPropertyName("confidenceFactors")]
    public ArchitectureDesignConfidenceFactors ConfidenceFactors { get; set; } = new();
}
