// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using System.Text.Json.Serialization;

namespace AzureMcp.Arguments.Storage;

public class BaseStorageArguments : SubscriptionArguments
{
    [JsonPropertyName(ArgumentDefinitions.Storage.AccountName)]
    public string? Account { get; set; }
}