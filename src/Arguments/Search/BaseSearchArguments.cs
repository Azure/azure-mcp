// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Search;

public class BaseSearchArguments : GlobalArguments
{
    [JsonPropertyName(ArgumentDefinitions.Search.ServiceName)]
    public string? Service { get; set; }
}
