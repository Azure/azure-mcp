// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Extension;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Runtime.InteropServices;

namespace AzureMcp.Commands.Extension;

public sealed class AzCommand(ILogger<AzCommand> logger, int processTimeoutSeconds = 300) : GlobalCommand<AzArguments>()
{
    private readonly ILogger<AzCommand> _logger = logger;
    private readonly int _processTimeoutSeconds = processTimeoutSeconds;
    private readonly Option<string> _commandOption = ArgumentDefinitions.Extension.Az.Command.ToOption();
    private static string? _cachedAzPath;


    protected override string GetCommandName() => "az";

    protected override string GetCommandDescription() =>
        """
Your job is to answer questions about an Azure environment by executing Azure CLI commands. You have the following rules:

- Use the Azure CLI to manage Azure resources and services. Do not use any other tool.
- Provide a valid Azure CLI command. For example: 'group list'.
- When deleting or modifying resources, ALWAYS request user confirmation.
- If a command fails, retry 3 times before giving up with an improved version of the code based on the returned feedback.
- When listing resources, ensure pagination is handled correctly so that all resources are returned.
- You can ONLY write code that interacts with Azure. It CANNOT generate charts, tables, graphs, etc.
- You can delete or modify resources in your Azure environment. Always be cautious and include appropriate warnings when providing commands to users.
- Be concise, professional and to the point. Do not give generic advice, always reply with detailed & contextual data sourced from the current Azure environment.
""";

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

    private static ArgumentBuilder<AzArguments> CreateCommandArgument() =>
        ArgumentBuilder<AzArguments>
            .Create(ArgumentDefinitions.Extension.Az.Command.Name, ArgumentDefinitions.Extension.Az.Command.Description)
            .WithValueAccessor(args => args.Command ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.Extension.Az.Command.Required);

    protected override AzArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Command = parseResult.GetValueForOption(_commandOption);
        return args;
    }

    private static string? FindAzCliPath()
    {
        string executableName = "az";

        // Return cached path if available and still exists
        if (!string.IsNullOrEmpty(_cachedAzPath) && File.Exists(_cachedAzPath))
        {
            return _cachedAzPath;
        }

        var pathEnv = Environment.GetEnvironmentVariable("PATH");
        if (string.IsNullOrEmpty(pathEnv))
            return null;

        string[] paths = pathEnv.Split(Path.PathSeparator);
        foreach (string path in paths)
        {
            string fullPath = Path.Combine(path.Trim(), executableName);
            if (File.Exists(fullPath))
            {
                var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
                if (isWindows)
                {
                    string exePath = Path.ChangeExtension(fullPath, ".cmd");
                    if (File.Exists(exePath))
                    {
                        _cachedAzPath = exePath;
                        return _cachedAzPath;
                    }
                    string batPath = Path.ChangeExtension(fullPath, ".bat");
                    if (File.Exists(batPath))
                    {
                        _cachedAzPath = batPath;
                        return _cachedAzPath;
                    }
                }
                _cachedAzPath = fullPath;
                return _cachedAzPath;
            }
        }
        return null;
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
            var processService = context.GetService<IExternalProcessService>();

            var azPath = FindAzCliPath() ?? throw new FileNotFoundException("Azure CLI executable not found in PATH or common installation locations. Please ensure Azure CLI is installed.");
            var result = await processService.ExecuteAsync(azPath, command, _processTimeoutSeconds);

            if (result.ExitCode != 0)
            {
                context.Response.Status = 500;
                context.Response.Message = result.Error;
            }

            context.Response.Results = processService.ParseJsonOutput(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred executing command. Command: {Command}.", args.Command);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
}