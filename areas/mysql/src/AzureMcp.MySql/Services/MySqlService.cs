// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using Azure.Core;
using Azure.ResourceManager.MySql.FlexibleServers;
using AzureMcp.Core.Services.Azure;
using AzureMcp.Core.Services.Azure.ResourceGroup;
using MySqlConnector;

namespace AzureMcp.MySql.Services;

public class MySqlService : BaseAzureService, IMySqlService
{
    private readonly IResourceGroupService _resourceGroupService;
    private string? _cachedEntraIdAccessToken;
    private DateTime _tokenExpiryTime;
    private readonly object _tokenLock = new object();

    public MySqlService(IResourceGroupService resourceGroupService)
    {
        _resourceGroupService = resourceGroupService ?? throw new ArgumentNullException(nameof(resourceGroupService));
    }

    private async Task<string> GetEntraIdAccessTokenAsync()
    {
        if (_cachedEntraIdAccessToken != null && DateTime.UtcNow < _tokenExpiryTime)
        {
            return _cachedEntraIdAccessToken;
        }

        var tokenRequestContext = new TokenRequestContext(new[] { "https://ossrdbms-aad.database.windows.net/.default" });
        var tokenCredential = await GetCredential();
        var accessToken = await tokenCredential
            .GetTokenAsync(tokenRequestContext, CancellationToken.None)
            .ConfigureAwait(false);
        _cachedEntraIdAccessToken = accessToken.Token;
        _tokenExpiryTime = accessToken.ExpiresOn.UtcDateTime.AddSeconds(-60); // Subtract 60 seconds as a buffer.

        return _cachedEntraIdAccessToken;
    }

    private static string NormalizeServerName(string server)
    {
        if (!server.Contains('.'))
        {
            return server + ".mysql.database.azure.com";
        }
        return server;
    }

    public async Task<List<string>> ListDatabasesAsync(string subscriptionId, string resourceGroup, string user, string server)
    {
        var entraIdAccessToken = await GetEntraIdAccessTokenAsync();
        var host = NormalizeServerName(server);
        var connectionString = $"Server={host};Database=mysql;User ID={user};Password={entraIdAccessToken};SSL Mode=Required;";

        await using var resource = await MySqlResource.CreateAsync(connectionString);
        var query = "SHOW DATABASES;";
        await using var command = new MySqlCommand(query, resource.Connection);
        await using var reader = await command.ExecuteReaderAsync();
        var dbs = new List<string>();
        while (await reader.ReadAsync())
        {
            var dbName = reader.GetString(0);
            // Filter out system databases
            if (dbName != "information_schema" && dbName != "mysql" && dbName != "performance_schema" && dbName != "sys")
            {
                dbs.Add(dbName);
            }
        }
        return dbs;
    }

