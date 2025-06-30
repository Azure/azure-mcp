// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Areas.Server.Commands.ToolLoading;

public sealed class CompositeToolLoader : IToolLoader
{
    private readonly ILogger<CompositeToolLoader> _logger;
    private readonly List<IToolLoader> _toolLoaders = new List<IToolLoader>();
    private readonly Dictionary<string, IToolLoader> _toolLoaderMap = new Dictionary<string, IToolLoader>();

    public bool ReadOnly { get; set; } = false;
    public string? Namespace { get; set; } = null;

    public CompositeToolLoader(IEnumerable<IToolLoader> toolLoaders, ILogger<CompositeToolLoader> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        if (toolLoaders == null)
        {
            throw new ArgumentNullException(nameof(toolLoaders));
        }

        _toolLoaders.AddRange(toolLoaders);
    }

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

        var toolCaller = _toolLoaderMap[request.Params.Name];
        if (toolCaller == null)
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

        return await toolCaller.CallToolHandler(request, cancellationToken);
    }
}
