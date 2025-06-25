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
/// Command to describe a specific AI model in the model catalog
/// </summary>
public sealed class ModelDescribeCommand(ILogger<ModelDescribeCommand> logger) : SubscriptionCommand<ModelDescribeOptions>()
{
    private const string CommandTitle = "Describe AI Model";
    private readonly ILogger<ModelDescribeCommand> _logger = logger;
    private readonly Option<string> _projectEndpointOption = AiFoundryOptionDefinitions.ProjectEndpoint;
    private readonly Option<string> _modelIdOption = AiFoundryOptionDefinitions.ModelId;

    public override string Name => "describe";

    public override string Description =>
        $"""
        Get detailed information about a specific AI model in the model catalog. This command retrieves
        comprehensive details about a model including its capabilities, parameters, usage instructions,
        and technical specifications.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_projectEndpointOption);
        command.AddOption(_modelIdOption);
    }

    protected override ModelDescribeOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ProjectEndpoint = parseResult.GetValueForOption(_projectEndpointOption);
        options.ModelId = parseResult.GetValueForOption(_modelIdOption);
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
            var model = await service.GetModelAsync(
                options.ProjectEndpoint!,
                options.ModelId!);

            context.Response.Results = model != null ?
                ResponseResult.Create(model, AiFoundryJsonContext.Default.ModelInfo) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred describing AI model {ModelId}", options.ModelId);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
} 