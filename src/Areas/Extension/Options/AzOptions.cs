// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Extension;

public class AzOptions : GlobalOptions
{
    [JsonPropertyName(OptionDefinitions.Extension.Az.CommandName)]
    public string? Command { get; set; }
}
