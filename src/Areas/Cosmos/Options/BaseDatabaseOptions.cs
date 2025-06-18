// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Areas.Cosmos.Options;

public class BaseDatabaseOptions : BaseCosmosOptions
{
    [JsonPropertyName(CosmosOptionDefinitions.DatabaseName)]
    public string? Database { get; set; }
}
