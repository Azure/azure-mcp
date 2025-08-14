// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.Resources;
using AzureMcp.Core.Models.ResourceGroup;
using AzureMcp.Core.Options;

namespace AzureMcp.Core.Services.Azure.ResourceGroup;

public interface IResourceGroupService
{
    Task<List<ResourceGroupInfo>> GetResourceGroups(string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    Task<ResourceGroupInfo?> GetResourceGroup(string subscriptionId, string resourceGroupName, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    Task<ResourceGroupResource?> GetResourceGroupResource(string subscriptionId, string resourceGroupName, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
}
