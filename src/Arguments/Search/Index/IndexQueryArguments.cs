// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Search.Index;

public class IndexQueryArguments : BaseIndexArguments
{
    [JsonPropertyName(ArgumentDefinitions.Search.QueryName)]
    public string? Query { get; set; }
}
