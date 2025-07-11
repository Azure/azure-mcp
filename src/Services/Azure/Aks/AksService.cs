using Azure.ResourceManager.ContainerService;
using Azure.ResourceManager.ContainerService.Models;
using AzureMcp.Options;
using AzureMcp.Services.Azure.ResourceGroup;
using AzureMcp.Services.Azure.Subscription;
using AzureMcp.Services.Azure.Tenant;
using AzureMcp.Services.Interfaces;

namespace AzureMcp.Services.Azure.Aks;

public class AksService(
    ISubscriptionService subscriptionService,
    IResourceGroupService resourceGroupService,
    ITenantService tenantService)
    : BaseAzureService(tenantService), IAksService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService;
    private readonly IResourceGroupService _resourceGroupService = resourceGroupService;

    public async Task<List<string>> ListClusters(string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId);
        var subscription = await _subscriptionService.GetSubscription(subscriptionId, tenant, retryPolicy);
        var clusters = new List<string>();
        foreach (var cluster in subscription.GetContainerServiceManagedClusters())
        {
            if (!string.IsNullOrEmpty(cluster.Data.Name))
            {
                clusters.Add(cluster.Data.Name);
            }
        }
        return clusters;
    }

    public async Task<ContainerServiceManagedClusterData> GetCluster(
        string clusterName,
        string resourceGroupName,
        string subscriptionId,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(clusterName, resourceGroupName, subscriptionId);
        var resourceGroup = await _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroupName, tenant, retryPolicy)
            ?? throw new Exception($"Resource group named '{resourceGroupName}' not found");
        var cluster = await resourceGroup.GetContainerServiceManagedClusters().GetAsync(clusterName);
        return cluster.Value.Data;
    }
}
