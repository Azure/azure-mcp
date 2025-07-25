// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.CosmosDB;
using AzureMcp.Core.Options;
using AzureMcp.Core.Services.Azure;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.Core.Services.Caching;
using Microsoft.Azure.Cosmos;

namespace AzureMcp.Cosmos.Services;

public class CosmosService(ISubscriptionService subscriptionService, ITenantService tenantService, ICacheService cacheService)
    : BaseAzureService(tenantService), ICosmosService, IDisposable
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private readonly ICacheService _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
    private const string CosmosBaseUri = "https://{0}.documents.azure.com:443/";
    private const string CacheGroup = "cosmos";
    private const string CosmosClientsCacheKeyPrefix = "clients_";
    private static readonly TimeSpan s_cacheDurationClients = TimeSpan.FromMinutes(15);
    private bool _disposed;

    private async Task<CosmosDBAccountResource> GetCosmosAccountAsync(
        string subscriptionId,
        string accountName,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
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

    private async Task<CosmosClient> CreateCosmosClientWithAuth(
        string accountName,
        string subscriptionId,
        AuthMethod authMethod,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        var clientOptions = new CosmosClientOptions { AllowBulkExecution = true };
        clientOptions.CosmosClientTelemetryOptions.DisableDistributedTracing = false;
        clientOptions.CustomHandlers.Add(new UserPolicyRequestHandler(UserAgent));

        if (retryPolicy != null)
        {
            clientOptions.MaxRetryAttemptsOnRateLimitedRequests = retryPolicy.MaxRetries;
            clientOptions.MaxRetryWaitTimeOnRateLimitedRequests = TimeSpan.FromSeconds(retryPolicy.MaxDelaySeconds);
        }

        CosmosClient cosmosClient;
        switch (authMethod)
        {
            case AuthMethod.Key:
                var cosmosAccount = await GetCosmosAccountAsync(subscriptionId, accountName, tenant);
                var keys = await cosmosAccount.GetKeysAsync();
                cosmosClient = new CosmosClient(
                    string.Format(CosmosBaseUri, accountName),
                    keys.Value.PrimaryMasterKey,
                    clientOptions);
                break;

            case AuthMethod.Credential:
            default:
                cosmosClient = new CosmosClient(
                    string.Format(CosmosBaseUri, accountName),
                    await GetCredential(tenant),
                    clientOptions);
                break;
        }

        // Validate the client by performing a lightweight operation
        await ValidateCosmosClientAsync(cosmosClient);

        return cosmosClient;
    }

    private async Task ValidateCosmosClientAsync(CosmosClient client)
    {
        try
        {
            // Perform a lightweight operation to validate the client
            await client.ReadAccountAsync();
        }
        catch (CosmosException ex)
        {
            throw new Exception($"Failed to validate CosmosClient: {ex.StatusCode} - {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Unexpected error while validating CosmosClient: {ex.Message}", ex);
        }
    }

    private async Task<CosmosClient> GetCosmosClientAsync(
        string accountName,
        string subscriptionId,
        AuthMethod authMethod = AuthMethod.Credential,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(accountName, subscriptionId);

        var key = CosmosClientsCacheKeyPrefix + accountName;
        var cosmosClient = await _cacheService.GetAsync<CosmosClient>(CacheGroup, key, s_cacheDurationClients);
        if (cosmosClient != null)
            return cosmosClient;

        try
        {
            // First attempt with requested auth method
            cosmosClient = await CreateCosmosClientWithAuth(
                accountName,
                subscriptionId,
                authMethod,
                tenant,
                retryPolicy);

            await _cacheService.SetAsync(CacheGroup, key, cosmosClient, s_cacheDurationClients);
            return cosmosClient;
        }
        catch (Exception ex) when (
            authMethod == AuthMethod.Credential &&
            (ex.Message.Contains("401") || ex.Message.Contains("403")))
        {
            // If credential auth fails with 401/403, try key auth
            cosmosClient = await CreateCosmosClientWithAuth(
                accountName,
                subscriptionId,
                AuthMethod.Key,
                tenant,
                retryPolicy);

            await _cacheService.SetAsync(CacheGroup, key, cosmosClient, s_cacheDurationClients);
            return cosmosClient;
        }

        throw new Exception($"Failed to create Cosmos client for account '{accountName}' with any authentication method");
    }

    public async Task<List<string>> GetCosmosAccounts(string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
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

    public async Task<List<string>> ListDatabases(
        string accountName,
        string subscriptionId,
        AuthMethod authMethod = AuthMethod.Credential,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(accountName, subscriptionId);

        var client = await GetCosmosClientAsync(accountName, subscriptionId, authMethod, tenant, retryPolicy);
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

    public async Task<List<string>> ListContainers(
        string accountName,
        string databaseName,
        string subscriptionId,
        AuthMethod authMethod = AuthMethod.Credential,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(accountName, databaseName, subscriptionId);

        var client = await GetCosmosClientAsync(accountName, subscriptionId, authMethod, tenant, retryPolicy);
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

    public async Task<List<JsonElement>> QueryItems(
        string accountName,
        string databaseName,
        string containerName,
        string? query,
        string subscriptionId,
        AuthMethod authMethod = AuthMethod.Credential,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(accountName, databaseName, containerName, subscriptionId);

        var client = await GetCosmosClientAsync(accountName, subscriptionId, authMethod, tenant, retryPolicy);

        try
        {
            var container = client.GetContainer(databaseName, containerName);
            var baseQuery = string.IsNullOrEmpty(query) ? "SELECT * FROM c" : query;
            var queryDef = new QueryDefinition(baseQuery);

            var items = new List<JsonElement>();
            var queryIterator = container.GetItemQueryStreamIterator(
                queryDef,
                requestOptions: new QueryRequestOptions { MaxItemCount = -1 }
            );

            while (queryIterator.HasMoreResults)
            {
                var response = await queryIterator.ReadNextAsync();
                using var document = JsonDocument.Parse(response.Content);
                items.Add(document.RootElement.Clone());
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

    protected virtual async void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Get all cached client keys
                var keys = await _cacheService.GetGroupKeysAsync(CacheGroup);

                // Filter for client keys only (those that start with the client prefix)
                var clientKeys = keys.Where(k => k.StartsWith(CosmosClientsCacheKeyPrefix));

                // Retrieve and dispose each client
                foreach (var key in clientKeys)
                {
                    var client = await _cacheService.GetAsync<CosmosClient>(CacheGroup, key);
                    client?.Dispose();
                }
                _disposed = true;
            }
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
