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
/// Command to list connections configured in an AI Foundry project
/// </summary>
public sealed class ConnectionListCommand(ILogger<ConnectionListCommand> logger) : SubscriptionCommand<ConnectionListOptions>()
{
    private const string CommandTitle = "List Project Connections";
    private readonly ILogger<ConnectionListCommand> _logger = logger;
    private readonly Option<string> _projectEndpointOption = AiFoundryOptionDefinitions.ProjectEndpoint;

    public override string Name => "list";

    public override string Description =>
        $"""
        List connections configured in an Azure AI Foundry project. This command retrieves all external
        service connections and integrations configured for the specified project, including Azure services,
        third-party APIs, and data sources. Results include connection names, types, and status information.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_projectEndpointOption);
    }

    protected override ConnectionListOptions BindOptions(ParseResult parseResult)
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
            var connections = await service.ListConnectionsAsync(
                options.ProjectEndpoint ?? throw new InvalidOperationException("ProjectEndpoint is required"));

            context.Response.Results = connections?.Any() == true ?
                ResponseResult.Create(new ConnectionListResult(connections.ToList()), AiFoundryJsonContext.Default.ConnectionListResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing connections.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record ConnectionListResult(List<ConnectionInfo> Connections);
} 