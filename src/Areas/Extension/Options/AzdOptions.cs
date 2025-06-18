// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Extension;

public class AzdOptions : GlobalOptions
{
    [JsonPropertyName(OptionDefinitions.Extension.Azd.CommandName)]
    public string? Command { get; set; }

    [JsonPropertyName(OptionDefinitions.Extension.Azd.CwdName)]
    public string? Cwd { get; set; }

    [JsonPropertyName(OptionDefinitions.Extension.Azd.EnvironmentName)]
    public string? Environment { get; set; }

    [JsonPropertyName(OptionDefinitions.Extension.Azd.LearnName)]
    public bool Learn { get; set; }
}
