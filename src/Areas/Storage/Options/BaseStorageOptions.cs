// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Options;

namespace AzureMcp.Areas.Storage.Options;

public class BaseStorageOptions : SubscriptionOptions
{
    [JsonPropertyName(StorageOptionDefinitions.AccountName)]
    public string? Account { get; set; }
}
