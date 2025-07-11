using Azure.ResourceManager.ContainerService;
using Azure.ResourceManager.ContainerService.Models;
using AzureMcp.Options;

namespace AzureMcp.Services.Interfaces;

public interface IAksService
{
    Task<List<string>> ListClusters(
        string subscriptionId,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<ContainerServiceManagedClusterData> GetCluster(
        string clusterName,
        string resourceGroupName,
        string subscriptionId,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);
}
