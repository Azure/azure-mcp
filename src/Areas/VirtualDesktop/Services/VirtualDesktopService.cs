// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.ResourceManager.DesktopVirtualization;
using AzureMcp.Services.Azure.Subscription;
using AzureMcp.Options;
using AzureMcp.Areas.VirtualDesktop.Models;

namespace AzureMcp.Areas.VirtualDesktop.Services;

public class VirtualDesktopService(ISubscriptionService subscriptionService) : IVirtualDesktopService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService;

    public async Task<IReadOnlyList<HostPool>> ListHostpoolsAsync(string subscription, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        var sub = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
        var hostpools = new List<HostPool>();
        await foreach (HostPoolResource resource in sub.GetHostPoolsAsync())
        {
            hostpools.Add(new HostPool(resource));
        }
        return hostpools;
    }

    public async Task<IReadOnlyList<SessionHost>> ListSessionHostsAsync(string subscription, string hostPoolName, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        var sub = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
        var sessionHosts = new List<SessionHost>();
        
        await foreach (HostPoolResource resource in sub.GetHostPoolsAsync())
        {
            if (resource.Data.Name == hostPoolName)
            {
                var armClient = sub.GetCachedClient(client => client);
                var hostPool = armClient.GetHostPoolResource(resource.Id);
                await foreach (SessionHostResource sessionHost in hostPool.GetSessionHosts().GetAllAsync())
                {
                    sessionHosts.Add(new SessionHost(sessionHost));
                }
                break; // Found the host pool, no need to continue
            }
        }
        
        return sessionHosts;
    }

    public async Task<IReadOnlyList<UserSession>> ListUserSessionsAsync(string subscription, string hostPoolName, string sessionHostName, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        var sub = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
        var userSessions = new List<UserSession>();
        
        await foreach (HostPoolResource resource in sub.GetHostPoolsAsync())
        {
            if (resource.Data.Name == hostPoolName)
            {
                var armClient = sub.GetCachedClient(client => client);
                var hostPool = armClient.GetHostPoolResource(resource.Id);
                await foreach (SessionHostResource sessionHost in hostPool.GetSessionHosts().GetAllAsync())
                {
                    if (sessionHost.Data.Name == sessionHostName || sessionHost.Data.Name == $"{hostPoolName}/{sessionHostName}")
                    {
                        await foreach (UserSessionResource userSession in sessionHost.GetUserSessions().GetAllAsync())
                        {
                            userSessions.Add(new UserSession(userSession));
                        }
                        break; // Found the session host, no need to continue
                    }
                }
                break; // Found the host pool, no need to continue
            }
        }
        
        return userSessions;
    }
}
