// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.Resources;
using AzureMcp.Arguments;
using AzureMcp.Models.ResourceGroup;

namespace AzureMcp.Services.Interfaces;

public interface IResourceGroupService
{
    Task<List<ResourceGroupInfo>> GetResourceGroups(string subscriptionId, string? tenant = null, RetryPolicyArguments? retryPolicy = null);
    Task<ResourceGroupInfo?> GetResourceGroup(string subscriptionId, string resourceGroupName, string? tenant = null, RetryPolicyArguments? retryPolicy = null);
    Task<ResourceGroupResource?> GetResourceGroupResource(string subscriptionId, string resourceGroupName, string? tenant = null, RetryPolicyArguments? retryPolicy = null);
}
