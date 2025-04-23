// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.Compute;
using AzureMcp.Arguments;
using AzureMcp.Services.Interfaces;

namespace AzureMcp.Services.Azure.Compute
{
    public class ComputeService(ISubscriptionService subscriptionService, ICacheService cacheService) : BaseAzureService, IComputeService

    {
        private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
        private readonly ICacheService _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        private const string COMPUTE_VMS_CACHE_KEY = "compute_vms";
        private static readonly TimeSpan CACHE_DURATION = TimeSpan.FromHours(1);


        public async Task<List<string>> GetVirtualMachines(string subscriptionId, string? tenant = null, RetryPolicyArguments? retryPolicy = null)
        {
            ValidateRequiredParameters(subscriptionId);

            // Create cache key
            var cacheKey = string.IsNullOrEmpty(tenant)
                ? $"{COMPUTE_VMS_CACHE_KEY}_{subscriptionId}"
                : $"{COMPUTE_VMS_CACHE_KEY}_{subscriptionId}_{tenant}";

            // Try to get from cache first
            var cachedVms = await _cacheService.GetAsync<List<string>>(cacheKey, CACHE_DURATION);
            if (cachedVms != null)
            {
                return cachedVms;
            }

            var subscription = await _subscriptionService.GetSubscription(subscriptionId, tenant, retryPolicy);
            var vms = new List<string>();
            try
            {
                await foreach (var vm in subscription.GetVirtualMachinesAsync())
                {
                    if (vm?.Data?.Name != null)
                    {
                        vms.Add(vm.Data.Name);
                    }
                }

                // Cache the results
                await _cacheService.SetAsync(cacheKey, vms, CACHE_DURATION);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving compute vms: {ex.Message}", ex);
            }

            return vms;
        }
    }
}