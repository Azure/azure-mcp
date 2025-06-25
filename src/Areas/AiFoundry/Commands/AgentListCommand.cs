using System.CommandLine.Parsing;
using AzureMcp.Areas.AiFoundry.Models;
using AzureMcp.Areas.AiFoundry.Options;
using AzureMcp.Areas.AiFoundry.Services;
using AzureMcp.Commands;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Command;
using AzureMcp.Models.Option;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.AiFoundry.Commands;

/// <summary>
/// Command to list AI agents configured in an AI Foundry project
/// </summary>
public sealed class AgentListCommand(ILogger<AgentListCommand> logger) : SubscriptionCommand<AgentListOptions>()
{
    private const string CommandTitle = "List AI Agents";
    private readonly ILogger<AgentListCommand> _logger = logger;
    private readonly Option<string> _projectEndpointOption = AiFoundryOptionDefinitions.ProjectEndpoint;

    public override string Name => "list";

    public override string Description =>
        $"""
        List AI agents configured in an Azure AI Foundry project. This command retrieves all AI agents
        and assistants created in the specified project, including their capabilities, tools, and
        configuration. Results include agent names, descriptions, tool integrations, and status information.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_projectEndpointOption);
    }

    protected override AgentListOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ProjectEndpoint = parseResult.GetValueForOption(_projectEndpointOption);
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

            var service = context.GetService<IAiFoundryService>();
            var agents = await service.ListAgentsAsync(
                options.ProjectEndpoint ?? throw new InvalidOperationException("ProjectEndpoint is required"));

            context.Response.Results = agents?.Any() == true ?
                ResponseResult.Create(new AgentListResult(agents.ToList()), AiFoundryJsonContext.Default.AgentListResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing agents.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record AgentListResult(List<AgentInfo> Agents);
} 