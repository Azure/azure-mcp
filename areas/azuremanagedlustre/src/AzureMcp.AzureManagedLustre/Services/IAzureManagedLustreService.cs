// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.AzureManagedLustre.Models;
using AzureMcp.Core.Options;

namespace AzureMcp.AzureManagedLustre.Services;

public interface IAzureManagedLustreService
{
    Task<List<LustreFileSystem>> ListFileSystemsAsync(
        string subscription,
        string? resourceGroup = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<int> GetRequiredAmlFSSubnetsSize(string subscription,
        string sku, int size,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);
}
