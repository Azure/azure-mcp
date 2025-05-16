// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Extension;

public class AzdArguments : GlobalArguments
{
    [JsonPropertyName(ArgumentDefinitions.Extension.Azd.CommandName)]
    public string? Command { get; set; }

    [JsonPropertyName(ArgumentDefinitions.Extension.Azd.CwdName)]
    public string? Cwd { get; set; }

    [JsonPropertyName(ArgumentDefinitions.Extension.Azd.EnvironmentName)]
    public string? Environment { get; set; }

    [JsonPropertyName(ArgumentDefinitions.Extension.Azd.LearnName)]
    public bool Learn { get; set; }
}
