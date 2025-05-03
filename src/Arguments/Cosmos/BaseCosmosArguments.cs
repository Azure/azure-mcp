// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Cosmos;

public class BaseCosmosArguments : SubscriptionArguments
{
    [JsonPropertyName(ArgumentDefinitions.Cosmos.AccountName)]
    public string? Account { get; set; }
}
