// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Quota.Services.Util;

namespace AzureMcp.Quota.Services;

public interface IQuotaService
{
    Task<Dictionary<string, List<UsageInfo>>> GetAzureQuotaAsync(
        List<string> resourceTypes,
        string subscriptionId,
        string location);

    Task<List<string>> GetAvailableRegionsForResourceTypesAsync(
        string[] resourceTypes,
        string subscriptionId,
        string? cognitiveServiceModelName = null,
        string? cognitiveServiceModelVersion = null,
        string? cognitiveServiceDeploymentSkuName = null);
}
