// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.Core.Pipeline;
using AzureMcp.Areas.AppService.Models;

namespace AzureMcp.Areas.AppService.Services;

public interface IAppServiceService
{
    Task<DatabaseConnectionInfo> AddDatabaseAsync(
        string appName,
        string resourceGroup,
        string databaseType,
        string databaseServer,
        string databaseName,
        string? connectionString,
        string subscription,
        RetryPolicy retryPolicy);
    Task<DatabaseConnectionInfo> AddDatabaseAsync(string v1, string v2, string v3, string v4, string v5, string? connectionString, object v6, RetryPolicy retryPolicy);
}
