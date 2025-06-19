// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.AppConfig;
using AzureMcp.Options;

namespace AzureMcp.Services.Interfaces;

public interface IAppConfigService
{
    Task<List<AppConfigurationAccount>> GetAppConfigAccounts(string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    Task<List<KeyValueSetting>> ListKeyValues(
        string accountName,
        string subscriptionId,
        string? key = null, string? label = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);
    Task<KeyValueSetting> GetKeyValue(string accountName, string key, string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null, string? label = null);
    Task LockKeyValue(string accountName, string key, string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null, string? label = null);
    Task UnlockKeyValue(string accountName, string key, string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null, string? label = null);
    Task SetKeyValue(string accountName, string key, string value, string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null, string? label = null);
    Task DeleteKeyValue(string accountName, string key, string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null, string? label = null);    Task SetFeatureFlag(
        string accountName, 
        string featureFlagName, 
        string subscriptionId, 
        bool? enabled = null,
        string? description = null,
        string? displayName = null,
        string? conditions = null,
        string? tenant = null, 
        RetryPolicyOptions? retryPolicy = null, 
        string? label = null);
}
