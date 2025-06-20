// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Options;

namespace AzureMcp.Areas.Search.Options;

public class BaseSearchOptions : GlobalOptions
{
    [JsonPropertyName(SearchOptionDefinitions.ServiceName)]
    public string? Service { get; set; }
}
