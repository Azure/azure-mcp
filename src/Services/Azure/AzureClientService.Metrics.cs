// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.Monitor.Query;

namespace AzureMcp.Services.Azure;

public partial class AzureClientService
{
    /// <summary>
    /// Creates a MetricsQueryClient with the specified token credential and options
    /// </summary>
    /// <param name="tokenCredential">The token credential to use for authentication</param>
    /// <param name="options">The metrics query client options including retry policy</param>
    /// <returns>A MetricsQueryClient instance</returns>
    public virtual MetricsQueryClient GetMetricsQueryClient(TokenCredential tokenCredential, MetricsQueryClientOptions options)
    {
        return new MetricsQueryClient(tokenCredential, options);
    }
}