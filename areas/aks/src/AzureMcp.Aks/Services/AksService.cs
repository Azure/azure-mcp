// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.ResourceManager.ResourceGraph;
using Azure.ResourceManager.ResourceGraph.Models;
using AzureMcp.Aks.Models;
using AzureMcp.Core.Options;
using AzureMcp.Core.Services.Azure;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.Core.Services.Caching;

namespace AzureMcp.Aks.Services;

public sealed class AksService(
    ISubscriptionService subscriptionService,
    ITenantService tenantService,
    ICacheService cacheService) : BaseAzureService(tenantService), IAksService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private readonly ICacheService _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
    private readonly ITenantService _tenantService = tenantService ?? throw new ArgumentNullException(nameof(tenantService));

    private const string CacheGroup = "aks";
    private const string AksClustersCacheKey = "clusters";
    private static readonly TimeSpan s_cacheDuration = TimeSpan.FromHours(1);

    public async Task<List<Cluster>> ListClusters(
        string subscription,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription);

        // Create cache key
        var cacheKey = string.IsNullOrEmpty(tenant)
            ? $"{AksClustersCacheKey}_{subscription}"
            : $"{AksClustersCacheKey}_{subscription}_{tenant}";

        // Try to get from cache first
        var cachedClusters = await _cacheService.GetAsync<List<Cluster>>(CacheGroup, cacheKey, s_cacheDuration);
        if (cachedClusters != null)
        {
            return cachedClusters;
        }

        var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
        var clusters = new List<Cluster>();

        try
        {
            var tenantResource = (await _tenantService.GetTenants()).FirstOrDefault(t => t.Data.TenantId == subscriptionResource.Data.TenantId);
            if (tenantResource == null)
            {
                return clusters; // Return empty list if no tenant found
            }

            var queryContent = new ResourceQueryContent("Resources | where type =~ 'Microsoft.ContainerService/managedClusters' | project id, name, type, location, tags, sku, properties")
            {
                Subscriptions = { subscriptionResource.Data.SubscriptionId }
            };
            ResourceQueryResult result = await tenantResource.GetResourcesAsync(queryContent);
            if (result == null || result.Count == 0)
            {
                return clusters; // Return empty list if no clusters found
            }

            using var jsonDocument = JsonDocument.Parse(result.Data);
            var dataArray = jsonDocument.RootElement;
            if (dataArray.ValueKind != JsonValueKind.Array)
            {
                return clusters; // Return empty list if data is not an array
            }
            foreach (var item in dataArray.EnumerateArray())
            {
                clusters.Add(ConvertToClusterModel(item));
            }

            // Cache the results
            await _cacheService.SetAsync(CacheGroup, cacheKey, clusters, s_cacheDuration);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving AKS clusters: {ex.Message}", ex);
        }

        return clusters;
    }

    public async Task<Cluster?> GetCluster(
        string subscription,
        string clusterName,
        string resourceGroup,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription, clusterName, resourceGroup);

        // Create cache key
        var cacheKey = string.IsNullOrEmpty(tenant)
            ? $"cluster_{subscription}_{resourceGroup}_{clusterName}"
            : $"cluster_{subscription}_{resourceGroup}_{clusterName}_{tenant}";

        // Try to get from cache first
        var cachedCluster = await _cacheService.GetAsync<Cluster>(CacheGroup, cacheKey, s_cacheDuration);
        if (cachedCluster != null)
        {
            return cachedCluster;
        }

        var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
        var clusters = new List<Cluster>();

        var tenantResource = (await _tenantService.GetTenants()).FirstOrDefault(t => t.Data.TenantId == subscriptionResource.Data.TenantId);
        if (tenantResource == null)
        {
            return null;
        }

        try
        {
            var queryContent = new ResourceQueryContent($"Resources | where type =~ 'Microsoft.ContainerService/managedClusters' and name =~ '{clusterName}' | project id, name, type, location, tags, sku, properties")
            {
                Subscriptions = { subscriptionResource.Data.SubscriptionId }
            };
            ResourceQueryResult result = await tenantResource.GetResourcesAsync(queryContent);
            if (result == null || result.Count == 0)
            {
                return null;
            }

            using var jsonDocument = JsonDocument.Parse(result.Data);
            var dataArray = jsonDocument.RootElement;
            var item = dataArray.ValueKind == JsonValueKind.Array && dataArray.GetArrayLength() > 0
                ? dataArray[0]
                : default;
            if (item.ValueKind == JsonValueKind.Object)
            {
                var cluster = ConvertToClusterModel(item);

                // Cache the result
                await _cacheService.SetAsync(CacheGroup, cacheKey, cluster, s_cacheDuration);

                return cluster;
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving AKS cluster '{clusterName}': {ex.Message}", ex);
        }

        return null;
    }

    private static Cluster ConvertToClusterModel(JsonElement item)
    {
        AksCluster aksCluster = AksCluster.FromJson(item);
        var agentPool = aksCluster.Properties.AgentPoolProfiles?.FirstOrDefault();

        // Resource identity
        var id = new ResourceIdentifier(aksCluster.ResourceId);

        return new Cluster
        {
            Name = aksCluster.ResourceName,
            SubscriptionId = id.SubscriptionId,
            ResourceGroupName = id.ResourceGroupName,
            Location = aksCluster.Location,
            IdentityType = aksCluster.IdentityType,
            ProvisioningState = aksCluster.Properties.ProvisioningState,
            SkuTier = aksCluster.Sku.Tier,
            Tags = aksCluster.Tags,
            KubernetesVersion = aksCluster.Properties.KubernetesVersion,
            PowerState = aksCluster.Properties.PowerState.Code,
            DnsPrefix = aksCluster.Properties.DnsPrefix,
            Fqdn = aksCluster.Properties.Fqdn,
            NodeCount = agentPool?.Count,
            NodeVmSize = agentPool?.VmSize,
            EnableRbac = aksCluster.Properties.EnableRbac,
            NetworkPlugin = aksCluster.Properties.NetworkProfile?.NetworkPlugin,
            NetworkPolicy = aksCluster.Properties.NetworkProfile?.NetworkPolicy,
            ServiceCidr = aksCluster.Properties.NetworkProfile?.ServiceCidr,
            DnsServiceIP = aksCluster.Properties.NetworkProfile?.DnsServiceIP
        };
    }
}
