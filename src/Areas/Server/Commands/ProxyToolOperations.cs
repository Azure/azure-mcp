// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.Server.Commands.Tools;
using AzureMcp.Commands.Server.Tools;
using Json.Schema;
using Microsoft.Extensions.Logging;
using ModelContextProtocol;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Commands.Server;

[JsonSerializable(typeof(JsonSchema))]
[JsonSerializable(typeof(Tool))]
[JsonSerializable(typeof(List<Tool>))]
[JsonSerializable(typeof(Dictionary<string, object?>))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    WriteIndented = true
)]
internal partial class ProxyToolOperationsSerializationContext : JsonSerializerContext
{
}

public class ProxyToolOperations(IMcpClientService mcpClientService, ILogger<ProxyToolOperations> logger)
{
    private readonly IMcpClientService _mcpClientService = mcpClientService;
    private readonly ILogger<ProxyToolOperations> _logger = logger;
    private readonly Dictionary<string, List<Tool>> _cachedToolLists = new(StringComparer.OrdinalIgnoreCase);
    private static readonly string ToolCallProxySchemaJson = JsonSerializer.Serialize(ToolCallProxySchema, ProxyToolOperationsSerializationContext.Default.JsonSchema);

    public bool ReadOnly { get; set; } = false;

    private static readonly JsonSchema ToolSchema = new JsonSchemaBuilder()
        .Type(SchemaValueType.Object)
        .Properties(
            ("intent", new JsonSchemaBuilder()
                .Type(SchemaValueType.String)
                .Required()
                .Description("The intent of the operation to perform.")
            ),
            ("command", new JsonSchemaBuilder()
                .Type(SchemaValueType.String)
                .Description("The sub command to invoke to within the tool.")
            ),
            ("parameters", new JsonSchemaBuilder()
                .Type(SchemaValueType.Object)
                .Description("Wrapped set of arguments to pass to the sub command.")
            ),
            ("learn", new JsonSchemaBuilder()
                .Type(SchemaValueType.Boolean)
                .Description("When set to true returns a list of available sub commands and their parameters.")
                .Default(false)
            )
        )
        .AdditionalProperties(false)
        .Build();

    private static readonly JsonSchema ToolCallProxySchema = new JsonSchemaBuilder()
        .Type(SchemaValueType.Object)
        .Properties(
            ("tool", new JsonSchemaBuilder()
                .Type(SchemaValueType.String)
                .Description("The name of the tool to call.")
            ),
            ("parameters", new JsonSchemaBuilder()
                .Type(SchemaValueType.Object)
                .Description("A key/value pair of parameters names nad values to pass to the tool call command.")
            )
        )
        .AdditionalProperties(false)
        .Build();

    public ValueTask<ListToolsResult> ListToolsHandler(RequestContext<ListToolsRequestParams> request, CancellationToken cancellationToken)
    {
        var tools = _mcpClientService.ListProviderMetadata()
            .Select(metadata => new Tool
            {
                Name = metadata.Name,
                Description = metadata.Description + """
                    This tool is a hierarchical MCP command router.
                    Sub commands are routed to MCP servers that require specific fields inside the "parameters" object.
                    To invoke a command, set "command" and wrap its args in "parameters".
                    Set "learn=true" to discover available sub commands.
                    """,
                InputSchema = JsonSerializer.SerializeToElement(ToolSchema, ProxyToolOperationsSerializationContext.Default.JsonSchema),
            });

        var listToolsResult = new ListToolsResult
        {
            Tools = [.. tools],
        };

        return ValueTask.FromResult(listToolsResult);
    }

