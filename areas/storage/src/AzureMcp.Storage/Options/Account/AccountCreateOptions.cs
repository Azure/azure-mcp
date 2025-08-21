// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Options;

namespace AzureMcp.Storage.Options.Account;

public class AccountCreateOptions : SubscriptionOptions
{
    [JsonPropertyName(StorageOptionDefinitions.AccountCreateName)]
    public string? Account { get; set; }

    [JsonPropertyName(StorageOptionDefinitions.LocationName)]
    public string? Location { get; set; }

    [JsonPropertyName(StorageOptionDefinitions.SkuName)]
    public string? Sku { get; set; }

    [JsonPropertyName(StorageOptionDefinitions.KindName)]
    public string? Kind { get; set; }

    [JsonPropertyName(StorageOptionDefinitions.AccessTierName)]
    public string? AccessTier { get; set; }

    [JsonPropertyName(StorageOptionDefinitions.EnableHttpsTrafficOnlyName)]
    public bool? EnableHttpsTrafficOnly { get; set; }

    [JsonPropertyName(StorageOptionDefinitions.AllowBlobPublicAccessName)]
    public bool? AllowBlobPublicAccess { get; set; }

    [JsonPropertyName(StorageOptionDefinitions.EnableHierarchicalNamespaceName)]
    public bool? EnableHierarchicalNamespace { get; set; }
}
