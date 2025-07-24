// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Aks.Commands.Cluster;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Extension.Commands;

public sealed class ReverseAzCommand
{
    private readonly Dictionary<string, Func<CommandContext, string[], Task<CommandResponse>>> _azCommandHandlers;
    private readonly ILogger<AzCommand> _logger;

    public ReverseAzCommand(ILogger<AzCommand> logger)
    {
        _logger = logger;

        // Initialize command handlers
        _azCommandHandlers = new Dictionary<string, Func<CommandContext, string[], Task<CommandResponse>>>
        {
            ["aks list"] = HandleAksList,
        };
    }

    public async Task<CommandResponse> ExecuteAzCommand(string commandLine, CommandContext context)
    {
        if (string.IsNullOrWhiteSpace(commandLine))
        {
            _logger.LogWarning("Command line is empty or null");
            return CreateErrorResponse($"Empty command line", 500);
        }

        commandLine = commandLine.Trim();

        // Find matching Azure command
        foreach (var (azCommand, handler) in _azCommandHandlers)
        {
            if (commandLine.StartsWith(azCommand, StringComparison.OrdinalIgnoreCase))
            {
                var remainingArgs = commandLine[azCommand.Length..].Trim();
                var args = string.IsNullOrWhiteSpace(remainingArgs)
                    ? Array.Empty<string>()
                    : CommandLineStringSplitter.Instance.Split(remainingArgs).ToArray();

                try
                {
                    var response = await handler(context, args);
                    _logger.LogInformation("Successfully executed Azure command '{AzCommand}'", azCommand);
                    return response;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error executing Azure command '{AzCommand}'", azCommand);
                    return CreateErrorResponse($"Error executing command: {ex.Message}", 500);
                }
            }
        }

        _logger.LogWarning("Azure command '{Command}' not recognized", commandLine);
        return CreateErrorResponse($"Command '{commandLine}' is not recognized", 404);
    }

    private async Task<CommandResponse> HandleAksList(CommandContext context, string[] args)
    {
        var loggerFactory = context.GetService<ILoggerFactory>();
        var logger = loggerFactory?.CreateLogger<ClusterListCommand>()
            ?? throw new InvalidOperationException("Logger not found for ClusterListCommand");
        var command = new ClusterListCommand(logger);
        var systemCommand = command.GetCommand();
        var parseResult = systemCommand.Parse(args);
        return await command.ExecuteAsync(context, parseResult);
    }

    private static CommandResponse CreateErrorResponse(string message, int statusCode)
    {
        return new CommandResponse
        {
            Status = statusCode,
            Message = message,
            Results = null
        };
    }
}
