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
/// Command to list datasets in an Azure AI Foundry project
/// </summary>
public sealed class DatasetListCommand(ILogger<DatasetListCommand> logger) : SubscriptionCommand<DatasetListOptions>()
{
    private const string CommandTitle = "List AI Foundry Datasets";
    private readonly ILogger<DatasetListCommand> _logger = logger;
    private readonly Option<string> _projectEndpointOption = AiFoundryOptionDefinitions.ProjectEndpoint;

    public override string Name => "list";

    public override string Description =>
        $"""
        List all datasets in an Azure AI Foundry project. This command retrieves all datasets
        available in the specified project endpoint.
        Results include dataset names, types, versions, and metadata.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_projectEndpointOption);
    }

    protected override DatasetListOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ProjectEndpoint = parseResult.GetValueForOption(_projectEndpointOption) ?? string.Empty;
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
            var datasets = await service.ListDatasetsAsync(options.ProjectEndpoint);

            context.Response.Results = datasets?.Any() == true ?
                ResponseResult.Create(new DatasetListResult(datasets.ToList()), AiFoundryJsonContext.Default.DatasetListResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing datasets.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record DatasetListResult(List<DatasetInfo> Datasets);
} 