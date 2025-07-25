// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Cosmos.Options;

public class BaseDatabaseOptions : BaseCosmosOptions
{
    [JsonPropertyName(CosmosOptionDefinitions.DatabaseName)]
    public string? Database { get; set; }
}
