// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.ResourceManager;
using AzureMcp.Areas.Quota.Models;
using AzureMcp.Areas.Quota.Services.Util;
using AzureMcp.Services.Azure;

namespace AzureMcp.Areas.Quota.Services;

public class QuotaService() : BaseAzureService, IQuotaService
{
    public async Task<Dictionary<string, List<UsageInfo>>> GetAzureQuotaAsync(
        List<string> resourceTypes,
        string subscriptionId,
        string location)
    {
        TokenCredential credential = await GetCredential();
        Dictionary<string, List<UsageInfo>> quotaByResourceTypes = await AzureQuotaService.GetAzureQuotaAsync(
            credential,
            resourceTypes,
            subscriptionId,
            location
            );
        return quotaByResourceTypes;
    }

    public async Task<List<string>> GetAvailableRegionsForResourceTypesAsync(
        string[] resourceTypes,
        string subscriptionId,
        string? cognitiveServiceModelName = null,
        string? cognitiveServiceModelVersion = null,
        string? cognitiveServiceDeploymentSkuName = null)
    {
        ArmClient armClient = await CreateArmClientAsync();

        // Create cognitive service properties if any of the parameters are provided
        CognitiveServiceProperties? cognitiveServiceProperties = null;
        if (!string.IsNullOrWhiteSpace(cognitiveServiceModelName) ||
            !string.IsNullOrWhiteSpace(cognitiveServiceModelVersion) ||
            !string.IsNullOrWhiteSpace(cognitiveServiceDeploymentSkuName))
        {
            cognitiveServiceProperties = new CognitiveServiceProperties
            {
                ModelName = cognitiveServiceModelName,
                ModelVersion = cognitiveServiceModelVersion,
                DeploymentSkuName = cognitiveServiceDeploymentSkuName
            };
        }

        var availableRegions = await AzureRegionService.GetAvailableRegionsForResourceTypesAsync(armClient, resourceTypes, subscriptionId, cognitiveServiceProperties);
        var allRegions = availableRegions.Values
            .Where(regions => regions.Count > 0)
            .SelectMany(regions => regions)
            .Distinct()
            .ToList();

        List<string> commonValidRegions = availableRegions.Values
            .Aggregate((current, next) => current.Intersect(next).ToList());

        return commonValidRegions;
    }
}
