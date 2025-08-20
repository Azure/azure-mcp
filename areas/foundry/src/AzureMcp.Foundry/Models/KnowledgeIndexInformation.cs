// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Foundry.Models;

public class KnowledgeIndexInformation
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("version")]
    public string? Version { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("tags")]
    public Dictionary<string, string?>? Tags { get; set; }
}
