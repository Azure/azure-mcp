// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using Azure.ResourceManager.AppService;
using Azure.ResourceManager.AppService.Models;
using Azure.ResourceManager.Storage;
using Azure.ResourceManager.Storage.Models;
using Azure.ResourceManager.Resources;
using AzureMcp.Core.Options;
using AzureMcp.Core.Services.Azure;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.Core.Services.Caching;
using AzureMcp.Core.Services.Azure.ResourceGroup;
using AzureMcp.FunctionApp.Models;

namespace AzureMcp.FunctionApp.Services;

public sealed class FunctionAppService(
    ISubscriptionService subscriptionService,
    ITenantService tenantService,
    ICacheService cacheService,
    IResourceGroupService resourceGroupService) : BaseAzureService(tenantService), IFunctionAppService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private readonly ICacheService _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
    private readonly IResourceGroupService _resourceGroupService = resourceGroupService ?? throw new ArgumentNullException(nameof(resourceGroupService));

    private const string CacheGroup = "functionapp";
    private static readonly TimeSpan s_cacheDuration = TimeSpan.FromHours(1);

    public async Task<List<FunctionAppInfo>?> ListFunctionApps(
        string subscription,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription);

        var cacheKey = string.IsNullOrEmpty(tenant)
            ? subscription
            : $"{subscription}_{tenant}";

        var cachedResults = await _cacheService.GetAsync<List<FunctionAppInfo>>(CacheGroup, cacheKey, s_cacheDuration);
        if (cachedResults != null)
        {
            return cachedResults;
        }

        var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
        var functionApps = new List<FunctionAppInfo>();

        try
        {
            await foreach (var site in subscriptionResource.GetWebSitesAsync())
            {
                if (site?.Data != null && IsFunctionApp(site.Data))
                {
                    functionApps.Add(ConvertToFunctionAppModel(site));
                }
            }

            await _cacheService.SetAsync(CacheGroup, cacheKey, functionApps, s_cacheDuration);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving Function Apps: {ex.Message}", ex);
        }

        return functionApps;
    }

    public async Task<FunctionAppInfo> CreateFunctionApp(
        string subscription,
        string resourceGroup,
        string functionAppName,
        string location,
        string? appServicePlan = null,
        string? planType = null,
        string? storageConnectionString = null,
        string? runtime = null,
        string? runtimeVersion = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription, resourceGroup, functionAppName, location);

        var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
        var rg = await _resourceGroupService.CreateOrUpdateResourceGroup(subscription, resourceGroup, location, tenant, retryPolicy);

        var options = NormalizeOptions(runtime, runtimeVersion, planType);
        var plan = await EnsureAppServicePlan(rg, appServicePlan, functionAppName, location, options);
        var site = await CreateFunctionApp(rg, functionAppName, location, plan, options);

        var storage = await ResolveStorageConnectionString(subscriptionResource, rg, functionAppName, location, storageConnectionString);
        await ApplyAppSettings(site, options, storage);

        return ConvertToFunctionAppModel(site);
    }

    private static CreateOptions NormalizeOptions(string? runtime, string? runtimeVersion, string? planType)
    {
        var rt = string.IsNullOrWhiteSpace(runtime) ? "dotnet" : runtime.Trim().ToLowerInvariant();
        var rtv = string.IsNullOrWhiteSpace(runtimeVersion) ? null : runtimeVersion!.Trim();
        var kind = ParsePlanKind(planType);
        var requiresLinux = rt == "python" || kind == PlanKind.Flex;
        return new CreateOptions(rt, rtv, kind, requiresLinux);
    }

    private static PlanKind ParsePlanKind(string? planType) => planType?.Trim().ToLowerInvariant() switch
    {
        "flex" => PlanKind.Flex,
        "premium" => PlanKind.Premium,
        _ => PlanKind.Consumption
    };

    private static async Task<AppServicePlanResource> CreatePlan(ResourceGroupResource rg, string planName, string location, CreateOptions options)
    {
        var data = new AppServicePlanData(location)
        {
            Sku = CreateSkuForPlanKind(options.PlanKind),
            IsReserved = options.RequiresLinux
        };
        var op = await rg.GetAppServicePlans().CreateOrUpdateAsync(WaitUntil.Completed, planName, data);
        return op.Value;
    }

    private static void ValidateExistingPlan(AppServicePlanResource plan, string planName, CreateOptions options)
    {
        if (options.RequiresLinux && plan.Data.IsReserved != true)
            throw new InvalidOperationException($"App Service plan '{planName}' must be Linux for runtime '{options.Runtime}'.");

        if (options.PlanKind == PlanKind.Flex && !IsFlexConsumption(plan.Data))
            throw new InvalidOperationException($"App Service plan '{planName}' is not a Flex Consumption plan.");
    }

    private static SiteConfigProperties? BuildSiteConfig(bool isLinux, CreateOptions options)
    {
        if (!isLinux) return null;
        return CreateLinuxSiteConfig(options.Runtime, options.RuntimeVersion);
    }

    private static string BuildKind(bool isLinux) => isLinux ? "functionapp,linux" : "functionapp";

    private static async Task<AppServicePlanResource> EnsureAppServicePlan(ResourceGroupResource rg, string? planName, string functionAppName, string location, CreateOptions options)
    {
        if (!string.IsNullOrWhiteSpace(planName))
        {
            var plans = rg.GetAppServicePlans();
            if (await plans.ExistsAsync(planName))
            {
                var existing = await plans.GetAsync(planName);
                ValidateExistingPlan(existing, planName, options);
                return existing;
            }
            return await CreatePlan(rg, planName, location, options);
        }

        var autoName = $"{functionAppName}-plan";
        return await CreatePlan(rg, autoName, location, options);
    }

    private static async Task<WebSiteResource> CreateFunctionApp(ResourceGroupResource rg, string functionAppName, string location, AppServicePlanResource plan, CreateOptions options)
    {
        try
        {
            var isLinux = plan.Data.IsReserved == true;
            var data = new WebSiteData(location)
            {
                Kind = BuildKind(isLinux),
                AppServicePlanId = plan.Id,
                SiteConfig = BuildSiteConfig(isLinux, options)
            };
            var op = await rg.GetWebSites().CreateOrUpdateAsync(WaitUntil.Completed, functionAppName, data);
            return op.Value;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to create Function App '{functionAppName}': {ex.Message}", ex);
        }
    }

    private static async Task<string> ResolveStorageConnectionString(SubscriptionResource subscription, ResourceGroupResource rg, string functionAppName, string location, string? storageConnectionString)
    {
        if (!string.IsNullOrWhiteSpace(storageConnectionString))
            return storageConnectionString;
        return await EnsureStorageForFunctionApp(subscription, rg, functionAppName, location);
    }

    private static async Task ApplyAppSettings(WebSiteResource site, CreateOptions options, string storageConnectionString)
    {
        try
        {
            var appSettings = BuildAppSettings(options.Runtime, options.RuntimeVersion, options.RequiresLinux, storageConnectionString);

            await site.UpdateApplicationSettingsAsync(appSettings);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to configure application settings for Function App '{site.Data.Name}': {ex.Message}", ex);
        }
    }

    private static bool IsFunctionApp(WebSiteData siteData)
    {
        return siteData.Kind?.Contains("functionapp", StringComparison.OrdinalIgnoreCase) == true;
    }

    private static FunctionAppInfo ConvertToFunctionAppModel(WebSiteResource siteResource)
    {
        var data = siteResource.Data;

        return new FunctionAppInfo(
            data.Name,
            siteResource.Id.ResourceGroupName,
            data.Location.ToString(),
            data.AppServicePlanId.Name,
            data.State,
            data.DefaultHostName,
            data.Tags?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
        );
    }

    private static string CreateStorageAccountName(string functionAppName)
    {
        var baseName = new string(functionAppName.ToLowerInvariant().Where(char.IsLetterOrDigit).ToArray());
        if (string.IsNullOrEmpty(baseName))
        {
            baseName = "func";
        }
        var trimmed = baseName.Length > 18 ? baseName.Substring(0, 18) : baseName;
        var suffix = Guid.NewGuid().ToString("N").Substring(0, 6);
        return $"{trimmed}{suffix}";
    }

    private static string BuildConnectionString(string accountName, string key)
    {
        return $"DefaultEndpointsProtocol=https;AccountName={accountName};AccountKey={key};EndpointSuffix=core.windows.net";
    }

    private static async Task<string> EnsureStorageForFunctionApp(SubscriptionResource subscription, ResourceGroupResource rg, string functionAppName, string location)
    {
        var storageAccountName = CreateStorageAccountName(functionAppName);

        var createOptions = new StorageAccountCreateOrUpdateContent(
            new StorageSku(StorageSkuName.StandardLrs),
            StorageKind.StorageV2,
            location)
        {
            AccessTier = StorageAccountAccessTier.Hot,
            EnableHttpsTrafficOnly = true,
            AllowBlobPublicAccess = false,
            IsHnsEnabled = false
        };

        var op = await rg.GetStorageAccounts().CreateOrUpdateAsync(Azure.WaitUntil.Completed, storageAccountName, createOptions);
        var storage = op.Value;

        var keys = new List<StorageAccountKey>();
        await foreach (var key in storage.GetKeysAsync())
        {
            keys.Add(key);
        }
        var primary = keys.FirstOrDefault() ?? throw new Exception($"No keys found for storage account '{storageAccountName}'");
        return BuildConnectionString(storageAccountName, primary.Value);
    }

    internal static SiteConfigProperties? CreateLinuxSiteConfig(string runtime, string? runtimeVersion)
    {
        var cfg = new SiteConfigProperties();
        switch (runtime)
        {
            case "python":
                cfg.LinuxFxVersion = $"Python|{(string.IsNullOrWhiteSpace(runtimeVersion) ? "3.12" : runtimeVersion)}";
                break;
            case "node":
                cfg.LinuxFxVersion = $"Node|{(string.IsNullOrWhiteSpace(runtimeVersion) ? "22" : runtimeVersion)}";
                break;
            case "dotnet":
                cfg.LinuxFxVersion = $"DOTNET|{(string.IsNullOrWhiteSpace(runtimeVersion) ? "8.0" : runtimeVersion)}";
                break;
            case "java":
                cfg.LinuxFxVersion = $"Java|{(string.IsNullOrWhiteSpace(runtimeVersion) ? "21.0" : runtimeVersion)}";
                break;
            case "powershell":
                cfg.LinuxFxVersion = $"PowerShell|{(string.IsNullOrWhiteSpace(runtimeVersion) ? "7.4" : runtimeVersion)}";
                break;
            default:
                break;
        }
        return cfg;
    }

    internal static AppServiceConfigurationDictionary BuildAppSettings(string runtime, string? runtimeVersion, bool requiresLinux, string storageConnectionString)
    {
        var appServiceConfig = new AppServiceConfigurationDictionary
        {
            Properties =
            {
                ["FUNCTIONS_WORKER_RUNTIME"] = runtime,
                ["AzureWebJobsStorage"] = storageConnectionString,
                ["FUNCTIONS_EXTENSION_VERSION"] = "~4"
            }
        };

        if (requiresLinux || runtime != "node" || string.IsNullOrWhiteSpace(runtimeVersion))
        {
            return appServiceConfig;
        }

        var major = ExtractMajorVersion(runtimeVersion!);
        if (!string.IsNullOrEmpty(major))
        {
            appServiceConfig.Properties["WEBSITE_NODE_DEFAULT_VERSION"] = $"~{major}";
        }

        return appServiceConfig;
    }

    private static string? ExtractMajorVersion(string version)
    {
        if (string.IsNullOrWhiteSpace(version)) return null;
        var digits = new string(version.Trim().TakeWhile(ch => char.IsDigit(ch)).ToArray());
        return string.IsNullOrEmpty(digits) ? null : digits;
    }

    private static AppServiceSkuDescription CreateSkuForPlanKind(PlanKind planKind)
    {
        return planKind switch
        {
            PlanKind.Flex => new AppServiceSkuDescription { Name = "FC1", Tier = "FlexConsumption" },
            PlanKind.Premium => new AppServiceSkuDescription { Name = "EP1", Tier = "ElasticPremium" },
            _ => new AppServiceSkuDescription { Name = "Y1", Tier = "Dynamic" }
        };
    }

    private static bool IsFlexConsumption(AppServicePlanData plan)
    {
        return string.Equals(plan.Sku?.Tier, "FlexConsumption", StringComparison.OrdinalIgnoreCase);
    }

    private enum PlanKind
    {
        Consumption,
        Flex,
        Premium
    }

    private readonly record struct CreateOptions(
        string Runtime,
        string? RuntimeVersion,
        PlanKind PlanKind,
        bool RequiresLinux);
}
