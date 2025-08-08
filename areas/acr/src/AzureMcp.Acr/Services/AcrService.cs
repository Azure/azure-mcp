// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.ContainerRegistry;
using AzureMcp.Core.Services.Azure;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;

namespace AzureMcp.Acr.Services;

public class AcrService(ISubscriptionService subscriptionService, ITenantService tenantService) : BaseAzureService(tenantService), IAcrService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));

    public async Task<List<string>> ListRegistries(
        string subscription,
        string? resourceGroup = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription);

        var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
        var registries = new List<string>();

        try
        {
            if (!string.IsNullOrEmpty(resourceGroup))
            {
                // List registries in a specific resource group
                var resourceGroupResource = await subscriptionResource.GetResourceGroupAsync(resourceGroup);
                await foreach (var registry in resourceGroupResource.Value.GetContainerRegistries().GetAllAsync())
                {
                    if (registry?.Data?.Name != null)
                    {
                        registries.Add(registry.Data.Name);
                    }
                }
            }
            else
            {
                // List all registries in the subscription
                await foreach (var registry in subscriptionResource.GetContainerRegistriesAsync())
                {
                    if (registry?.Data?.Name != null)
                    {
                        registries.Add(registry.Data.Name);
                    }
                }
            }

            return registries;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving Azure Container Registries: {ex.Message}", ex);
        }
    }
}
