// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using System.Text.Json.Serialization;

namespace AzureMcp.Arguments.Search.Index;

public class IndexQueryArguments : BaseIndexArguments
{
    [JsonPropertyName(ArgumentDefinitions.Search.QueryName)]
    public string? Query { get; set; }
}