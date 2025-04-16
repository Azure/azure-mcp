// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using System.Text.Json.Serialization;

namespace AzureMcp.Models.Command;

public class CommandResponse
{
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("args")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ArgumentInfo>? Arguments { get; set; }

    [JsonPropertyName("results")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Results { get; set; }

    [JsonPropertyName("duration")]
    public long Duration { get; set; }
}