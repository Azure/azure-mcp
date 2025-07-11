// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Monitor.Query;
using AzureMcp.Options;

namespace AzureMcp.Areas.Monitor.Services;

/// <summary>
/// Service interface for creating and configuring MetricsQueryClient instances
/// </summary>
public interface IMetricsQueryClientService
{
    /// <summary>
    /// Creates a configured MetricsQueryClient instance
    /// </summary>
    /// <param name="tenant">Optional tenant ID for authentication</param>
    /// <param name="retryPolicy">Optional retry policy configuration</param>
    /// <returns>A configured MetricsQueryClient instance</returns>
    Task<MetricsQueryClient> CreateClientAsync(string? tenant = null, RetryPolicyOptions? retryPolicy = null);
}
