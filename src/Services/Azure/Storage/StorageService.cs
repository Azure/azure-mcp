// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using Azure.Data.Tables;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Storage;
using Azure.ResourceManager.Storage.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureMcp.Arguments;
using AzureMcp.Models;
using AzureMcp.Services.Interfaces;

namespace AzureMcp.Services.Azure.Storage;

public class StorageService(ISubscriptionService subscriptionService, ITenantService tenantService,ICacheService cacheService) : BaseAzureService(tenantService), IStorageService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private readonly ICacheService _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
    private const string STORAGE_ACCOUNTS_CACHE_KEY = "storage_accounts";
    private static readonly TimeSpan CACHE_DURATION = TimeSpan.FromHours(1);

    public async Task<List<string>> GetStorageAccounts(string subscriptionId, string? tenant = null, RetryPolicyArguments? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId);

        // Create cache key
        var cacheKey = string.IsNullOrEmpty(tenant)
            ? $"{STORAGE_ACCOUNTS_CACHE_KEY}_{subscriptionId}"
            : $"{STORAGE_ACCOUNTS_CACHE_KEY}_{subscriptionId}_{tenant}";

        // Try to get from cache first
        var cachedAccounts = await _cacheService.GetAsync<List<string>>(cacheKey, CACHE_DURATION);
        if (cachedAccounts != null)
        {
            return cachedAccounts;
        }

        var subscription = await _subscriptionService.GetSubscription(subscriptionId, tenant, retryPolicy);
        var accounts = new List<string>();
        try
        {
            await foreach (var account in subscription.GetStorageAccountsAsync())
            {
                if (account?.Data?.Name != null)
                {
                    accounts.Add(account.Data.Name);
                }
            }

            // Cache the results
            await _cacheService.SetAsync(cacheKey, accounts, CACHE_DURATION);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving Storage accounts: {ex.Message}", ex);
        }

        return accounts;
    }

    public async Task<List<string>> ListContainers(string accountName, string subscriptionId, string? tenant = null, RetryPolicyArguments? retryPolicy = null)
    {
        ValidateRequiredParameters(accountName, subscriptionId);

        var blobServiceClient = await CreateBlobServiceClient(accountName, tenant, retryPolicy);
        var containers = new List<string>();

        try
        {
            await foreach (var container in blobServiceClient.GetBlobContainersAsync())
            {
                containers.Add(container.Name);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error listing containers: {ex.Message}", ex);
        }

        return containers;
    }

    public async Task<List<string>> ListTables(
        string accountName,
        string subscriptionId,
        AuthMethod authMethod = AuthMethod.Credential,
        string? connectionString = null,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null)
    {
        ValidateRequiredParameters(accountName, subscriptionId);

        var tables = new List<string>();

        try
        {
            // First attempt with requested auth method
            var tableServiceClient = await CreateTableServiceClientWithAuth(
                accountName,
                subscriptionId,
                authMethod,
                connectionString,
                tenant,
                retryPolicy);

            await foreach (var table in tableServiceClient.QueryAsync())
            {
                tables.Add(table.Name);
            }
            return tables;
        }
        catch (Exception ex) when (
            authMethod == AuthMethod.Credential &&
            ex is RequestFailedException rfEx &&
            (rfEx.Status == 403 || rfEx.Status == 401))
        {
            try
            {
                // If credential auth fails with 403/401, try key auth
                var keyClient = await CreateTableServiceClientWithAuth(
                    accountName, subscriptionId, AuthMethod.Key, connectionString, tenant, retryPolicy);

                tables.Clear(); // Reset the list for reuse
                await foreach (var table in keyClient.QueryAsync())
                {
                    tables.Add(table.Name);
                }
                return tables;
            }
            catch (Exception keyEx) when (keyEx is RequestFailedException keyRfEx && keyRfEx.Status == 403)
            {
                // If key auth fails with 403, try connection string
                var connStringClient = await CreateTableServiceClientWithAuth(
                    accountName, subscriptionId, AuthMethod.ConnectionString, connectionString, tenant, retryPolicy);

                tables.Clear(); // Reset the list for reuse
                await foreach (var table in connStringClient.QueryAsync())
                {
                    tables.Add(table.Name);
                }
                return tables;
            }
            catch (Exception keyEx)
            {
                throw new Exception($"Error listing tables with key auth: {keyEx.Message}", keyEx);
            }
        }
        catch (Exception ex) when (
            authMethod == AuthMethod.Key &&
            (ex is RequestFailedException rfEx && rfEx.Status == 403))
        {
            try
            {
                // If key auth fails with 403, try connection string
                var connStringClient = await CreateTableServiceClientWithAuth(
                    accountName, subscriptionId, AuthMethod.ConnectionString, connectionString, tenant, retryPolicy);

                tables.Clear(); // Reset the list for reuse
                await foreach (var table in connStringClient.QueryAsync())
                {
                    tables.Add(table.Name);
                }
                return tables;
            }
            catch (Exception connStringEx)
            {
                throw new Exception($"Error listing tables with connection string: {connStringEx.Message}", connStringEx);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error listing tables: {ex.Message}", ex);
        }
    }

    public async Task<List<string>> ListBlobs(string accountName, string containerName, string subscriptionId, string? tenant = null, RetryPolicyArguments? retryPolicy = null)
    {
        ValidateRequiredParameters(accountName, containerName, subscriptionId);

        var blobServiceClient = await CreateBlobServiceClient(accountName, tenant, retryPolicy);
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        var blobs = new List<string>();

        try
        {
            await foreach (var blob in containerClient.GetBlobsAsync())
            {
                blobs.Add(blob.Name);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error listing blobs: {ex.Message}", ex);
        }

        return blobs;
    }

    public async Task<BlobContainerProperties> GetContainerDetails(
        string accountName,
        string containerName,
        string subscriptionId,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null)
    {
        ValidateRequiredParameters(accountName, containerName);

        var blobServiceClient = await CreateBlobServiceClient(accountName, tenant, retryPolicy);
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        try
        {
            var properties = await containerClient.GetPropertiesAsync();
            return properties.Value;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting container details: {ex.Message}", ex);
        }
    }

    private async Task<string> GetStorageAccountKey(string accountName, string subscriptionId, string? tenant = null)
    {
        var subscription = await _subscriptionService.GetSubscription(subscriptionId, tenant);
        var storageAccount = await GetStorageAccount(subscription, accountName) ??
            throw new Exception($"Storage account '{accountName}' not found in subscription '{subscriptionId}'");

        var keys = new List<StorageAccountKey>();
        await foreach (var key in storageAccount.GetKeysAsync())
        {
            keys.Add(key);
        }

        var firstKey = keys.FirstOrDefault() ?? throw new Exception($"No keys found for storage account '{accountName}'");
        return firstKey.Value;
    }

    private async Task<string> GetStorageAccountConnectionString(string accountName, string subscriptionId, string? tenant = null)
    {
        var subscription = await _subscriptionService.GetSubscription(subscriptionId, tenant);
        var storageAccount = await GetStorageAccount(subscription, accountName) ??
            throw new Exception($"Storage account '{accountName}' not found in subscription '{subscriptionId}'");

        var keys = new List<StorageAccountKey>();
        await foreach (var key in storageAccount.GetKeysAsync())
        {
            keys.Add(key);
        }

        var firstKey = keys.FirstOrDefault() ?? throw new Exception($"No keys found for storage account '{accountName}'");
        return $"DefaultEndpointsProtocol=https;AccountName={accountName};AccountKey={firstKey.Value};EndpointSuffix=core.windows.net";
    }

    // Helper method to get storage account
    private static async Task<StorageAccountResource?> GetStorageAccount(SubscriptionResource subscription, string accountName)
    {
        await foreach (var account in subscription.GetStorageAccountsAsync())
        {
            if (account.Data.Name == accountName)
            {
                return account;
            }
        }
        return null;
    }

    protected async Task<TableServiceClient> CreateTableServiceClientWithAuth(
        string accountName,
        string subscriptionId,
        AuthMethod authMethod,
        string? connectionString = null,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null)
    {
        var options = AddDefaultPolicies(new TableClientOptions());

        if (retryPolicy != null)
        {
            options.Retry.Delay = TimeSpan.FromSeconds(retryPolicy.DelaySeconds);
            options.Retry.MaxDelay = TimeSpan.FromSeconds(retryPolicy.MaxDelaySeconds);
            options.Retry.MaxRetries = retryPolicy.MaxRetries;
            options.Retry.Mode = retryPolicy.Mode;
            options.Retry.NetworkTimeout = TimeSpan.FromSeconds(retryPolicy.NetworkTimeoutSeconds);
        }

        switch (authMethod)
        {
            case AuthMethod.Key:
                var key = await GetStorageAccountKey(accountName, subscriptionId, tenant);
                var uri = $"https://{accountName}.table.core.windows.net";
                return new TableServiceClient(new Uri(uri), new TableSharedKeyCredential(accountName, key), options);

            case AuthMethod.ConnectionString:
                var connString = await GetStorageAccountConnectionString(accountName, subscriptionId, tenant);
                return new TableServiceClient(connString, options);

            case AuthMethod.Credential:
            default:
                var defaultUri = $"https://{accountName}.table.core.windows.net";
                return new TableServiceClient(new Uri(defaultUri), await GetCredential(tenant), options);
        }
    }

    private async Task<BlobServiceClient> CreateBlobServiceClient(string accountName, string? tenant = null, RetryPolicyArguments? retryPolicy = null)
    {
        var uri = $"https://{accountName}.blob.core.windows.net";
        var options = AddDefaultPolicies(new BlobClientOptions());

        if (retryPolicy != null)
        {
            options.Retry.Delay = TimeSpan.FromSeconds(retryPolicy.DelaySeconds);
            options.Retry.MaxDelay = TimeSpan.FromSeconds(retryPolicy.MaxDelaySeconds);
            options.Retry.MaxRetries = retryPolicy.MaxRetries;
            options.Retry.Mode = retryPolicy.Mode;
            options.Retry.NetworkTimeout = TimeSpan.FromSeconds(retryPolicy.NetworkTimeoutSeconds);
        }
        return new BlobServiceClient(new Uri(uri), await GetCredential(tenant), options);
    }
}