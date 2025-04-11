// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMCP.Models.Argument;
using System.Text.Json.Serialization;

namespace AzureMCP.Arguments.Cosmos;

public class ItemQueryArguments : BaseContainerArguments
{
    [JsonPropertyName(ArgumentDefinitions.Cosmos.QueryText)]
    public string? Query { get; set; }
}