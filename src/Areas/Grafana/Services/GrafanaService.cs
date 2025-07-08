// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.Grafana;
using AzureMcp.Models.Identity;
using AzureMcp.Options;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Azure.Subscription;
using AzureMcp.Services.Azure.Tenant;

namespace AzureMcp.Areas.Grafana.Services;

public class GrafanaService(ISubscriptionService _subscriptionService, ITenantService tenantService)
    : BaseAzureService(tenantService), IGrafanaService
{
    public async Task<IEnumerable<Models.Workspace.Workspace>> ListWorkspacesAsync(
        string subscriptionId,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId);

        try
        {
            var subscriptionResource = await _subscriptionService.GetSubscription(subscriptionId, tenant, retryPolicy) ?? throw new Exception($"Subscription '{subscriptionId}' not found");
            var workspaces = new List<Models.Workspace.Workspace>();

            await foreach (var workspaceResource in subscriptionResource.GetManagedGrafanasAsync())
            {
                if (string.IsNullOrWhiteSpace(workspaceResource?.Id.ToString())
                    || string.IsNullOrWhiteSpace(workspaceResource.Data.Name))
                {
                    continue;
                }

                var workspace = workspaceResource.Data;
                workspaces.Add(new()
                {
                    Name = workspace.Name,
                    ResourceGroupName = workspaceResource.Id.ResourceGroupName,
                    SubscriptionId = workspaceResource.Id.SubscriptionId,
                    Location = workspace.Location,
                    Sku = workspace.SkuName,
                    ProvisioningState = workspace.Properties.ProvisioningState?.ToString(),
                    Endpoint = workspace.Properties?.Endpoint,
                    ZoneRedundancy = workspace.Properties?.ZoneRedundancy?.ToString(),
                    PublicNetworkAccess = workspace.Properties?.PublicNetworkAccess?.ToString(),
                    GrafanaVersion = workspace.Properties?.GrafanaVersion,
                    Identity = workspace.Identity is null ? null : new ManagedIdentityInfo
                    {
                        SystemAssignedIdentity = new SystemAssignedIdentityInfo
                        {
                            Enabled = workspace.Identity != null,
                            TenantId = workspace.Identity?.TenantId?.ToString(),
                            PrincipalId = workspace.Identity?.PrincipalId?.ToString()
                        },
                        UserAssignedIdentities = workspace.Identity?.UserAssignedIdentities?
                            .Select(identity => new UserAssignedIdentityInfo
                            {
                                ClientId = identity.Value.ClientId?.ToString(),
                                PrincipalId = identity.Value.PrincipalId?.ToString()
                            }).ToArray()
                    },
                    Tags = workspace.Tags?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
                });
            }

            return workspaces;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to list Grafana workspaces: {ex.Message}", ex);
        }
    }
}
