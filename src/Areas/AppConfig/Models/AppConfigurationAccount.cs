// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Identity;

namespace AzureMcp.Areas.AppConfig.Models;

public class AppConfigurationAccount
{
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public bool PublicNetworkAccess { get; set; }
    public string? Sku { get; set; }
    public IDictionary<string, string> Tags { get; set; } = new Dictionary<string, string>();
    public bool? DisableLocalAuth { get; set; }
    public int? SoftDeleteRetentionInDays { get; set; }
    public bool? EnablePurgeProtection { get; set; }
    public string? CreateMode { get; set; }

    public ManagedIdentityInfo? ManagedIdentity { get; set; }

    // Full encryption properties
    public EncryptionProperties? Encryption { get; set; }
}
