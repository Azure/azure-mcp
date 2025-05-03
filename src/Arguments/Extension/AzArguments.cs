// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Extension;

public class AzArguments : GlobalArguments
{
    [JsonPropertyName(ArgumentDefinitions.Extension.Az.CommandName)]
    public string? Command { get; set; }
}
