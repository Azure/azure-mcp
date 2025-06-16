// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.Kusto;
using AzureMcp.Commands.Kusto;
using AzureMcp.Options;
using AzureMcp.Services.Interfaces;
using Kusto.Cloud.Platform.Data;
using Kusto.Data;
using Kusto.Data.Common;
using Kusto.Data.Net.Client;

namespace AzureMcp.Services.Azure.Kusto;

public sealed class KustoService(
    ISubscriptionService subscriptionService,
    ITenantService tenantService,
    ICacheService cacheService) : BaseAzureService(tenantService), IKustoService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private readonly ICacheService _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));

    private const string CacheGroup = "kusto";
    private const string KustoClustersCacheKey = "clusters";
    private const string KustoAdminProviderCacheKey = "adminprovider";
    private static readonly TimeSpan s_cacheDuration = TimeSpan.FromHours(1);
    private static readonly TimeSpan s_providerCacheDuration = TimeSpan.FromHours(2);

    // Provider cache key generator
    private static string GetProviderCacheKey(KustoConnectionStringBuilder kcsb)
        => $"{KustoAdminProviderCacheKey}_{kcsb.DataSource}_{kcsb.InitialCatalog}_{kcsb.Authority}_{kcsb.ToString()}";

    private ClientRequestProperties CreateClientRequestProperties()
    {
        return new ClientRequestProperties
        {
            ClientRequestId = $"AzMcp;{Guid.NewGuid()}",
            Application = "AzureMCP"
        };
    }

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

        var kcsb = await CreateKustoConnectionStringBuilder(
            clusterUri.TrimEnd('/'),
            authMethod,
            null,
            tenant);

        var cslAdminProvider = await GetOrCreateCslAdminProvider(kcsb);

        var clientRequestProperties = CreateClientRequestProperties();
        var result = new List<string>();
        using (var reader = await cslAdminProvider.ExecuteControlCommandAsync(
            cslAdminProvider.DefaultDatabaseName,
            ".show databases",
            clientRequestProperties))
        {
            while (reader.Read())
            {
                result.Add(reader["DatabaseName"].ToString()!);
            }
        }
        return result;
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

        var kcsb = await CreateKustoConnectionStringBuilder(
            clusterUri.TrimEnd('/'),
            authMethod,
            null,
            tenant);

        var cslAdminProvider = await GetOrCreateCslAdminProvider(kcsb);

        var clientRequestProperties = CreateClientRequestProperties();
        var result = new List<string>();
        using (var reader = await cslAdminProvider.ExecuteControlCommandAsync(
            databaseName,
            ".show tables",
            clientRequestProperties))
        {
            while (reader.Read())
            {
                result.Add(reader["TableName"].ToString()!);
            }
        }

        return result;
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

        var kcsb = await CreateKustoConnectionStringBuilder(clusterUri.TrimEnd(), authMethod, null, tenant);
        var cslAdminProvider = await GetOrCreateCslAdminProvider(kcsb);
        var clientRequestProperties = CreateClientRequestProperties();

        using (var reader = await cslAdminProvider.ExecuteControlCommandAsync(
            databaseName,
            $".show table {tableName} cslschema",
            clientRequestProperties))
        {
            if (reader.Read())
            {
                var schema = reader["Schema"].ToString();
                if (string.IsNullOrEmpty(schema))
                {
                    throw new Exception($"No schema found for table '{tableName}' in database '{databaseName}'.");
                }

                return schema!;
            }
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

        var results = await QueryItems(clusterUri, databaseName, query, tenant, authMethod, retryPolicy);
        return results;
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

        var kcsb = await CreateKustoConnectionStringBuilder(
            clusterUri,
            authMethod,
            null,
            tenant);

        var cslQueryProvider = await GetOrCreateCslQueryProvider(kcsb);
        var clientRequestProperties = CreateClientRequestProperties();

        var results = new List<JsonElement>();
        using (var reader = await cslQueryProvider.ExecuteQueryAsync(databaseName, query, clientRequestProperties))
        {
            var items = reader.ToJObjects();
            foreach (var item in items)
            {
                var json = item.ToString();
                results.Add(JsonDocument.Parse(json).RootElement);
            }
        }

        return results;
    }

    private async Task<ICslAdminProvider> GetOrCreateCslAdminProvider(KustoConnectionStringBuilder kcsb)
    {
        var providerCacheKey = GetProviderCacheKey(kcsb);
        var cslAdminProvider = await _cacheService.GetAsync<ICslAdminProvider>(CacheGroup, providerCacheKey, s_providerCacheDuration);
        if (cslAdminProvider == null)
        {
            cslAdminProvider = KustoClientFactory.CreateCslAdminProvider(kcsb);
            await _cacheService.SetAsync(CacheGroup, providerCacheKey, cslAdminProvider, s_providerCacheDuration);
        }

        return cslAdminProvider;
    }

    private async Task<ICslQueryProvider> GetOrCreateCslQueryProvider(KustoConnectionStringBuilder kcsb)
    {
        var providerCacheKey = GetProviderCacheKey(kcsb) + "_query";
        var cslQueryProvider = await _cacheService.GetAsync<ICslQueryProvider>(CacheGroup, providerCacheKey, s_providerCacheDuration);
        if (cslQueryProvider == null)
        {
            cslQueryProvider = KustoClientFactory.CreateCslQueryProvider(kcsb);
            await _cacheService.SetAsync(CacheGroup, providerCacheKey, cslQueryProvider, s_providerCacheDuration);
        }

        return cslQueryProvider;
    }

    private async Task<KustoConnectionStringBuilder> CreateKustoConnectionStringBuilder(
        string uri,
        AuthMethod? authMethod,
        string? connectionString = null,
        string? tenant = null)
    {
        switch (authMethod)
        {
            case AuthMethod.Key:
                throw new NotSupportedException("Not Supported. Supported Types are: AAD credential or connection string.");
            case AuthMethod.ConnectionString:
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ArgumentNullException(nameof(connectionString));
                }

                return new KustoConnectionStringBuilder(connectionString);
            case AuthMethod.Credential:
            default:
                var credential = await GetCredential(tenant);
                var builder = new KustoConnectionStringBuilder(uri).WithAadAzureTokenCredentialsAuthentication(credential);
                if (!string.IsNullOrEmpty(tenant))
                {
                    builder.Authority = $"https://login.microsoftonline.com/{tenant}";
                }

                return builder;
        }
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
}
