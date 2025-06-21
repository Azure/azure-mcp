// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Monitor.Query;
using AzureMcp.Options;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Azure.Tenant;

namespace AzureMcp.Areas.Monitor.Services;

public class MetricsQueryClientService(ITenantService tenantService) : BaseAzureService(tenantService), IMetricsQueryClientService
{
    public async Task<MetricsQueryClient> CreateClientAsync(string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        var credential = await GetCredential(tenant);
        var options = AddDefaultPolicies(new MetricsQueryClientOptions());

        if (retryPolicy != null)
        {
            options.Retry.Delay = TimeSpan.FromSeconds(retryPolicy.DelaySeconds);
            options.Retry.MaxDelay = TimeSpan.FromSeconds(retryPolicy.MaxDelaySeconds);
            options.Retry.MaxRetries = retryPolicy.MaxRetries;
            options.Retry.Mode = retryPolicy.Mode;
            options.Retry.NetworkTimeout = TimeSpan.FromSeconds(retryPolicy.NetworkTimeoutSeconds);
        }

        return new MetricsQueryClient(credential, options);
    }
}
