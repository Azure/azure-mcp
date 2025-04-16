// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using System.Text.Json.Serialization;

namespace AzureMcp.Arguments.Extension;

public class AzdArguments : GlobalArguments
{
    [JsonPropertyName(ArgumentDefinitions.Extension.Azd.CommandName)]
    public string? Command { get; set; }
}