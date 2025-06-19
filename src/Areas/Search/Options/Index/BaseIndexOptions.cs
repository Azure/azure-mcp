// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Search.Options.Index;

public class BaseIndexOptions : BaseSearchOptions
{
    [JsonPropertyName(SearchOptionDefinitions.IndexName)]
    public string? Index { get; set; }
}
