// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Options;
using AzureMcp.Search.Models;
using static AzureMcp.Search.Commands.Index.IndexDescribeCommand;

namespace AzureMcp.Search.Services;

public interface ISearchService
{
    Task<List<string>> ListServices(
        string subscription,
        string? tenantId = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<List<IndexInfo>> ListIndexes(
        string serviceName,
        RetryPolicyOptions? retryPolicy = null);

    Task<SearchIndexProxy?> DescribeIndex(
        string serviceName,
        string indexName,
        RetryPolicyOptions? retryPolicy = null);

    Task<List<JsonElement>> QueryIndex(
        string serviceName,
        string indexName,
        string searchText,
        RetryPolicyOptions? retryPolicy = null);
}
