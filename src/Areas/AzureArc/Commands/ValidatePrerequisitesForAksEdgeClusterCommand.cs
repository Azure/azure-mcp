using AzureMcp.Areas.AzureArc.Services;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Options.Arc;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Threading.Tasks;

namespace AzureMcp.Commands.Arc;

public sealed class ValidatePrerequisitesForAksEdgeClusterCommand : GlobalCommand<ArcConnectOptions>
{
    private const string _commandTitle = "Validate prerequisites for AKS Edge Essentials Cluster";
    private readonly ILogger<ValidatePrerequisitesForAksEdgeClusterCommand> _logger;

    public ValidatePrerequisitesForAksEdgeClusterCommand(ILogger<ValidatePrerequisitesForAksEdgeClusterCommand> logger)
    {
        _logger = logger;
    }

    public override string Name => "validate-prerequisites-aksee-cluster";

    public override string Description =>
        "Validates prerequisites for installing AKS Edge Essentials cluster by loading steps from the resource file and suggesting next steps";

    public override string Title => _commandTitle;

    [McpServerTool(
        Destructive = false,
        ReadOnly = true,
        Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        try
        {
            var arcService = context.GetService<IArcServices>();
            var result = await arcService.ValidatePrerequisitesForAksEdgeClusterAsync();

            context.Response.Status = result.Success ? 200 : 500;
            context.Response.Message = result.Steps;

            return context.Response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to validate prerequisites for AKS Edge Essentials cluster");
            context.Response.Status = 500;
            context.Response.Message = ex.Message;
            return context.Response;
        }
    }
}
