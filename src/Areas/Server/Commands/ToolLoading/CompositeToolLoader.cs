// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Areas.Server.Commands.ToolLoading;

public sealed class CompositeToolLoader(IEnumerable<IToolLoader> toolLoaders, ILogger<CompositeToolLoader> logger) : IToolLoader
{
    private readonly ILogger<CompositeToolLoader> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IEnumerable<IToolLoader> _toolLoaders = InitializeToolLoaders(toolLoaders);
    private readonly Dictionary<string, IToolLoader> _toolLoaderMap = new();

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

        if (!_toolLoaderMap.TryGetValue(request.Params.Name, out var toolCaller))
        {
            var content = new TextContentBlock
            {
                Text = "The tool ${request.Params.Name} was not found",
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
}
