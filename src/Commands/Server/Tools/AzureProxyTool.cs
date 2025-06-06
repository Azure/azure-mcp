using ModelContextProtocol.Protocol;
using Json.Schema;
using Json.More;
using ModelContextProtocol.Client;
using Microsoft.Extensions.Logging;
using ModelContextProtocol;
using System.Diagnostics.CodeAnalysis;

namespace AzureMcp.Commands.Server.Tools;

[McpServerToolType]
[UnconditionalSuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code", Justification = "<Pending>")]
[UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.", Justification = "<Pending>")]
public class AzureProxyTool : McpServerTool
{
    public AzureProxyTool(ILogger<AzureProxyTool> logger, McpClientProvider mcpClientProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mcpClientProvider = mcpClientProvider ?? throw new ArgumentNullException(nameof(mcpClientProvider));

        _logger.LogInformation("AzureTool initialized.");
    }

    private static readonly JsonElement ToolCallSchema = new JsonSchemaBuilder()
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
        .Build()
        .ToJsonDocument()
        .RootElement;

    private static readonly string ToolCallSchemaJson = JsonSerializer.Serialize(ToolCallSchema, new JsonSerializerOptions { WriteIndented = true });
    private readonly ILogger<AzureProxyTool> _logger;
    private readonly McpClientProvider _mcpClientProvider;
    private string? _cachedRootToolsJson;
    private readonly Dictionary<string, string> _cachedToolListsJson = new(StringComparer.OrdinalIgnoreCase);

    private string GetRootToolsJson()
    {
        if (this._cachedRootToolsJson != null)
        {
            return this._cachedRootToolsJson;
        }

        var providerMetadataList = _mcpClientProvider.ListProviderMetadata();
        var tools = new List<Tool>();
        foreach (var meta in providerMetadataList)
        {
            tools.Add(new Tool
            {
                Name = meta.Id,
                Description = meta.Description,
            });
        }
        var toolsResult = new ListToolsResult { Tools = tools };
        var toolsJson = JsonSerializer.Serialize(toolsResult, new JsonSerializerOptions { WriteIndented = true });
        this._cachedRootToolsJson = toolsJson;

        return toolsJson;
    }

    private async Task<string> GetToolListJsonAsync(string tool)
    {
        if (this._cachedToolListsJson.TryGetValue(tool, out var cachedJson))
        {
            return cachedJson;
        }

        var client = await _mcpClientProvider.GetProviderClientAsync(tool);
        if (client == null)
        {
            return string.Empty;
        }

        var listTools = await client.ListToolsAsync();
        var toolsJson = JsonSerializer.Serialize(listTools, new JsonSerializerOptions { WriteIndented = true });
        this._cachedToolListsJson[tool] = toolsJson;
        return toolsJson;
    }

    [UnconditionalSuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code", Justification = "<Pending>")]
    [UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.", Justification = "<Pending>")]
    public override Tool ProtocolTool => new Tool
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
        InputSchema = new JsonSchemaBuilder()
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
            .Build()
            .ToJsonDocument()
            .RootElement
    };

    public override async ValueTask<CallToolResponse> InvokeAsync(RequestContext<CallToolRequestParams> request, CancellationToken cancellationToken = default)
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

        _logger.LogDebug("InvokeAsync called: Intent={Intent}, Learn={Learn}, Tool={Tool}, Command={Command}",
            intent,
            learn,
            tool,
            command
        );

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

        return new CallToolResponse
        {
            Content =
            [
                new Content {
                    Type = "text",
                    Text = """
                        The "tool" and "command" parameters are required when not learning
                        Run again with the "learn" argument to get a list of available tools and their parameters.
                        To learn about a specific tool, use the "tool" argument with the name of the tool.
                    """
                }
            ]
        };
    }

    private async Task<CallToolResponse> RootLearnModeAsync(RequestContext<CallToolRequestParams> request, string intent, CancellationToken cancellationToken)
    {
        var toolsJson = GetRootToolsJson();
        var learnResponse = new CallToolResponse
        {
            Content =
            [
                new Content {
                    Type = "text",
                    Text = $"""
                        Here are the available list of tools.
                        Next, identify the tool you want to learn about and run again with the "learn" argument and the "tool" name to get a list of available commands and their parameters.

                        {toolsJson}
                        """
                }
            ]
        };
        var response = learnResponse;
        if (SupportsSampling(request) && !string.IsNullOrWhiteSpace(intent))
        {
            var toolName = await GetToolNameFromIntentAsync(request, intent, toolsJson, cancellationToken);
            if (toolName != null)
            {
                response = await ToolLearnModeAsync(request, intent, toolName, cancellationToken);
            }
        }
        return response;
    }

    private async Task<CallToolResponse> ToolLearnModeAsync(RequestContext<CallToolRequestParams> request, string intent, string tool, CancellationToken cancellationToken)
    {
        var toolsJson = await GetToolListJsonAsync(tool);
        if (string.IsNullOrEmpty(toolsJson))
        {
            return await RootLearnModeAsync(request, intent, cancellationToken);
        }

        var learnResponse = new CallToolResponse
        {
            Content =
            [
                new Content {
                    Type = "text",
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
        if (SupportsSampling(request) && !string.IsNullOrWhiteSpace(intent))
        {
            var (commandName, parameters) = await GetCommandAndParametersFromIntentAsync(request, intent, tool, toolsJson, cancellationToken);
            if (commandName != null)
            {
                response = await CommandModeAsync(request, intent, tool, commandName, parameters, cancellationToken);
            }
        }
        return response;
    }

    private async Task<CallToolResponse> CommandModeAsync(RequestContext<CallToolRequestParams> request, string intent, string tool, string command, Dictionary<string, object?> parameters, CancellationToken cancellationToken)
    {
        IMcpClient? client;

        try
        {
            client = await _mcpClientProvider.GetProviderClientAsync(tool);
            if (client == null)
            {
                return await RootLearnModeAsync(request, intent, cancellationToken);
            }
        }
        catch
        {
            return await RootLearnModeAsync(request, intent, cancellationToken);
        }

        try
        {
            var progressToken = request.Params?.Meta?.ProgressToken;
            if (progressToken != null)
            {
                await request.Server.NotifyProgressAsync(progressToken.Value,
                new ProgressNotificationValue
                {
                    Progress = 0f,
                    Message = $"Calling {tool} {command}...",
                }, cancellationToken);
            }

            return await client.CallToolAsync(command, parameters, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
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

                            Run again with the "learn" argument and the "tool" name to get a list of available tools and their parameters.
                            """
                    }
                ]
            };
        }
    }

    // --- Private helper methods moved to bottom ---
    private static bool SupportsSampling(RequestContext<CallToolRequestParams> request)
    {
        return request.Server?.ClientCapabilities?.Sampling != null;
    }

    private async Task<string?> GetToolNameFromIntentAsync(RequestContext<CallToolRequestParams> request, string intent, string toolsJson, CancellationToken cancellationToken)
    {
        var progressToken = request.Params?.Meta?.ProgressToken;
        if (progressToken != null)
        {
            await request.Server.NotifyProgressAsync(progressToken.Value,
            new ProgressNotificationValue
            {
                Progress = 0f,
                Message = $"Learning about azure capabilities...",
            }, cancellationToken);
        }

        var samplingRequest = new CreateMessageRequestParams
        {
            Messages = [
                new SamplingMessage
                {
                    Role = Role.Assistant,
                    Content = new Content{
                        Type = "text",
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
            var samplingResponse = await request.Server.RequestSamplingAsync(samplingRequest, cancellationToken);
            var toolName = samplingResponse.Content.Text?.Trim();
            if (!string.IsNullOrEmpty(toolName) && toolName != "Unknown")
            {
                return toolName;
            }
        }
        catch
        {
            // ignore and return null
        }
        return null;
    }

    [UnconditionalSuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code", Justification = "<Pending>")]
    private async Task<(string? commandName, Dictionary<string, object?> parameters)> GetCommandAndParametersFromIntentAsync(
        RequestContext<CallToolRequestParams> request,
        string intent,
        string tool,
        string toolsJson,
        CancellationToken cancellationToken)
    {
        var progressToken = request.Params?.Meta?.ProgressToken;
        if (progressToken != null)
        {
            await request.Server.NotifyProgressAsync(progressToken.Value,
            new ProgressNotificationValue
            {
                Progress = 0f,
                Message = $"Learning about {tool} capabilities...",
            }, cancellationToken);
        }

        var toolParams = GetParametersDictionary(request.Params?.Arguments);
        var toolParamsJson = JsonSerializer.Serialize(toolParams, new JsonSerializerOptions { WriteIndented = true });

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
                            {ToolCallSchemaJson}

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
            var samplingResponse = await request.Server.RequestSamplingAsync(samplingRequest, cancellationToken);
            var toolCallJson = samplingResponse.Content.Text?.Trim();
            string? commandName = null;
            Dictionary<string, object?> parameters = new();
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
                    parameters = JsonSerializer.Deserialize<Dictionary<string, object?>>(paramsProp.GetRawText()) ?? new();
                }
            }
            if (commandName != null && commandName != "Unknown")
            {
                return (commandName, parameters);
            }
        }
        catch
        {
            // ignore and return default
        }
        return (null, new Dictionary<string, object?>());
    }

    [UnconditionalSuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code", Justification = "<Pending>")]
    private static Dictionary<string, object?> GetParametersDictionary(IReadOnlyDictionary<string, JsonElement>? args)
    {
        if (args != null && args.TryGetValue("parameters", out var parametersElem) && parametersElem.ValueKind == JsonValueKind.Object)
        {
            return JsonSerializer.Deserialize<Dictionary<string, object?>>(parametersElem.GetRawText()) ?? new();
        }

        return new Dictionary<string, object?>();
    }
}
