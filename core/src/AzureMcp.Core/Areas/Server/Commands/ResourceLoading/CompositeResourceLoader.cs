// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Core.Areas.Server.Commands.ResourceLoading;

/// <summary>
/// A composite resource loader that combines multiple resource loaders.
/// Routes requests to the appropriate loader based on resource URI schemes.
/// </summary>
public sealed class CompositeResourceLoader(IEnumerable<IResourceLoader> resourceLoaders, ILogger<CompositeResourceLoader> logger) : BaseResourceLoader(logger)
{
    private readonly IList<IResourceLoader> _resourceLoaders = resourceLoaders?.ToList() ?? throw new ArgumentNullException(nameof(resourceLoaders));

    /// <summary>
    /// Lists all resources from all registered resource loaders.
    /// </summary>
    public override async ValueTask<ListResourcesResult> ListResourcesHandler(RequestContext<ListResourcesRequestParams> request, CancellationToken cancellationToken)
    {
        var allResources = new List<Resource>();
        var nextCursor = request?.Params?.Cursor;

        foreach (var loader in _resourceLoaders)
        {
            try
            {
                var result = await loader.ListResourcesHandler(request!, cancellationToken);
                if (result?.Resources != null)
                {
                    allResources.AddRange(result.Resources);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Resource loader {LoaderType} failed to list resources", loader.GetType().Name);
            }
        }

        return new ListResourcesResult
        {
            Resources = allResources,
            NextCursor = nextCursor
        };
    }

    /// <summary>
    /// Reads a resource by delegating to the appropriate resource loader based on URI scheme.
    /// </summary>
    public override async ValueTask<ReadResourceResult> ReadResourceHandler(RequestContext<ReadResourceRequestParams> request, CancellationToken cancellationToken)
    {
        if (request?.Params?.Uri == null)
        {
            throw new ArgumentException("Resource URI is required", nameof(request));
        }

        var uri = request.Params.Uri;
        _logger.LogDebug("Reading resource: {Uri}", uri);

        // Try each resource loader until one succeeds
        foreach (var loader in _resourceLoaders)
        {
            try
            {
                var result = await loader.ReadResourceHandler(request, cancellationToken);
                if (result?.Contents?.Any() == true)
                {
                    _logger.LogDebug("Resource {Uri} successfully read by {LoaderType}", uri, loader.GetType().Name);
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "Resource loader {LoaderType} failed to read resource {Uri}", loader.GetType().Name, uri);
            }
        }

        throw new InvalidOperationException($"No resource loader could handle URI: {uri}");
    }

    /// <summary>
    /// Disposes all resource loaders.
    /// </summary>
    public override async ValueTask DisposeAsync()
    {
        foreach (var loader in _resourceLoaders)
        {
            try
            {
                await loader.DisposeAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error disposing resource loader {LoaderType}", loader.GetType().Name);
            }
        }

        await base.DisposeAsync();
    }
}
