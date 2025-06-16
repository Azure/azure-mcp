// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Sql;
using AzureMcp.Options;

namespace AzureMcp.Services.Interfaces;

/// <summary>
/// Service interface for managing Azure SQL resources.
/// </summary>
public interface ISqlService
{
    /// <summary>
    /// Lists all SQL servers in a subscription.
    /// </summary>
    /// <param name="subscription">The subscription ID or name.</param>
    /// <param name="tenant">Optional tenant ID.</param>
    /// <param name="retryPolicy">Optional retry policy for the operation.</param>
    /// <returns>A list of SQL server names.</returns>
    Task<List<string>> ListServers(
        string subscription,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);

    /// <summary>
    /// Lists all databases on a SQL server.
    /// </summary>
    /// <param name="subscription">The subscription ID or name.</param>
    /// <param name="resourceGroup">The resource group name.</param>
    /// <param name="server">The SQL server name.</param>
    /// <param name="tenant">Optional tenant ID.</param>
    /// <param name="authMethod">Optional authentication method.</param>
    /// <param name="retryPolicy">Optional retry policy for the operation.</param>
    /// <returns>A list of database names.</returns>
    Task<List<string>> ListDatabases(
        string subscription,
        string resourceGroup,
        string server,
        string? tenant = null,
        AuthMethod? authMethod = null, RetryPolicyOptions? retryPolicy = null);    /// <summary>
                                                                                   /// Gets recommendations for an Azure SQL database from specified advisor types.
                                                                                   /// </summary>
                                                                                   /// <param name="database">The database name to get recommendations for.</param>
                                                                                   /// <param name="server">The name of the SQL server containing the database.</param>
                                                                                   /// <param name="resourceGroup">The resource group name.</param>
                                                                                   /// <param name="tableName">Optional name of a specific table to get recommendations for.</param>
                                                                                   /// <param name="minImpact">Optional minimum impact threshold.</param>
                                                                                   /// <param name="advisorType">Optional advisor type to filter by (CreateIndex, DropIndex, ForceLastGoodPlan, DbParameterization).</param>
                                                                                   /// <param name="subscription">Azure subscription ID containing the database.</param>
                                                                                   /// <param name="tenant">Optional tenant ID.</param>
                                                                                   /// <param name="retryPolicy">Optional retry policy for the operation.</param>
                                                                                   /// <returns>Analysis result containing recommendations and analysis metadata.</returns>
    Task<SqlAnalysisResult> GetRecommendationsAsync(
        string database,
        string server,
        string resourceGroup,
        string? tableName,
        int? minImpact,
        string? advisorType,
        string subscription,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);
}
