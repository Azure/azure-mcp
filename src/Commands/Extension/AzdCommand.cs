// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Runtime.InteropServices;
using AzureMcp.Arguments.Extension;
using AzureMcp.Helpers;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.Extension;

public sealed class AzdCommand(ILogger<AzdCommand> logger, int processTimeoutSeconds = 300) : GlobalCommand<AzdArguments>()
{
    private readonly ILogger<AzdCommand> _logger = logger;
    private readonly int _processTimeoutSeconds = processTimeoutSeconds;
    private readonly Option<string> _commandOption = ArgumentDefinitions.Extension.Azd.Command.ToOption();
    private readonly Option<string> _cwdOption = ArgumentDefinitions.Extension.Azd.Cwd.ToOption();
    private readonly Option<string> _environmentOption = ArgumentDefinitions.Extension.Azd.Environment.ToOption();
    private readonly Option<bool> _learnOption = ArgumentDefinitions.Extension.Azd.Learn.ToOption();
    private static string? _cachedAzdPath;

    private readonly IEnumerable<string> longRunningCommands =
    [
        "provision",
        "package",
        "deploy",
        "up",
        "down",
    ];

    private static readonly string _bestPracticesText = LoadBestPracticesText();

    private static string LoadBestPracticesText()
    {
        var assembly = typeof(AzdCommand).Assembly;
        const string resourceName = "AzureMcp.Resources.azd-best-practices.txt";
        return EmbeddedResourceHelper.ReadEmbeddedResource(assembly, resourceName);
    }

    private static readonly string[] AzdCliPaths =
    [
        // Windows
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData", "Local", "Programs", "Azure Dev CLI"),
        // Linux and MacOS
        Path.Combine("usr", "local", "bin"),
    ];

    protected override string GetCommandName() => "azd";

    protected override string GetCommandDescription() =>
        """
        Runs Azure Developer CLI (azd) commands.
        Agents and LLM's must always run this tool with the 'learn' parameter and empty 'command' on first use to learn more about 'azd' best practices and usage patterns.

        This tool supports the following:
        - List, search and show templates to start your project
        - Create and initialize new projects and templates
        - Show and manage azd configuration
        - Show and manage environments and values
        - Provision Azure resources
        - Deploy applications
        - Bring the whole project up and online
        - Bring the whole project down and deallocate all Azure resources
        - Setup CI/CD pipelines
        - Monitor Azure applications
        - Show information about the project and its resources
        - Show and manage extensions and extension sources
        - Show and manage templates and template sources

        If unsure about available commands or their parameters, run azd help or azd <group> --help in the command to discover them.
        """;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_commandOption);
        command.AddOption(_cwdOption);
        command.AddOption(_environmentOption);
        command.AddOption(_learnOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        foreach (var arg in CreateArguments())
        {
            AddArgument(arg);
        }
    }

    private static ArgumentBuilder<AzdArguments>[] CreateArguments() =>
        [
            ArgumentBuilder<AzdArguments>
                .Create(ArgumentDefinitions.Extension.Azd.Command.Name, ArgumentDefinitions.Extension.Azd.Command.Description)
                .WithValueAccessor(args => args.Command ?? string.Empty)
                .WithIsRequired(ArgumentDefinitions.Extension.Azd.Command.Required),

            ArgumentBuilder<AzdArguments>
                .Create(ArgumentDefinitions.Extension.Azd.Cwd.Name, ArgumentDefinitions.Extension.Azd.Cwd.Description)
                .WithValueAccessor(args => args.Cwd ?? string.Empty)
                .WithIsRequired(ArgumentDefinitions.Extension.Azd.Cwd.Required),

            ArgumentBuilder<AzdArguments>
                .Create(ArgumentDefinitions.Extension.Azd.Environment.Name, ArgumentDefinitions.Extension.Azd.Environment.Description)
                .WithValueAccessor(args => args.Environment ?? string.Empty)
                .WithIsRequired(ArgumentDefinitions.Extension.Azd.Environment.Required),

            ArgumentBuilder<AzdArguments>
                .Create(ArgumentDefinitions.Extension.Azd.Learn.Name, ArgumentDefinitions.Extension.Azd.Learn.Description)
                .WithValueAccessor(args => args.Learn.ToString())
                .WithIsRequired(ArgumentDefinitions.Extension.Azd.Learn.Required),
        ];

    protected override AzdArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Command = parseResult.GetValueForOption(_commandOption);
        args.Cwd = parseResult.GetValueForOption(_cwdOption);
        args.Environment = parseResult.GetValueForOption(_environmentOption);
        args.Learn = parseResult.GetValueForOption(_learnOption);

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

            // If the agent is asking for help, return the best practices text
            if (args.Learn && string.IsNullOrWhiteSpace(args.Command))
            {
                context.Response.Message = _bestPracticesText;
                context.Response.Status = 200;
                return context.Response;
            }

            ArgumentNullException.ThrowIfNull(args.Command);
            ArgumentNullException.ThrowIfNull(args.Cwd);

            // Check if the command is a long-running command. The command can contain other flags.
            // If is long running command return error message to the user.
            if (longRunningCommands.Any(c => args.Command.StartsWith(c, StringComparison.OrdinalIgnoreCase)))
            {
                var terminalCommand = $"azd {args.Command}";

                if (!args.Command.Contains("--cwd", StringComparison.OrdinalIgnoreCase))
                {
                    terminalCommand += $" --cwd {args.Cwd}";
                }
                if (!args.Command.Contains("-e", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(args.Environment))
                {
                    terminalCommand += $" -e {args.Environment}";
                }

                context.Response.Status = 400;
                context.Response.Message =
                    $"""
                    The requested command is a long-running command and is better suited to be run in a terminal.
                    Invoke the following command in a terminal window instead of using this tool so the user can see incremental progress.

                    ```bash
                    {terminalCommand}
                    ```
                    """;

                return context.Response;
            }

            var command = args.Command;

            command += $" --cwd {args.Cwd}";

            if (args.Environment is not null)
            {
                command += $" -e {args.Environment}";
            }

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

        response.Results = ResponseResult.Create(contentResults, JsonSourceGenerationContext.Default.ListString);

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
                """
                The command requires user input. Prompt the user for the required information.
                - If missing Azure subscription use other tools to query and list available subscriptions for the user to select, then set the subscription ID (UUID) in the azd environment.
                - If missing Azure location, use other tools to query and list available locations for the user to select, then set the location name in the azd environment.
                - To set values in the azd environment use the command 'azd env set' command."
                """
            );
        }
        else if (result.Output.Contains("user denied delete confirmation"))
        {
            contentResults.Add(
                """
                The command requires user confirmation to delete resources. Prompt the user for confirmation before proceeding.
                - If the user confirms, re-run the command with the '--force' flag to bypass the confirmation prompt.
                - To permanently delete the resources include the '--purge` flag
                """
            );
        }
        else
        {
            contentResults.Add(
                """
                The command failed. Rerun the command with the '--help' flag to get more information about the command and its parameters.
                After reviewing the help information, run the command again with updated parameters.
                """
            );
        }

        response.Results = ResponseResult.Create(contentResults, JsonSourceGenerationContext.Default.ListString);

        return response;
    }
}
