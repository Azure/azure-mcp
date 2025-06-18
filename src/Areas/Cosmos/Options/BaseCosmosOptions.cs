// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Cosmos;

public class BaseCosmosOptions : SubscriptionOptions
{
    [JsonPropertyName(OptionDefinitions.Cosmos.AccountName)]
    public string? Account { get; set; }
}
