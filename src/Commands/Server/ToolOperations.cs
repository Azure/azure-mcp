// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Reflection;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging;
using ModelContextProtocol;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Commands.Server;

public class ToolOperations
{
    private readonly IServiceProvider _serviceProvider;
    private readonly CommandFactory _commandFactory;
    private readonly ILogger<ToolOperations> _logger;

    public ToolOperations(IServiceProvider serviceProvider, CommandFactory commandFactory, ILogger<ToolOperations> logger)
    {
        _serviceProvider = serviceProvider;
        _commandFactory = commandFactory;
        _logger = logger;

        ToolsCapability = new ToolsCapability
        {
            CallToolHandler = OnCallTools,
            ListToolsHandler = OnListTools,
        };
    }

    public ToolsCapability ToolsCapability { get; }

    private ValueTask<ListToolsResult> OnListTools(RequestContext<ListToolsRequestParams> requestContext,
        CancellationToken cancellationToken)
    {
        var allCommands = _commandFactory.AllCommands;
        if (allCommands.Count == 0)
        {
            return ValueTask.FromResult(new ListToolsResult { Tools = [] });
        }

        var tools = CommandFactory.GetVisibleCommands(allCommands)
            .Select(kvp => GetTool(kvp.Key, kvp.Value))
            .ToList();

        var listToolsResult = new ListToolsResult { Tools = tools };

        _logger.LogInformation("Listing {NumberOfTools} tools.", tools.Count);

        return ValueTask.FromResult(listToolsResult);
    }

    private async ValueTask<CallToolResponse> OnCallTools(RequestContext<CallToolRequestParams> parameters,
        CancellationToken cancellationToken)
    {
        if (parameters.Params == null)
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

        var command = _commandFactory.FindCommandByName(parameters.Params.Name);
        if (command == null)
        {
            var content = new Content
            {
                Text = $"Could not find command: {parameters.Params.Name}",
            };

            _logger.LogWarning(content.Text);

            return new CallToolResponse
            {
                Content = [content],
                IsError = true,
            };
        }

        var commandContext = new CommandContext(_serviceProvider);

        var args = parameters.Params.Arguments != null
            ? string.Join(" ", parameters.Params.Arguments.Select(kvp => $"--{kvp.Key} \"{kvp.Value}\""))
            : string.Empty;
        var realCommand = command.GetCommand();
        var commandOptions = realCommand.Parse(args);

        _logger.LogTrace("Invoking '{Tool}'.", realCommand.Name);

        try
        {
            var commandResponse = await command.ExecuteAsync(commandContext, commandOptions);
            var jsonResponse = JsonSerializer.Serialize(commandResponse, ModelsJsonContext.Default.CommandResponse);

            return new CallToolResponse
            {
                Content = [
                    new Content {
                        Text = jsonResponse,
                        MimeType = "application/json" }],
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

        // Get the GetCommand method info to check for McpServerToolAttribute
        var getCommandMethod = command.GetType().GetMethod(nameof(IBaseCommand.GetCommand));
        if (getCommandMethod?.GetCustomAttribute<McpServerToolAttribute>() is { } mcpServerToolAttr)
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

        var newOptions = new JsonSerializerOptions(McpJsonUtilities.DefaultOptions);

        tool.InputSchema = JsonSerializer.SerializeToElement(schema, new JsonSourceGenerationContext(newOptions).JsonNode);

        return tool;
    }
}
