// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Identity;

namespace AzureMcp.Areas.ContainerApps.Models;

public class ContainerRegistry
{
    public string Name { get; set; } = string.Empty;
    public string ResourceGroup { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string LoginServer { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public string Sku { get; set; } = string.Empty;
    public bool AdminUserEnabled { get; set; }
    public IDictionary<string, string> Tags { get; set; } = new Dictionary<string, string>();
    public string ProvisioningState { get; set; } = string.Empty;
    public string? PublicNetworkAccess { get; set; }
    public ManagedIdentityInfo? ManagedIdentity { get; set; }
}
