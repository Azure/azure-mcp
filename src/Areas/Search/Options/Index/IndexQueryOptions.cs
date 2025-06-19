// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Search.Options.Index;

public class IndexQueryOptions : BaseIndexOptions
{
    [JsonPropertyName(SearchOptionDefinitions.QueryName)]
    public string? Query { get; set; }
}
