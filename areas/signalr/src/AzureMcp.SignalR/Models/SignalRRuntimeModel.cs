// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.SignalR.Models;

public class SignalRRuntimeModel
{
    public string? Name { get; set; }
    public string? ResourceGroupName { get; set; }
    public string? Location { get; set; }
    public string? SkuName { get; set; }
    public string? SkuTier { get; set; }
    public string? ProvisioningState { get; set; }
    public string? HostName { get; set; }
    public int? PublicPort { get; set; }
    public int? ServerPort { get; set; }
}
