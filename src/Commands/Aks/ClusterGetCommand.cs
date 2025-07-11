using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Command;
using AzureMcp.Options.Aks.Cluster;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Aks;

public sealed class ClusterGetCommand(ILogger<ClusterGetCommand> logger) : BaseAksCommand<ClusterGetOptions>
{
    private const string CommandTitle = "Get AKS Cluster";
    private readonly ILogger<ClusterGetCommand> _logger = logger;

    public override string Name => "get";

    public override string Description => "Get details for an AKS cluster.";

    public override string Title => CommandTitle;

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
            var cluster = await aksService.GetCluster(
                options.Cluster!,
                options.ResourceGroup!,
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            var result = new ClusterGetCommandResult(
                cluster.Name,
                cluster.Location.ToString(),
                cluster.KubernetesVersion,
                cluster.ProvisioningState?.ToString(),
                cluster.NodeResourceGroup);

            context.Response.Results = ResponseResult.Create(result, AksJsonContext.Default.ClusterGetCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving AKS cluster.");
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record ClusterGetCommandResult(
        string? Name,
        string Location,
        string? KubernetesVersion,
        string? ProvisioningState,
        string? NodeResourceGroup);
}
