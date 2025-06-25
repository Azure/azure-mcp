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
/// Command to describe a specific model deployment in an AI Foundry project
/// </summary>
public sealed class DeploymentDescribeCommand(ILogger<DeploymentDescribeCommand> logger) : SubscriptionCommand<DeploymentDescribeOptions>()
{
    private const string CommandTitle = "Describe Model Deployment";
    private readonly ILogger<DeploymentDescribeCommand> _logger = logger;
    private readonly Option<string> _projectEndpointOption = AiFoundryOptionDefinitions.ProjectEndpoint;
    private readonly Option<string> _deploymentNameOption = AiFoundryOptionDefinitions.DeploymentName;

    public override string Name => "describe";

    public override string Description =>
        $"""
        Get detailed information about a specific model deployment in an AI Foundry project. This command retrieves
        comprehensive details about a deployment including configuration, performance metrics, scaling settings,
        and operational status information.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_projectEndpointOption);
        command.AddOption(_deploymentNameOption);
    }

    protected override DeploymentDescribeOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ProjectEndpoint = parseResult.GetValueForOption(_projectEndpointOption);
        options.DeploymentName = parseResult.GetValueForOption(_deploymentNameOption);
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
            var deployment = await service.GetDeploymentAsync(
                options.ProjectEndpoint!,
                options.DeploymentName!);

            context.Response.Results = deployment != null ?
                ResponseResult.Create(deployment, AiFoundryJsonContext.Default.DeploymentInfo) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred describing deployment {DeploymentName}", options.DeploymentName);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
} 