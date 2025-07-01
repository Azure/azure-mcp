// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Reflection;
using System.Text.Json.Nodes;
using AzureMcp.Areas.Server.Options;
using AzureMcp.Commands;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ModelContextProtocol;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Areas.Server.Commands.ToolLoading;

public sealed class CommandFactoryToolLoader(
    IServiceProvider serviceProvider,
    CommandFactory commandFactory,
    IOptions<ServiceStartOptions> options,
    ILogger<CommandFactoryToolLoader> logger) : IToolLoader
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly CommandFactory _commandFactory = commandFactory;
    private readonly IOptions<ServiceStartOptions> _options = options;
    private IReadOnlyDictionary<string, IBaseCommand> _toolCommands =
        (options.Value.Service == null || options.Value.Service.Length == 0)
            ? commandFactory.AllCommands
            : commandFactory.GroupCommands(options.Value.Service);
    private readonly ILogger<CommandFactoryToolLoader> _logger = logger;

    private bool ReadOnly
    {
        get => _options.Value.ReadOnly ?? false;
    }

    private string[]? Namespaces
    {
        get => _options.Value.Service;
    }

    public ValueTask<ListToolsResult> ListToolsHandler(RequestContext<ListToolsRequestParams> request, CancellationToken cancellationToken)
    {
        var tools = CommandFactory.GetVisibleCommands(_toolCommands)
            .Select(kvp => GetTool(kvp.Key, kvp.Value))
            .Where(tool => !ReadOnly || (tool.Annotations?.ReadOnlyHint == true))
            .ToList();

        var listToolsResult = new ListToolsResult { Tools = tools };

        _logger.LogInformation("Listing {NumberOfTools} tools.", tools.Count);

        return ValueTask.FromResult(listToolsResult);
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

        var command = _toolCommands.GetValueOrDefault(request.Params.Name);
        if (command == null)
        {
            var content = new TextContentBlock
            {
                Text = $"Could not find command: {request.Params.Name}",
            };

            _logger.LogWarning(content.Text);

            return new CallToolResult
            {
                Content = [content],
                IsError = true,
            };
        }
        var commandContext = new CommandContext(_serviceProvider);

        var realCommand = command.GetCommand();
        var commandOptions = realCommand.ParseFromDictionary(request.Params.Arguments);

        _logger.LogTrace("Invoking '{Tool}'.", realCommand.Name);

        try
        {
            var commandResponse = await command.ExecuteAsync(commandContext, commandOptions);
            var jsonResponse = JsonSerializer.Serialize(commandResponse, ModelsJsonContext.Default.CommandResponse);

            return new CallToolResult
            {
                Content = [
                    new TextContentBlock {
                        Text = jsonResponse
                    }
                ],
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred running '{Tool}'. ", realCommand.Name);

            throw;
        }
        finally
        {
            _logger.LogTrace("Finished executing '{Tool}'.", realCommand.Name);
        }
    }

    private static Tool GetTool(string fullName, IBaseCommand command)
    {
        var underlyingCommand = command.GetCommand();
        var tool = new Tool
        {
            Name = fullName,
            Description = underlyingCommand.Description,
        };

        // Get the ExecuteAsync method info to check for McpServerToolAttribute
        var executeAsyncMethod = command.GetType().GetMethod(nameof(IBaseCommand.ExecuteAsync));
        if (executeAsyncMethod?.GetCustomAttribute<McpServerToolAttribute>() is { } mcpServerToolAttr)
        {
            tool.Annotations = new ToolAnnotations()
            {
                DestructiveHint = mcpServerToolAttr.Destructive,
                IdempotentHint = mcpServerToolAttr.Idempotent,
                OpenWorldHint = mcpServerToolAttr.OpenWorld,
                ReadOnlyHint = mcpServerToolAttr.ReadOnly,
                Title = mcpServerToolAttr.Title,
            };
        }

        var options = command.GetCommand().Options;

        var schema = new JsonObject
        {
            ["type"] = "object"
        };

        if (options != null && options.Count > 0)
        {
            var arguments = new JsonObject();
            foreach (var option in options)
            {
                arguments.Add(option.Name, new JsonObject()
                {
                    ["type"] = option.ValueType.ToJsonType(),
                    ["description"] = option.Description,
                });
            }

            schema["properties"] = arguments;
            schema["required"] = new JsonArray(options.Where(p => p.IsRequired).Select(p => (JsonNode)p.Name).ToArray());
        }
        else
        {
            var arguments = new JsonObject();
            schema["properties"] = arguments;
        }

        var newOptions = new JsonSerializerOptions(McpJsonUtilities.DefaultOptions);

        tool.InputSchema = JsonSerializer.SerializeToElement(schema, new JsonSourceGenerationContext(newOptions).JsonNode);

        return tool;
    }
}
