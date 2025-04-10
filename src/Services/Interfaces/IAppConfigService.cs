// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMCP.Arguments;
using AzureMCP.Models.AppConfig;

namespace AzureMCP.Services.Interfaces;

public interface IAppConfigService
{
    Task<List<AppConfigurationAccount>> GetAppConfigAccounts(string subscriptionId, string? tenant = null, RetryPolicyArguments? retryPolicy = null);
    Task<List<KeyValueSetting>> ListKeyValues(
        string accountName,
        string subscriptionId,
        string? key = null, string? label = null,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null);
    Task<KeyValueSetting> GetKeyValue(string accountName, string key, string subscriptionId, string? tenant = null, RetryPolicyArguments? retryPolicy = null, string? label = null);
    Task LockKeyValue(string accountName, string key, string subscriptionId, string? tenant = null, RetryPolicyArguments? retryPolicy = null, string? label = null);
    Task UnlockKeyValue(string accountName, string key, string subscriptionId, string? tenant = null, RetryPolicyArguments? retryPolicy = null, string? label = null);
    Task SetKeyValue(string accountName, string key, string value, string subscriptionId, string? tenant = null, RetryPolicyArguments? retryPolicy = null, string? label = null);
    Task DeleteKeyValue(string accountName, string key, string subscriptionId, string? tenant = null, RetryPolicyArguments? retryPolicy = null, string? label = null);
}