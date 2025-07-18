using AzureMcp.Areas.AzureArc.Services;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Options.Arc;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Threading.Tasks;

namespace AzureMcp.Commands.Arc;

public sealed class QuickDeployAksEdgeEssentialsCommand : GlobalCommand<ArcConnectOptions>
{
    private const string _commandTitle = "Quick Deploy AKS Edge Essentials";
    private readonly ILogger<QuickDeployAksEdgeEssentialsCommand> _logger;

    private readonly Option<string> _clusterNameOption = new("--cluster-name", "Name of the AKS Edge Essentials cluster.") { IsRequired = true };
    private readonly Option<string> _resourceGroupNameOption = new("--resource-group-name", "Name of the resource group.") { IsRequired = true };
    private readonly Option<string> _subscriptionIdOption = new("--subscription-id", "Azure subscription ID.") { IsRequired = true };
    private readonly Option<string> _tenantIdOption = new("--tenant-id", "Azure tenant ID.") { IsRequired = true };
    private readonly Option<string> _locationOption = new("--location", "Azure region where the cluster will be registered.") { IsRequired = true };
    private readonly Option<string> _userProvidedPathOption = new("--user-provided-path", "User-provided path for temporary files.") { IsRequired = true };


    public QuickDeployAksEdgeEssentialsCommand(ILogger<QuickDeployAksEdgeEssentialsCommand> logger)
    {
        _logger = logger;
    }

    public override string Name => "quick-deploy-aks-edge-essentials";

    public override string Description =>
        "Performs a quick deployment of AKS Edge Essentials by executing the necessary scripts and configurations to set up the environment efficiently. After the Edge Essentials deployment, this also connects the cluster to Azure Arc";

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);

        // Add options specific to ArcConnectOptions
        command.AddOption(_clusterNameOption);
        command.AddOption(_resourceGroupNameOption);
        command.AddOption(_subscriptionIdOption);
        command.AddOption(_tenantIdOption);
        command.AddOption(_locationOption);
        command.AddOption(_userProvidedPathOption);
    }

    protected override ArcConnectOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);

        // Bind additional options
        options.ClusterName = parseResult.GetValueForOption(_clusterNameOption);
        options.ResourceGroupName = parseResult.GetValueForOption(_resourceGroupNameOption);
        options.SubscriptionId = parseResult.GetValueForOption(_subscriptionIdOption);
        options.TenantId = parseResult.GetValueForOption(_tenantIdOption);
        options.Location = parseResult.GetValueForOption(_locationOption);
        options.UserProvidedPath = parseResult.GetValueForOption(_userProvidedPathOption);

        return options;
    }

    [McpServerTool(
        Destructive = false,
        ReadOnly = true,
        Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        try
        {

            // Retrieve options using model binding
            var options = BindOptions(parseResult);

            var arcService = context.GetService<IArcServices>();
            var result = await arcService.QuickDeployAksEdgeEssentialsAsync(
                options.ClusterName!,
                options.ResourceGroupName!,
                options.SubscriptionId!,
                options.TenantId!,
                options.Location!,
                options.UserProvidedPath!);

            context.Response.Status = result.Success ? 200 : 500;
            context.Response.Message = result.Steps;

            return context.Response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to perform quick deployment of AKS Edge Essentials");
            context.Response.Status = 500;
            context.Response.Message = ex.Message;
            return context.Response;
        }
    }
}
