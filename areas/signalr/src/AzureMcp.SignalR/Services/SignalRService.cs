// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Options;
using AzureMcp.Core.Services.Azure;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.SignalR.Models;

namespace AzureMcp.SignalR.Services;

/// <summary>
/// Service for Azure SignalR operations.
/// </summary>
public class SignalRService(ISubscriptionService _subscriptionService, ITenantService tenantService)
    : BaseAzureService(tenantService), ISignalRService
{
    public async Task<IEnumerable<SignalRRuntimeModel>> ListRuntimesAsync(
        string subscription,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription);

        try
        {
            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy)
                                       ?? throw new Exception($"Subscription '{subscription}' not found");

            var runtimes = new List<SignalRRuntimeModel>();

            await foreach (var signalR in subscriptionResource.GetSignalRsAsync())
            {
                runtimes.Add(new SignalRRuntimeModel
                {
                    Name = signalR.Data.Name,
                    ResourceGroupName = signalR.Id.ResourceGroupName ?? string.Empty,
                    Location = signalR.Data.Location.Name,
                    SkuName = signalR.Data.Sku?.Name ?? string.Empty,
                    SkuTier = signalR.Data.Sku?.Tier?.ToString() ?? string.Empty,
                    ProvisioningState = signalR.Data.ProvisioningState?.ToString() ?? string.Empty,
                    HostName = signalR.Data.HostName ?? string.Empty,
                    PublicPort = signalR.Data.PublicPort,
                    ServerPort = signalR.Data.ServerPort
                });
            }

            return runtimes;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to list SignalR services: {ex.Message}", ex);
        }
    }

    public async Task<SignalRKeyModel> ListKeysAsync(
        string subscription,
        string resourceGroupName,
        string signalRName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription, resourceGroupName, signalRName);

        try
        {
            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy)
                                       ?? throw new Exception($"Subscription '{subscription}' not found");

            var resourceGroupResource = await subscriptionResource
                .GetResourceGroupAsync(resourceGroupName);

            var signalRResource = await resourceGroupResource.Value
                .GetSignalRs()
                .GetAsync(signalRName);

            var keys = await signalRResource.Value.GetKeysAsync();

            return new SignalRKeyModel
            {
                KeyType = "Both",
                PrimaryKey = keys.Value.PrimaryKey ?? string.Empty,
                SecondaryKey = keys.Value.SecondaryKey ?? string.Empty,
                PrimaryConnectionString = keys.Value.PrimaryConnectionString ?? string.Empty,
                SecondaryConnectionString = keys.Value.SecondaryConnectionString ?? string.Empty,
                ConnectionString = keys.Value.PrimaryConnectionString ?? string.Empty
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to list SignalR keys for service '{signalRName}': {ex.Message}", ex);
        }
    }

    public async Task<SignalRRuntimeModel?> GetRuntimeAsync(
        string subscription,
        string resourceGroupName,
        string signalRName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription, resourceGroupName, signalRName);

        try
        {
            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy)
                                       ?? throw new Exception($"Subscription '{subscription}' not found");

            var resourceGroupResource = await subscriptionResource
                .GetResourceGroupAsync(resourceGroupName);

            var signalRResource = await resourceGroupResource.Value
                .GetSignalRs()
                .GetAsync(signalRName);

            var signalR = signalRResource.Value;

            return new SignalRRuntimeModel
            {
                Name = signalR.Data.Name,
                ResourceGroupName = signalR.Id.ResourceGroupName ?? string.Empty,
                Location = signalR.Data.Location.Name,
                SkuName = signalR.Data.Sku?.Name ?? string.Empty,
                SkuTier = signalR.Data.Sku?.Tier?.ToString() ?? string.Empty,
                ProvisioningState = signalR.Data.ProvisioningState?.ToString() ?? string.Empty,
                HostName = signalR.Data.HostName ?? string.Empty,
                PublicPort = signalR.Data.PublicPort,
                ServerPort = signalR.Data.ServerPort
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to get SignalR service '{signalRName}': {ex.Message}", ex);
        }
    }

    public async Task<SignalRIdentityModel?> GetSignalRIdentityAsync(
        string subscription,
        string resourceGroupName,
        string signalRName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription, resourceGroupName, signalRName);

        try
        {
            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy)
                                       ?? throw new Exception($"Subscription '{subscription}' not found");

            var resourceGroupResource = await subscriptionResource
                .GetResourceGroupAsync(resourceGroupName);

            var signalRResource = await resourceGroupResource.Value
                .GetSignalRs()
                .GetAsync(signalRName);

            var signalR = signalRResource.Value;
            var identity = signalR.Data.Identity;

            if (identity == null)
            {
                return null;
            }

            var userAssignedIdentities = new Dictionary<string, UserAssignedIdentity>();
            if (identity.UserAssignedIdentities != null)
            {
                foreach (var (key, value) in identity.UserAssignedIdentities)
                {
                    if (!string.IsNullOrEmpty(key) && value != null)
                    {
                        userAssignedIdentities[key!] = new UserAssignedIdentity
                        {
                            PrincipalId = value.PrincipalId?.ToString(),
                            ClientId = value.ClientId?.ToString()
                        };
                    }
                }
            }

            return new SignalRIdentityModel
            {
                Type = identity.ManagedServiceIdentityType.ToString(),
                PrincipalId = identity.PrincipalId?.ToString(),
                TenantId = identity.TenantId?.ToString(),
                UserAssignedIdentities = userAssignedIdentities.Count > 0 ? userAssignedIdentities : null
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to get SignalR identity for service '{signalRName}': {ex.Message}", ex);
        }
    }

    public async Task<SignalRNetworkAclModel?> GetNetworkRulesAsync(
        string subscription,
        string resourceGroup,
        string signalRName,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription, resourceGroup, signalRName);

        try
        {
            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, null, retryPolicy);

            var resourceGroupResource = await subscriptionResource
                .GetResourceGroupAsync(resourceGroup, cancellationToken: default);

            var signalRResource = await resourceGroupResource.Value
                .GetSignalRs()
                .GetAsync(signalRName, cancellationToken: default);

            var networkAcls = signalRResource.Value.Data.NetworkACLs;
            if (networkAcls == null)
            {
                return null;
            }

            return new SignalRNetworkAclModel
            {
                DefaultAction = networkAcls.DefaultAction?.ToString(),
                PublicNetwork =
                    networkAcls.PublicNetwork != null
                        ? new SignalRNetworkRuleModel
                        {
                            Allow = networkAcls.PublicNetwork.Allow?.Select(a => a.ToString()),
                            Deny = networkAcls.PublicNetwork.Deny?.Select(d => d.ToString())
                        }
                        : null,
                PrivateEndpoints = networkAcls.PrivateEndpoints?.Select(pe => new SignalRPrivateEndpointModel
                {
                    Name = pe.Name,
                    Allow = pe.Allow?.Select(a => a.ToString()),
                    Deny = pe.Deny?.Select(d => d.ToString())
                })
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to get SignalR identity for service '{signalRName}': {ex.Message}", ex);
        }
    }
}
