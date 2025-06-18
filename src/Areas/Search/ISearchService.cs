// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Options;
using static AzureMcp.Commands.Search.Index.IndexDescribeCommand;

namespace AzureMcp.Services.Interfaces;

public interface ISearchService
{
    Task<List<string>> ListServices(
        string subscription,
        string? tenantId = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<List<string>> ListIndexes(
        string serviceName,
        RetryPolicyOptions? retryPolicy = null);

    /// <summary>
    /// Gets the full definition of a search index
    /// </summary>
    /// <param name="serviceName">The name of the search service</param>
    /// <param name="indexName">The name of the search index</param>
    /// <param name="retryPolicy">Optional retry policy for the operation</param>
    /// <returns>The search index definition object</returns>
    Task<SearchIndexProxy?> DescribeIndex(
        string serviceName,
        string indexName,
        RetryPolicyOptions? retryPolicy = null);

    /// <summary>
    /// Queries an Azure Search index
    /// </summary>
    /// <param name="serviceName">The name of the search service</param>
    /// <param name="indexName">The name of the search index</param>
    /// <param name="searchText">The search query text</param>
    /// <param name="retryPolicy">Optional retry policy for the operation</param>
    /// <returns>The search results excluding large vector fields</returns>
    Task<List<JsonElement>> QueryIndex(
        string serviceName,
        string indexName,
        string searchText,
        RetryPolicyOptions? retryPolicy = null);
}
