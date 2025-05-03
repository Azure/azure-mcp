// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Storage;

public class BaseStorageArguments : SubscriptionArguments
{
    [JsonPropertyName(ArgumentDefinitions.Storage.AccountName)]
    public string? Account { get; set; }
}
