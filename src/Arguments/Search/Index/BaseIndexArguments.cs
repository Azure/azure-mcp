// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using System.Text.Json.Serialization;

namespace AzureMcp.Arguments.Search.Index;

public class BaseIndexArguments : BaseSearchArguments
{
    [JsonPropertyName(ArgumentDefinitions.Search.IndexName)]
    public string? Index { get; set; }
}