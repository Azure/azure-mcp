using AzureMcp.Areas.AzureArc.Services;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Options.Arc;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Threading.Tasks;

namespace AzureMcp.Commands.Arc;

public sealed class ValidateSystemRequirementsAndSetupHyperVCommand : GlobalCommand<ArcConnectOptions>
{
    private const string _commandTitle = "Validate System Requirements and Setup Hyper-V";
    private readonly ILogger<ValidateSystemRequirementsAndSetupHyperVCommand> _logger;

    private readonly Option<string> _pathOption = new Option<string>("--path", "The path to validate system requirements and setup Hyper-V") { IsRequired = true };

    public ValidateSystemRequirementsAndSetupHyperVCommand(ILogger<ValidateSystemRequirementsAndSetupHyperVCommand> logger)
    {
        _logger = logger;
    }

    public override string Name => "validate-system-requirements-hyperv";

    public override string Description =>
        "Validates system requirements and sets up Hyper-V for AKS Edge Essentials or Aksee cluster installation.";

    public override string Title => _commandTitle;

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
        Destructive = false,
        ReadOnly = true,
        Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        try
        {
            var options = BindOptions(parseResult);
            if (string.IsNullOrEmpty(options.UserProvidedPath))
            {
                throw new ArgumentException("The --path option is required.");
            }

            // Informational warning about system restart
            string warningMessage = "Note: The system may restart after Hyper-V installation.";
            _logger.LogInformation(warningMessage);

            var arcService = context.GetService<IArcServices>();
            var result = await arcService.ValidateSystemRequirementsAndSetupHyperVAsync(options.UserProvidedPath);

            context.Response.Status = result.Success ? 200 : 500;
            context.Response.Message = $"{warningMessage}\n{result.Steps}";

            return context.Response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to validate system requirements and setup Hyper-V");
            context.Response.Status = 500;
            context.Response.Message = ex.Message;
            return context.Response;
        }
    }
}