    public async Task<List<string>> ExecuteQueryAsync(string subscriptionId, string resourceGroup, string user, string server, string database, string query)
    {
        // Only allow SELECT statements for safety
        if (string.IsNullOrWhiteSpace(query) || !query.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Only SELECT statements are allowed.", nameof(query));
        }
        var entraIdAccessToken = await GetEntraIdAccessTokenAsync();
        var host = NormalizeServerName(server);
        var connectionString = $"Server={host};Database={database};User ID={user};Password={entraIdAccessToken};SSL Mode=Required;";

        await using var resource = await MySqlResource.CreateAsync(connectionString);
        await using var command = new MySqlCommand(query, resource.Connection);
        await using var reader = await command.ExecuteReaderAsync();

        var rows = new List<string>();

        var columnNames = Enumerable.Range(0, reader.FieldCount)
                               .Select(reader.GetName)
                               .ToArray();
        rows.Add(string.Join(", ", columnNames));
        while (await reader.ReadAsync())
        {
            var row = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                row.Add(reader[i]?.ToString() ?? "NULL");
            }
            rows.Add(string.Join(", ", row));
        }
        return rows;
    }

    public async Task<List<string>> GetTableSchemaAsync(string subscriptionId, string resourceGroup, string user, string server, string database, string table)
    {
        var entraIdAccessToken = await GetEntraIdAccessTokenAsync();
        var host = NormalizeServerName(server);
        var connectionString = $"Server={host};Database={database};User ID={user};Password={entraIdAccessToken};SSL Mode=Required;";

        await using var resource = await MySqlResource.CreateAsync(connectionString);
        var query = "SELECT column_name, data_type FROM information_schema.columns WHERE table_schema = DATABASE() AND table_name = @table;";
        await using var command = new MySqlCommand(query, resource.Connection);
        command.Parameters.AddWithValue("@table", table);
        await using var reader = await command.ExecuteReaderAsync();
        var schema = new List<string>();
        while (await reader.ReadAsync())
        {
            schema.Add($"{reader.GetString(0)}: {reader.GetString(1)}");
        }
        return schema;
    }

    public async Task<List<string>> ListServersAsync(string subscriptionId, string resourceGroup, string user)
    {
        var rg = await _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup);
        if (rg == null)
        {
            throw new Exception($"Resource group '{resourceGroup}' not found.");
        }
        var serverList = new List<string>();
        await foreach (MySqlFlexibleServerResource server in rg.GetMySqlFlexibleServers().GetAllAsync())
        {
            serverList.Add(server.Data.Name);
        }
        return serverList;
    }

    public async Task<List<string>> GetTablesAsync(string subscriptionId, string resourceGroup, string user, string server, string database)
    {
        var rg = await _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup);
        if (rg == null)
        {
            throw new Exception($"Resource group '{resourceGroup}' not found.");
        }
        var mysqlServer = await rg.GetMySqlFlexibleServerAsync(server);
        var host = mysqlServer.Value.Data.FullyQualifiedDomainName;
        var entraIdAccessToken = await GetEntraIdAccessTokenAsync();
        var connectionString = $"Server={host};Database={database};User ID={user};Password={entraIdAccessToken};SSL Mode=Required;";

        await using var resource = await MySqlResource.CreateAsync(connectionString);
        var query = "SELECT table_name FROM information_schema.tables WHERE table_schema = DATABASE();";
        await using var command = new MySqlCommand(query, resource.Connection);
        await using var reader = await command.ExecuteReaderAsync();
        var tables = new List<string>();
        while (await reader.ReadAsync())
        {
            tables.Add(reader.GetString(0));
        }

        return tables;
    }

    public async Task<string> GetServerConfigAsync(string subscriptionId, string resourceGroup, string user, string server)
    {
        var rg = await _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup);
        if (rg == null)
        {
            throw new Exception($"Resource group '{resourceGroup}' not found.");
        }
        var mysqlServer = await rg.GetMySqlFlexibleServerAsync(server);
        var mysqlServerData = mysqlServer.Value.Data;
        var result = $"Server Name: {mysqlServerData.Name}\n" +
                 $"Location: {mysqlServerData.Location}\n" +
                 $"Version: {mysqlServerData.Version}\n" +
                 $"SKU: {mysqlServerData.Sku?.Name}\n" +
                 $"Storage Size (GB): {mysqlServerData.Storage?.StorageSizeInGB}\n" +
                 $"Backup Retention Days: {mysqlServerData.Backup?.BackupRetentionDays}\n" +
                 $"Geo-Redundant Backup: {mysqlServerData.Backup?.GeoRedundantBackup}";
        return result;
    }

    public async Task<string> GetServerParameterAsync(string subscriptionId, string resourceGroup, string user, string server, string param)
    {
        var rg = await _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup);
        if (rg == null)
        {
            throw new Exception($"Resource group '{resourceGroup}' not found.");
        }
        var mysqlServer = await rg.GetMySqlFlexibleServerAsync(server);

        var configResponse = await mysqlServer.Value.GetMySqlFlexibleServerConfigurationAsync(param);
        if (configResponse?.Value?.Data == null)
        {
            throw new Exception($"Parameter '{param}' not found.");
        }
        return configResponse.Value.Data.Value;
    }

    public async Task<string> SetServerParameterAsync(string subscriptionId, string resourceGroup, string user, string server, string param, string value)
    {
        var rg = await _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup);
        if (rg == null)
        {
            throw new Exception($"Resource group '{resourceGroup}' not found.");
        }
        var mysqlServer = await rg.GetMySqlFlexibleServerAsync(server);

        var configuration = await mysqlServer.Value.GetMySqlFlexibleServerConfigurationAsync(param);
        if (configuration?.Value?.Data == null)
        {
            throw new Exception($"Parameter '{param}' not found.");
        }

        var configData = configuration.Value.Data;
        configData.Value = value;

        var updateOperation = await mysqlServer.Value.GetMySqlFlexibleServerConfigurations().CreateOrUpdateAsync(WaitUntil.Completed, param, configData);
        return updateOperation.Value.Data.Value;
    }

    private sealed class MySqlResource : IAsyncDisposable
    {
        public MySqlConnection Connection { get; }

        public static async Task<MySqlResource> CreateAsync(string connectionString)
        {
            var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();
            return new MySqlResource(connection);
        }

        public async ValueTask DisposeAsync()
        {
            await Connection.DisposeAsync();
        }

        private MySqlResource(MySqlConnection connection)
        {
            Connection = connection;
        }
    }
}
