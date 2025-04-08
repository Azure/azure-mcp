using AzureMCP.Arguments;
using System.Text.Json;

namespace AzureMCP.Services.Interfaces;

public interface ICosmosService
{
    Task<List<string>> GetCosmosAccounts(
        string subscriptionId,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null);

    Task<List<string>> ListDatabases(
        string accountName,
        string subscriptionId,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null);

    Task<List<string>> ListContainers(
        string accountName,
        string databaseName,
        string subscriptionId,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null);

    Task<List<JsonDocument>> QueryItems(
        string accountName,
        string databaseName,
        string containerName,
        string? query,
        string subscriptionId,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null);
}