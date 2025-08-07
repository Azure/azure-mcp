// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.AzureSignalR.Models;

public class SignalRServiceModel
{
    public string Name { get; set; } = string.Empty;
    public string ResourceGroupName { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string SkuName { get; set; } = string.Empty;
    public string SkuTier { get; set; } = string.Empty;
    public string ProvisioningState { get; set; } = string.Empty;
    public string HostName { get; set; } = string.Empty;
    public int? PublicPort { get; set; }
    public int? ServerPort { get; set; }
}
