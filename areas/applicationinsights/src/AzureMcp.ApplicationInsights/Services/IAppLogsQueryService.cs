// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using AzureMcp.Core.Options;

namespace AzureMcp.ApplicationInsights.Services
{
    public interface IAppLogsQueryService
    {
        Task<IAppLogsQueryClient> CreateClientAsync(ResourceIdentifier resolvedResource, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    }
}
