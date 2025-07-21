// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.ContainerApps.Models;
using AzureMcp.Options;

namespace AzureMcp.Areas.ContainerApps.Services;

public interface IContainerAppsService
{
    Task<ContainerRegistry> CreateContainerRegistry(
        string registryName,
        string resourceGroup,
        string subscriptionId,
        string location,
        string? skuName = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);
}
