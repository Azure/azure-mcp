// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.CosmosDB;
using AzureMcp.Arguments;
using AzureMcp.Services.Interfaces;
using Microsoft.Azure.Cosmos;
using System.Text.Json;

namespace AzureMcp.Services.Azure.Cosmos;

public class CosmosService(ISubscriptionService subscriptionService, ITenantService tenantService)
    : BaseAzureService(tenantService), ICosmosService, IDisposable
{
    private const string CosmosBaseUri = "https://{0}.documents.azure.com:443/";
    private CosmosClient? _cosmosClient;
    private bool _disposed;
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));

    private async Task<CosmosDBAccountResource> GetCosmosAccountAsync(
        string subscriptionId,
        string accountName,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId, accountName);

        var subscription = await _subscriptionService.GetSubscription(subscriptionId, tenant, retryPolicy);

        await foreach (var account in subscription.GetCosmosDBAccountsAsync())
        {
            if (account.Data.Name == accountName)
            {
                return account;
            }
        }
        throw new Exception($"Cosmos DB account '{accountName}' not found in subscription '{subscriptionId}'");
    }

    private async Task<CosmosClient> GetCosmosClientAsync(string accountName, string subscriptionId, string? tenant = null, RetryPolicyArguments? retryPolicy = null)
    {
        ValidateRequiredParameters(accountName, subscriptionId);

        if (_cosmosClient != null)
            return _cosmosClient;

        var cosmosAccount = await GetCosmosAccountAsync(subscriptionId, accountName, tenant, retryPolicy);
        var keys = await cosmosAccount.GetKeysAsync();

        var clientOptions = new CosmosClientOptions { AllowBulkExecution = true };
        clientOptions.CosmosClientTelemetryOptions.DisableDistributedTracing = false;
        clientOptions.CustomHandlers.Add(new UserPolicyRequestHandler(UserAgent));

        if (retryPolicy != null)
        {
            clientOptions.MaxRetryAttemptsOnRateLimitedRequests = retryPolicy.MaxRetries;
            clientOptions.MaxRetryWaitTimeOnRateLimitedRequests = TimeSpan.FromSeconds(retryPolicy.MaxDelaySeconds);
        }

        _cosmosClient = new CosmosClient(
            string.Format(CosmosBaseUri, accountName),
            keys.Value.PrimaryMasterKey,
            clientOptions
        );

        return _cosmosClient;
    }

    public async Task<List<string>> GetCosmosAccounts(string subscriptionId, string? tenant = null, RetryPolicyArguments? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId);

        var subscription = await _subscriptionService.GetSubscription(subscriptionId, tenant, retryPolicy);
        var accounts = new List<string>();
        try
        {
            await foreach (var account in subscription.GetCosmosDBAccountsAsync())
            {
                if (account?.Data?.Name != null)
                {
                    accounts.Add(account.Data.Name);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving Cosmos DB accounts: {ex.Message}", ex);
        }

        return accounts;
    }

    public async Task<List<string>> ListDatabases(string accountName, string subscriptionId, string? tenant = null, RetryPolicyArguments? retryPolicy = null)
    {
        ValidateRequiredParameters(accountName, subscriptionId);

        var client = await GetCosmosClientAsync(accountName, subscriptionId, tenant, retryPolicy);
        var databases = new List<string>();

        try
        {
            var iterator = client.GetDatabaseQueryIterator<DatabaseProperties>();
            while (iterator.HasMoreResults)
            {
                var results = await iterator.ReadNextAsync();
                databases.AddRange(results.Select(r => r.Id));
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error listing databases: {ex.Message}", ex);
        }

        return databases;
    }

    public async Task<List<string>> ListContainers(string accountName, string databaseName, string subscriptionId, string? tenant = null, RetryPolicyArguments? retryPolicy = null)
    {
        ValidateRequiredParameters(accountName, databaseName, subscriptionId);

        var client = await GetCosmosClientAsync(accountName, subscriptionId, tenant, retryPolicy);
        var containers = new List<string>();

        try
        {
            var database = client.GetDatabase(databaseName);
            var iterator = database.GetContainerQueryIterator<ContainerProperties>();
            while (iterator.HasMoreResults)
            {
                var results = await iterator.ReadNextAsync();
                containers.AddRange(results.Select(r => r.Id));
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error listing containers: {ex.Message}", ex);
        }

        return containers;
    }

    public async Task<List<JsonDocument>> QueryItems(
        string accountName,
        string databaseName,
        string containerName,
        string? query,
        string subscriptionId,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null)
    {
        ValidateRequiredParameters(accountName, databaseName, containerName, subscriptionId);

        var client = await GetCosmosClientAsync(accountName, subscriptionId, tenant, retryPolicy);

        try
        {
            var container = client.GetContainer(databaseName, containerName);
            var baseQuery = string.IsNullOrEmpty(query) ? "SELECT * FROM c" : query;
            var queryDef = new QueryDefinition(baseQuery);

            var items = new List<JsonDocument>();
            var queryIterator = container.GetItemQueryIterator<System.Dynamic.ExpandoObject>(
                queryDef,
                requestOptions: new QueryRequestOptions { MaxItemCount = -1 }
            );

            while (queryIterator.HasMoreResults)
            {
                var response = await queryIterator.ReadNextAsync();
                foreach (var item in response)
                {
                    var json = JsonSerializer.Serialize(item);
                    items.Add(JsonDocument.Parse(json));
                }
            }

            return items;
        }
        catch (CosmosException ex)
        {
            throw new Exception($"Cosmos DB error occurred while querying items: {ex.StatusCode} - {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error querying items: {ex.Message}", ex);
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _cosmosClient?.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    internal class UserPolicyRequestHandler : RequestHandler
    {
        private readonly string userAgent;

        internal UserPolicyRequestHandler(string userAgent) => this.userAgent = userAgent;

        public override Task<ResponseMessage> SendAsync(RequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Set(UserAgentPolicy.UserAgentHeader, userAgent);
            return base.SendAsync(request, cancellationToken);
        }
    }
}