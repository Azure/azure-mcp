// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Areas.Server.Commands.ToolLoading;

/// <summary>
/// Base class for tool loaders that provides common functionality including disposal patterns.
/// </summary>
/// <param name="logger">Logger instance for this tool loader.</param>
public abstract class BaseToolLoader(ILogger logger) : IToolLoader
{
    /// <summary>
    /// Logger instance for this tool loader.
    /// </summary>
    protected readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    private bool _disposed = false;

    /// <summary>
    /// Handles requests to list all tools available in the MCP server.
    /// </summary>
    /// <param name="request">The request context containing metadata and parameters.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A result containing the list of available tools.</returns>
    public abstract ValueTask<ListToolsResult> ListToolsHandler(RequestContext<ListToolsRequestParams> request, CancellationToken cancellationToken);

    /// <summary>
    /// Handles requests to call a specific tool with the provided parameters.
    /// </summary>
    /// <param name="request">The request context containing the tool name and parameters.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A result containing the output of the tool invocation.</returns>
    public abstract ValueTask<CallToolResult> CallToolHandler(RequestContext<CallToolRequestParams> request, CancellationToken cancellationToken);

    /// <summary>
    /// Disposes resources owned by this tool loader with double disposal protection.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return;
        }

        try
        {
            await DisposeAsyncCore();
        }
        catch (Exception ex)
        {
            // Log disposal failures but don't throw - we want to ensure cleanup continues
            // Individual disposal errors shouldn't stop the overall disposal process
            _logger.LogError(ex, "Error occurred while disposing tool loader {LoaderType}. Some resources may not have been properly disposed.", GetType().Name);
        }
        finally
        {
            _disposed = true;
        }
    }

    /// <summary>
    /// Override this method in derived classes to implement disposal logic.
    /// This method is called exactly once during disposal.
    /// </summary>
    /// <returns>A task representing the asynchronous disposal operation.</returns>
    protected virtual ValueTask DisposeAsyncCore()
    {
        // Default implementation does nothing
        return ValueTask.CompletedTask;
    }
}
