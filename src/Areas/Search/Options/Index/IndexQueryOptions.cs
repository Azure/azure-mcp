// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Search.Index;

public class IndexQueryOptions : BaseIndexOptions
{
    [JsonPropertyName(OptionDefinitions.Search.QueryName)]
    public string? Query { get; set; }
}
