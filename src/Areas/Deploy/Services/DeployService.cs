// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using Areas.Deploy.Services.Util;
using Azure.Core;
using Azure.ResourceManager;
using AzureMcp.Areas.Deploy.Models;
using AzureMcp.Services.Azure;

namespace AzureMcp.Areas.Deploy.Services;

public class DeployService() : BaseAzureService, IDeployService
{

    [UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.", Justification = "<Pending>")]
    public async Task<string> GetAzdResourceLogsAsync(
         string workspaceFolder,
         string azdEnvName,
         string subscriptionId,
         int? limit = null)
    {
        TokenCredential credential = await GetCredential();
        string result = await AzdResourceLogService.GetAzdResourceLogsAsync(
            credential,
            workspaceFolder,
            azdEnvName,
            subscriptionId,
            limit);
        return result;
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
