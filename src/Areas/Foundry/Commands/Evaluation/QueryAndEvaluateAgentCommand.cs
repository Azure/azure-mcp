// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.AI.Agents.Persistent;
using AzureMcp.Areas.Foundry.Options;
using AzureMcp.Areas.Foundry.Options.Evaluation;
using AzureMcp.Areas.Foundry.Services;
using AzureMcp.Commands;

namespace AzureMcp.Areas.Foundry.Commands.Evaluation;

public sealed class QueryAndEvaluateAgentCommand : GlobalCommand<QueryAndEvaluateAgentOptions>
{
    private const string CommandTitle = "Query and Evaluate Agent";
    private readonly Option<string> _agentIdOption = FoundryOptionDefinitions.AgentIdOption;
    private readonly Option<string> _queryOption = FoundryOptionDefinitions.QueryOption;
    private readonly Option<string> _endpointOption = FoundryOptionDefinitions.EndpointOption;
    private readonly Option<string> _evaluators = FoundryOptionDefinitions.EvaluatorsOption;

    public override string Name => "query-and-evaluate";

    public override string Description =>
        """
        Query an agent and evaluate its response in a single operation.

        Parameters:
        - agent_id: ID of the agent to query
        - query: Text query to send to the agent
        - evaluators: Optional list of agent evaluator names to use (intent_resolution, tool_call_accuracy, task_adherence). Default is all evaluators if not specified.

        Returns both the agent response and evaluation results
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_agentIdOption);
        command.AddOption(_queryOption);
        command.AddOption(_endpointOption);
        command.AddOption(_evaluators);
    }

    protected override QueryAndEvaluateAgentOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Endpoint = parseResult.GetValueForOption(_endpointOption);
        options.AgentId = parseResult.GetValueForOption(_agentIdOption);
        options.Query = parseResult.GetValueForOption(_queryOption);
        options.Evaluators = parseResult.GetValueForOption(_evaluators);

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
            var result = await service.QueryAndEvaluateAgent(
                options.AgentId!,
                options.Query!,
                options.Endpoint!,
                options.Tenant,
                options.Evaluators?.Split(',').Select(e => e.Trim()).ToList());

            context.Response.Results = ResponseResult.Create(
                new QueryAndEvaluateAgentCommandResult(result),
                FoundryJsonContext.Default.QueryAndEvaluateAgentCommandResult);
        }
        catch (Exception ex)
        {
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record QueryAndEvaluateAgentCommandResult(Dictionary<string, object> Response);
}