    public async ValueTask<CallToolResponse> CallToolHandler(RequestContext<CallToolRequestParams> request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Params?.Name))
        {
            throw new ArgumentNullException(nameof(request.Params.Name), "Tool name cannot be null or empty.");
        }

        string tool = request.Params.Name;
        var args = request.Params?.Arguments;
        string? intent = null;
        string? command = null;
        bool learn = false;

        if (args != null)
        {
            if (args.TryGetValue("intent", out var intentElem) && intentElem.ValueKind == JsonValueKind.String)
            {
                intent = intentElem.GetString();
            }
            if (args.TryGetValue("learn", out var learnElem) && learnElem.ValueKind == JsonValueKind.True)
            {
                learn = true;
            }
            if (args.TryGetValue("command", out var commandElem) && commandElem.ValueKind == JsonValueKind.String)
            {
                command = commandElem.GetString();
            }
        }

        if (!learn && !string.IsNullOrEmpty(intent) && string.IsNullOrEmpty(command))
        {
            learn = true;
        }

        if (learn && string.IsNullOrEmpty(command))
        {
            return await InvokeToolLearn(request, intent ?? "", tool, cancellationToken);
        }
        else if (!string.IsNullOrEmpty(tool) && !string.IsNullOrEmpty(command))
        {
            var toolParams = GetParametersDictionary(args);
            return await InvokeChildToolAsync(request, intent ?? "", tool, command, toolParams, cancellationToken);
        }

        return new CallToolResponse
        {
            Content =
            [
                new Content {
                    Type = "text",
                    Text = """
                        The "command" parameters are required when not learning
                        Run again with the "learn" argument to get a list of available tools and their parameters.
                        To learn about a specific tool, use the "tool" argument with the name of the tool.
                    """
                }
            ]
        };
    }

    private async Task<CallToolResponse> InvokeChildToolAsync(RequestContext<CallToolRequestParams> request, string? intent, string tool, string command, Dictionary<string, object?> parameters, CancellationToken cancellationToken)
    {
        IMcpClient? client;

        try
        {
            var clientOptions = CreateClientOptions(request.Server);
            client = await _mcpClientService.GetProviderClientAsync(tool, clientOptions);
            if (client == null)
            {
                _logger.LogError("Failed to get provider client for tool: {Tool}", tool);
                return await InvokeToolLearn(request, intent, tool, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception thrown while getting provider client for tool: {Tool}", tool);
            return await InvokeToolLearn(request, intent, tool, cancellationToken);
        }

        try
        {
            var availableTools = await GetChildToolListAsync(request, tool);

            // When the specified command is not available, we try to learn about the tool's capabilities
            // and infer the command and parameters from the users intent.
            if (!availableTools.Any(t => string.Equals(t.Name, command, StringComparison.OrdinalIgnoreCase)))
            {
                _logger.LogWarning("Tool {Tool} does not have a command {Command}.", tool, command);
                if (string.IsNullOrWhiteSpace(intent))
                {
                    return await InvokeToolLearn(request, intent, tool, cancellationToken);
                }

                var samplingResult = await GetCommandAndParametersFromIntentAsync(request, intent, tool, availableTools, cancellationToken);
                if (string.IsNullOrWhiteSpace(samplingResult.commandName))
                {
                    return await InvokeToolLearn(request, intent ?? "", tool, cancellationToken);
                }

                command = samplingResult.commandName;
                parameters = samplingResult.parameters;
            }

            // At this point we should always have a valid command (child tool) call to invoke.
            await NotifyProgressAsync(request, $"Calling {tool} {command}...", cancellationToken);
            var toolCallResponse = await client.CallToolAsync(command, parameters, cancellationToken: cancellationToken);
            if (toolCallResponse.IsError)
            {
                _logger.LogWarning("Tool {Tool} command {Command} returned an error.", tool, command);
            }

            foreach (var content in toolCallResponse.Content)
            {
                if (content.Type == "text")
                {
                    if (string.IsNullOrWhiteSpace(content.Text))
                    {
                        continue;
                    }

                    if (content.Text.Contains("Missing required options", StringComparison.OrdinalIgnoreCase))
                    {
                        var childToolSpecJson = await GetChildToolJsonAsync(request, tool, command);

                        _logger.LogWarning("Tool {Tool} command {Command} requires additional parameters.", tool, command);
                        var finalResponse = new CallToolResponse
                        {
                            Content =
                            [
                                new Content {
                                    Type = "text",
                                    Text = $"""
                                        The '{command}' command is missing required parameters.

                                        - Review the following command spec and identify the required arguments from the input schema.
                                        - Omit any arguments that are not required or do not apply to your use case.
                                        - Wrap all command arguments into the root "parameters" argument.
                                        - If required data is missing infer the data from your context or prompt the user as needed.
                                        - Run the tool again with the "command" and root "parameters" object.

                                        Command Spec:
                                        {childToolSpecJson}
                                        """
                                }
                            ]
                        };

                        finalResponse.Content.AddRange(toolCallResponse.Content);
                        return finalResponse;
                    }
                }
            }

            return toolCallResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception thrown while calling tool: {Tool}, command: {Command}", tool, command);
            return new CallToolResponse
            {
                Content =
                [
                    new Content {
                        Type = "text",
                        Text = $"""
                            There was an error finding or calling tool and command.
                            Failed to call tool: {tool}, command: {command}
                            Error: {ex.Message}

                            Run again with the "learn=true" to get a list of available commands and their parameters.
                            """
                    }
                ]
            };
        }
    }

    private async Task<CallToolResponse> InvokeToolLearn(RequestContext<CallToolRequestParams> request, string? intent, string tool, CancellationToken cancellationToken)
    {
        var toolsJson = await GetChildToolListJsonAsync(request, tool);

        var learnResponse = new CallToolResponse
        {
            Content =
            [
                new Content {
                    Type = "text",
                    Text = $"""
                        Here are the available command and their parameters for '{tool}' tool.
                        If you do not find a suitable command, run again with the "learn=true" to get a list of available commands and their parameters.
                        Next, identify the command you want to execute and run again with the "command" and "parameters" arguments.

                        {toolsJson}
                        """
                }
            ]
        };
        var response = learnResponse;
        if (SupportsSampling(request.Server) && !string.IsNullOrWhiteSpace(intent))
        {
            var availableTools = await GetChildToolListAsync(request, tool);
            (string? commandName, Dictionary<string, object?> parameters) = await GetCommandAndParametersFromIntentAsync(request, intent, tool, availableTools, cancellationToken);
            if (commandName != null)
            {
                response = await InvokeChildToolAsync(request, intent, tool, commandName, parameters, cancellationToken);
            }
        }
        return response;
    }

    /// <summary>
    /// Gets the available tools from the child MCP server and caches the result as JSON.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="tool"></param>
    /// <returns></returns>
    private async Task<List<Tool>> GetChildToolListAsync(RequestContext<CallToolRequestParams> request, string tool)
    {
        if (_cachedToolLists.TryGetValue(tool, out var cachedList))
        {
            return cachedList;
        }

        var clientOptions = CreateClientOptions(request.Server);
        var client = await _mcpClientService.GetProviderClientAsync(tool, clientOptions);
        if (client == null)
        {
            return [];
        }

        var listTools = await client.ListToolsAsync();
        if (listTools == null)
        {
            _logger.LogWarning("No tools found for tool: {Tool}", tool);
            return [];
        }

        var list = listTools
            .Select(t => t.ProtocolTool)
            .Where(t => !ReadOnly || (t.Annotations?.ReadOnlyHint == true))
            .ToList();

        _cachedToolLists[tool] = list;
        return list;
    }

    private async Task<string> GetChildToolListJsonAsync(RequestContext<CallToolRequestParams> request, string tool)
    {
        var listTools = await GetChildToolListAsync(request, tool);
        return JsonSerializer.Serialize(listTools, AzureProxyToolSerializationContext.Default.ListTool);
    }

    private async Task<Tool> GetChildToolAsync(RequestContext<CallToolRequestParams> request, string toolName, string commandName)
    {
        var tools = await GetChildToolListAsync(request, toolName);
        return tools.First(t => string.Equals(t.Name, commandName, StringComparison.OrdinalIgnoreCase));
    }

    private async Task<string> GetChildToolJsonAsync(RequestContext<CallToolRequestParams> request, string toolName, string commandName)
    {
        var tool = await GetChildToolAsync(request, toolName, commandName);
        return JsonSerializer.Serialize(tool, ProxyToolOperationsSerializationContext.Default.Tool);
    }

    private static bool SupportsSampling(IMcpServer server)
    {
        return server?.ClientCapabilities?.Sampling != null;
    }

    private static async Task NotifyProgressAsync(RequestContext<CallToolRequestParams> request, string message, CancellationToken cancellationToken)
    {
        var progressToken = request.Params?.Meta?.ProgressToken;
        if (progressToken == null)
        {
            return;
        }

        await request.Server.NotifyProgressAsync(progressToken.Value,
            new ProgressNotificationValue
            {
                Progress = 0f,
                Message = message,
            }, cancellationToken);
    }
    private async Task<(string? commandName, Dictionary<string, object?> parameters)> GetCommandAndParametersFromIntentAsync(
        RequestContext<CallToolRequestParams> request,
        string intent,
        string tool,
        List<Tool> availableTools,
        CancellationToken cancellationToken)
    {
        await NotifyProgressAsync(request, $"Learning about {tool} capabilities...", cancellationToken);

        var toolParams = GetParametersDictionary(request.Params?.Arguments);
        var toolParamsJson = JsonSerializer.Serialize(toolParams, ProxyToolOperationsSerializationContext.Default.DictionaryStringObject);
        var availableToolsJson = JsonSerializer.Serialize(availableTools, ProxyToolOperationsSerializationContext.Default.ListTool);

        var samplingRequest = new CreateMessageRequestParams
        {
            Messages = [
                new SamplingMessage
                {
                    Role = Role.Assistant,
                    Content = new Content{
                        Type = "text",
                        Text = $"""
                            This is a list of available commands for the {tool} server.

                            Your task:
                            - Select the single command that best matches the user's intent.
                            - Return a valid JSON object that matches the provided result schema.
                            - Map the user's intent and known parameters to the command's input schema, ensuring parameter names and types match the schema exactly (no extra or missing parameters).
                            - Only include parameters that are defined in the selected command's input schema.
                            - Do not guess or invent parameters.
                            - If no command matches, return JSON schema with "Unknown" tool name.

                            Result Schema:
                            {ToolCallProxySchemaJson}                            Intent:
                            {intent ?? "No specific intent provided"}

                            Known Parameters:
                            {toolParamsJson}

                            Available Commands:
                            {availableToolsJson}
                            """
                    }
                }
            ],
        };
        try
        {
            var samplingResponse = await request.Server.RequestSamplingAsync(samplingRequest, cancellationToken);
            var toolCallJson = samplingResponse.Content.Text?.Trim();
            string? commandName = null;
            Dictionary<string, object?> parameters = [];
            if (!string.IsNullOrEmpty(toolCallJson))
            {
                using var doc = JsonDocument.Parse(toolCallJson);
                var root = doc.RootElement;
                if (root.TryGetProperty("tool", out var toolProp) && toolProp.ValueKind == JsonValueKind.String)
                {
                    commandName = toolProp.GetString();
                }
                if (root.TryGetProperty("parameters", out var paramsProp) && paramsProp.ValueKind == JsonValueKind.Object)
                {
                    parameters = JsonSerializer.Deserialize(paramsProp.GetRawText(), ProxyToolOperationsSerializationContext.Default.DictionaryStringObject) ?? [];
                }
            }
            if (commandName != null && commandName != "Unknown")
            {
                return (commandName, parameters);
            }
        }
        catch
        {
            _logger.LogError("Failed to get command and parameters from intent: {Intent} for tool: {Tool}", intent, tool);
        }

        return (null, new Dictionary<string, object?>());
    }

    private static Dictionary<string, object?> GetParametersDictionary(IReadOnlyDictionary<string, JsonElement>? args)
    {
        if (args != null && args.TryGetValue("parameters", out var parametersElem) && parametersElem.ValueKind == JsonValueKind.Object)
        {
            return JsonSerializer.Deserialize(parametersElem.GetRawText(), ProxyToolOperationsSerializationContext.Default.DictionaryStringObject) ?? [];
        }

        return [];
    }

    private McpClientOptions CreateClientOptions(IMcpServer server)
    {
        var clientOptions = new McpClientOptions
        {
            ClientInfo = server.ClientInfo,
            Capabilities = new ClientCapabilities(),
        };

        return clientOptions;
    }
}
