// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.ResourceManager.DesktopVirtualization;
using AzureMcp.Services.Azure.Subscription;
using AzureMcp.Options;

namespace AzureMcp.Areas.VirtualDesktop.Services;

public class VirtualDesktopService(ISubscriptionService subscriptionService) : IVirtualDesktopService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService;

    public async Task<IReadOnlyList<string>> ListHostpoolsAsync(string subscription, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        var sub = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
        var hostpoolNames = new List<string>();
        await foreach (HostPoolResource resource in sub.GetHostPoolsAsync())
        {
            hostpoolNames.Add(resource.Data.Name);
        }
        return hostpoolNames;
    }

    public async Task<IReadOnlyList<string>> ListSessionHostsAsync(string subscription, string hostPoolName, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        var sub = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
        var sessionHostNames = new List<string>();
        
        await foreach (HostPoolResource resource in sub.GetHostPoolsAsync())
        {
            if (resource.Data.Name == hostPoolName)
            {
                var armClient = sub.GetCachedClient(client => client);
                var hostPool = armClient.GetHostPoolResource(resource.Id);
                await foreach (SessionHostResource sessionHost in hostPool.GetSessionHosts().GetAllAsync())
                {
                    sessionHostNames.Add(sessionHost.Data.Name);
                }
                break; // Found the host pool, no need to continue
            }
        }
        
        return sessionHostNames;
    }
}
