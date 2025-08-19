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
    private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(1);

    public async Task<List<FunctionAppInfo>?> ListFunctionApps(
        string subscription,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription);
        var cacheKey = string.IsNullOrEmpty(tenant) ? subscription : $"{subscription}_{tenant}";
        var cachedResults = await _cacheService.GetAsync<List<FunctionAppInfo>>(CacheGroup, cacheKey, CacheDuration);
        if (cachedResults != null)
        {
            return cachedResults;
        }

        var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
        var functionApps = new List<FunctionAppInfo>();

        await foreach (var site in subscriptionResource.GetWebSitesAsync())
        {
            if (site?.Data is { } d && IsFunctionApp(d))
                functionApps.Add(ConvertToFunctionAppModel(site));
        }
        await _cacheService.SetAsync(CacheGroup, cacheKey, functionApps, CacheDuration);

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
        string? runtime = null,
        string? runtimeVersion = null,
        string? operatingSystem = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription, resourceGroup, functionAppName, location);
        var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
        var rg = await _resourceGroupService.CreateOrUpdateResourceGroup(subscription, resourceGroup, location, tenant, retryPolicy);
        var options = BuildCreateOptions(runtime, runtimeVersion, planType, planSku, operatingSystem);

        return options.HostingKind == HostingKind.ContainerApp
            ? await CreateContainerHostedFunctionAppAsync(subscriptionResource, rg, functionAppName, location, options)
            : await CreateAppServiceHostedFunctionAppAsync(subscriptionResource, rg, functionAppName, location, appServicePlan, options);
    }

    private static CreateOptions BuildCreateOptions(string? runtime, string? runtimeVersion, string? planType, string? planSku, string? operatingSystem)
    {
        var selectedRuntime = string.IsNullOrWhiteSpace(runtime) ? "dotnet" : runtime.Trim().ToLowerInvariant();
        var hostingKind = ParseHostingKind(planType);
        if (hostingKind == HostingKind.FlexConsumption && selectedRuntime == "dotnet")
            selectedRuntime = "dotnet-isolated";
        var selectedRuntimeVersion = string.IsNullOrWhiteSpace(runtimeVersion) ? GetDefaultRuntimeVersion(selectedRuntime) : runtimeVersion!.Trim();
        var (requiresLinux, normalizedOs) = ResolveOs(selectedRuntime, hostingKind, operatingSystem);
        return new CreateOptions(selectedRuntime, selectedRuntimeVersion, hostingKind, requiresLinux, planSku, normalizedOs);
    }

    internal static HostingKind ParseHostingKind(string? planType)
    {
        var pt = planType?.Trim().ToLowerInvariant();
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
        if (isLinux)
        {
            return CreateLinuxSiteConfig(options.Runtime, options.RuntimeVersion);
        }

        if (options.Runtime != "powershell")
            return null;

        var version = string.IsNullOrWhiteSpace(options.RuntimeVersion)
            ? GetDefaultRuntimeVersion("powershell")
            : options.RuntimeVersion;
        if (string.IsNullOrWhiteSpace(version))
            return null;

        return new SiteConfigProperties
        {
            PowerShellVersion = version
        };
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
        var isLinux = plan.Data.IsReserved == true;
        var data = new WebSiteData(location)
        {
            Kind = BuildKind(isLinux),
            AppServicePlanId = plan.Id,
            SiteConfig = BuildSiteConfig(isLinux, options)
        };
        if (options.HostingKind == HostingKind.FlexConsumption)
        {
            if (data.SiteConfig is not null)
            {
                data.SiteConfig.LinuxFxVersion = null;
            }
            data.FunctionAppConfig = new FunctionAppConfig
            {
                Runtime = new FunctionAppRuntime
                {
                    Name = MapToFunctionAppRuntimeName(options.Runtime),
                    Version = NormalizeRuntimeVersionForConfig(options.Runtime, options.RuntimeVersion)
                },
                DeploymentStorage = new FunctionAppStorage
                {
                    StorageType = FunctionAppStorageType.BlobContainer,
                    Value = new Uri($"https://{CreateStorageAccountName(functionAppName)}.blob.core.windows.net/{functionAppName}"),
                    Authentication = new FunctionAppStorageAuthentication
                    {
                        AuthenticationType = FunctionAppStorageAccountAuthenticationType.StorageAccountConnectionString,
                        StorageAccountConnectionStringName = "AzureWebJobsStorage"
                    }
                },
                ScaleAndConcurrency = new FunctionAppScaleAndConcurrency
                {
                    InstanceMemoryMB = 2048,
                    MaximumInstanceCount = 100
                }
            };
        }
        var op = await rg.GetWebSites().CreateOrUpdateAsync(WaitUntil.Completed, functionAppName, data);
        return op.Value;
    }

    private static async Task<FunctionAppInfo> CreateContainerHostedFunctionAppAsync(
        SubscriptionResource subscription,
        ResourceGroupResource rg,
        string functionAppName,
        string location,
        CreateOptions options)
    {
        var storage = await EnsureStorageForFunctionApp(subscription, rg, functionAppName, location);
        var containerApp = await EnsureMinimalContainerApp(rg, functionAppName, location, options.Runtime, storage);
        var host = containerApp.Data.Configuration?.Ingress?.Fqdn ?? containerApp.Data.LatestRevisionName ?? containerApp.Data.Name;
        return new FunctionAppInfo(
            containerApp.Data.Name,
            rg.Data.Name,
            location,
            "containerapp",
            containerApp.Data.ProvisioningState.ToString(),
            host,
            "linux",
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
        var storageConnection = await EnsureStorageForFunctionApp(subscription, rg, functionAppName, location);
        var site = await CreateAppServiceSiteAsync(
            rg,
            functionAppName,
            location,
            plan,
            options);
        await ApplyAppSettings(site, options, storageConnection);
        return ConvertToFunctionAppModel(site);
    }

    private static async Task ApplyAppSettings(WebSiteResource site, CreateOptions options, string storageConnectionString)
    {
        var appSettings = BuildAppSettings(options.Runtime, options.RuntimeVersion, options.RequiresLinux, storageConnectionString, includeWorkerRuntime: options.HostingKind != HostingKind.FlexConsumption);
        if (options.HostingKind == HostingKind.FlexConsumption)
            SanitizeAppSettingsForFlexConsumption(appSettings);
        await site.UpdateApplicationSettingsAsync(appSettings);
    }

    private static bool IsFunctionApp(WebSiteData siteData)
    {
        return siteData.Kind?.Contains("functionapp", StringComparison.OrdinalIgnoreCase) == true;
    }

    private static FunctionAppInfo ConvertToFunctionAppModel(WebSiteResource siteResource)
    {
        var data = siteResource.Data;

        var os = data.Kind?.Contains("linux", StringComparison.OrdinalIgnoreCase) == true ? "linux" : "windows";
        return new FunctionAppInfo(
            data.Name,
            siteResource.Id.ResourceGroupName,
            data.Location.ToString(),
            data.AppServicePlanId.Name,
            data.State,
            data.DefaultHostName,
            os,
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
        var config = new SiteConfigProperties();
        var version = string.IsNullOrWhiteSpace(runtimeVersion) ? GetDefaultRuntimeVersion(runtime) : runtimeVersion;
        if (runtime == "java" && version != null && version.EndsWith(".0", StringComparison.Ordinal))
            version = version.Substring(0, version.Length - 2);
        config.LinuxFxVersion = runtime switch
        {
            "python" => $"Python|{version}",
            "node" => $"Node|{version}",
            "dotnet" => $"DOTNET|{version}",
            "dotnet-isolated" => $"DOTNET-ISOLATED|{version}",
            "java" => $"Java|{version}",
            "powershell" => $"PowerShell|{version}",
            _ => config.LinuxFxVersion
        };
        return config;
    }

    private static FunctionAppRuntimeName? MapToFunctionAppRuntimeName(string runtime) => runtime switch
    {
        "dotnet-isolated" => FunctionAppRuntimeName.DotnetIsolated,
        "node" => FunctionAppRuntimeName.Node,
        "java" => FunctionAppRuntimeName.Java,
        "powershell" => FunctionAppRuntimeName.Powershell,
        "python" => FunctionAppRuntimeName.Python,
        "custom" => FunctionAppRuntimeName.Custom,
        "dotnet" => FunctionAppRuntimeName.DotnetIsolated,
        _ => null
    };

    private static string NormalizeRuntimeVersionForConfig(string runtime, string? runtimeVersion)
    {
        var version = string.IsNullOrWhiteSpace(runtimeVersion) ? GetDefaultRuntimeVersion(runtime) : runtimeVersion;
        if (string.IsNullOrWhiteSpace(version)) return string.Empty;
        if (runtime == "java" && version.EndsWith(".0", StringComparison.Ordinal))
            version = version[..^2];
        return version;
    }

    internal static AppServiceConfigurationDictionary BuildAppSettings(string runtime, string? runtimeVersion, bool requiresLinux, string storageConnectionString, bool includeWorkerRuntime = true)
    {
        var settings = new AppServiceConfigurationDictionary
        {
            Properties =
            {
                ["AzureWebJobsStorage"] = storageConnectionString,
                ["FUNCTIONS_EXTENSION_VERSION"] = "~4"
            }
        };
        if (includeWorkerRuntime)
        {
            settings.Properties["FUNCTIONS_WORKER_RUNTIME"] = runtime;
        }
        var effectiveVersion = string.IsNullOrWhiteSpace(runtimeVersion) ? GetDefaultRuntimeVersion(runtime) : runtimeVersion;
        if (!requiresLinux && runtime == "node" && !string.IsNullOrWhiteSpace(effectiveVersion))
        {
            var major = ExtractMajorVersion(effectiveVersion!);
            if (!string.IsNullOrEmpty(major))
                settings.Properties["WEBSITE_NODE_DEFAULT_VERSION"] = $"~{major}";
        }
        return settings;
    }

    private static string? GetDefaultRuntimeVersion(string runtime) => runtime switch
    {
        "python" => "3.12",
        "node" => "22",
        "dotnet" => "8.0",
        "dotnet-isolated" => "8.0",
        "java" => "17",
        "powershell" => "7.4",
        _ => null
    };

    private static string? ExtractMajorVersion(string version) => string.IsNullOrWhiteSpace(version)
        ? null
        : new string(version.Trim().TakeWhile(char.IsDigit).ToArray()) switch { "" => null, var v => v };

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

    internal static void SanitizeAppSettingsForFlexConsumption(AppServiceConfigurationDictionary settings)
    {
        if (settings?.Properties is null)
            return;
        settings.Properties.Remove("WEBSITE_NODE_DEFAULT_VERSION");
        settings.Properties.Remove("FUNCTIONS_WORKER_RUNTIME");
    }

    private static async Task<ContainerAppResource> EnsureMinimalContainerApp(ResourceGroupResource rg, string name, string location, string runtime, string storageConnectionString)
    {
        var envs = rg.GetContainerAppManagedEnvironments();
        var envName = $"{name}-env";
        var envData = new ContainerAppManagedEnvironmentData(location);
        var env = (await envs.CreateOrUpdateAsync(WaitUntil.Completed, envName, envData)).Value;

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
                        Name = "functions", //TODO: set to name
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

    internal static bool RequiresLinuxFor(string? runtime, string? planType)
    {
        var rt = string.IsNullOrWhiteSpace(runtime) ? "dotnet" : runtime.Trim().ToLowerInvariant();
        var hostingKind = ParseHostingKind(planType);
        return rt == "python" || hostingKind == HostingKind.FlexConsumption || hostingKind == HostingKind.ContainerApp;
    }

    internal static (bool RequiresLinux, string? NormalizedOs) ResolveOs(string runtime, HostingKind hostingKind, string? operatingSystem)
    {
        var forcedLinux = runtime == "python" || hostingKind == HostingKind.FlexConsumption || hostingKind == HostingKind.ContainerApp;
        var os = string.IsNullOrWhiteSpace(operatingSystem) ? null : operatingSystem!.Trim().ToLowerInvariant();
        bool requiresLinux = forcedLinux;

        if (string.IsNullOrEmpty(os))
        {
            return (requiresLinux, null);
        }

        if (os is not ("linux" or "windows"))
            throw new ArgumentException("--os must be either 'windows' or 'linux'.");

        if (forcedLinux && os == "windows")
            throw new InvalidOperationException("Selected runtime/plan requires Linux. Remove --os or set --os linux.");

        if (!forcedLinux)
        {
            requiresLinux = os == "linux";
        }

        return (requiresLinux, os);
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
        string? ExplicitSku,
        string? ExplicitOs);
}
