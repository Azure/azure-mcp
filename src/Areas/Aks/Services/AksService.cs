// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.ContainerService;
using AzureMcp.Areas.Aks.Models;
using AzureMcp.Options;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Azure.Subscription;
using AzureMcp.Services.Azure.Tenant;
using AzureMcp.Services.Caching;

namespace AzureMcp.Areas.Aks.Services;

public sealed class AksService(
    ISubscriptionService subscriptionService,
    ITenantService tenantService,
    ICacheService cacheService) : BaseAzureService(tenantService), IAksService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private readonly ICacheService _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));

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
            await foreach (var cluster in subscriptionResource.GetContainerServiceManagedClustersAsync())
            {
                if (cluster?.Data != null)
                {
                    clusters.Add(ConvertToClusterModel(cluster));
                }
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

    private static Cluster ConvertToClusterModel(ContainerServiceManagedClusterResource clusterResource)
    {
        var data = clusterResource.Data;
        var agentPool = data.AgentPoolProfiles?.FirstOrDefault();

        return new Cluster
        {
            Name = data.Name,
            SubscriptionId = clusterResource.Id.SubscriptionId,
            ResourceGroupName = clusterResource.Id.ResourceGroupName,
            Location = data.Location.ToString(),
            KubernetesVersion = data.KubernetesVersion,
            ProvisioningState = data.ProvisioningState?.ToString(),
            PowerState = data.PowerStateCode?.ToString(),
            DnsPrefix = data.DnsPrefix,
            Fqdn = data.Fqdn,
            NodeCount = agentPool?.Count,
            NodeVmSize = agentPool?.VmSize,
            IdentityType = data.Identity?.ManagedServiceIdentityType.ToString(),
            EnableRbac = data.EnableRbac,
            NetworkPlugin = data.NetworkProfile?.NetworkPlugin?.ToString(),
            NetworkPolicy = data.NetworkProfile?.NetworkPolicy?.ToString(),
            ServiceCidr = data.NetworkProfile?.ServiceCidr,
            DnsServiceIP = data.NetworkProfile?.DnsServiceIP?.ToString(),
            SkuTier = data.Sku?.Tier?.ToString(),
            Tags = data.Tags?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
        };
    }
}
