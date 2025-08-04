// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Options;

namespace AzureMcp.Foundry.Options;

public class EvaluateAgentOptions : GlobalOptions
{
    [JsonPropertyName(FoundryOptionDefinitions.AgentId)]
    public string? AgentId { get; set; }

    [JsonPropertyName(FoundryOptionDefinitions.Query)]
    public string? Query { get; set; }

    [JsonPropertyName(FoundryOptionDefinitions.Endpoint)]
    public string? Endpoint { get; set; }

    [JsonPropertyName(FoundryOptionDefinitions.EvaluatorName)]
    public string? EvaluatorName { get; set; }

    [JsonPropertyName(FoundryOptionDefinitions.Response)]
    public string? Response { get; set; }

    [JsonPropertyName(FoundryOptionDefinitions.ToolCalls)]
    public string? ToolCalls { get; set; }

    [JsonPropertyName(FoundryOptionDefinitions.ToolDefinitions)]
    public string? ToolDefinitions { get; set; }
}
