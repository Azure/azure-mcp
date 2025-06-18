// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Search;

public class BaseSearchOptions : GlobalOptions
{
    [JsonPropertyName(OptionDefinitions.Search.ServiceName)]
    public string? Service { get; set; }
}
