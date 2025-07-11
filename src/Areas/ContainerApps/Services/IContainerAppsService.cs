// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Options;

namespace AzureMcp.Areas.ContainerApps.Services;

public interface IContainerAppsService
{
    Task<List<ContainerAppInfo>> ListContainerAppsAsync(
        string subscription,
        string? resourceGroup = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);
}

public record ContainerAppInfo(
    string Name,
    string? ResourceGroup,
    string? Location,
    string? Status,
    string? Fqdn);