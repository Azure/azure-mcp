// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Threading;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Areas.Server.Commands.ToolLoading;

/// <summary>
/// A tool loader that combines multiple tool loaders into one.
/// This allows aggregating tools from multiple sources and delegating tool calls to the appropriate loader.
/// </summary>
/// <param name="toolLoaders">The collection of tool loaders to combine.</param>
/// <param name="logger">Logger for tool loading operations.</param>
public sealed class CompositeToolLoader(IEnumerable<IToolLoader> toolLoaders, ILogger<CompositeToolLoader> logger) : IToolLoader, IDisposable
{
    private readonly ILogger<CompositeToolLoader> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IEnumerable<IToolLoader> _toolLoaders = InitializeToolLoaders(toolLoaders);
    private readonly Dictionary<string, IToolLoader> _toolLoaderMap = new();
    private readonly SemaphoreSlim _initializationSemaphore = new(1, 1);

    /// <summary>
    /// Initializes the list of tool loaders, validating that at least one is provided.
    /// </summary>
    /// <param name="toolLoaders">The collection of tool loaders to initialize.</param>
    /// <returns>A list of initialized tool loaders.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the toolLoaders parameter is null.</exception>
    /// <exception cref="ArgumentException">Thrown when no tool loaders are provided.</exception>
    private static IEnumerable<IToolLoader> InitializeToolLoaders(IEnumerable<IToolLoader> toolLoaders)
    {
        ArgumentNullException.ThrowIfNull(toolLoaders);

        var loadersList = toolLoaders.ToList();

        if (loadersList.Count == 0)
        {
            throw new ArgumentException("At least one tool loader must be provided.", nameof(toolLoaders));
        }

        return loadersList;
    }

    /// <summary>
    /// Lists all tools from all tool loaders and builds a mapping of tool names to their respective loaders.
    /// </summary>
    /// <param name="request">The request context containing metadata and parameters.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A result containing the combined list of all available tools.</returns>
    /// <exception cref="InvalidOperationException">Thrown when a tool loader returns a null response.</exception>
    public async ValueTask<ListToolsResult> ListToolsHandler(RequestContext<ListToolsRequestParams> request, CancellationToken cancellationToken)
    {
        var allToolsResponse = new ListToolsResult
        {
            Tools = new List<Tool>()
        };

        foreach (var loader in _toolLoaders)
        {
            var toolsResponse = await loader.ListToolsHandler(request, cancellationToken);
            if (toolsResponse == null)
            {
                throw new InvalidOperationException("Tool loader returned null response.");
            }

            foreach (var tool in toolsResponse.Tools)
            {
                allToolsResponse.Tools.Add(tool);
                _toolLoaderMap[tool.Name] = loader;
            }
        }

        return allToolsResponse;
    }

    public async ValueTask<CallToolResult> CallToolHandler(RequestContext<CallToolRequestParams> request, CancellationToken cancellationToken)
    {
        if (request.Params == null)
        {
            var content = new TextContentBlock
            {
                Text = "Cannot call tools with null parameters.",
            };

            _logger.LogWarning(content.Text);

            return new CallToolResult
            {
                Content = [content],
                IsError = true,
            };
        }

        // Ensure tool loader map is populated before attempting tool lookup
        await EnsureToolLoaderMapInitializedAsync(request, cancellationToken);

        if (!_toolLoaderMap.TryGetValue(request.Params.Name, out var toolCaller))
        {
            var content = new TextContentBlock
            {
                Text = $"The tool {request.Params.Name} was not found",
            };

            _logger.LogWarning(content.Text);

            return new CallToolResult
            {
                Content = [content],
                IsError = true,
            };
        }

        return await toolCaller.CallToolHandler(request, cancellationToken);
    }

    /// <summary>
    /// Ensures that the tool loader map is initialized by populating it if it's empty.
    /// This provides lazy initialization to handle cases where tool calls occur before ListToolsHandler is called.
    /// Thread-safe initialization using a semaphore to prevent race conditions.
    /// </summary>
    /// <param name="request">The request context containing metadata and parameters.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    private async ValueTask EnsureToolLoaderMapInitializedAsync(RequestContext<CallToolRequestParams> request, CancellationToken cancellationToken)
    {
        if (_toolLoaderMap.Count == 0)
        {
            await _initializationSemaphore.WaitAsync(cancellationToken);
            try
            {
                // Double-check pattern: verify the map is still empty after acquiring the lock
                if (_toolLoaderMap.Count == 0)
                {
                    // Create a dummy request for listing tools to populate the tool loader map
                    var listToolsRequest = new RequestContext<ListToolsRequestParams>(request.Server)
                    {
                        Params = new ListToolsRequestParams()
                    };

                    // Populate the tool loader map by calling ListToolsHandler internally
                    await ListToolsHandler(listToolsRequest, cancellationToken);
                }
            }
            finally
            {
                _initializationSemaphore.Release();
            }
        }
    }

    /// <summary>
    /// Disposes the semaphore used for thread-safe initialization.
    /// </summary>
    public void Dispose()
    {
        _initializationSemaphore?.Dispose();
    }
}
