// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureMcp.Areas.VirtualDesktop.Services;

using AzureMcp.Options;

public interface IVirtualDesktopService
{
    Task<IReadOnlyList<string>> ListHostpoolsAsync(string subscription, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    Task<IReadOnlyList<string>> ListSessionHostsAsync(string subscription, string hostPoolName, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
}
