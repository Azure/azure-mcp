// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.ResourceManager;
using Azure.ResourceManager.ContainerRegistry;
using Azure.ResourceManager.ContainerRegistry.Models;
using Azure.ResourceManager.Resources;
using AzureMcp.Areas.ContainerApps.Models;
using AzureMcp.Models.Identity;
using AzureMcp.Options;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Azure.Subscription;
using AzureMcp.Services.Azure.Tenant;

namespace AzureMcp.Areas.ContainerApps.Services;

public class ContainerAppsService(ISubscriptionService subscriptionService, ITenantService tenantService)
    : BaseAzureService(tenantService), IContainerAppsService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));

    public async Task<ContainerRegistry> CreateContainerRegistry(
        string registryName,
        string resourceGroup,
        string subscriptionId,
        string location,
        string? skuName = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(registryName, resourceGroup, subscriptionId, location);

        var subscription = await _subscriptionService.GetSubscription(subscriptionId, tenant, retryPolicy);
        var resourceGroupResource = await subscription.GetResourceGroupAsync(resourceGroup);

        if (!resourceGroupResource.HasValue)
        {
            throw new Exception($"Resource group '{resourceGroup}' not found in subscription '{subscriptionId}'");
        }

        // Set default SKU if not provided
        var sku = string.IsNullOrEmpty(skuName) ? "Basic" : skuName;

        // Create the SKU object
        var registrySku = sku.ToLowerInvariant() switch
        {
            "basic" => new ContainerRegistrySku(ContainerRegistrySkuName.Basic),
            "standard" => new ContainerRegistrySku(ContainerRegistrySkuName.Standard),
            "premium" => new ContainerRegistrySku(ContainerRegistrySkuName.Premium),
            _ => new ContainerRegistrySku(ContainerRegistrySkuName.Basic)
        };

        // Create the container registry data
        var registryData = new ContainerRegistryData(new AzureLocation(location), registrySku);

        // Create the container registry
        var registryCollection = resourceGroupResource.Value.GetContainerRegistries();
        var createOperation = await registryCollection.CreateOrUpdateAsync(
            Azure.WaitUntil.Completed,
            registryName,
            registryData,
            cancellationToken: default);

        var registry = createOperation.Value;

        return new ContainerRegistry
        {
            Name = registry.Data.Name,
            ResourceGroup = resourceGroup,
            Location = registry.Data.Location.ToString(),
            LoginServer = registry.Data.LoginServer,
            CreationDate = registry.Data.CreatedOn?.DateTime ?? DateTime.MinValue,
            Sku = registry.Data.Sku.Name.ToString(),
            AdminUserEnabled = registry.Data.IsAdminUserEnabled ?? false,
            Tags = registry.Data.Tags ?? new Dictionary<string, string>(),
            ProvisioningState = registry.Data.ProvisioningState?.ToString() ?? string.Empty,
            PublicNetworkAccess = registry.Data.PublicNetworkAccess?.ToString(),
            ManagedIdentity = registry.Data.Identity == null ? null : new ManagedIdentityInfo
            {
                SystemAssignedIdentity = new SystemAssignedIdentityInfo
                {
                    Enabled = registry.Data.Identity != null,
                    TenantId = registry.Data.Identity?.TenantId?.ToString(),
                    PrincipalId = registry.Data.Identity?.PrincipalId?.ToString()
                },
                UserAssignedIdentities = registry.Data.Identity?.UserAssignedIdentities?
                    .Select(id => new UserAssignedIdentityInfo
                    {
                        ClientId = id.Value.ClientId?.ToString(),
                        PrincipalId = id.Value.PrincipalId?.ToString()
                    })
                    .ToArray()
            }
        };
    }
}
