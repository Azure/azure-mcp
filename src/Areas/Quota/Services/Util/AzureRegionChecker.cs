// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using Azure.Core;
using Azure.ResourceManager;
using Azure.ResourceManager.CognitiveServices;
using Azure.ResourceManager.CognitiveServices.Models;
using Azure.ResourceManager.PostgreSql.FlexibleServers;
using Azure.ResourceManager.PostgreSql.FlexibleServers.Models;
using AzureMcp.Areas.Quota.Models;

namespace AzureMcp.Areas.Quota.Services.Util;

public interface IRegionChecker
{
    Task<List<string>> GetAvailableRegionsAsync(string resourceType);
}

public abstract class AzureRegionChecker : IRegionChecker
{
    protected readonly string SubscriptionId;
    protected readonly ArmClient ResourceClient;
    protected AzureRegionChecker(ArmClient armClient, string subscriptionId)
    {
        SubscriptionId = subscriptionId;
        ResourceClient = armClient;
        Console.WriteLine($"AzureRegionChecker initialized for subscription: {subscriptionId}");
    }
    public abstract Task<List<string>> GetAvailableRegionsAsync(string resourceType);
}

public class DefaultRegionChecker(ArmClient armClient, string subscriptionId) : AzureRegionChecker(armClient, subscriptionId)
{
    public override async Task<List<string>> GetAvailableRegionsAsync(string resourceType)
    {
        try
        {
            var parts = resourceType.Split('/');
            var providerNamespace = parts[0];
            var resourceTypeName = parts[1];

            var subscription = ResourceClient.GetSubscriptionResource(new ResourceIdentifier($"/subscriptions/{SubscriptionId}"));
            var provider = await subscription.GetResourceProviderAsync(providerNamespace);

            if (provider?.Value?.Data?.ResourceTypes == null)
            {
                return [];
            }

            var resourceTypeInfo = provider.Value.Data.ResourceTypes
                .FirstOrDefault(rt => rt.ResourceType.Equals(resourceTypeName, StringComparison.OrdinalIgnoreCase));

            if (resourceTypeInfo?.Locations == null)
            {
                return [];
            }

            return resourceTypeInfo.Locations
                .Select(location => location.Replace(" ", "").ToLowerInvariant())
                .ToList();
        }
        catch (Exception error)
        {
            throw new Exception($"Error fetching regions for resource type {resourceType}: {error.Message}");
        }
    }
}

public class CognitiveServicesRegionChecker : AzureRegionChecker
{
    private readonly string? _skuName;
    private readonly string? _apiVersion;
    private readonly string? _modelName;

    public CognitiveServicesRegionChecker(ArmClient armClient, string subscriptionId, string? skuName = null, string? apiVersion = null, string? modelName = null)
        : base(armClient, subscriptionId)
    {
        _skuName = skuName;
        _apiVersion = apiVersion;
        _modelName = modelName;
    }

    public override async Task<List<string>> GetAvailableRegionsAsync(string resourceType)
    {
        var parts = resourceType.Split('/');
        var providerNamespace = parts[0];
        var resourceTypeName = parts[1];

        var subscription = ResourceClient.GetSubscriptionResource(new ResourceIdentifier($"/subscriptions/{SubscriptionId}"));
        var provider = await subscription.GetResourceProviderAsync(providerNamespace);

        List<string> regions = provider?.Value?.Data?.ResourceTypes?
            .FirstOrDefault(rt => rt.ResourceType.Equals(resourceTypeName, StringComparison.OrdinalIgnoreCase))
            ?.Locations?
            .Select(location => location.Replace(" ", "").ToLowerInvariant())
            .ToList() ?? new List<string>();

        var availableRegions = new List<string>();

        foreach (var region in regions)
        {
            try
            {
                var quotas = subscription.GetModels(region);

                bool hasMatchingModel = false;

                foreach (CognitiveServicesModel modelElement in quotas)
                {
                    var nameMatch = string.IsNullOrEmpty(_modelName) ||
                        (modelElement.Model?.Name == _modelName);

                    var versionMatch = string.IsNullOrEmpty(_apiVersion) ||
                        (modelElement.Model?.Version == _apiVersion);


                    var skuMatch = string.IsNullOrEmpty(_skuName) ||
                        (modelElement.Model?.Skus?.Any(sku => sku.Name == _skuName) ?? false);

                    if (nameMatch && versionMatch && skuMatch)
                    {
                        hasMatchingModel = true;
                        break;
                    }
                }

                if (hasMatchingModel)
                {
                    availableRegions.Add(region);
                }
            }
            catch (Exception error)
            {
                throw new Exception($"Error checking cognitive services models for region {region}: {error.Message}");
            }
        }

        return availableRegions;
    }
}

public class PostgreSqlRegionChecker(ArmClient armClient, string subscriptionId) : AzureRegionChecker(armClient, subscriptionId)
{
    public override async Task<List<string>> GetAvailableRegionsAsync(string resourceType)
    {
        var parts = resourceType.Split('/');
        var providerNamespace = parts[0];
        var resourceTypeName = parts[1];

        var subscription = ResourceClient.GetSubscriptionResource(new ResourceIdentifier($"/subscriptions/{SubscriptionId}"));
        var provider = await subscription.GetResourceProviderAsync(providerNamespace);
        var regions = provider?.Value?.Data?.ResourceTypes?
            .FirstOrDefault(rt => rt.ResourceType.Equals(resourceTypeName, StringComparison.OrdinalIgnoreCase))
            ?.Locations?
            .Select(location => location.Replace(" ", "").ToLowerInvariant())
            .ToList() ?? new List<string>();

        var availableRegions = new List<string>();

        foreach (var region in regions)
        {
            try
            {
                Pageable<PostgreSqlFlexibleServerCapabilityProperties> result = subscription.ExecuteLocationBasedCapabilities(region);
                foreach (var capability in result)
                {
                    if (capability.SupportedServerEditions?.Any() == true)
                    {
                        availableRegions.Add(region);
                        break; // No need to check further capabilities for this region
                    }
                }
            }
            catch (Exception error)
            {
                throw new Exception($"Error checking PostgreSQL capabilities for region {region}: {error.Message}");
            }
        }

        return availableRegions;
    }
}

public static class RegionCheckerFactory
{
    public static IRegionChecker CreateRegionChecker(
        ArmClient armClient,
        string subscriptionId,
        string resourceType,
        CognitiveServiceProperties? properties = null)
    {
        var provider = resourceType.Split('/')[0].ToLowerInvariant();

        return provider switch
        {
            "microsoft.cognitiveservices" => new CognitiveServicesRegionChecker(
                armClient,
                subscriptionId,
                properties?.DeploymentSkuName,
                properties?.ModelVersion,
                properties?.ModelName),
            "microsoft.dbforpostgresql" => new PostgreSqlRegionChecker(armClient, subscriptionId),
            _ => new DefaultRegionChecker(armClient, subscriptionId)
        };
    }
}

public static class AzureRegionService
{
    public static async Task<Dictionary<string, List<string>>> GetAvailableRegionsForResourceTypesAsync(
        ArmClient armClient,
        string[] resourceTypes,
        string subscriptionId,
        CognitiveServiceProperties? cognitiveServiceProperties = null)
    {
        var result = new Dictionary<string, List<string>>();

        foreach (var resourceType in resourceTypes)
        {
            var checker = RegionCheckerFactory.CreateRegionChecker(armClient, subscriptionId, resourceType, cognitiveServiceProperties);
            result[resourceType] = await checker.GetAvailableRegionsAsync(resourceType);
        }

        return result;
    }
}
