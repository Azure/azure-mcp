// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Extension;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Runtime.InteropServices;

namespace AzureMcp.Commands.Extension;

public sealed class AzdCommand : GlobalCommand<AzdArguments>
{
    private readonly ILogger<AzdCommand> _logger;
    private readonly int _processTimeoutSeconds;
    private readonly Option<string> _commandOption = ArgumentDefinitions.Extension.Azd.Command.ToOption();
    private static string? _cachedAzdPath;

    private static readonly string[] AzdCliPaths =
    [
        // Windows
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData", "Local", "Programs", "Azure Dev CLI"),
        // Linux and MacOS
        Path.Combine("usr", "local", "bin"),
    ];

    public AzdCommand(ILogger<AzdCommand> logger, int processTimeoutSeconds = 300) : base()
    {
        _logger = logger;
        _processTimeoutSeconds = processTimeoutSeconds;
    }

    protected override string GetCommandName() => "azd";

    protected override string GetCommandDescription() =>
        "Use this tool to always run all Azure Developer CLI (azd) commands. " +
        "Most common activities will be helping users initialize projects, manage environments, provision and deploy infrastructure and apps to their Azure environment. " +
        "If an azure.yaml file exists in the workspace, treat it as an existing azd project; otherwise, use init to create one. " +
        "Always pass --cwd with the full workspace path, and use -e to specify the environment when required. Confirm with the user before performing destructive operations. " +
        "After each command, suggest next steps if available, and ask before proceeding. On errors, prompt for missing info and retry. " +
        "Be concise and contextual, using data from the user's environment and workspace to provide accurate, actionable responses. " +
        "This tool can create, modify or delete resources in Azure. Always warn and confirm action with the user before performing destructive commands like 'up', 'down', 'provision' or 'deploy'.";

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_commandOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateCommandArgument());
    }

    private static ArgumentBuilder<AzdArguments> CreateCommandArgument() =>
        ArgumentBuilder<AzdArguments>
            .Create(ArgumentDefinitions.Extension.Azd.Command.Name, ArgumentDefinitions.Extension.Azd.Command.Description)
            .WithValueAccessor(args => args.Command ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.Extension.Azd.Command.Required);

    protected override AzdArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Command = parseResult.GetValueForOption(_commandOption);
        return args;
    }

    [McpServerTool(Destructive = true, ReadOnly = false)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);

        try
        {
            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            ArgumentNullException.ThrowIfNull(args.Command);
            var command = args.Command;
            // We need to always pass the --no-prompt flag to avoid prompting for user input and getting the process stuck
            command += " --no-prompt";

            var processService = context.GetService<IExternalProcessService>();
            processService.SetEnvironmentVariables(new Dictionary<string, string>
            {
                ["AZURE_DEV_USER_AGENT"] = BaseAzureService.DefaultUserAgent,
            });

            var azdPath = FindAzdCliPath() ?? throw new FileNotFoundException("Azure Developer CLI executable not found in PATH or common installation locations. Please ensure Azure Developer CLI is installed.");
            var result = await processService.ExecuteAsync(azdPath, command, _processTimeoutSeconds);

            if (string.IsNullOrWhiteSpace(result.Error) && result.ExitCode == 0)
            {
                return HandleSuccess(result, command, context.Response);
            }
            else
            {
                return HandleError(result, context.Response);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred executing command. Command: {Command}.", args.Command);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    private static string? FindAzdCliPath()
    {
        // Return cached path if available and still exists
        if (!string.IsNullOrEmpty(_cachedAzdPath) && File.Exists(_cachedAzdPath))
        {
            return _cachedAzdPath;
        }

        var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        // Add PATH environment directories followed by the common installation locations
        // This will capture any custom AZD installations as well as standard installations.
        var searchPaths = new List<string>();
        if (Environment.GetEnvironmentVariable("PATH")?.Split(Path.PathSeparator) is { } pathDirs)
        {
            searchPaths.AddRange(pathDirs);
        }

        searchPaths.AddRange(AzdCliPaths);

        foreach (var dir in searchPaths.Where(d => !string.IsNullOrEmpty(d)))
        {
            if (isWindows)
            {
                var cmdPath = Path.Combine(dir, "azd.exe");
                if (File.Exists(cmdPath))
                {
                    _cachedAzdPath = cmdPath;
                    return cmdPath;
                }
            }
            else
            {
                var fullPath = Path.Combine(dir, "azd");
                if (File.Exists(fullPath))
                {
                    _cachedAzdPath = fullPath;
                    return fullPath;
                }
            }
        }

        return null;
    }

    private static CommandResponse HandleSuccess(ProcessResult result, string command, CommandResponse response)
    {
        var contentResults = new List<string>();
        if (!string.IsNullOrWhiteSpace(result.Output))
        {
            contentResults.Add(result.Output);
        }

        var commandArgs = command.Split(' ');

        // Help the user decide what the next steps might be based on the command they just executed
        switch (commandArgs[0])
        {
            case "init":
                contentResults.Add(
                    "Most common commands after initializing a template are the following:\n" +
                    "- 'azd up': Provision and deploy the application.\n" +
                    "- 'azd provision': Provision the infrastructure resources.\n" +
                    "- 'azd deploy': Deploy the application code to the Azure infrastructure.\n"
                );
                break;

            case "provision":
                contentResults.Add(
                    "Most common commands after provisioning the application are the following:\n" +
                    "- 'azd deploy': Deploy the application code to the Azure infrastructure.\n"
                );
                break;

            case "deploy":
                contentResults.Add(
                    "Most common commands after deploying the application are the following:\n" +
                    "- 'azd monitor': Allows the user to monitor the running application and its resources.\n" +
                    "- 'azd show': Displays the current state of the application and its resources.\n" +
                    "- 'azd pipeline config': Allows the user to configure a CI/CD pipeline for the application.\n"
                );
                break;

            case "up":
                contentResults.Add(
                    "Most common commands after provisioning the application are the following:\n" +
                    "- 'azd pipeline' config: Allows the user to configure a CI/CD pipeline for the application.\n" +
                    "- 'azd monitor': Allows the user to monitor the running application and its resources.\n"
                );
                break;
        }

        response.Results = contentResults;

        return response;
    }

    private static CommandResponse HandleError(ProcessResult result, CommandResponse response)
    {
        response.Status = 500;
        response.Message = result.Error;

        var contentResults = new List<string>
            {
                result.Output
            };

        // Check for specific error messages and provide contextual help
        if (result.Output.Contains("ERROR: no project exists"))
        {
            contentResults.Add(
                "An azd project is required to run this command. Create a new project by calling 'azd init'"
            );
        }
        else if (result.Output.Contains("ERROR: infrastructure has not been provisioned."))
        {
            contentResults.Add(
                "The azd project has not been provisioned yet. Run 'azd provision' to create the Azure infrastructure resources."
            );
        }
        else if (result.Output.Contains("not logged in") || result.Output.Contains("fetching current principal"))
        {
            contentResults.Add(
                "User is not logged in our auth token is expired. Login by calling 'azd auth login'."
            );
        }
        else if (result.Output.Contains("ERROR: no environment exists"))
        {
            contentResults.Add(
                "An azd environment is required to run this command. Create a new environment by calling 'azd env new'\n" +
                "- Prompt the user for the environment name if needed.\n"
            );
        }
        else if (result.Output.Contains("no default response for prompt"))
        {
            contentResults.Add(
                "The command requires user input. Prompt the user for the required information.\n" +
                "- If missing Azure subscription use other tools to query and list available subscriptions for the user to select, then set the subscription ID (UUID) in the azd environment.\n" +
                "- If missing Azure location, use other tools to query and list available locations for the user to select, then set the location name in the azd environment.\n" +
                "- To set values in the azd environment use the command 'azd env set' command."
            );
        }
        else if (result.Output.Contains("user denied delete confirmation"))
        {
            contentResults.Add(
                "The command requires user confirmation to delete resources. Prompt the user for confirmation before proceeding.\n" +
                "- If the user confirms, re-run the command with the '--force' flag to bypass the confirmation prompt.\n" +
                "- To permanently delete the resources include the '--purge` flag\n"
            );
        }

        response.Results = contentResults;

        return response;
    }
}