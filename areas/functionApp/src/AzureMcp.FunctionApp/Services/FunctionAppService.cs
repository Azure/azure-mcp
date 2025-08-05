// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.AppService;
using AzureMcp.Core.Options;
using AzureMcp.Core.Services.Azure;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.Core.Services.Caching;
using AzureMcp.FunctionApp.Models;

namespace AzureMcp.FunctionApp.Services;

public sealed class FunctionAppService(
    ISubscriptionService subscriptionService,
    ITenantService tenantService,
    ICacheService cacheService) : BaseAzureService(tenantService), IFunctionAppService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private readonly ICacheService _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));

    private const string CacheGroup = "functionapp";
    private const string FunctionAppsCacheKey = "functionapps";
    private static readonly TimeSpan s_cacheDuration = TimeSpan.FromHours(1);

    public async Task<List<FunctionAppModel>?> ListFunctionApps(
        string subscriptionId,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId);

        // Create cache key
        var cacheKey = string.IsNullOrEmpty(tenant)
            ? $"{FunctionAppsCacheKey}_{subscriptionId}"
            : $"{FunctionAppsCacheKey}_{subscriptionId}_{tenant}";

        // Try to get from cache first
        var cachedResults = await _cacheService.GetAsync<List<FunctionAppModel>>(CacheGroup, cacheKey, s_cacheDuration);
        if (cachedResults != null)
        {
            return cachedResults;
        }

        var subscriptionResource = await _subscriptionService.GetSubscription(subscriptionId, tenant, retryPolicy);
        var functionApps = new List<FunctionAppModel>();

        try
        {
            await foreach (var site in subscriptionResource.GetWebSitesAsync())
            {
                if (site?.Data != null && IsFunctionApp(site.Data))
                {
                    functionApps.Add(ConvertToFunctionAppModel(site));
                }
            }

            // Cache the results
            await _cacheService.SetAsync(CacheGroup, cacheKey, functionApps, s_cacheDuration);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving Function Apps: {ex.Message}", ex);
        }

        return functionApps;
    }

    private static bool IsFunctionApp(WebSiteData siteData)
    {
        return siteData.Kind?.Contains("functionapp", StringComparison.OrdinalIgnoreCase) == true;
    }

    private static FunctionAppModel ConvertToFunctionAppModel(WebSiteResource siteResource)
    {
        var data = siteResource.Data;

        return new FunctionAppModel
        {
            Name = data.Name,
            SubscriptionId = data.Id.SubscriptionId,
            ResourceGroupName = siteResource.Id.ResourceGroupName,
            Location = data.Location.ToString(),
            AppServicePlanName = data.AppServicePlanId.Name,
            Status = data.State,
            DefaultHostName = data.DefaultHostName,
            Tags = data.Tags?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
        };
    }
}
