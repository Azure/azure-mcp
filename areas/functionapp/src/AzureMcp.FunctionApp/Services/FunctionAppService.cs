// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using Azure.ResourceManager.AppContainers;
using Azure.ResourceManager.AppContainers.Models;
using Azure.ResourceManager.AppService;
using Azure.ResourceManager.AppService.Models;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Storage;
using Azure.ResourceManager.Storage.Models;
using AzureMcp.Core.Services.Azure;
using AzureMcp.Core.Services.Azure.ResourceGroup;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.Core.Services.Caching;
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
        string? planSku = null,
        string? containerAppName = null,
        string? runtime = null,
        string? runtimeVersion = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription, resourceGroup, functionAppName, location);
        var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
        var rg = await _resourceGroupService.CreateOrUpdateResourceGroup(subscription, resourceGroup, location, tenant, retryPolicy);
        var options = BuildCreateOptions(runtime, runtimeVersion, planType, planSku, containerAppName);

        return options.HostingKind == HostingKind.ContainerApp
            ? await CreateContainerHostedFunctionAppAsync(subscriptionResource, rg, functionAppName, location, containerAppName, options)
            : await CreateAppServiceHostedFunctionAppAsync(subscriptionResource, rg, functionAppName, location, appServicePlan, options);
    }

    private static CreateOptions BuildCreateOptions(string? runtime, string? runtimeVersion, string? planType, string? planSku, string? containerAppName)
    {
        var rt = string.IsNullOrWhiteSpace(runtime) ? "dotnet" : runtime.Trim().ToLowerInvariant();
        var rtv = string.IsNullOrWhiteSpace(runtimeVersion) ? null : runtimeVersion!.Trim();
        var hostingKind = ParseHostingKind(planType, containerAppName);
        var requiresLinux = rt == "python" || hostingKind == HostingKind.FlexConsumption;
        return new CreateOptions(rt, rtv, hostingKind, requiresLinux, planSku);
    }

    internal static HostingKind ParseHostingKind(string? planType, string? containerAppName)
    {
        var pt = planType?.Trim().ToLowerInvariant();
        if (!string.IsNullOrWhiteSpace(containerAppName) && pt is null)
            return HostingKind.ContainerApp;

        return pt switch
        {
            "flex" or "flexconsumption" => HostingKind.FlexConsumption,
            "premium" or "functionspremium" => HostingKind.Premium,
            "appservice" => HostingKind.AppService,
            "containerapp" or "containerapps" => HostingKind.ContainerApp,
            _ => HostingKind.Consumption
        };
    }

    private static async Task<AppServicePlanResource> CreatePlan(ResourceGroupResource rg, string planName, string location, CreateOptions options)
    {
        var sku = !string.IsNullOrWhiteSpace(options.ExplicitSku)
            ? new AppServiceSkuDescription { Name = options.ExplicitSku!.Trim(), Tier = InferTier(options.ExplicitSku!) }
            : CreateSkuForHostingKind(options.HostingKind);

        var data = new AppServicePlanData(location) { Sku = sku, IsReserved = options.RequiresLinux };
        var op = await rg.GetAppServicePlans().CreateOrUpdateAsync(WaitUntil.Completed, planName, data);
        return op.Value;
    }

    private static void ValidateExistingPlan(AppServicePlanResource plan, string planName, CreateOptions options)
    {
        if (options.RequiresLinux && plan.Data.IsReserved != true)
            throw new InvalidOperationException($"App Service plan '{planName}' must be Linux for runtime '{options.Runtime}'.");

        if (options.HostingKind == HostingKind.FlexConsumption && !IsFlexConsumption(plan.Data))
            throw new InvalidOperationException($"App Service plan '{planName}' is not a Flex Consumption plan.");
        if (options.HostingKind == HostingKind.Premium && !string.Equals(plan.Data.Sku?.Tier, "ElasticPremium", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException($"App Service plan '{planName}' is not an Elastic Premium plan.");
    }

    private static SiteConfigProperties? BuildSiteConfig(bool isLinux, CreateOptions options)
    {
        if (!isLinux)
            return null;
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

    private static async Task<WebSiteResource> CreateAppServiceSiteAsync(ResourceGroupResource rg, string functionAppName, string location, AppServicePlanResource plan, CreateOptions options)
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

    private static async Task<FunctionAppInfo> CreateContainerHostedFunctionAppAsync(
        SubscriptionResource subscription,
        ResourceGroupResource rg,
        string functionAppName,
        string location,
        string? containerAppName,
        CreateOptions options)
    {
        var effectiveName = string.IsNullOrWhiteSpace(containerAppName) ? functionAppName : containerAppName!;
        var storage = await EnsureStorageForFunctionApp(subscription, rg, functionAppName, location);
        var containerApp = await EnsureMinimalContainerApp(rg, effectiveName, location, options.Runtime, storage);
        var host = containerApp.Data.Configuration?.Ingress?.Fqdn ?? containerApp.Data.LatestRevisionName ?? containerApp.Data.Name;
        return new FunctionAppInfo(
            containerApp.Data.Name,
            rg.Data.Name,
            location,
            "containerapp",
            containerApp.Data.ProvisioningState.ToString(),
            host,
            containerApp.Data.Tags?.ToDictionary(k => k.Key, v => v.Value));
    }

    private static async Task<FunctionAppInfo> CreateAppServiceHostedFunctionAppAsync(
        SubscriptionResource subscription,
        ResourceGroupResource rg,
        string functionAppName,
        string location,
        string? appServicePlan,
        CreateOptions options)
    {
        var plan = await EnsureAppServicePlan(rg, appServicePlan, functionAppName, location, options);
        var site = await CreateAppServiceSiteAsync(rg, functionAppName, location, plan, options);
        var storage = await EnsureStorageForFunctionApp(subscription, rg, functionAppName, location);
        await ApplyAppSettings(site, options, storage);
        return ConvertToFunctionAppModel(site);
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
        var createOptions = CreateStorageAccountOptions(location);

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

    internal static StorageAccountCreateOrUpdateContent CreateStorageAccountOptions(string location)
    {
        return new StorageAccountCreateOrUpdateContent(
            new StorageSku(StorageSkuName.StandardLrs),
            StorageKind.StorageV2,
            location)
        {
            AccessTier = StorageAccountAccessTier.Hot,
            EnableHttpsTrafficOnly = true,
            AllowBlobPublicAccess = false,
            IsHnsEnabled = false
        };
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
        if (string.IsNullOrWhiteSpace(version))
            return null;
        var digits = new string(version.Trim().TakeWhile(ch => char.IsDigit(ch)).ToArray());
        return string.IsNullOrEmpty(digits) ? null : digits;
    }

    private static AppServiceSkuDescription CreateSkuForHostingKind(HostingKind kind) => kind switch
    {
        HostingKind.FlexConsumption => new AppServiceSkuDescription { Name = "FC1", Tier = "FlexConsumption" },
        HostingKind.Premium => new AppServiceSkuDescription { Name = "EP1", Tier = "ElasticPremium" },
        HostingKind.Consumption => new AppServiceSkuDescription { Name = "Y1", Tier = "Dynamic" },
        HostingKind.AppService => new AppServiceSkuDescription { Name = "B1", Tier = "Basic" },
        _ => new AppServiceSkuDescription { Name = "Y1", Tier = "Dynamic" }
    };

    private static string InferTier(string skuName)
    {
        var upper = skuName.Trim().ToUpperInvariant();
        if (upper.StartsWith("B"))
            return "Basic";
        if (upper.StartsWith("S"))
            return "Standard";
        if (upper.StartsWith("P1V3") || upper.StartsWith("P2V3") || upper.StartsWith("P3V3") || upper.StartsWith("P"))
            return "PremiumV3";
        if (upper.StartsWith("P"))
            return "Premium";
        return "Standard";
    }

    private static async Task<ContainerAppResource> EnsureMinimalContainerApp(ResourceGroupResource rg, string name, string location, string runtime, string storageConnectionString)
    {
        var envs = rg.GetContainerAppManagedEnvironments();
        var envData = new ContainerAppManagedEnvironmentData(location);
        var env = (await envs.CreateOrUpdateAsync(WaitUntil.Completed, name, envData)).Value;

        var apps = rg.GetContainerApps();
        var image = ResolveFunctionsContainerImage(runtime);
        var data = new ContainerAppData(location)
        {
            ManagedEnvironmentId = env.Id,
            Configuration = new ContainerAppConfiguration()
            {
                Ingress = new ContainerAppIngressConfiguration()
                {
                    External = true,
                    TargetPort = 80
                }
            },
            Template = new ContainerAppTemplate()
            {
                Containers =
                {
                    new ContainerAppContainer()
                    {
                        Name = "functions",
                        Image = image,
                        Resources = new AppContainerResources()
                        {
                            Cpu = 0.25,
                            Memory = "0.5Gi"
                        },
                        Env =
                        {
                            new ContainerAppEnvironmentVariable()
                            {
                                Name = "FUNCTIONS_WORKER_RUNTIME",
                                Value = runtime
                            },
                            new ContainerAppEnvironmentVariable()
                            {
                                Name = "FUNCTIONS_EXTENSION_VERSION",
                                Value = "~4"
                            },
                            new ContainerAppEnvironmentVariable()
                            {
                                Name = "AzureWebJobsStorage",
                                Value = storageConnectionString
                            }
                        }
                    }
                }
            }
        };

        var app = (await apps.CreateOrUpdateAsync(WaitUntil.Completed, name, data)).Value;
        return app;
    }

    internal static string ResolveFunctionsContainerImage(string runtime)
    {
        var rt = (runtime ?? "dotnet").Trim().ToLowerInvariant();
        return rt switch
        {
            "dotnet" => "mcr.microsoft.com/azure-functions/dotnet:4",
            "dotnet-isolated" => "mcr.microsoft.com/azure-functions/dotnet-isolated:4",
            "node" => "mcr.microsoft.com/azure-functions/node:4",
            "python" => "mcr.microsoft.com/azure-functions/python:4",
            "java" => "mcr.microsoft.com/azure-functions/java:4",
            "powershell" => "mcr.microsoft.com/azure-functions/powershell:4",
            _ => "mcr.microsoft.com/azure-functions/dotnet-isolated:4"
        };
    }

    internal static bool RequiresLinuxFor(string? runtime, string? planType, string? containerAppName)
    {
        var rt = string.IsNullOrWhiteSpace(runtime) ? "dotnet" : runtime.Trim().ToLowerInvariant();
        var hostingKind = ParseHostingKind(planType, containerAppName);
        return rt == "python" || hostingKind == HostingKind.FlexConsumption;
    }


    private static bool IsFlexConsumption(AppServicePlanData plan)
    {
        return string.Equals(plan.Sku?.Tier, "FlexConsumption", StringComparison.OrdinalIgnoreCase);
    }

    internal enum HostingKind
    {
        Consumption,
        FlexConsumption,
        Premium,
        AppService,
        ContainerApp
    }

    private readonly record struct CreateOptions(
        string Runtime,
        string? RuntimeVersion,
        HostingKind HostingKind,
        bool RequiresLinux,
        string? ExplicitSku);
}
