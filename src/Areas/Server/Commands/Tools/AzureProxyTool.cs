// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.Server.Commands.Tools;
using Json.Schema;
using Microsoft.Extensions.Logging;
using ModelContextProtocol;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Commands.Server.Tools;

[JsonSerializable(typeof(JsonSchema))]
[JsonSerializable(typeof(ListToolsResult))]
[JsonSerializable(typeof(List<McpClientTool>))]
[JsonSerializable(typeof(List<Tool>))]
[JsonSerializable(typeof(Dictionary<string, object?>))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
    WriteIndented = true
)]
internal partial class AzureProxyToolSerializationContext : JsonSerializerContext
{
}

/// <summary>
/// Initializes a new instance of the <see cref="AzureProxyTool"/> class.
/// </summary>
/// <param name="logger"></param>
/// <param name="mcpClientService"></param>
/// <exception cref="ArgumentNullException"></exception>
[McpServerToolType]
public sealed class AzureProxyTool(ILogger<AzureProxyTool> logger, IMcpClientService mcpClientService) : McpServerTool
{
    private static readonly JsonSchema ToolSchema = new JsonSchemaBuilder()
        .Type(SchemaValueType.Object)
        .Properties(
            ("intent", new JsonSchemaBuilder()
                .Type(SchemaValueType.String)
                .Required()
                .Description("The intent of the azure operation to perform.")
            ),
            ("tool", new JsonSchemaBuilder()
                .Type(SchemaValueType.String)
                .Description("The azure tool to use to execute the operation.")
            ),
            ("command", new JsonSchemaBuilder()
                .Type(SchemaValueType.String)
                .Description("The command to execute against the specified tool.")
            ),
            ("parameters", new JsonSchemaBuilder()
                .Type(SchemaValueType.Object)
                .Description("The parameters to pass to the tool command.")
            ),
            ("learn", new JsonSchemaBuilder()
                .Type(SchemaValueType.Boolean)
                .Description("To learn about the tool and its supported child tools and parameters.")
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

    private static readonly string ToolCallProxySchemaJson = JsonSerializer.Serialize(ToolCallProxySchema, AzureProxyToolSerializationContext.Default.JsonSchema);
    private readonly ILogger<AzureProxyTool> _logger = logger;
    private readonly IMcpClientService _mcpClientService = mcpClientService;
    private string? _cachedRootToolsJson;
    private readonly Dictionary<string, string> _cachedToolListsJson = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Gets the protocol tool definition for Azure operations.
    /// </summary>
    public override Tool ProtocolTool => new()
    {
        Name = "azure",
        Description = """
            This server/tool provides real-time, programmatic access to all Azure products, services, and resources,
            as well as all interactions with the Azure Developer CLI (azd).
            Use this tool for any Azure control plane or data plane operation, including resource management and automation.
            To discover available capabilities, call the tool with the "learn" parameter to get a list of top-level tools.
            To explore further, set "learn" and specify a tool name to retrieve supported commands and their parameters.
            To execute an action, set the "tool", "command", and convert the users intent into the "parameters" based on the discovered schema.
            Always use this tool for any Azure or "azd" related operation requiring up-to-date, dynamic, and interactive capabilities.
            Always include the "intent" parameter to specify the operation you want to perform.
        """,
        Annotations = new ToolAnnotations(),
        InputSchema = JsonSerializer.SerializeToElement(ToolSchema, AzureProxyToolSerializationContext.Default.JsonSchema),
    };

    /// <summary>
    /// Handles invocation of the Azure proxy tool, routing requests to the correct Azure tool or command.
    /// </summary>
    /// <param name="request">The request context containing parameters and metadata.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="CallToolResponse"/> representing the result of the operation.</returns>
    public override async ValueTask<CallToolResult> InvokeAsync(RequestContext<CallToolRequestParams> request, CancellationToken cancellationToken = default)
    {
        var args = request.Params?.Arguments;
        string? intent = null;
        bool learn = false;
        string? tool = null;
        string? command = null;

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
            if (args.TryGetValue("tool", out var toolElem) && toolElem.ValueKind == JsonValueKind.String)
            {
                tool = toolElem.GetString();
            }
            if (args.TryGetValue("command", out var commandElem) && commandElem.ValueKind == JsonValueKind.String)
            {
                command = commandElem.GetString();
            }
        }

        if (!string.IsNullOrEmpty(intent) && string.IsNullOrEmpty(tool) && string.IsNullOrEmpty(command) && !learn)
        {
            learn = true;
        }

        if (learn && string.IsNullOrEmpty(tool) && string.IsNullOrEmpty(command))
        {
            return await RootLearnModeAsync(request, intent ?? "", cancellationToken);
        }
        else if (learn && !string.IsNullOrEmpty(tool) && string.IsNullOrEmpty(command))
        {
            return await ToolLearnModeAsync(request, intent ?? "", tool!, cancellationToken);
        }
        else if (!learn && !string.IsNullOrEmpty(tool) && !string.IsNullOrEmpty(command))
        {
            var toolParams = GetParametersDictionary(args);
            return await CommandModeAsync(request, intent ?? "", tool!, command!, toolParams, cancellationToken);
        }

        return new CallToolResult
        {
            Content =
            [
                new TextContentBlock {
                    Text = """
                        The "tool" and "command" parameters are required when not learning
                        Run again with the "learn" argument to get a list of available tools and their parameters.
                        To learn about a specific tool, use the "tool" argument with the name of the tool.
                    """
                }
            ]
        };
    }

    private string GetRootToolsJson()
    {
        if (_cachedRootToolsJson != null)
        {
            return _cachedRootToolsJson;
        }

        var providerMetadataList = _mcpClientService.ListProviderMetadata();
        var tools = new List<Tool>(providerMetadataList.Count);
        foreach (var meta in providerMetadataList)
        {
            tools.Add(new Tool
            {
                Name = meta.Id,
                Description = meta.Description,
            });
        }
        var toolsResult = new ListToolsResult { Tools = tools };
        var toolsJson = JsonSerializer.Serialize(toolsResult, AzureProxyToolSerializationContext.Default.ListToolsResult);
        _cachedRootToolsJson = toolsJson;

        return toolsJson;
    }

    private async Task<string> GetToolListJsonAsync(RequestContext<CallToolRequestParams> request, string tool)
    {
        if (_cachedToolListsJson.TryGetValue(tool, out var cachedJson))
        {
            return cachedJson;
        }

        var clientOptions = CreateClientOptions(request.Server);
        var client = await _mcpClientService.GetProviderClientAsync(tool, clientOptions);
        if (client == null)
        {
            return string.Empty;
        }

        var listTools = await client.ListToolsAsync();
        var toolsJson = JsonSerializer.Serialize(listTools, AzureProxyToolSerializationContext.Default.ListMcpClientTool);
        _cachedToolListsJson[tool] = toolsJson;

        return toolsJson;
    }

    private async Task<CallToolResult> RootLearnModeAsync(RequestContext<CallToolRequestParams> request, string intent, CancellationToken cancellationToken)
    {
        var toolsJson = GetRootToolsJson();
        var learnResponse = new CallToolResult
        {
            Content =
            [
                new TextContentBlock {
                    Text = $"""
                        Here are the available list of tools.
                        Next, identify the tool you want to learn about and run again with the "learn" argument and the "tool" name to get a list of available commands and their parameters.

                        {toolsJson}
                        """
                }
            ]
        };
        var response = learnResponse;
        if (SupportsSampling(request.Server) && !string.IsNullOrWhiteSpace(intent))
        {
            var toolName = await GetToolNameFromIntentAsync(request, intent, toolsJson, cancellationToken);
            if (toolName != null)
            {
                response = await ToolLearnModeAsync(request, intent, toolName, cancellationToken);
            }
        }

        return response;
    }

    private async Task<CallToolResult> ToolLearnModeAsync(RequestContext<CallToolRequestParams> request, string intent, string tool, CancellationToken cancellationToken)
    {
        var toolsJson = await GetToolListJsonAsync(request, tool);
        if (string.IsNullOrEmpty(toolsJson))
        {
            return await RootLearnModeAsync(request, intent, cancellationToken);
        }

        var learnResponse = new CallToolResult
        {
            Content =
            [
                new TextContentBlock {
                    Text = $"""
                        Here are the available command and their parameters for '{tool}' tool.
                        If you do not find a suitable tool, run again with the "learn" argument and empty "tool" to get a list of available tools and their parameters.
                        Next, identify the command you want to execute and run again with the "tool", "command", and "parameters" arguments.

                        {toolsJson}
                        """
                }
            ]
        };

        var response = learnResponse;
        if (SupportsSampling(request.Server) && !string.IsNullOrWhiteSpace(intent))
        {
            var (commandName, parameters) = await GetCommandAndParametersFromIntentAsync(request, intent, tool, toolsJson, cancellationToken);
            if (commandName != null)
            {
                response = await CommandModeAsync(request, intent, tool, commandName, parameters, cancellationToken);
            }
        }
        return response;
    }

    private async Task<CallToolResult> CommandModeAsync(RequestContext<CallToolRequestParams> request, string intent, string tool, string command, Dictionary<string, object?> parameters, CancellationToken cancellationToken)
    {
        IMcpClient? client;

        try
        {
            var clientOptions = CreateClientOptions(request.Server);
            client = await _mcpClientService.GetProviderClientAsync(tool, clientOptions);
            if (client == null)
            {
                _logger.LogError("Failed to get provider client for tool: {Tool}", tool);
                return await RootLearnModeAsync(request, intent, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception thrown while getting provider client for tool: {Tool}", tool);
            return await RootLearnModeAsync(request, intent, cancellationToken);
        }

        try
        {
            await NotifyProgressAsync(request, $"Calling {tool} {command}...", cancellationToken);
            return await client.CallToolAsync(command, parameters, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception thrown while calling tool: {Tool}, command: {Command}", tool, command);
            return new CallToolResult
            {
                Content =
                [
                    new TextContentBlock {
                        Text = $"""
                            There was an error finding or calling tool and command.
                            Failed to call tool: {tool}, command: {command}
                            Error: {ex.Message}

                            Run again with the "learn" argument and the "tool" name to get a list of available tools and their parameters.
                            """
                    }
                ]
            };
        }
    }

    private static bool SupportsSampling(IMcpServer server)
    {
        return server?.ClientCapabilities?.Sampling != null;
    }

    private static async Task NotifyProgressAsync(RequestContext<CallToolRequestParams> request, string message, CancellationToken cancellationToken)
    {
        var progressToken = request.Params?.ProgressToken;
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

    private async Task<string?> GetToolNameFromIntentAsync(RequestContext<CallToolRequestParams> request, string intent, string toolsJson, CancellationToken cancellationToken)
    {
        await NotifyProgressAsync(request, "Learning about Azure capabilities...", cancellationToken);

        var samplingRequest = new CreateMessageRequestParams
        {
            Messages = [
                new SamplingMessage
                {
                    Role = Role.Assistant,
                    Content = new TextContentBlock {
                        Text = $"""
                            The following is a list of available tools for the Azure server.

                            Your task:
                            - Select a single tool that best matches the user's intent and return the name of the tool.
                            - Only return tool names that are defined in the provided list.
                            - If no tool matches, return "Unknown".

                            Intent:
                            {intent}

                            Available Tools:
                            {toolsJson}
                            """
                    }
                }
            ],
        };
        try
        {
            var samplingResponse = await request.Server.SampleAsync(samplingRequest, cancellationToken);
            var toolName = (samplingResponse.Content as TextContentBlock)?.Text?.Trim();
            if (!string.IsNullOrEmpty(toolName) && toolName != "Unknown")
            {
                return toolName;
            }
        }
        catch
        {
            _logger.LogError("Failed to get tool name from intent: {Intent}", intent);
        }

        return null;
    }

    private async Task<(string? commandName, Dictionary<string, object?> parameters)> GetCommandAndParametersFromIntentAsync(
        RequestContext<CallToolRequestParams> request,
        string intent,
        string tool,
        string toolsJson,
        CancellationToken cancellationToken)
    {
        await NotifyProgressAsync(request, $"Learning about {tool} capabilities...", cancellationToken);

        var toolParams = GetParametersDictionary(request.Params?.Arguments);
        var toolParamsJson = JsonSerializer.Serialize(toolParams, AzureProxyToolSerializationContext.Default.DictionaryStringObject);

        var samplingRequest = new CreateMessageRequestParams
        {
            Messages = [
                new SamplingMessage
                {
                    Role = Role.Assistant,
                    Content = new TextContentBlock{
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
                            {ToolCallProxySchemaJson}

                            Intent:
                            {intent}

                            Known Parameters:
                            {toolParamsJson}

                            Available Commands:
                            {toolsJson}
                            """
                    }
                }
            ],
        };
        try
        {
            var samplingResponse = await request.Server.SampleAsync(samplingRequest, cancellationToken);
            var toolCallJson = (samplingResponse.Content as TextContentBlock)?.Text?.Trim();
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
                    parameters = JsonSerializer.Deserialize(paramsProp.GetRawText(), AzureProxyToolSerializationContext.Default.DictionaryStringObject) ?? [];
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
            return JsonSerializer.Deserialize(parametersElem.GetRawText(), AzureProxyToolSerializationContext.Default.DictionaryStringObject) ?? [];
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
