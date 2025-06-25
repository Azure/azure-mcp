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
/// Command to get detailed information about a specific dataset
/// </summary>
public sealed class DatasetDescribeCommand(ILogger<DatasetDescribeCommand> logger) : SubscriptionCommand<DatasetDescribeOptions>()
{
    private const string CommandTitle = "Describe AI Foundry Dataset";
    private readonly ILogger<DatasetDescribeCommand> _logger = logger;
    private readonly Option<string> _projectEndpointOption = AiFoundryOptionDefinitions.ProjectEndpoint;
    private readonly Option<string> _datasetIdOption = AiFoundryOptionDefinitions.DatasetId;

    public override string Name => "describe";

    public override string Description =>
        $"""
        Get detailed information about a specific dataset in an Azure AI Foundry project.
        This command retrieves comprehensive information about the dataset including metadata,
        size, version, and configuration details.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_projectEndpointOption);
        command.AddOption(_datasetIdOption);
    }

    protected override DatasetDescribeOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ProjectEndpoint = parseResult.GetValueForOption(_projectEndpointOption) ?? string.Empty;
        options.DatasetId = parseResult.GetValueForOption(_datasetIdOption) ?? string.Empty;
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
            var result = await service.GetDatasetAsync(options.ProjectEndpoint, options.DatasetId);

            context.Response.Results = result != null ?
                ResponseResult.Create(result, AiFoundryJsonContext.Default.DatasetInfo) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred describing dataset {DatasetId}.", options.DatasetId);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
} 