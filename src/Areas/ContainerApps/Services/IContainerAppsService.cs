// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.ContainerApps.Models;
using AzureMcp.Options;

namespace AzureMcp.Areas.ContainerApps.Services;

public interface IContainerAppsService
{
    Task<List<ContainerApp>> ListApps(
        string subscription,
        string? resourceGroupName = null,
        string? environmentName = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);
}
