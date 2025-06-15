// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.Kusto;
using AzureMcp.Commands.Kusto;
using AzureMcp.Options;
using AzureMcp.Services.Interfaces;

namespace AzureMcp.Services.Azure.Kusto;

public sealed class KustoService(
    ISubscriptionService subscriptionService,
    ITenantService tenantService,
    ICacheService cacheService) : BaseAzureService(tenantService), IKustoService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private readonly ICacheService _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
    private readonly HttpClientFactory _httpClientFactory = new();

    private const string CacheGroup = "kusto";
    private const string KustoClustersCacheKey = "clusters";
    private const string KustoAdminProviderCacheKey = "adminprovider";
    private static readonly TimeSpan s_cacheDuration = TimeSpan.FromHours(1);
    private static readonly TimeSpan s_providerCacheDuration = TimeSpan.FromHours(2);

    // Provider cache key generator
    private static string GetProviderCacheKey(string clusterUri)
        => $"{clusterUri}";
   
    #region Public Methods
    public async Task<List<string>> ListClusters(
        string subscriptionId,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(subscriptionId, nameof(subscriptionId));

        // Create cache key
        var cacheKey = string.IsNullOrEmpty(tenant)
            ? $"{KustoClustersCacheKey}_{subscriptionId}"
            : $"{KustoClustersCacheKey}_{subscriptionId}_{tenant}";

        // Try to get from cache first
        var cachedClusters = await _cacheService.GetAsync<List<string>>(CacheGroup, cacheKey, s_cacheDuration);
        if (cachedClusters != null)
        {
            return cachedClusters;
        }

        var subscription = await _subscriptionService.GetSubscription(subscriptionId, tenant, retryPolicy);
        var clusters = new List<string>();

        await foreach (var cluster in subscription.GetKustoClustersAsync())
        {
            if (cluster?.Data?.Name != null)
            {
                clusters.Add(cluster.Data.Name);
            }
        }
        await _cacheService.SetAsync(CacheGroup, cacheKey, clusters, s_cacheDuration);

        return clusters;
    }

    public async Task<KustoClusterResourceProxy?> GetCluster(
            string subscriptionId,
            string clusterName,
            string? tenant = null,
            RetryPolicyOptions? retryPolicy = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(subscriptionId, nameof(subscriptionId));

        var subscription = await _subscriptionService.GetSubscription(subscriptionId, tenant, retryPolicy);

        await foreach (var cluster in subscription.GetKustoClustersAsync())
        {
            if (string.Equals(cluster.Data.Name, clusterName, StringComparison.OrdinalIgnoreCase))
            {
                return new KustoClusterResourceProxy(cluster);
            }
        }

        return null;
    }

    public async Task<List<string>> ListDatabases(
        string subscriptionId,
        string clusterName,
        string? tenant = null,
        AuthMethod? authMethod =
        AuthMethod.Credential,
        RetryPolicyOptions? retryPolicy = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(subscriptionId, nameof(subscriptionId));
        ArgumentException.ThrowIfNullOrEmpty(clusterName, nameof(clusterName));

        string clusterUri = await GetClusterUri(subscriptionId, clusterName, tenant, retryPolicy);

        return await ListDatabases(clusterUri, tenant, authMethod, retryPolicy);
    }

    public async Task<List<string>> ListDatabases(
        string clusterUri,
        string? tenant = null,
        AuthMethod? authMethod = AuthMethod.Credential,
        RetryPolicyOptions? retryPolicy = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(clusterUri, nameof(clusterUri));

        var kustoClient = await GetOrCreateKustoClient(clusterUri, tenant).ConfigureAwait(false);

        using (var kustoResult = await kustoClient.ExecuteControlCommandAsync(
            "NetDefaultDB",
            ".show databases | project DatabaseName",
            CancellationToken.None))
        {
            return KustoResultToStringList(kustoResult);
        }
    }

    public async Task<List<string>> ListTables(
        string subscriptionId,
        string clusterName,
        string databaseName,
        string? tenant = null,
        AuthMethod? authMethod = AuthMethod.Credential,
        RetryPolicyOptions? retryPolicy = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(subscriptionId, nameof(subscriptionId));
        ArgumentException.ThrowIfNullOrEmpty(clusterName, nameof(clusterName));
        ArgumentException.ThrowIfNullOrEmpty(databaseName, nameof(databaseName));

        string clusterUri = await GetClusterUri(subscriptionId, clusterName, tenant, retryPolicy);

        return await ListTables(clusterUri, databaseName, tenant, authMethod, retryPolicy);
    }

    public async Task<List<string>> ListTables(
        string clusterUri,
        string databaseName,
        string? tenant = null,
        AuthMethod? authMethod = AuthMethod.Credential,
        RetryPolicyOptions? retryPolicy = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(clusterUri, nameof(clusterUri));
        ArgumentException.ThrowIfNullOrEmpty(databaseName, nameof(databaseName));

        var kustoClient = await GetOrCreateKustoClient(clusterUri, tenant);

        using (var kustoResult = await kustoClient.ExecuteControlCommandAsync(
            databaseName,
            ".show tables",
            CancellationToken.None))
        {
            return KustoResultToStringList(kustoResult);
        }
    }

    public async Task<string> GetTableSchema(
        string subscriptionId,
        string clusterName,
        string databaseName,
        string tableName,
        string? tenant = null,
        AuthMethod? authMethod = AuthMethod.Credential,
        RetryPolicyOptions? retryPolicy = null)
    {
        string clusterUri = await GetClusterUri(subscriptionId, clusterName, tenant, retryPolicy);
        return await GetTableSchema(clusterUri, databaseName, tableName, tenant, authMethod, retryPolicy);
    }

    public async Task<string> GetTableSchema(
        string clusterUri,
        string databaseName,
        string tableName,
        string? tenant = null,
        AuthMethod? authMethod = AuthMethod.Credential,
        RetryPolicyOptions? retryPolicy = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(tableName, nameof(tableName));
        ArgumentException.ThrowIfNullOrEmpty(databaseName, nameof(databaseName));
        ArgumentException.ThrowIfNullOrEmpty(clusterUri, nameof(clusterUri));
       
        var kustoClient = await GetOrCreateKustoClient(clusterUri, tenant);

        using (var kustoResult = await kustoClient.ExecuteQueryAsync(
            databaseName,
            $".show table {tableName} cslschema", CancellationToken.None))
        {
            var result = KustoResultToStringList(kustoResult);
            return result.FirstOrDefault();
        }

        throw new Exception($"No schema found for table '{tableName}' in database '{databaseName}'.");
    }

    public async Task<List<JsonElement>> QueryItems(
            string subscriptionId,
            string clusterName,
            string databaseName,
            string query,
            string? tenant = null,
            AuthMethod? authMethod = AuthMethod.Credential,
            RetryPolicyOptions? retryPolicy = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(subscriptionId, nameof(subscriptionId));
        ArgumentException.ThrowIfNullOrEmpty(clusterName, nameof(clusterName));
        ArgumentException.ThrowIfNullOrEmpty(databaseName, nameof(databaseName));
        ArgumentException.ThrowIfNullOrEmpty(query, nameof(query));


        string clusterUri = await GetClusterUri(subscriptionId, clusterName, tenant, retryPolicy);
        return await QueryItems(clusterUri, databaseName, query, tenant, authMethod, retryPolicy);
    }

    public async Task<List<JsonElement>> QueryItems(
        string clusterUri,
        string databaseName,
        string query,
        string? tenant = null,
        AuthMethod? authMethod = AuthMethod.Credential,
        RetryPolicyOptions? retryPolicy = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(clusterUri, nameof(clusterUri));
        ArgumentException.ThrowIfNullOrEmpty(databaseName, nameof(databaseName));
        ArgumentException.ThrowIfNullOrEmpty(query, nameof(query));
     
        var cslQueryProvider = await GetOrCreateCslQueryProvider(clusterUri, tenant);

        var result = new List<JsonElement>();
        using (var kustoResult = await cslQueryProvider.ExecuteQueryAsync(databaseName, query, CancellationToken.None))
        {
            var columnsDict = kustoResult.JsonDocument.RootElement
                .GetProperty("Tables")[0]
                .GetProperty("Columns")
                .EnumerateArray()
                .ToDictionary(
                    column => column.GetProperty("ColumnName").GetString()!,
                    column => column.GetProperty("ColumnType").GetString()!
                );

            var columnsDictJson = "{" + string.Join(",", columnsDict.Select(kvp =>
                        $"\"{JsonEncodedText.Encode(kvp.Key)}\":\"{JsonEncodedText.Encode(kvp.Value)}\"")) + "}";
            result.Add(JsonDocument.Parse(columnsDictJson).RootElement);

            var items = kustoResult.JsonDocument.RootElement.GetProperty("Tables")[0].GetProperty("Rows");
            foreach (var item in items.EnumerateArray())
            {
                var json = item.ToString();
                result.Add(JsonDocument.Parse(json).RootElement);
            }
        }

        return result;
    }
    #endregion

    #region Private Methods
    private List<string> KustoResultToStringList(KustoResult kustoResult)
    {

        var columns = kustoResult.JsonDocument.RootElement.GetProperty("Tables")[0].GetProperty("Columns").EnumerateArray().
            Select(column => ($"{column.GetProperty("ColumnName").GetString()}:{column.GetProperty("ColumnType").GetString()}"));

        var columnsAsString = string.Join(",", columns);

        var result = new List<string>() { columnsAsString };
        var items = kustoResult.JsonDocument.RootElement.GetProperty("Tables")[0].GetProperty("Rows");
        foreach (var item in items.EnumerateArray())
        {
            var jsonAsString = item.ToString();
            result.Add(jsonAsString);
        }
        return result;
    }

    private async Task<KustoClient> GetOrCreateKustoClient(string clusterUri, string tenant)
    {
        var providerCacheKey = GetProviderCacheKey(clusterUri) + "_command";
        var kustoClient = await _cacheService.GetAsync<KustoClient>(CacheGroup, providerCacheKey, s_providerCacheDuration);
        if (kustoClient == null)
        {
            var tokenCredential = await GetCredential(tenant);
            kustoClient = new KustoClient(clusterUri, _httpClientFactory, tokenCredential);
            await _cacheService.SetAsync(CacheGroup, providerCacheKey, kustoClient, s_providerCacheDuration);
        }

        return kustoClient;
    }

    private async Task<KustoClient> GetOrCreateCslQueryProvider(string clusterUri, string tenant)
    {
        var providerCacheKey = GetProviderCacheKey(clusterUri) + "_query";
        var kustoClient = await _cacheService.GetAsync<KustoClient>(CacheGroup, providerCacheKey, s_providerCacheDuration);
        if (kustoClient == null)
        {
            var tokenCredential = await GetCredential(tenant);
            kustoClient = new KustoClient(clusterUri, _httpClientFactory, tokenCredential);
            await _cacheService.SetAsync(CacheGroup, providerCacheKey, kustoClient, s_providerCacheDuration);
        }

        return kustoClient;
    }

    private async Task<string> GetClusterUri(
        string subscriptionId,
        string clusterName,
        string? tenant,
        RetryPolicyOptions? retryPolicy)
    {
        var cluster = await GetCluster(subscriptionId, clusterName, tenant, retryPolicy);
        var value = cluster?.ClusterUri;

        if (string.IsNullOrEmpty(value))
        {
            throw new Exception($"Could not retrieve ClusterUri for cluster '{clusterName}'");
        }

        return value!;
    }
    #endregion
}
