using AzureMcp.Models.Command;
using AzureMcp.Models.Option;
using AzureMcp.Options.Aks.Kubectl;
using AzureMcp.Services.Interfaces;
using AzureMcp.Services.ProcessExecution;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Aks;

public sealed class KubectlCommand(ILogger<KubectlCommand> logger, int processTimeoutSeconds = 300) : GlobalCommand<KubectlOptions>
{
    private const string CommandTitle = "kubectl Command";
    private readonly ILogger<KubectlCommand> _logger = logger;
    private readonly int _timeoutSeconds = processTimeoutSeconds; private readonly Option<string> _commandOption = OptionDefinitions.Aks.Command;
    private readonly Option<string> _kubeConfigOption = OptionDefinitions.Aks.KubeConfig;
    private readonly Option<bool> _autoInstallKubectlOption = OptionDefinitions.Aks.AutoInstallKubectl;

    public override string Name => "kubectl";

    public override string Description => "Execute kubectl commands against a cluster.";

    public override string Title => CommandTitle; protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_commandOption);
        command.AddOption(_kubeConfigOption);
        command.AddOption(_autoInstallKubectlOption);
    }
    protected override KubectlOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Command = parseResult.GetValueForOption(_commandOption);
        options.KubeConfig = parseResult.GetValueForOption(_kubeConfigOption);
        options.AutoInstallKubectl = parseResult.GetValueForOption(_autoInstallKubectlOption);
        return options;
    }

    [McpServerTool(Destructive = true, ReadOnly = false, Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);
        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            ArgumentNullException.ThrowIfNull(options.Command);
            ArgumentNullException.ThrowIfNull(options.KubeConfig);
            var processService = context.GetService<IExternalProcessService>();
            var toolInstallationService = context.GetService<IToolInstallationService>();

            // First, check if kubectl is already available
            var existingKubectlPath = await toolInstallationService.FindKubectlAsync();

            string kubectlPath;
            if (existingKubectlPath != null)
            {
                kubectlPath = existingKubectlPath;
                _logger.LogDebug("Using existing kubectl at: {KubectlPath}", kubectlPath);
            }
            else if (options.AutoInstallKubectl)
            {
                _logger.LogInformation("kubectl not found. Installing kubectl automatically...");
                var installedPath = await toolInstallationService.InstallKubectlAsync();

                if (installedPath == null)
                {
                    context.Response.Status = 404;
                    context.Response.Message = CreateKubectlNotFoundMessage(true);
                    return context.Response;
                }

                kubectlPath = installedPath;
                _logger.LogInformation("kubectl installed and ready to use.");
            }
            else
            {
                context.Response.Status = 404;
                context.Response.Message = CreateKubectlNotFoundMessage(false);
                return context.Response;
            }

            var args = $"--kubeconfig {options.KubeConfig} {options.Command}";
            var result = await processService.ExecuteAsync(kubectlPath, args, _timeoutSeconds);

            if (result.ExitCode != 0)
            {
                context.Response.Status = 500;
                context.Response.Message = result.Error;
            }

            var elem = processService.ParseJsonOutput(result);
            context.Response.Results = ResponseResult.Create(elem, JsonSourceGenerationContext.Default.JsonElement);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing kubectl command.");
            HandleException(context, ex);
        }
        return context.Response;
    }
    private static string CreateKubectlNotFoundMessage(bool autoInstallAttempted)
    {
        var platform = GetPlatformInstallInstructions();

        if (autoInstallAttempted)
        {
            return $"""
                kubectl is not installed or could not be found.
                
                Auto-installation failed. Please install kubectl manually:
                
                {platform}
                
                Alternatively, you can:
                1. Use your system's package manager
                2. Download from https://kubernetes.io/docs/tasks/tools/install-kubectl/
                3. Ensure kubectl is in your PATH environment variable
                
                After installation, please retry the command.
                """;
        }
        else
        {
            return $"""
                kubectl is not installed or could not be found.
                
                Auto-installation is disabled. Please install kubectl manually:
                
                {platform}
                
                Alternatively, you can:
                1. Use your system's package manager
                2. Download from https://kubernetes.io/docs/tasks/tools/install-kubectl/
                3. Ensure kubectl is in your PATH environment variable
                4. Enable auto-installation with --auto-install-kubectl=true
                
                After installation, please retry the command.
                """;
        }
    }

    private static string GetPlatformInstallInstructions()
    {
        if (OperatingSystem.IsWindows())
        {
            return """
                For Windows:
                - Using Chocolatey: choco install kubernetes-cli
                - Using Scoop: scoop install kubectl
                - Using winget: winget install Kubernetes.kubectl
                """;
        }
        else if (OperatingSystem.IsMacOS())
        {
            return """
                For macOS:
                - Using Homebrew: brew install kubectl
                - Using MacPorts: sudo port install kubectl
                """;
        }
        else if (OperatingSystem.IsLinux())
        {
            return """
                For Linux:
                - Using snap: sudo snap install kubectl --classic
                - Using apt (Ubuntu/Debian): sudo apt-get install kubectl
                - Using yum (RHEL/CentOS): sudo yum install kubectl
                - Using dnf (Fedora): sudo dnf install kubectl
                """;
        }
        else
        {
            return "Please refer to https://kubernetes.io/docs/tasks/tools/install-kubectl/ for installation instructions.";
        }
    }
}
