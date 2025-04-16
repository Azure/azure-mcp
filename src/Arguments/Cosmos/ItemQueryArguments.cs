// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using System.Text.Json.Serialization;

namespace AzureMcp.Arguments.Cosmos;

public class ItemQueryArguments : BaseContainerArguments
{
    [JsonPropertyName(ArgumentDefinitions.Cosmos.QueryText)]
    public string? Query { get; set; }
}