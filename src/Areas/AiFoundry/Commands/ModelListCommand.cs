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
/// Command to list AI models available in the model catalog
/// </summary>
public sealed class ModelListCommand(ILogger<ModelListCommand> logger) : SubscriptionCommand<ModelListOptions>()
{
    private const string CommandTitle = "List AI Models";
    private readonly ILogger<ModelListCommand> _logger = logger;
    private readonly Option<string> _projectEndpointOption = AiFoundryOptionDefinitions.ProjectEndpoint;
    private readonly Option<string> _modelProviderOption = AiFoundryOptionDefinitions.ModelProvider;
    private readonly Option<string> _modelCategoryOption = AiFoundryOptionDefinitions.ModelCategory;

    public override string Name => "list";

    public override string Description =>
        $"""
        List available AI models in the model catalog for a specified project. This command retrieves all models
        available in the AI Foundry model catalog, optionally filtered by provider or category.
        Results include model names, providers, capabilities, and metadata.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_projectEndpointOption);
        command.AddOption(_modelProviderOption);
        command.AddOption(_modelCategoryOption);
    }

    protected override ModelListOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ProjectEndpoint = parseResult.GetValueForOption(_projectEndpointOption);
        options.ModelProvider = parseResult.GetValueForOption(_modelProviderOption);
        options.Category = parseResult.GetValueForOption(_modelCategoryOption);
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
            var models = await service.ListModelsAsync(
                options.ProjectEndpoint ?? throw new InvalidOperationException("ProjectEndpoint is required"),
                options.ModelProvider,
                options.Category);

            context.Response.Results = models?.Any() == true ?
                ResponseResult.Create(new ModelListResult(models.ToList()), AiFoundryJsonContext.Default.ModelListResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing AI models.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record ModelListResult(List<ModelInfo> Models);
} 