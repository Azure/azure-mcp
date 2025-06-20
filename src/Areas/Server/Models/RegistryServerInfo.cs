// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Server.Models;

public sealed class RegistryServerInfo
{
    [JsonIgnore]
    public string? Name { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("command")]
    public string? Command { get; init; }

    [JsonPropertyName("args")]
    public List<string>? Args { get; init; }

    [JsonPropertyName("env")]
    public Dictionary<string, string>? Env { get; init; }
}
