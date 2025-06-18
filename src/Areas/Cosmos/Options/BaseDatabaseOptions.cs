// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Cosmos;

public class BaseDatabaseOptions : BaseCosmosOptions
{
    [JsonPropertyName(OptionDefinitions.Cosmos.DatabaseName)]
    public string? Database { get; set; }
}
