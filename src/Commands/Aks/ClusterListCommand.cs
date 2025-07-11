using AzureMcp.Options.Aks.Cluster;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Aks;

public sealed class ClusterListCommand(ILogger<ClusterListCommand> logger) : BaseAksCommand<ClusterListOptions>
{
    private const string CommandTitle = "List AKS Clusters";
    private readonly ILogger<ClusterListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        "List all AKS clusters in a subscription.";

    public override string Title => CommandTitle;

    // Resource group is optional for list command - will list from all resource groups if not specified
    protected override bool RequiresResourceGroup => false;

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

            var aksService = context.GetService<IAksService>();
            var clusters = await aksService.ListClusters(
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = clusters.Count > 0 ?
                ResponseResult.Create(new ClusterListCommandResult(clusters), AksJsonContext.Default.ClusterListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing AKS clusters.");
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record ClusterListCommandResult(IEnumerable<string> Clusters);
}
