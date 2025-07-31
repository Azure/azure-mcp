// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using Azure.Data.Tables;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Storage;
using Azure.ResourceManager.Storage.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Files.DataLake;
using AzureMcp.Core.Options;
using AzureMcp.Core.Services.Azure;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.Core.Services.Caching;
using AzureMcp.Storage.Models;

namespace AzureMcp.Storage.Services;

public class StorageService(ISubscriptionService subscriptionService, ITenantService tenantService, ICacheService cacheService) : BaseAzureService(tenantService), IStorageService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private readonly ICacheService _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
    private const string CacheGroup = "storage";
    private const string StorageAccountsCacheKey = "accounts";
    private static readonly TimeSpan s_cacheDuration = TimeSpan.FromHours(1);

    public async Task<List<string>> GetStorageAccounts(string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId);

        // Create cache key
        var cacheKey = string.IsNullOrEmpty(tenant)
            ? $"{StorageAccountsCacheKey}_{subscriptionId}"
            : $"{StorageAccountsCacheKey}_{subscriptionId}_{tenant}";

        // Try to get from cache first
        var cachedAccounts = await _cacheService.GetAsync<List<string>>(CacheGroup, cacheKey, s_cacheDuration);
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
            await _cacheService.SetAsync(CacheGroup, cacheKey, accounts, s_cacheDuration);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving Storage accounts: {ex.Message}", ex);
        }

        return accounts;
    }

    public async Task<List<string>> ListContainers(string accountName, string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
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
        RetryPolicyOptions? retryPolicy = null)
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
            ex is RequestFailedException rfEx && rfEx.Status == 403)
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

    public async Task<List<string>> ListBlobs(string accountName, string containerName, string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
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
        RetryPolicyOptions? retryPolicy = null)
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
        RetryPolicyOptions? retryPolicy = null)
    {
        var options = ConfigureRetryPolicy(AddDefaultPolicies(new TableClientOptions()), retryPolicy);

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

    private async Task<BlobServiceClient> CreateBlobServiceClient(string accountName, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        var uri = $"https://{accountName}.blob.core.windows.net";
        var options = ConfigureRetryPolicy(AddDefaultPolicies(new BlobClientOptions()), retryPolicy);
        return new BlobServiceClient(new Uri(uri), await GetCredential(tenant), options);
    }

    private async Task<DataLakeServiceClient> CreateDataLakeServiceClient(string accountName, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        var uri = $"https://{accountName}.dfs.core.windows.net";
        var options = ConfigureRetryPolicy(AddDefaultPolicies(new DataLakeClientOptions()), retryPolicy);
        return new DataLakeServiceClient(new Uri(uri), await GetCredential(tenant), options);
    }

    public async Task<List<DataLakePathInfo>> ListDataLakePaths(
        string accountName,
        string fileSystemName,
        bool recursive,
        string subscriptionId,
        string? filterPath = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(accountName, fileSystemName, subscriptionId);

        var dataLakeServiceClient = await CreateDataLakeServiceClient(accountName, tenant, retryPolicy);
        var fileSystemClient = dataLakeServiceClient.GetFileSystemClient(fileSystemName);
        var paths = new List<DataLakePathInfo>();

        try
        {
            await foreach (var pathItem in fileSystemClient.GetPathsAsync(filterPath, recursive))
            {
                var pathInfo = new DataLakePathInfo(
                    pathItem.Name,
                    pathItem.IsDirectory == true ? "directory" : "file",
                    pathItem.ContentLength,
                    pathItem.LastModified,
                    pathItem.ETag.ToString());

                paths.Add(pathInfo);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error listing Data Lake paths: {ex.Message}", ex);
        }

        return paths;
    }

    public async Task<DataLakePathInfo> CreateDirectory(
        string accountName,
        string directoryPath,
        string subscriptionId,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(accountName, directoryPath, subscriptionId);

        var dataLakeServiceClient = await CreateDataLakeServiceClient(accountName, tenant, retryPolicy);

        try
        {
            // Parse the directory path to extract file system name and directory path
            var pathParts = directoryPath.Split('/', 2);
            if (pathParts.Length < 2)
            {
                throw new ArgumentException("DirectoryPath must include file system name (e.g., 'myfilesystem/path/to/directory')");
            }

            var fileSystemName = pathParts[0];
            var directoryPathWithinFileSystem = pathParts[1];

            var fileSystemClient = dataLakeServiceClient.GetFileSystemClient(fileSystemName);
            var directoryClient = fileSystemClient.GetDirectoryClient(directoryPathWithinFileSystem);
            var response = await directoryClient.CreateIfNotExistsAsync();

            if (response?.Value == null)
            {
                // Directory already exists, get its properties
                var properties = await directoryClient.GetPropertiesAsync();
                return new DataLakePathInfo(
                    directoryPath,
                    "directory",
                    null, // Directories don't have content length
                    properties.Value.LastModified,
                    properties.Value.ETag.ToString());
            }
            else
            {
                // Directory was created, return new directory info
                return new DataLakePathInfo(
                    directoryPath,
                    "directory",
                    null, // Directories don't have content length
                    response.Value.LastModified,
                    response.Value.ETag.ToString());
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error creating directory: {ex.Message}", ex);
        }
    }

    public async Task<(List<string> SuccessfulBlobs, List<string> FailedBlobs)> SetBlobTierBatch(
        string accountName,
        string containerName,
        string tier,
        string[] blobNames,
        string subscriptionId,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(accountName, containerName, tier, subscriptionId);

        if (blobNames == null || blobNames.Length == 0)
        {
            throw new ArgumentException("At least one blob name must be provided.", nameof(blobNames));
        }

        var blobServiceClient = await CreateBlobServiceClient(accountName, tenant, retryPolicy);
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        var batchClient = blobServiceClient.GetBlobBatchClient();
        var accessTier = new AccessTier(tier);

        try
        {
            // Use Azure.Storage.Blobs.Batch for true batch operations
            var batch = batchClient.CreateBatch();

            // Add all blob tier operations to the batch
            var batchOperations = new List<(string blobName, Response batchOperationResponse)>();
            foreach (var blobName in blobNames)
            {
                var blobClient = containerClient.GetBlobClient(blobName);
                var batchOperationResponse = batch.SetBlobAccessTier(blobClient.Uri, accessTier);
                batchOperations.Add((blobName, batchOperationResponse));
            }

            // Submit the batch operation
            var batchResponse = await batchClient.SubmitBatchAsync(batch);

            // Process results
            var successfulBlobs = new List<string>();
            var failedBlobs = new List<string>();

            for (int i = 0; i < batchOperations.Count; i++)
            {
                var (blobName, batchOperationResponse) = batchOperations[i];
                try
                {
                    // Check if the individual operation succeeded
                    if (batchOperationResponse.Status >= 200 && batchOperationResponse.Status < 300)
                    {
                        successfulBlobs.Add(blobName);
                    }
                    else
                    {
                        failedBlobs.Add($"{blobName}: HTTP {batchOperationResponse.Status}");
                    }
                }
                catch (Exception ex)
                {
                    failedBlobs.Add($"{blobName}: {ex.Message}");
                }
            }

            return (successfulBlobs, failedBlobs);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error setting blob tier batch: {ex.Message}", ex);
        }
    }
}
