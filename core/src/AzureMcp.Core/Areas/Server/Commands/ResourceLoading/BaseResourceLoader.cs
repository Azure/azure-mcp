// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Core.Areas.Server.Commands.ResourceLoading;

/// <summary>
/// Base implementation of the IResourceLoader interface providing common functionality.
/// </summary>
public abstract class BaseResourceLoader(ILogger logger) : IResourceLoader
{
    protected readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    /// <summary>
    /// Handles requests to list all resources available in the MCP server.
    /// </summary>
    public abstract ValueTask<ListResourcesResult> ListResourcesHandler(RequestContext<ListResourcesRequestParams> request, CancellationToken cancellationToken);

    /// <summary>
    /// Handles requests to read the contents of a specific resource.
    /// </summary>
    public abstract ValueTask<ReadResourceResult> ReadResourceHandler(RequestContext<ReadResourceRequestParams> request, CancellationToken cancellationToken);

    /// <summary>
    /// Disposes the resource loader and cleans up any subscriptions.
    /// </summary>
    public virtual ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }
}
