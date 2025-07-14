// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.ContainerApps;
using AzureMcp.Options;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Azure.Subscription;
using AzureMcp.Services.Azure.Tenant;

namespace AzureMcp.Areas.ContainerApps.Services;

public class ContainerAppsService(ISubscriptionService subscriptionService, ITenantService tenantService) 
    : BaseAzureService(tenantService), IContainerAppsService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));

    public async Task<List<ContainerAppInfo>> ListContainerAppsAsync(
        string subscription,
        string? resourceGroup = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription);

        var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
        var containerApps = new List<ContainerAppInfo>();

        if (!string.IsNullOrEmpty(resourceGroup))
        {
            // List Container Apps in specific resource group
            var resourceGroupResource = await subscriptionResource.GetResourceGroupAsync(resourceGroup);
            
            await foreach (var containerApp in resourceGroupResource.Value.GetContainerAppsAsync())
            {
                if (containerApp?.Data != null)
                {
                    containerApps.Add(CreateContainerAppInfo(containerApp.Data));
                }
            }
        }
        else
        {
            // List all Container Apps in subscription
            await foreach (var containerApp in subscriptionResource.GetContainerAppsAsync())
            {
                if (containerApp?.Data != null)
                {
                    containerApps.Add(CreateContainerAppInfo(containerApp.Data));
                }
            }
        }

        return containerApps;
    }

    private static ContainerAppInfo CreateContainerAppInfo(ContainerAppData data)
    {
        return new ContainerAppInfo(
            Name: data.Name,
            ResourceGroup: ExtractResourceGroupFromId(data.Id?.ToString()),
            Location: data.Location.ToString(),
            Status: data.ProvisioningState?.ToString(),
            Fqdn: data.Configuration?.Ingress?.Fqdn);
    }

    private static string? ExtractResourceGroupFromId(string? resourceId)
    {
        if (string.IsNullOrEmpty(resourceId))
            return null;

        var segments = resourceId.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var rgIndex = Array.IndexOf(segments, "resourceGroups");
        
        return rgIndex >= 0 && rgIndex + 1 < segments.Length 
            ? segments[rgIndex + 1] 
            : null;
    }
}