// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using System.Text.Json.Serialization;

namespace AzureMcp.Arguments.Cosmos;

public class BaseCosmosArguments : SubscriptionArguments
{
    [JsonPropertyName(ArgumentDefinitions.Cosmos.AccountName)]
    public string? Account { get; set; }
}