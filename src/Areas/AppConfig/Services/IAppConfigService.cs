// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.AppConfig.Models;
using AzureMcp.Options;

namespace AzureMcp.Areas.AppConfig.Services;

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
    Task DeleteKeyValue(string accountName, string key, string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null, string? label = null);
}
