// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Server.Commands.ToolLoading;
using AzureMcp.Areas.Server.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Areas.Server.Commands.Runtime;


public sealed class McpRuntime : IMcpRuntime
{
    private readonly IToolLoader _toolLoader;
    private readonly IOptions<ServiceStartOptions> _options;
    private readonly ILogger<McpRuntime> _logger;

    public McpRuntime(
        IToolLoader toolLoader,
        IOptions<ServiceStartOptions> options,
        ILogger<McpRuntime> logger)
    {
        _toolLoader = toolLoader ?? throw new ArgumentNullException(nameof(toolLoader));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _logger.LogInformation("McpRuntime initialized with tool loader of type {ToolLoaderType}.", _toolLoader.GetType().Name);
        _logger.LogInformation("ReadOnly mode is set to {ReadOnly}.", _options.Value.ReadOnly ?? false);
        _logger.LogInformation("Namespace is set to {Namespace}.", string.Join(",", _options.Value.Service ?? Array.Empty<string>()));
    }

    public ValueTask<CallToolResult> CallToolHandler(RequestContext<CallToolRequestParams> request, CancellationToken cancellationToken)
        => _toolLoader.CallToolHandler(request, cancellationToken);

    public ValueTask<ListToolsResult> ListToolsHandler(RequestContext<ListToolsRequestParams> request, CancellationToken cancellationToken)
        => _toolLoader.ListToolsHandler(request, cancellationToken);
}
