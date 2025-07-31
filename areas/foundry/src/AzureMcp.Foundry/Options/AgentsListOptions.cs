// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Options;

namespace AzureMcp.Foundry.Options;

public class AgentsListOptions : GlobalOptions
{
    [JsonPropertyName(FoundryOptionDefinitions.Endpoint)]
    public string? Endpoint { get; set; }
}
