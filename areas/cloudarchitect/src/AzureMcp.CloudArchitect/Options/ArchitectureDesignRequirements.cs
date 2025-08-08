// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.CloudArchitect.Options;

/// <summary>
/// Contains all requirements for the architecture design.
/// </summary>
public class ArchitectureDesignRequirements
{
    [JsonPropertyName("explicit")]
    public List<ArchitectureDesignRequirement> Explicit { get; set; } = new();

    [JsonPropertyName("implicit")]
    public List<ArchitectureDesignRequirement> Implicit { get; set; } = new();

    [JsonPropertyName("assumed")]
    public List<ArchitectureDesignRequirement> Assumed { get; set; } = new();
}
