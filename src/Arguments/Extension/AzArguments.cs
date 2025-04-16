// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using System.Text.Json.Serialization;

namespace AzureMcp.Arguments.Extension;

public class AzArguments : GlobalArguments
{
    [JsonPropertyName(ArgumentDefinitions.Extension.Az.CommandName)]
    public string? Command { get; set; }
}