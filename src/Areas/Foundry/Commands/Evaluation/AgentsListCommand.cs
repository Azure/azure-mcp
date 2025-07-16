// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.AI.Agents.Persistent;
using AzureMcp.Areas.Foundry.Options;
using AzureMcp.Areas.Foundry.Options.Evaluation;
using AzureMcp.Areas.Foundry.Services;
using AzureMcp.Commands;

namespace AzureMcp.Areas.Foundry.Commands.Evaluation;

public sealed class AgentsListCommand : GlobalCommand<AgentsListOptions>
{
    private const string CommandTitle = "List Evaluation Agents";
    private readonly Option<string> _endpointOption = FoundryOptionDefinitions.EndpointOption;

    public override string Name => "list";

    public override string Description =>
        """
        List all Azure AI Agents available in the configured project.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_endpointOption);
    }

    protected override AgentsListOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Endpoint = parseResult.GetValueForOption(_endpointOption);

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
            var agents = await service.ListAgents(
                options.Endpoint!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = agents?.Count > 0 ?
                ResponseResult.Create(
                    new AgentsListCommandResult(agents),
                    FoundryJsonContext.Default.AgentsListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record AgentsListCommandResult(IEnumerable<PersistentAgent> Agents);
}
