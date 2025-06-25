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
/// Command to list Azure AI Foundry projects in a subscription
/// </summary>
public sealed class ProjectListCommand(ILogger<ProjectListCommand> logger) : SubscriptionCommand<ProjectListOptions>()
{
    private const string CommandTitle = "List AI Foundry Projects";
    private readonly ILogger<ProjectListCommand> _logger = logger;
    private readonly Option<string> _optionalResourceGroupOption = AiFoundryOptionDefinitions.OptionalResourceGroup;

    public override string Name => "list";

    public override string Description =>
        $"""
        List all Azure AI Foundry projects in a subscription. This command retrieves all AI Foundry projects
        available in the specified {OptionDefinitions.Common.SubscriptionName}, optionally filtered by resource group.
        Results include project names, locations, endpoints, and metadata.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_optionalResourceGroupOption);
    }

    protected override ProjectListOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ResourceGroup = parseResult.GetValueForOption(_optionalResourceGroupOption);
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
            var projects = await service.ListProjectsAsync(
                options.Subscription!,
                options.ResourceGroup);

            context.Response.Results = projects?.Any() == true ?
                ResponseResult.Create(new ProjectListResult(projects.ToList()), AiFoundryJsonContext.Default.ProjectListResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing AI Foundry projects.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record ProjectListResult(List<ProjectInfo> Projects);
} 