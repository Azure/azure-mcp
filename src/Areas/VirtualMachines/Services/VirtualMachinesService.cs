// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using AzureMcp.Models;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Azure.Subscription;
using AzureMcp.Services.Azure.Tenant;

namespace AzureMcp.Areas.VirtualMachines.Services;

public class VirtualMachinesService(ISubscriptionService subscriptionService, ITenantService tenantService)
    : BaseAzureService(tenantService), IVirtualMachinesService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService;

    public async Task<List<VirtualMachineInfo>> ListVirtualMachinesAsync(
        string subscription,
        string? resourceGroup,
        string? tenant,
        RetryPolicyOptions? retryPolicy,
        CancellationToken cancellationToken = default)
    {
        var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
        var virtualMachines = new List<VirtualMachineInfo>();

        if (!string.IsNullOrEmpty(resourceGroup))
        {
            // List VMs in a specific resource group
            var resourceGroupResource = await subscriptionResource
                .GetResourceGroupAsync(resourceGroup, cancellationToken);
            
            var vms = resourceGroupResource.Value.GetVirtualMachines();
            await foreach (var vm in vms.GetAllAsync(cancellationToken: cancellationToken))
            {
                var vmInfo = await CreateVirtualMachineInfoAsync(vm, cancellationToken);
                virtualMachines.Add(vmInfo);
            }
        }
        else
        {
            // List all VMs in the subscription
            var vms = subscriptionResource.GetVirtualMachines();
            await foreach (var vm in vms.GetAllAsync(cancellationToken: cancellationToken))
            {
                var vmInfo = await CreateVirtualMachineInfoAsync(vm, cancellationToken);
                virtualMachines.Add(vmInfo);
            }
        }

        return virtualMachines;
    }

    private static async Task<VirtualMachineInfo> CreateVirtualMachineInfoAsync(
        VirtualMachineResource vm, 
        CancellationToken cancellationToken)
    {
        // Get instance view for status information
        VirtualMachineInstanceView? instanceView = null;
        try
        {
            var response = await vm.InstanceViewAsync(cancellationToken);
            instanceView = response.Value;
        }
        catch
        {
            // If we can't get instance view, continue without status
        }

        var status = instanceView?.Statuses?
            .FirstOrDefault(s => s.Code?.StartsWith("PowerState/") == true)?
            .DisplayStatus ?? "Unknown";

        return new VirtualMachineInfo(
            Name: vm.Data.Name,
            ResourceGroup: vm.Data.Id.ResourceGroupName,
            Location: vm.Data.Location.ToString(),
            Status: status,
            Size: vm.Data.HardwareProfile?.VmSize?.ToString(),
            OsType: vm.Data.StorageProfile?.OSDisk?.OSType?.ToString());
    }
}