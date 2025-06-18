// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.Search.Options;
using AzureMcp.Models.Option;

namespace AzureMcp.Areas.Search.Options.Index;

public class BaseIndexOptions : BaseSearchOptions
{
    [JsonPropertyName(SearchOptionDefinitions.IndexName)]
    public string? Index { get; set; }
}
