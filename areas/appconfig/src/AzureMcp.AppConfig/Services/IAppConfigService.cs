// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.AppConfig.Models;
using AzureMcp.Core.Options;

namespace AzureMcp.AppConfig.Services;

public interface IAppConfigService
{
    Task<List<AppConfigurationAccount>> GetAppConfigAccounts(
        string subscriptionId,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);
    Task<List<KeyValueSetting>> ListKeyValues(
        string accountName,
        string subscriptionId,
        string? key = null, string? label = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);
    Task<KeyValueSetting> GetKeyValue(
        string accountName,
        string key,
        string subscriptionId,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null,
        string? label = null,
        string? contentType = null);
    Task LockKeyValue(
        string accountName,
        string key,
        string subscriptionId,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null,
        string? label = null);
    Task UnlockKeyValue(
        string accountName,
        string key,
        string subscriptionId,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null,
        string? label = null);
    Task SetKeyValue(
        string accountName,
        string key,
        string value,
        string subscriptionId,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null,
        string? label = null,
        string? contentType = null,
        string[]? tags = null);
    Task DeleteKeyValue(
        string accountName,
        string key,
        string subscriptionId,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null,
        string? label = null);
}
