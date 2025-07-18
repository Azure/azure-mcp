// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Server.Commands.Discovery;
using AzureMcp.Areas.Server.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Areas.Server.Commands.ToolLoading;

/// <summary>
/// RegistryToolLoader is a tool loader that retrieves tools from a registry.
/// Tools are loaded from each MCP server and exposed through the MCP server.
/// It handles tool call proxying and provides a unified interface for tool operations.
/// </summary>
public sealed class RegistryToolLoader(
    IMcpDiscoveryStrategy discoveryStrategy,
    IOptions<ServiceStartOptions> options,
    ILogger<RegistryToolLoader> logger) : IToolLoader
{
    private readonly IMcpDiscoveryStrategy _serverDiscoveryStrategy = discoveryStrategy;
    private readonly IOptions<ServiceStartOptions> _options = options;
    private readonly ILogger<RegistryToolLoader> _logger = logger;
    private Dictionary<string, IMcpClient> _toolClientMap = new();
    private readonly SemaphoreSlim _initializationSemaphore = new(1, 1);
    private bool _isInitialized = false;

    /// <summary>
    /// Gets or sets the client options used when creating MCP clients.
    /// </summary>
    public McpClientOptions ClientOptions { get; set; } = new McpClientOptions();

    private bool ReadOnly
    {
        get => _options.Value.ReadOnly ?? false;
    }

    /// <summary>
    /// Lists all tools available from registered MCP servers.
    /// </summary>
    /// <param name="request">The request context containing parameters and metadata.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A result containing the list of available tools.</returns>
    public async ValueTask<ListToolsResult> ListToolsHandler(RequestContext<ListToolsRequestParams> request, CancellationToken cancellationToken)
    {
        await InitializeAsync(cancellationToken);

        var allToolsResponse = new ListToolsResult
        {
            Tools = new List<Tool>()
        };

        var serverList = await _serverDiscoveryStrategy.DiscoverServersAsync();

        foreach (var server in serverList)
        {
            var serverMetadata = server.CreateMetadata();
            IMcpClient? mcpClient;
            try
            {
                mcpClient = await _serverDiscoveryStrategy.GetOrCreateClientAsync(serverMetadata.Name, ClientOptions);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Failed to create client for provider {ProviderName}: {Error}", serverMetadata.Name, ex.Message);
                continue;
            }

            if (mcpClient == null)
            {
                _logger.LogWarning("Failed to get MCP client for provider {ProviderName}.", serverMetadata.Name);
                continue;
            }

            var toolsResponse = await mcpClient.ListToolsAsync(cancellationToken: cancellationToken);
            var filteredTools = toolsResponse
                .Select(t => t.ProtocolTool)
                .Where(t => !ReadOnly || (t.Annotations?.ReadOnlyHint == true));

            foreach (var tool in filteredTools)
            {
                allToolsResponse.Tools.Add(tool);
            }
        }

        return allToolsResponse;
    }

    /// <summary>
    /// Handles tool calls by routing them to the appropriate MCP client.
    /// </summary>
    /// <param name="request">The request context containing parameters and metadata.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The result of the tool call operation.</returns>
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

        // Initialize the tool client map if not already done
        await InitializeAsync(cancellationToken);

        if (!_toolClientMap.TryGetValue(request.Params.Name, out var mcpClient) || mcpClient == null)
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

        var parameters = TransformArgumentsToDictionary(request.Params.Arguments);
        return await mcpClient.CallToolAsync(request.Params.Name, parameters, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Transforms tool call arguments to a parameters dictionary.
    /// </summary>
    /// <param name="args">The arguments to transform to parameters.</param>
    /// <returns>A dictionary of parameter names and values.</returns>
    private static Dictionary<string, object?> TransformArgumentsToDictionary(IReadOnlyDictionary<string, JsonElement>? args)
    {
        if (args == null)
        {
            return [];
        }

        var parameters = new Dictionary<string, object?>();
        foreach (var kvp in args)
        {
            // For simple types, extract the value directly
            // For complex types, keep as JsonElement (which MCP client can handle)
            parameters[kvp.Key] = kvp.Value.ValueKind switch
            {
                JsonValueKind.String => kvp.Value.GetString(),
                JsonValueKind.Number when kvp.Value.TryGetInt32(out var intValue) => intValue,
                JsonValueKind.Number when kvp.Value.TryGetInt64(out var longValue) => longValue,
                JsonValueKind.Number when kvp.Value.TryGetDouble(out var doubleValue) => doubleValue,
                JsonValueKind.True => true,
                JsonValueKind.False => false,
                JsonValueKind.Null => null,
                _ => kvp.Value // Keep as JsonElement for objects/arrays
            };
        }

        return parameters;
    }

    /// <summary>
    /// Initializes the tool client map by discovering servers and populating tools.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (_isInitialized)
        {
            return;
        }

        await _initializationSemaphore.WaitAsync(cancellationToken);
        try
        {
            // Double-check pattern: verify we're still not initialized after acquiring the lock
            if (_isInitialized)
            {
                return;
            }

            var serverList = await _serverDiscoveryStrategy.DiscoverServersAsync();

            foreach (var server in serverList)
            {
                var serverMetadata = server.CreateMetadata();
                IMcpClient? mcpClient;
                try
                {
                    mcpClient = await _serverDiscoveryStrategy.GetOrCreateClientAsync(serverMetadata.Name, ClientOptions);
                }
                catch (InvalidOperationException ex)
                {
                    _logger.LogWarning("Failed to create client for provider {ProviderName}: {Error}", serverMetadata.Name, ex.Message);
                    continue;
                }

                if (mcpClient == null)
                {
                    _logger.LogWarning("Failed to get MCP client for provider {ProviderName}.", serverMetadata.Name);
                    continue;
                }

                var toolsResponse = await mcpClient.ListToolsAsync(cancellationToken: cancellationToken);
                var filteredTools = toolsResponse
                    .Select(t => t.ProtocolTool)
                    .Where(t => !ReadOnly || (t.Annotations?.ReadOnlyHint == true));

                foreach (var tool in filteredTools)
                {
                    _toolClientMap[tool.Name] = mcpClient;
                }
            }

            _isInitialized = true;
        }
        finally
        {
            _initializationSemaphore.Release();
        }
    }
}
