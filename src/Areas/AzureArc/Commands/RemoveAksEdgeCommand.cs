using AzureMcp.Areas.AzureArc.Services;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Options.Arc;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Threading.Tasks;

namespace AzureMcp.Commands.Arc;

public sealed class RemoveAksEdgeCommand : GlobalCommand<ArcConnectOptions>
{
    private const string _commandTitle = "Remove installation for existing cluster";
    private readonly ILogger<RemoveAksEdgeCommand> _logger;

    public RemoveAksEdgeCommand(ILogger<RemoveAksEdgeCommand> logger)
    {
        _logger = logger;
    }

    public override string Name => "remove-Aks-Edge-installation";

    public override string Description =>
        "Completely removes AKS Edge Essentials from the machine. Ensures that any existing installation is removed and settings are preset for new installation.";

    public override string Title => _commandTitle;

    private readonly Option<string> _pathOption = new Option<string>("--path", "The path to use for script execution") { IsRequired = true };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_pathOption);
    }

    protected override ArcConnectOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.UserProvidedPath = parseResult.GetValueForOption(_pathOption);
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
            var options = BindOptions(parseResult);
            if (string.IsNullOrEmpty(options.UserProvidedPath))
            {
                throw new ArgumentException("The path parameter is required.");
            }

            _logger.LogInformation("Starting AKS Edge Essentials removal.");

            var arcService = context.GetService<IArcServices>();
            var removalSuccess = await arcService.RemoveAksEdgeAsync(options.UserProvidedPath);

            if (removalSuccess)
            {
                context.Response.Status = 200;
                context.Response.Message = "AKS Edge Essentials removed successfully.";
            }
            else
            {
                context.Response.Status = 500;
                context.Response.Message = "Failed to remove AKS Edge Essentials.";
            }

            return context.Response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to remove AKS Edge Essentials.");
            context.Response.Status = 500;
            context.Response.Message = ex.Message;
            return context.Response;
        }
    }
}
