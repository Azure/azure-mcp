using AzureMcp.Areas.AzureArc.Services;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Options.Arc;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Threading.Tasks;

namespace AzureMcp.Commands.Arc;

public sealed class OnboardClusterToArcCommand : GlobalCommand<ArcConnectOptions>
{
    private const string _commandTitle = "Onboards existing cluster to Azure Arc";
    private readonly ILogger<OnboardClusterToArcCommand> _logger;

    // Define options as fields
    private readonly Option<string> _clusterNameOption = new("--cluster-name", "Name of the cluster to connect to Azure Arc");
    private readonly Option<string> _resourceGroupNameOption = new("--resource-group-name", "Name of the resource group");
    private readonly Option<string> _locationOption = new("--location", "Azure region");
    private readonly Option<string> _subscriptionIdOption = new("--subscription-id", "Azure subscription ID");
    private readonly Option<string> _tenantIdOption = new("--tenant-id", "Azure tenant ID");
    private readonly Option<string> _kubeConfigPathOption = new("--kube-config-path", "Path to the kubeconfig file");
    private readonly Option<string> _userProvidedPathOption = new("--user-provided-path", "Path to the user-provided files");

    public OnboardClusterToArcCommand(ILogger<OnboardClusterToArcCommand> logger)
    {
        _logger = logger;
    }

    public override string Name => "onboard-cluster-to-arc";

    public override string Description =>
        "Onboards or connects an existing cluster to Azure Arc by validating the cluster and executing the necessary commands.The --user-provided-path option is required.";

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);

        // Add options to the command
        command.AddOption(_clusterNameOption);
        command.AddOption(_resourceGroupNameOption);
        command.AddOption(_locationOption);
        command.AddOption(_subscriptionIdOption);
        command.AddOption(_tenantIdOption);
        command.AddOption(_kubeConfigPathOption);
        command.AddOption(_userProvidedPathOption);
    }

    protected override ArcConnectOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);

        // Use the defined Option instances
        options.ClusterName = parseResult.GetValueForOption(_clusterNameOption);
        options.ResourceGroupName = parseResult.GetValueForOption(_resourceGroupNameOption);
        options.Location = parseResult.GetValueForOption(_locationOption);
        options.SubscriptionId = parseResult.GetValueForOption(_subscriptionIdOption);
        options.TenantId = parseResult.GetValueForOption(_tenantIdOption);
        options.KubeConfigPath = parseResult.GetValueForOption(_kubeConfigPathOption);
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
            var result = await arcService.OnboardClusterToArcAsync(
                options.ClusterName!,
                options.ResourceGroupName!,
                options.Location!,
                options.SubscriptionId!,
                options.TenantId!,
                options.KubeConfigPath!,
                options.UserProvidedPath!);

            context.Response.Status = result.Success ? 200 : 500;
            context.Response.Message = result.Steps;

            return context.Response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to onboard cluster to Azure Arc");
            context.Response.Status = 500;
            context.Response.Message = $"Failed to onboard cluster to Azure Arc: {ex.Message}";
            return context.Response;
        }
    }
}
