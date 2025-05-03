// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Cosmos;

public class ItemQueryArguments : BaseContainerArguments
{
    [JsonPropertyName(ArgumentDefinitions.Cosmos.QueryText)]
    public string? Query { get; set; }
}
