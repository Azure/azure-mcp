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
/// Command to describe a specific AI agent in an AI Foundry project
/// </summary>
public sealed class AgentDescribeCommand(ILogger<AgentDescribeCommand> logger) : SubscriptionCommand<AgentDescribeOptions>()
{
    private const string CommandTitle = "Describe AI Agent";
    private readonly ILogger<AgentDescribeCommand> _logger = logger;
    private readonly Option<string> _projectEndpointOption = AiFoundryOptionDefinitions.ProjectEndpoint;
    private readonly Option<string> _agentIdOption = AiFoundryOptionDefinitions.AgentId;

    public override string Name => "describe";

    public override string Description =>
        $"""
        Get detailed information about a specific AI agent in an AI Foundry project. This command retrieves
        comprehensive details about an agent including configuration, capabilities, tool integrations,
        conversation settings, and operational status information.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_projectEndpointOption);
        command.AddOption(_agentIdOption);
    }

    protected override AgentDescribeOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ProjectEndpoint = parseResult.GetValueForOption(_projectEndpointOption);
        options.AgentId = parseResult.GetValueForOption(_agentIdOption);
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
            var agent = await service.GetAgentAsync(
                options.ProjectEndpoint!,
                options.AgentId!);

            context.Response.Results = agent != null ?
                ResponseResult.Create(agent, AiFoundryJsonContext.Default.AgentInfo) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred describing agent {AgentId}", options.AgentId);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
} 