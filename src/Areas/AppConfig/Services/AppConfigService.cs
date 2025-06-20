// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using Azure;
using Azure.Core;
using Azure.Data.AppConfiguration;
using Azure.ResourceManager.AppConfiguration;
using Azure.ResourceManager.Resources;
using AzureMcp.Areas.AppConfig.Models;
using AzureMcp.Models.Identity;
using AzureMcp.Options;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Azure.Subscription;
using AzureMcp.Services.Azure.Tenant;

namespace AzureMcp.Areas.AppConfig.Services;

public class AppConfigService(ISubscriptionService subscriptionService, ITenantService tenantService)
    : BaseAzureService(tenantService), IAppConfigService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));

    public async Task<List<AppConfigurationAccount>> GetAppConfigAccounts(string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId);

        var subscription = await _subscriptionService.GetSubscription(subscriptionId, tenant, retryPolicy);
        var accounts = new List<AppConfigurationAccount>();

        await foreach (var account in subscription.GetAppConfigurationStoresAsync())
        {
            ResourceIdentifier resourceId = account.Id;
            if (resourceId.ToString().Length == 0)
                continue;

            var acc = new AppConfigurationAccount
            {
                Name = account.Data.Name,
                Location = account.Data.Location.ToString(),
                Endpoint = account.Data.Endpoint,
                CreationDate = account.Data.CreatedOn?.DateTime ?? DateTime.MinValue,
                PublicNetworkAccess = account.Data.PublicNetworkAccess.HasValue &&
                    account.Data.PublicNetworkAccess.Value.ToString().Equals("Enabled", StringComparison.OrdinalIgnoreCase),
                Sku = account.Data.SkuName,
                Tags = account.Data.Tags ?? new Dictionary<string, string>(),
                DisableLocalAuth = account.Data.DisableLocalAuth,
                SoftDeleteRetentionInDays = account.Data.SoftDeleteRetentionInDays,
                EnablePurgeProtection = account.Data.EnablePurgeProtection,
                CreateMode = account.Data.CreateMode?.ToString(),

                // Map the new managed identity structure
                ManagedIdentity = account.Data.Identity == null ? null : new ManagedIdentityInfo
                {
                    SystemAssignedIdentity = new SystemAssignedIdentityInfo
                    {
                        Enabled = account.Data.Identity != null,
                        TenantId = account.Data.Identity?.TenantId?.ToString(),
                        PrincipalId = account.Data.Identity?.PrincipalId?.ToString()
                    },
                    UserAssignedIdentities = account.Data.Identity?.UserAssignedIdentities?
                        .Select(id => new UserAssignedIdentityInfo
                        {
                            ClientId = id.Value.ClientId?.ToString(),
                            PrincipalId = id.Value.PrincipalId?.ToString()
                        })
                        .ToArray()
                },

                // Full encryption properties from KeyVaultProperties
                Encryption = account.Data.EncryptionKeyVaultProperties == null ? null : new EncryptionProperties
                {
                    KeyIdentifier = account.Data.EncryptionKeyVaultProperties.KeyIdentifier,
                    IdentityClientId = account.Data.EncryptionKeyVaultProperties.IdentityClientId,
                }
            };

            accounts.Add(acc);
        }

        return accounts;
    }

    public async Task<List<KeyValueSetting>> ListKeyValues(
        string accountName,
        string subscriptionId,
        string? key = null,
        string? label = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(accountName, subscriptionId);

        var client = await GetConfigurationClient(accountName, subscriptionId, tenant, retryPolicy);
        var settings = new List<KeyValueSetting>();

        var selector = new SettingSelector
        {
            KeyFilter = string.IsNullOrEmpty(key) ? null : key,
            LabelFilter = string.IsNullOrEmpty(label) ? null : label
        };

        await foreach (var setting in client.GetConfigurationSettingsAsync(selector))
        {
            settings.Add(new KeyValueSetting
            {
                Key = setting.Key,
                Value = setting.Value,
                Label = setting.Label ?? string.Empty,
                ContentType = setting.ContentType ?? string.Empty,
                ETag = new Models.ETag { Value = setting.ETag.ToString() },
                LastModified = setting.LastModified,
                Locked = setting.IsReadOnly
            });
        }

        return settings;
    }

    public async Task<KeyValueSetting> GetKeyValue(string accountName, string key, string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null, string? label = null)
    {
        ValidateRequiredParameters(accountName, key, subscriptionId);
        var client = await GetConfigurationClient(accountName, subscriptionId, tenant, retryPolicy);
        var response = await client.GetConfigurationSettingAsync(key, label, cancellationToken: default);
        var setting = response.Value;

        return new KeyValueSetting
        {
            Key = setting.Key,
            Value = setting.Value,
            Label = setting.Label ?? string.Empty,
            ContentType = setting.ContentType ?? string.Empty,
            ETag = new Models.ETag { Value = setting.ETag.ToString() },
            LastModified = setting.LastModified,
            Locked = setting.IsReadOnly
        };
    }

    public async Task LockKeyValue(string accountName, string key, string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null, string? label = null)
    {
        await SetKeyValueReadOnlyState(accountName, key, subscriptionId, tenant, retryPolicy, label, true);
    }

    public async Task UnlockKeyValue(string accountName, string key, string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null, string? label = null)
    {
        await SetKeyValueReadOnlyState(accountName, key, subscriptionId, tenant, retryPolicy, label, false);
    }

    public async Task SetKeyValue(string accountName, string key, string value, string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null, string? label = null)
    {
        ValidateRequiredParameters(accountName, key, value, subscriptionId);
        var client = await GetConfigurationClient(accountName, subscriptionId, tenant, retryPolicy);
        await client.SetConfigurationSettingAsync(key, value, label, cancellationToken: default);
    }

    public async Task DeleteKeyValue(string accountName, string key, string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null, string? label = null)
    {
        ValidateRequiredParameters(accountName, key, subscriptionId);
        var client = await GetConfigurationClient(accountName, subscriptionId, tenant, retryPolicy);
        await client.DeleteConfigurationSettingAsync(key, label, cancellationToken: default);
    }

    public async Task SetFeatureFlag(
        string accountName,
        string featureFlagName,
        string subscriptionId,
        bool? enabled = null,
        string? description = null,
        string? displayName = null,
        string? conditions = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null,
        string? label = null)
    {
        ValidateRequiredParameters(accountName, featureFlagName, subscriptionId);
        var client = await GetConfigurationClient(accountName, subscriptionId, tenant, retryPolicy);

        FeatureFlagConfigurationSetting featureFlagSetting;

        try
        {
            // Try to get existing feature flag to preserve existing data
            var existingResponse = await client.GetConfigurationSettingAsync(
                FeatureFlagConfigurationSetting.KeyPrefix + featureFlagName,
                label,
                cancellationToken: default);

            if (existingResponse.Value is FeatureFlagConfigurationSetting existingFeatureFlag)
            {
                // Start with existing configuration
                featureFlagSetting = existingFeatureFlag;
            }
            else
            {
                // Existing setting is not a feature flag, create new one
                featureFlagSetting = new FeatureFlagConfigurationSetting(featureFlagName, enabled ?? false, label);
            }
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            // Feature flag doesn't exist, create a new one
            featureFlagSetting = new FeatureFlagConfigurationSetting(featureFlagName, enabled ?? false, label);
        }

        // Update basic properties if provided
        if (enabled.HasValue)
        {
            featureFlagSetting.IsEnabled = enabled.Value;
        }

        if (description != null)
        {
            featureFlagSetting.Description = description;
        }

        if (displayName != null)
        {
            featureFlagSetting.DisplayName = displayName;
        }

        // Parse and update complex JSON properties
        await UpdateFeatureFlagJsonProperties(featureFlagSetting, conditions);

        await client.SetConfigurationSettingAsync(featureFlagSetting, cancellationToken: default);
    }

    private static async Task UpdateFeatureFlagJsonProperties(
        FeatureFlagConfigurationSetting featureFlagSetting,
        string? conditions)
    {
        // Update conditions (client filters and requirement type)
        if (!string.IsNullOrEmpty(conditions))
        {
            try
            {
                var conditionsDoc = JsonDocument.Parse(conditions);
                var root = conditionsDoc.RootElement;

                // Handle client_filters
                if (root.TryGetProperty("client_filters", out var filtersElement) && filtersElement.ValueKind == JsonValueKind.Array)
                {
                    featureFlagSetting.ClientFilters.Clear();

                    foreach (var filterElement in filtersElement.EnumerateArray())
                    {
                        if (filterElement.TryGetProperty("name", out var nameElement))
                        {
                            var filterName = nameElement.GetString() ?? string.Empty;
                            var parameters = new Dictionary<string, object>();
                            if (filterElement.TryGetProperty("parameters", out var paramsElement))
                            {
                                var paramDict = JsonSerializer.Deserialize(paramsElement.GetRawText(), JsonSourceGenerationContext.Default.DictionaryStringObject) ?? new Dictionary<string, object?>();
                                foreach (var (key, value) in paramDict)
                                {
                                    parameters[key] = value;
                                }
                            }

                            featureFlagSetting.ClientFilters.Add(new FeatureFlagFilter(filterName, parameters));
                        }
                    }
                }
                // Note: requirement_type handling may require custom logic or may not be directly supported by this SDK version
            }
            catch (JsonException ex)
            {
                throw new ArgumentException($"Invalid JSON format for conditions: {ex.Message}", nameof(conditions));
            }
        }

        await Task.CompletedTask;
    }

    private async Task SetKeyValueReadOnlyState(string accountName, string key, string subscriptionId, string? tenant, RetryPolicyOptions? retryPolicy, string? label, bool isReadOnly)
    {
        ValidateRequiredParameters(accountName, key, subscriptionId);
        var client = await GetConfigurationClient(accountName, subscriptionId, tenant, retryPolicy);
        await client.SetReadOnlyAsync(key, label, isReadOnly, cancellationToken: default);
    }

    private async Task<ConfigurationClient> GetConfigurationClient(string accountName, string subscriptionId, string? tenant, RetryPolicyOptions? retryPolicy)
    {
        var subscription = await _subscriptionService.GetSubscription(subscriptionId, tenant, retryPolicy);
        var configStore = await FindAppConfigStore(subscription, accountName, subscriptionId);
        var endpoint = configStore.Data.Endpoint;
        var credential = await GetCredential(tenant);
        AddDefaultPolicies(new ConfigurationClientOptions());

        return new ConfigurationClient(new Uri(endpoint), credential);
    }

    private static async Task<AppConfigurationStoreResource> FindAppConfigStore(SubscriptionResource subscription, string accountName, string subscriptionId)
    {
        AppConfigurationStoreResource? configStore = null;
        await foreach (var store in subscription.GetAppConfigurationStoresAsync())
        {
            if (store.Data.Name == accountName)
            {
                configStore = store;
                break;
            }
        }

        if (configStore == null)
            throw new Exception($"App Configuration store '{accountName}' not found in subscription '{subscriptionId}'");

        return configStore;
    }
}
