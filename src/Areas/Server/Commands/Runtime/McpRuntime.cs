// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Server.Commands.ToolLoading;
using AzureMcp.Options.Server;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Areas.Server.Commands.Runtime;

public sealed class McpRuntime : IMcpRuntime
{
    private IToolLoader _toolLoader;
    private readonly IOptions<ServiceStartOptions> _options;
    private ILogger<McpRuntime> _logger;

    public McpRuntime(IToolLoader toolLoader, IOptions<ServiceStartOptions> options, ILogger<McpRuntime> logger)
    {
        ArgumentNullException.ThrowIfNull(toolLoader, nameof(toolLoader));
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        _toolLoader = toolLoader;
        _options = options;
        _logger = logger;

        _logger.LogInformation("McpRuntime initialized with tool loader of type {ToolLoaderType}.", _toolLoader.GetType().Name);
        _logger.LogInformation("ReadOnly mode is set to {ReadOnly}.", _options.Value.ReadOnly ?? false);
        _logger.LogInformation("Namespace is set to {Namespace}.", _options.Value.Service ?? "");
    }
    public ValueTask<CallToolResponse> CallToolHandler(RequestContext<CallToolRequestParams> request, CancellationToken cancellationToken)
    {
        return _toolLoader.CallToolHandler(request, cancellationToken);
    }

    public ValueTask<ListToolsResult> ListToolsHandler(RequestContext<ListToolsRequestParams> request, CancellationToken cancellationToken)
    {
        return _toolLoader.ListToolsHandler(request, cancellationToken);
    }
}
