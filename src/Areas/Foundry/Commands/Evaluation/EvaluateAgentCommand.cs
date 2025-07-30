// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.AI.Agents.Persistent;
using AzureMcp.Areas.Foundry.Options;
using AzureMcp.Areas.Foundry.Options.Evaluation;
using AzureMcp.Areas.Foundry.Services;
using AzureMcp.Commands;

namespace AzureMcp.Areas.Foundry.Commands.Evaluation;

public sealed class EvaluateAgentCommand : GlobalCommand<EvaluateAgentOptions>
{
    private const string CommandTitle = "Evaluate Agent";
    private readonly Option<string> _agentIdOption = FoundryOptionDefinitions.AgentIdOption;
    private readonly Option<string> _queryOption = FoundryOptionDefinitions.QueryOption;
    private readonly Option<string> _evaluatorNameOption = FoundryOptionDefinitions.EvaluatorNameOption;
    private readonly Option<string> _responseOption = FoundryOptionDefinitions.ResponseOption;
    private readonly Option<string> _toolDefinitionsOption = FoundryOptionDefinitions.ToolDefinitionsOption;

    public override string Name => "evaluate-data";

    public override string Description =>
        """
        Run agent evaluation on agent data. Accepts both plain text and JSON strings.

        Parameters:
        - evaluator_name: Name of the agent evaluator to use (intent_resolution, tool_call_accuracy, task_adherence)
        - query: User query (plain text or JSON string)
        - response: Agent response (plain text or JSON string)
        - tool_calls: Optional tool calls data (JSON string)
        - tool_definitions: Optional tool definitions (JSON string)
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_agentIdOption);
        command.AddOption(_queryOption);
        command.AddOption(_evaluatorNameOption);
        command.AddOption(_responseOption);
        command.AddOption(_toolDefinitionsOption);
    }

    protected override EvaluateAgentOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.AgentId = parseResult.GetValueForOption(_agentIdOption);
        options.Query = parseResult.GetValueForOption(_queryOption);
        options.EvaluatorName = parseResult.GetValueForOption(_evaluatorNameOption);
        options.Response = parseResult.GetValueForOption(_responseOption);
        options.ToolDefinitions = parseResult.GetValueForOption(_toolDefinitionsOption);

        return options;
    }

    [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            var service = context.GetService<IFoundryService>();
            var result = await service.EvaluateAgent(
                options.EvaluatorName!,
                options.Query!,
                options.Response!,
                options.ToolDefinitions);

            context.Response.Results = ResponseResult.Create(
                new EvaluateAgentCommandResult(result),
                FoundryJsonContext.Default.EvaluateAgentCommandResult);
        }
        catch (Exception ex)
        {
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record EvaluateAgentCommandResult(Dictionary<string, object> Response);
}
