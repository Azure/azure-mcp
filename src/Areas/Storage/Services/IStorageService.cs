// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Storage.Blobs.Models;
using AzureMcp.Areas.Storage.Models;
using AzureMcp.Options;

namespace AzureMcp.Areas.Storage.Services;

public interface IStorageService
{
    Task<List<string>> GetStorageAccounts(string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    Task<List<string>> ListContainers(string accountName, string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    Task<List<string>> ListTables(
        string accountName,
        string subscriptionId,
        AuthMethod authMethod = AuthMethod.Credential,
        string? connectionString = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);
    Task<List<string>> ListBlobs(string accountName, string containerName, string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    Task<BlobContainerProperties> GetContainerDetails(
        string accountName,
        string containerName,
        string subscriptionId,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);
    Task<List<DataLakePathInfo>> ListDataLakePaths(
        string accountName,
        string fileSystemName,
        string subscriptionId,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);
    Task<DataLakePathInfo> CreateDirectory(
        string accountName,
        string directoryPath,
        string subscriptionId,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);
}
