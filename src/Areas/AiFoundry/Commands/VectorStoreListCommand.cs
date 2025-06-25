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
/// Command to list vector stores in an Azure AI Foundry project
/// </summary>
public sealed class VectorStoreListCommand(ILogger<VectorStoreListCommand> logger) : SubscriptionCommand<VectorStoreListOptions>()
{
    private const string CommandTitle = "List AI Foundry Vector Stores";
    private readonly ILogger<VectorStoreListCommand> _logger = logger;
    private readonly Option<string> _projectEndpointOption = AiFoundryOptionDefinitions.ProjectEndpoint;

    public override string Name => "list";

    public override string Description =>
        $"""
        List all vector stores in an Azure AI Foundry project. This command retrieves all vector stores
        available in the specified project endpoint.
        Results include vector store names, dimensions, indexes, and metadata.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_projectEndpointOption);
    }

    protected override VectorStoreListOptions BindOptions(ParseResult parseResult)
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
            var vectorStores = await service.ListVectorStoresAsync(options.ProjectEndpoint);

            context.Response.Results = vectorStores?.Any() == true ?
                ResponseResult.Create(new VectorStoreListResult(vectorStores.ToList()), AiFoundryJsonContext.Default.VectorStoreListResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing vector stores.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record VectorStoreListResult(List<VectorStoreInfo> VectorStores);
} 