// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.AppContainers;
using AzureMcp.Areas.ContainerApps.Models;
using AzureMcp.Helpers;
using AzureMcp.Options;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Azure.Subscription;
using AzureMcp.Services.Azure.Tenant;

namespace AzureMcp.Areas.ContainerApps.Services;

public class ContainerAppsService(ISubscriptionService subscriptionService, ITenantService tenantService)
    : BaseAzureService(tenantService), IContainerAppsService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));

    public async Task<List<ContainerApp>> ListApps(
        string subscription,
        string? resourceGroupName = null,
        string? environmentName = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription);

        var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
        var apps = new List<ContainerApp>();

        try
        {
            if (!string.IsNullOrEmpty(resourceGroupName))
            {
                // List apps in specific resource group - enumerate subscription apps and filter efficiently
                var resourceGroup = await subscriptionResource.GetResourceGroupAsync(resourceGroupName);
                if (resourceGroup?.Value != null)
                {
                    // The Azure SDK doesn't provide resource group-level enumeration for Container Apps
                    // So we enumerate at subscription level but filter early to minimize processing
                    await foreach (var appResource in subscriptionResource.GetContainerAppsAsync())
                    {
                        // Early filter by resource group to avoid unnecessary object conversion
                        if (appResource.Id.ResourceGroupName?.Equals(resourceGroupName, StringComparison.OrdinalIgnoreCase) == true)
                        {
                            var app = ConvertToContainerApp(appResource);
                            if (string.IsNullOrEmpty(environmentName) ||
                                IsAppInEnvironment(app, environmentName))
                            {
                                apps.Add(app);
                            }
                        }
                    }
                }
            }
            else
            {
                // List all apps in subscription
                await foreach (var appResource in subscriptionResource.GetContainerAppsAsync())
                {
                    var app = ConvertToContainerApp(appResource);
                    if (string.IsNullOrEmpty(environmentName) ||
                        IsAppInEnvironment(app, environmentName))
                    {
                        apps.Add(app);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error listing container apps: {ex.Message}", ex);
        }

        return apps;
    }

    private static ContainerApp ConvertToContainerApp(ContainerAppResource appResource)
    {
        try
        {
            var data = appResource.Data;

            return new ContainerApp
            {
                Name = data.Name,
                Id = data.Id?.ToString(),
                Type = data.ResourceType.ToString(),
                Location = data.Location.ToString(),
                ResourceGroup = appResource.Id.ResourceGroupName,
                SubscriptionId = appResource.Id.SubscriptionId,
                ManagedEnvironmentId = data.EnvironmentId?.ToString(),
                ProvisioningState = data.ProvisioningState?.ToString(),
                Tags = TagConverter.ConvertTagsToString(data.Tags)
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error converting container app resource to model: {ex.Message}", ex);
        }
    }

    private static bool IsAppInEnvironment(ContainerApp app, string environmentName)
    {
        if (app.ManagedEnvironmentId == null)
            return false;

        // Check if the environment ID contains the environment name
        return app.ManagedEnvironmentId.Contains(environmentName, StringComparison.OrdinalIgnoreCase) ||
               app.ManagedEnvironmentId.EndsWith($"/{environmentName}", StringComparison.OrdinalIgnoreCase);
    }
}
