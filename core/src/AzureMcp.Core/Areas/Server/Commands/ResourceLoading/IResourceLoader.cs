// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using ModelContextProtocol.Protocol;

namespace AzureMcp.Core.Areas.Server.Commands.ResourceLoading;

/// <summary>
/// Defines the interface for resource loaders in the MCP server.
/// Resource loaders are responsible for discovering, listing, and reading resources.
/// </summary>
public interface IResourceLoader : IAsyncDisposable
{
    /// <summary>
    /// Handles requests to list all resources available in the MCP server.
    /// </summary>
    /// <param name="request">The request context containing metadata and parameters.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A result containing the list of available resources.</returns>
    ValueTask<ListResourcesResult> ListResourcesHandler(RequestContext<ListResourcesRequestParams> request, CancellationToken cancellationToken);

    /// <summary>
    /// Handles requests to read the contents of a specific resource.
    /// </summary>
    /// <param name="request">The request context containing the resource URI to read.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A result containing the resource contents.</returns>
    ValueTask<ReadResourceResult> ReadResourceHandler(RequestContext<ReadResourceRequestParams> request, CancellationToken cancellationToken);
}
