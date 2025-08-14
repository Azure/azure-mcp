// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.AzureManagedLustre.Models;

public sealed class LustreFileSystem
{
    public string? Name { get; set; }
    public string? Id { get; set; }
    public string? ResourceGroupName { get; set; }
    public string? SubscriptionId { get; set; }
    public string? Location { get; set; }
    public string? ProvisioningState { get; set; }
    public string? State { get; set; }
    public string? MgsAddress { get; set; }
    public string? SkuTier { get; set; }
    public long? StorageCapacityTiB { get; set; }
    public string? BlobContainerId { get; set; }
    public string? MaintenanceDay { get; set; }
    public string? MaintenanceTime { get; set; }
}
