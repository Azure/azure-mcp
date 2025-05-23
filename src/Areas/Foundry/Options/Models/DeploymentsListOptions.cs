// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Options;

namespace AzureMcp.Areas.Foundry.Options.Models;

public class DeploymentsListOptions : GlobalOptions
{
    [JsonPropertyName(FoundryOptionDefinitions.Endpoint)]
    public string? Endpoint { get; set; }
}
