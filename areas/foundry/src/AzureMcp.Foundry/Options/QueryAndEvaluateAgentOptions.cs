// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Options;

namespace AzureMcp.Foundry.Options;

public class QueryAndEvaluateAgentOptions : GlobalOptions
{
    [JsonPropertyName(FoundryOptionDefinitions.AgentId)]
    public string? AgentId { get; set; }

    [JsonPropertyName(FoundryOptionDefinitions.Query)]
    public string? Query { get; set; }

    [JsonPropertyName(FoundryOptionDefinitions.Endpoint)]
    public string? Endpoint { get; set; }

    [JsonPropertyName(FoundryOptionDefinitions.Evaluators)]
    public string? Evaluators { get; set; }
}
