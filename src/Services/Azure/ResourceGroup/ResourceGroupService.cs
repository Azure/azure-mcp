using Azure.ResourceManager.Resources;
using AzureMCP.Arguments;
using AzureMCP.Models.ResourceGroup;
using AzureMCP.Services.Interfaces;

namespace AzureMCP.Services.Azure.ResourceGroup;

public class ResourceGroupService(ICacheService cacheService, ISubscriptionService subscriptionService) : BaseAzureService, IResourceGroupService
{
    private readonly ICacheService _cacheService = cacheService;
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private const string CACHE_KEY = "resourcegroups";
    private static readonly TimeSpan CACHE_DURATION = TimeSpan.FromHours(1);

    public async Task<List<ResourceGroupInfo>> GetResourceGroups(string subscription, string? tenant = null, RetryPolicyArguments? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription);

        var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
        var subscriptionId = subscriptionResource.Data.SubscriptionId;

        // Try to get from cache first
        var cacheKey = $"{CACHE_KEY}_{subscriptionId}_{tenant ?? "default"}";
        var cachedResults = await _cacheService.GetAsync<List<ResourceGroupInfo>>(cacheKey, CACHE_DURATION);
        if (cachedResults != null)
        {
            return cachedResults;
        }

        // If not in cache, fetch from Azure
        try
        {
            var resourceGroups = await subscriptionResource.GetResourceGroups()
                .GetAllAsync()
                .Select(rg => new ResourceGroupInfo(
                    rg.Data.Name,
                    rg.Data.Id.ToString(),
                    rg.Data.Location.ToString()))
                .ToListAsync();

            // Cache the results
            await _cacheService.SetAsync(cacheKey, resourceGroups, CACHE_DURATION);

            return resourceGroups;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving resource groups: {ex.Message}", ex);
        }
    }

    public async Task<ResourceGroupInfo?> GetResourceGroup(string subscription, string resourceGroupName, string? tenant = null, RetryPolicyArguments? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription, resourceGroupName);

        var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
        var subscriptionId = subscriptionResource.Data.SubscriptionId;

        // Try to get from cache first
        var cacheKey = $"{CACHE_KEY}_{subscriptionId}_{tenant ?? "default"}";
        var cachedResults = await _cacheService.GetAsync<List<ResourceGroupInfo>>(cacheKey, CACHE_DURATION);
        if (cachedResults != null)
        {
            return cachedResults.FirstOrDefault(rg => rg.Name.Equals(resourceGroupName, StringComparison.OrdinalIgnoreCase));
        }

        try
        {
            var rg = await GetResourceGroupResource(subscription, resourceGroupName, tenant, retryPolicy);
            if (rg == null)
            {
                return null;
            }

            return new ResourceGroupInfo(
                rg.Data.Name,
                rg.Data.Id.ToString(),
                rg.Data.Location.ToString());
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving resource group {resourceGroupName}: {ex.Message}", ex);
        }
    }

    public async Task<ResourceGroupResource?> GetResourceGroupResource(string subscription, string resourceGroupName, string? tenant = null, RetryPolicyArguments? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription, resourceGroupName);

        try
        {
            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
            var resourceGroupResponse = await subscriptionResource.GetResourceGroups()
                .GetAsync(resourceGroupName)
                .ConfigureAwait(false);

            return resourceGroupResponse?.Value;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving resource group {resourceGroupName}: {ex.Message}", ex);
        }
    }
}