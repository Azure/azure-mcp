// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Search.Index;

public class BaseIndexArguments : BaseSearchArguments
{
    [JsonPropertyName(ArgumentDefinitions.Search.IndexName)]
    public string? Index { get; set; }
}
