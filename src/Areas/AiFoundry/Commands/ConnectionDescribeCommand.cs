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
/// Command to describe a specific connection in an AI Foundry project
/// </summary>
public sealed class ConnectionDescribeCommand(ILogger<ConnectionDescribeCommand> logger) : SubscriptionCommand<ConnectionDescribeOptions>()
{
    private const string CommandTitle = "Describe Project Connection";
    private readonly ILogger<ConnectionDescribeCommand> _logger = logger;
    private readonly Option<string> _projectEndpointOption = AiFoundryOptionDefinitions.ProjectEndpoint;
    private readonly Option<string> _connectionNameOption = AiFoundryOptionDefinitions.ConnectionName;

    public override string Name => "describe";

    public override string Description =>
        $"""
        Get detailed information about a specific connection in an AI Foundry project. This command retrieves
        comprehensive details about a connection including configuration, authentication settings, capabilities,
        and integration status information.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_projectEndpointOption);
        command.AddOption(_connectionNameOption);
    }

    protected override ConnectionDescribeOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ProjectEndpoint = parseResult.GetValueForOption(_projectEndpointOption);
        options.ConnectionName = parseResult.GetValueForOption(_connectionNameOption);
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
            var connection = await service.GetConnectionAsync(
                options.ProjectEndpoint!,
                options.ConnectionName!);

            context.Response.Results = connection != null ?
                ResponseResult.Create(connection, AiFoundryJsonContext.Default.ConnectionInfo) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred describing connection {ConnectionName}", options.ConnectionName);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
} 