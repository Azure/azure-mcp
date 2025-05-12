// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.Resources;
using AzureMcp.Arguments;
using AzureMcp.Models.Argument;
using AzureMcp.Services.Interfaces;

namespace AzureMcp.Services.Azure.Subscription;

public class SubscriptionService(ICacheService cacheService, ITenantService tenantService)
    : BaseAzureService(tenantService), ISubscriptionService
{
    private readonly ICacheService _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
    private const string CACHE_GROUP = "subscription";
    private const string CACHE_KEY = "subscriptions";
    private const string SUBSCRIPTION_CACHE_KEY = "subscription";
    private static readonly TimeSpan CACHE_DURATION = TimeSpan.FromHours(12);

    public async Task<List<ArgumentOption>> GetSubscriptions(string? tenant = null, RetryPolicyArguments? retryPolicy = null)
    {
        // Try to get from cache first
        var cacheKey = string.IsNullOrEmpty(tenant) ? CACHE_KEY : $"{CACHE_KEY}_{tenant}";
        var cachedResults = await _cacheService.GetAsync<List<ArgumentOption>>(CACHE_GROUP, cacheKey, CACHE_DURATION);
        if (cachedResults != null)
        {
            return cachedResults;
        }

        // If not in cache, fetch from Azure
        var armClient = await CreateArmClientAsync(tenant, retryPolicy);
        var subscriptions = armClient.GetSubscriptions();
        var results = new List<ArgumentOption>();

        foreach (var subscription in subscriptions)
        {
            results.Add(new ArgumentOption
            {
                Name = subscription.Data.DisplayName,
                Id = subscription.Data.SubscriptionId
            });
        }

        // Cache the results
        await _cacheService.SetAsync(CACHE_GROUP, cacheKey, results, CACHE_DURATION);

        return results;
    }

    public async Task<SubscriptionResource> GetSubscription(string subscription, string? tenant = null, RetryPolicyArguments? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription);

        // Get the subscription ID first, whether the input is a name or ID
        var subscriptionId = await GetSubscriptionId(subscription, tenant, retryPolicy);

        // Use subscription ID for cache key
        var cacheKey = string.IsNullOrEmpty(tenant)
            ? $"{SUBSCRIPTION_CACHE_KEY}_{subscriptionId}"
            : $"{SUBSCRIPTION_CACHE_KEY}_{subscriptionId}_{tenant}";
        var cachedSubscription = await _cacheService.GetAsync<SubscriptionResource>(CACHE_GROUP, cacheKey, CACHE_DURATION);
        if (cachedSubscription != null)
        {
            return cachedSubscription;
        }

        var armClient = await CreateArmClientAsync(tenant, retryPolicy);
        var response = await armClient.GetSubscriptionResource(SubscriptionResource.CreateResourceIdentifier(subscriptionId)).GetAsync();
        if (response?.Value == null)
        {
            throw new Exception($"Could not retrieve subscription {subscription}");
        }

        // Cache the result using subscription ID
        await _cacheService.SetAsync(CACHE_GROUP, cacheKey, response.Value, CACHE_DURATION);

        return response.Value;
    }

    public bool IsSubscriptionId(string subscription, string? tenant = null)
    {
        return Guid.TryParse(subscription, out _);
    }

    public async Task<string> GetSubscriptionIdByName(string subscriptionName, string? tenant = null, RetryPolicyArguments? retryPolicy = null)
    {
        var subscriptions = await GetSubscriptions(tenant, retryPolicy);
        var subscription = subscriptions.FirstOrDefault(s => s.Name.Equals(subscriptionName, StringComparison.OrdinalIgnoreCase)) ??
            throw new Exception($"Could not find subscription with name {subscriptionName}");

        return subscription.Id;
    }

    public async Task<string> GetSubscriptionNameById(string subscriptionId, string? tenant = null, RetryPolicyArguments? retryPolicy = null)
    {
        var subscriptions = await GetSubscriptions(tenant, retryPolicy);
        var subscription = subscriptions.FirstOrDefault(s => s.Id.Equals(subscriptionId, StringComparison.OrdinalIgnoreCase)) ??
            throw new Exception($"Could not find subscription with ID {subscriptionId}");

        return subscription.Name;
    }

    private async Task<string> GetSubscriptionId(string subscription, string? tenant, RetryPolicyArguments? retryPolicy)
    {
        if (IsSubscriptionId(subscription))
        {
            return subscription;
        }

        return await GetSubscriptionIdByName(subscription, tenant, retryPolicy);
    }
}
