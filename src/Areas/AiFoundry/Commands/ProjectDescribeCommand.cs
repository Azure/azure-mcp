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
/// Command to describe a specific Azure AI Foundry project
/// </summary>
public sealed class ProjectDescribeCommand(ILogger<ProjectDescribeCommand> logger) : SubscriptionCommand<ProjectDescribeOptions>()
{
    private const string CommandTitle = "Describe AI Foundry Project";
    private readonly ILogger<ProjectDescribeCommand> _logger = logger;
    private readonly Option<string> _projectNameOption = AiFoundryOptionDefinitions.ProjectName;

    public override string Name => "describe";

    public override string Description =>
        $"""
        Get detailed information about a specific Azure AI Foundry project. This command retrieves 
        comprehensive details for the specified project including configuration, endpoints, and metadata.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_resourceGroupOption);
        command.AddOption(_projectNameOption);
    }

    protected override ProjectDescribeOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        options.ProjectName = parseResult.GetValueForOption(_projectNameOption);
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
            var project = await service.GetProjectAsync(
                options.Subscription!,
                options.ResourceGroup!,
                options.ProjectName!);

            context.Response.Results = project != null ?
                ResponseResult.Create(new ProjectDescribeResult(project), AiFoundryJsonContext.Default.ProjectDescribeResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred describing AI Foundry project {ProjectName}.", options.ProjectName);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record ProjectDescribeResult(ProjectInfo Project);
} 