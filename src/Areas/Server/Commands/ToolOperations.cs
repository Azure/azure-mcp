// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics;
using System.Reflection;
using System.Text.Json.Nodes;
using AzureMcp.Commands;
using AzureMcp.Commands.Server;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;
using ModelContextProtocol;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Areas.Server.Commands;

public class ToolOperations
{
    private readonly IServiceProvider _serviceProvider;
    private readonly CommandFactory _commandFactory;
    private readonly ITelemetryService _telemetry;
    private IReadOnlyDictionary<string, IBaseCommand> _toolCommands;
    private readonly ILogger<ToolOperations> _logger;
    private string[]? _commandGroup = null;

    public ToolOperations(IServiceProvider serviceProvider, CommandFactory commandFactory, ITelemetryService telemetry, ILogger<ToolOperations> logger)
    {
        _serviceProvider = serviceProvider;
        _commandFactory = commandFactory;
        _telemetry = telemetry;
        _logger = logger;
        _toolCommands = _commandFactory.AllCommands;

        ToolsCapability = new ToolsCapability
        {
            CallToolHandler = OnCallTools,
            ListToolsHandler = OnListTools,
        };
    }

    public ToolsCapability ToolsCapability { get; }

    public bool ReadOnly { get; set; } = false;

    public string[]? CommandGroup
    {
        get => _commandGroup;
        set
        {
            _commandGroup = value;
            if (_commandGroup == null || _commandGroup.Length == 0 || _commandGroup.All(string.IsNullOrWhiteSpace))
            {
                _toolCommands = _commandFactory.AllCommands;
            }
            else
            {
                _toolCommands = _commandFactory.GroupCommands(_commandGroup);
            }
        }
    }
    private ValueTask<ListToolsResult> OnListTools(RequestContext<ListToolsRequestParams> requestContext, CancellationToken cancellationToken)
    {
        using var listActivity = _telemetry.StartActivity(nameof(OnListTools));

        listActivity?.AddTag("ClientName", "");

        var tools = CommandFactory.GetVisibleCommands(_toolCommands)
            .Select(kvp => GetTool(kvp.Key, kvp.Value))
            .Where(tool => !ReadOnly || tool.Annotations?.ReadOnlyHint == true)
            .ToList();

        var listToolsResult = new ListToolsResult { Tools = tools };

        _logger.LogInformation("Listing {NumberOfTools} tools.", tools.Count);

        return ValueTask.FromResult(listToolsResult);
    }

    private async ValueTask<CallToolResult> OnCallTools(RequestContext<CallToolRequestParams> parameters,
        CancellationToken cancellationToken)
    {
        using var activity = _telemetry
            .StartActivity(nameof(OnCallTools));

        AddClientInfo(activity, parameters.Server.ClientInfo);

        if (parameters.Params == null)
        {
            var content = new TextContentBlock
            {
                Text = "Cannot call tools with null parameters.",
            };

            activity?.SetStatus(ActivityStatusCode.Error);
            activity?.AddException(new ArgumentException(content.Text));

            return new CallToolResult
            {
                Content = [content],
                IsError = true,
            };
        }

        var toolName = parameters.Params.Name;

        activity?.AddTag(TelemetryConstants.ToolName, toolName);

        var command = _toolCommands.GetValueOrDefault(toolName);
        if (command == null)
        {
            var content = new TextContentBlock
            {
                Text = $"Could not find command: {toolName}",
            };

            activity?.SetStatus(ActivityStatusCode.Error)
                ?.AddException(new ArgumentException(content.Text));

            return new CallToolResult
            {
                Content = [content],
                IsError = true,
            };
        }
        var commandContext = new CommandContext(_serviceProvider);

        var realCommand = command.GetCommand();
        var commandOptions = realCommand.ParseFromDictionary(parameters.Params.Arguments);

        activity?.AddEvent(new ActivityEvent("Invoking tool"));
        _logger.LogTrace("Invoking '{Tool}'.", realCommand.Name);

        try
        {
            var commandResponse = await command.ExecuteAsync(commandContext, commandOptions);
            var jsonResponse = JsonSerializer.Serialize(commandResponse, ModelsJsonContext.Default.CommandResponse);

            if (!IsSuccessStatusCode(commandResponse.Status))
            {
                activity?.SetStatus(ActivityStatusCode.Error)
                    ?.AddException(new ToolFailedException(toolName, commandResponse.Message));
            }

            return new CallToolResult
            {
                Content =
                [
                    new TextContentBlock { Text = jsonResponse }
                ],
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred running '{Tool}'. ", realCommand.Name);

            activity?.SetStatus(ActivityStatusCode.Error)
                ?.AddException(ex);
            throw;
        }
        finally
        {
            activity?.AddEvent(new ActivityEvent("Finished invoking tool"));
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

    private void AddClientInfo(Activity? activity, Implementation? clientInfo)
    {
        if (activity == null || clientInfo == null)
        {
            return;
        }

        activity
            .AddTag(TelemetryConstants.ClientName, clientInfo.Name)
            .AddTag(TelemetryConstants.ClientVersion, clientInfo.Version);
    }

    private static bool IsSuccessStatusCode(int statusCode) => statusCode >= 200 && statusCode <= 299;
}
