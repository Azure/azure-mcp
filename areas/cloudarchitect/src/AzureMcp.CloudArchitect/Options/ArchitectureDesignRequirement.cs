// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.CloudArchitect.Options;

/// <summary>
/// Represents a single architecture design requirement.
/// </summary>
public class ArchitectureDesignRequirement
{
    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("source")]
    public string Source { get; set; } = string.Empty;

    [JsonPropertyName("importance")]
    public RequirementImportance Importance { get; set; }

    [JsonPropertyName("confidence")]
    public double Confidence { get; set; }
}
