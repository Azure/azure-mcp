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
/// Command to get detailed information about a specific vector store
/// </summary>
public sealed class VectorStoreDescribeCommand(ILogger<VectorStoreDescribeCommand> logger) : SubscriptionCommand<VectorStoreDescribeOptions>()
{
    private const string CommandTitle = "Describe AI Foundry Vector Store";
    private readonly ILogger<VectorStoreDescribeCommand> _logger = logger;
    private readonly Option<string> _projectEndpointOption = AiFoundryOptionDefinitions.ProjectEndpoint;
    private readonly Option<string> _vectorStoreIdOption = AiFoundryOptionDefinitions.VectorStoreId;

    public override string Name => "describe";

    public override string Description =>
        $"""
        Get detailed information about a specific vector store in an Azure AI Foundry project.
        This command retrieves comprehensive information about the vector store including dimensions,
        vector count, index type, and configuration details.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_projectEndpointOption);
        command.AddOption(_vectorStoreIdOption);
    }

    protected override VectorStoreDescribeOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ProjectEndpoint = parseResult.GetValueForOption(_projectEndpointOption) ?? string.Empty;
        options.VectorStoreId = parseResult.GetValueForOption(_vectorStoreIdOption) ?? string.Empty;
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
            var result = await service.GetVectorStoreAsync(options.ProjectEndpoint, options.VectorStoreId);

            context.Response.Results = result != null ?
                ResponseResult.Create(result, AiFoundryJsonContext.Default.VectorStoreInfo) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred describing vector store {VectorStoreId}.", options.VectorStoreId);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
} 