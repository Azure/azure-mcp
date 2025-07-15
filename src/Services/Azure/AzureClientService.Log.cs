// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.Monitor.Query;

namespace AzureMcp.Services.Azure;

public partial class AzureClientService
{
    /// <summary>
    /// Creates a LogsQueryClient with the specified token credential and options
    /// </summary>
    /// <param name="tokenCredential">The token credential to use for authentication</param>
    /// <param name="options">The logs query client options including retry policy</param>
    /// <returns>A LogsQueryClient instance</returns>
    public virtual LogsQueryClient GetLogsQueryClient(TokenCredential tokenCredential, LogsQueryClientOptions options)
    {
        return new LogsQueryClient(tokenCredential, options);
    }
}