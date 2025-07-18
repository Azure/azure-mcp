using AzureMcp.Areas.AzureArc.Services;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Options.Arc;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Threading.Tasks;

namespace AzureMcp.Commands.Arc;

public sealed class ValidateAndInstallSwRequirementCommand : GlobalCommand<ArcConnectOptions>
{
    private const string _commandTitle = "Validate and Install Software Requirements";
    private readonly ILogger<ValidateAndInstallSwRequirementCommand> _logger;

    private readonly Option<string> _pathOption = new Option<string>("--path", "The path to validate and install software requirements") { IsRequired = true };

    public ValidateAndInstallSwRequirementCommand(ILogger<ValidateAndInstallSwRequirementCommand> logger)
    {
        _logger = logger;
    }

    public override string Name => "validate-install-software-requirements";

    public override string Description =>
        "Validates and installs software requirements for AKS Edge. This command checks for required software components and installs them if missing.";

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
        ReadOnly = false,
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

            var arcService = context.GetService<IArcServices>();
            var result = await arcService.ValidateAndInstallSwRequirementAsync(options.UserProvidedPath);

            context.Response.Status = result.Success ? 200 : 500;
            context.Response.Message = result.Steps;

            return context.Response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to validate and install software requirements");
            context.Response.Status = 500;
            context.Response.Message = ex.Message;
            return context.Response;
        }
    }
}
