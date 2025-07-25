// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Aks.Models;
using AzureMcp.Core.Options;

namespace AzureMcp.Aks.Services;

public interface IAksService
{
    Task<List<Cluster>> ListClusters(
        string subscription,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<Cluster?> GetCluster(
        string subscription,
        string clusterName,
        string resourceGroup,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);
}
