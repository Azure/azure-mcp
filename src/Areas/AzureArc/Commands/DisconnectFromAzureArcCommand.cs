using AzureMcp.Areas.AzureArc.Services;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Options.Arc;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Threading.Tasks;

namespace AzureMcp.Commands.Arc;

public sealed class DisconnectFromAzureArcCommand : GlobalCommand<ArcConnectOptions>
{
    private const string _commandTitle = "Disconnects a cluster from Azure Arc";
    private readonly ILogger<DisconnectFromAzureArcCommand> _logger;

    // Define options as fields
    private readonly Option<string> _clusterNameOption = new("--cluster-name", "Name of the cluster to disconnect from Azure Arc");
    private readonly Option<string> _resourceGroupNameOption = new("--resource-group-name", "Name of the resource group");
    private readonly Option<string> _userProvidedPathOption = new("--user-provided-path", "Path to the user-provided files");

    public DisconnectFromAzureArcCommand(ILogger<DisconnectFromAzureArcCommand> logger)
    {
        _logger = logger;
    }

    public override string Name => "disconnect-from-azure-arc";

    public override string Description =>
        "Disconnects an arc connected cluster from Azure Arc. It deletes all extensions and removes the cluster from Arc. The --user-provided-path option is required.";

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);

        // Add options to the command
        command.AddOption(_clusterNameOption);
        command.AddOption(_resourceGroupNameOption);
        command.AddOption(_userProvidedPathOption);
    }

    protected override ArcConnectOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);

        // Use the defined Option instances
        options.ClusterName = parseResult.GetValueForOption(_clusterNameOption);
        options.ResourceGroupName = parseResult.GetValueForOption(_resourceGroupNameOption);
        options.UserProvidedPath = parseResult.GetValueForOption(_userProvidedPathOption);

        return options;
    }

    [McpServerTool(
        Destructive = true,
        ReadOnly = false,
        Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        try
        {
            // Retrieve options using model binding
            var options = BindOptions(parseResult);

            var arcService = context.GetService<IArcServices>();
            var result = await arcService.DisconnectFromAzureArcAsync(
                options.ResourceGroupName!,
                options.ClusterName!,
                options.UserProvidedPath!);

            context.Response.Status = result.Success ? 200 : 500;
            context.Response.Message = result.Success
                ? "Cluster successfully disconnected from Azure Arc."
                : "Failed to disconnect cluster from Azure Arc.";

            return context.Response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to disconnect cluster from Azure Arc");
            context.Response.Status = 500;
            context.Response.Message = $"Failed to disconnect cluster from Azure Arc: {ex.Message}";
            return context.Response;
        }
    }
}
