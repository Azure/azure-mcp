// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Search.Index;

public class BaseIndexOptions : BaseSearchOptions
{
    [JsonPropertyName(OptionDefinitions.Search.IndexName)]
    public string? Index { get; set; }
}
