// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;

namespace AzureMcp.Services.Interfaces;

public interface IPostgresService
{
    Task<List<string>> ListDatabasesAsync(string subscriptionId, string resourceGroup, string user, string server);
    Task<List<string>> ExecuteQueryAsync(string subscriptionId, string resourceGroup, string user, string server, string database, string query);

    Task<List<string>> ListTablesAsync(string subscriptionId, string resourceGroup, string user, string server, string database);
    Task<List<string>> GetTableSchemaAsync(string subscriptionId, string resourceGroup, string user, string server, string database, string table);

    Task<List<string>> ListServersAsync(string subscriptionId, string resourceGroup, string user);
    Task<string> GetServerConfigAsync(string subscriptionId, string resourceGroup, string user, string server);
    Task<string> GetServerParameterAsync(string subscriptionId, string resourceGroup, string user, string server, string param);
}
