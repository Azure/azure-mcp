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
public sealed class RegistryToolLoader : IToolLoader
{
    private readonly ILogger<RegistryToolLoader> _logger;
    private readonly IMcpDiscoveryStrategy _serverDiscoveryStrategy;
    private readonly IOptions<ServiceStartOptions> _options;
    private Dictionary<string, IMcpClient> _toolClientMap = new Dictionary<string, IMcpClient>();

    public McpClientOptions ClientOptions { get; set; } = new McpClientOptions();

    public RegistryToolLoader(IMcpDiscoveryStrategy discoveryStrategy, IOptions<ServiceStartOptions> options, ILogger<RegistryToolLoader> logger)
    {
        ArgumentNullException.ThrowIfNull(discoveryStrategy);
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(logger);

        _serverDiscoveryStrategy = discoveryStrategy;
        _options = options;
        _options = options;
        _logger = logger;
    }

    private bool ReadOnly
    {
        get => _options.Value.ReadOnly ?? false;
    }

    public async ValueTask<ListToolsResult> ListToolsHandler(RequestContext<ListToolsRequestParams> request, CancellationToken cancellationToken)
    {
        var serverList = await _serverDiscoveryStrategy.DiscoverServersAsync();
        var allToolsResponse = new ListToolsResult
        {
            Tools = new List<Tool>()
        };

        foreach (var server in serverList)
        {
            var serverMetadata = server.CreateMetadata();
            var mcpClient = await _serverDiscoveryStrategy.GetOrCreateClientAsync(serverMetadata.Name, ClientOptions);
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
                _toolClientMap[tool.Name] = mcpClient;
            }
        }

        return allToolsResponse;
    }

    public async ValueTask<CallToolResponse> CallToolHandler(RequestContext<CallToolRequestParams> request, CancellationToken cancellationToken)
    {
        if (request.Params == null)
        {
            var content = new Content
            {
                Text = "Cannot call tools with null parameters.",
            };

            _logger.LogWarning(content.Text);

            return new CallToolResponse
            {
                Content = [content],
                IsError = true,
            };
        }

        var mcpClient = _toolClientMap[request.Params.Name];
        if (mcpClient == null)
        {
            var content = new Content
            {
                Text = "The tool ${request.Params.Name} was not found",
            };

            _logger.LogWarning(content.Text);

            return new CallToolResponse
            {
                Content = [content],
                IsError = true,
            };
        }

        var parameters = GetParametersDictionary(request.Params.Arguments);
        return await mcpClient.CallToolAsync(request.Params.Name, parameters, cancellationToken: cancellationToken);
    }

    private static Dictionary<string, object?> GetParametersDictionary(IReadOnlyDictionary<string, JsonElement>? args)
    {
        if (args != null && args.TryGetValue("parameters", out var parametersElem) && parametersElem.ValueKind == JsonValueKind.Object)
        {
            return JsonSerializer.Deserialize(parametersElem.GetRawText(), SingleProxyToolLoaderSerializationContext.Default.DictionaryStringObject) ?? [];
        }

        return [];
    }
}
