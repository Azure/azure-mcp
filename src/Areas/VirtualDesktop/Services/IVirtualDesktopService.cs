// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading.Tasks;
using AzureMcp.Areas.VirtualDesktop.Models;

namespace AzureMcp.Areas.VirtualDesktop.Services;

using AzureMcp.Options;

public interface IVirtualDesktopService
{
    Task<IReadOnlyList<HostPool>> ListHostpoolsAsync(string subscription, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    Task<IReadOnlyList<SessionHost>> ListSessionHostsAsync(string subscription, string hostPoolName, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    Task<IReadOnlyList<UserSession>> ListUserSessionsAsync(string subscription, string hostPoolName, string sessionHostName, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
}
