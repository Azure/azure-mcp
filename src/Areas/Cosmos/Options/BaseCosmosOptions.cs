// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;
using AzureMcp.Options;

namespace AzureMcp.Areas.Cosmos.Options;

public class BaseCosmosOptions : SubscriptionOptions
{
    [JsonPropertyName(OptionDefinitions.Cosmos.AccountName)]
    public string? Account { get; set; }
}
