// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.CloudArchitect.Options;

/// <summary>
/// Represents the different architecture tiers.
/// </summary>
public class ArchitectureDesignTiers
{
    [JsonPropertyName("additionalProperties")]
    public List<string> AdditionalProperties { get; set; } = new();
}
