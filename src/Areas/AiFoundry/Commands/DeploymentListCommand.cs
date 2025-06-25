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
/// Command to list model deployments in an AI Foundry project
/// </summary>
public sealed class DeploymentListCommand(ILogger<DeploymentListCommand> logger) : SubscriptionCommand<DeploymentListOptions>()
{
    private const string CommandTitle = "List Model Deployments";
    private readonly ILogger<DeploymentListCommand> _logger = logger;
    private readonly Option<string> _projectEndpointOption = AiFoundryOptionDefinitions.ProjectEndpoint;

    public override string Name => "list";

    public override string Description =>
        $"""
        List model deployments in an Azure AI Foundry project. This command retrieves all model deployments
        currently active in the specified project, including their status, configuration, and performance metrics.
        Results include deployment names, model information, endpoints, and operational status.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_projectEndpointOption);
    }

    protected override DeploymentListOptions BindOptions(ParseResult parseResult)
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
            var deployments = await service.ListDeploymentsAsync(
                options.ProjectEndpoint ?? throw new InvalidOperationException("ProjectEndpoint is required"));

            context.Response.Results = deployments?.Any() == true ?
                ResponseResult.Create(new DeploymentListResult(deployments.ToList()), AiFoundryJsonContext.Default.DeploymentListResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing deployments.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record DeploymentListResult(List<DeploymentInfo> Deployments);
} 