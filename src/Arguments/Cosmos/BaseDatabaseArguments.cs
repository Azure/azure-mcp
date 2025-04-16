// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using System.Text.Json.Serialization;

namespace AzureMcp.Arguments.Cosmos;

public class BaseDatabaseArguments : BaseCosmosArguments
{
    [JsonPropertyName(ArgumentDefinitions.Cosmos.DatabaseName)]
    public string? Database { get; set; }
}