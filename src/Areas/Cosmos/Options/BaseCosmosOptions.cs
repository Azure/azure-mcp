// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Options;

namespace AzureMcp.Areas.Cosmos.Options;

public class BaseCosmosOptions : SubscriptionOptions
{
    [JsonPropertyName(CosmosOptionDefinitions.AccountName)]
    public string? Account { get; set; }
}
