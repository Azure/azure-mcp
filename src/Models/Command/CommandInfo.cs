// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using System.Text.Json.Serialization;

namespace AzureMcp.Models.Command;

public class CommandInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("command")]
    public string Command { get; set; } = string.Empty;

    [JsonPropertyName("subcommands")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<CommandInfo>? Subcommands { get; set; }

    [JsonPropertyName("args")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ArgumentInfo>? Arguments { get; set; }
}