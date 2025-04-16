// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments;
using AzureMcp.Models;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.CommandLine.Parsing;

namespace AzureMcp.Commands.Tools;

[HiddenCommand]
public sealed class ToolsListCommand : BaseCommand
{
    private readonly ILogger<ToolsListCommand> _logger;

    public ToolsListCommand(ILogger<ToolsListCommand> logger) : base()
    {
        _logger = logger;
    }

    protected override string GetCommandName() => "list";

    protected override string GetCommandDescription() =>
        """
        List all available commands and their tools in a hierarchical structure. This command returns detailed information
        about each command, including its name, description, full command path, available subcommands, and all supported 
        arguments. Use this to explore the CLI's functionality or to build interactive command interfaces.
        """;

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        try
        {
            var factory = context.GetService<CommandFactory>();
            var tools = await Task.Run(() => CommandFactory.GetVisibleCommands(factory.AllCommands)
                .Select(kvp => CreateCommand(kvp.Key, kvp.Value))
                .ToList());

            context.Response.Results = tools;
            return context.Response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred processing tool.");
            HandleException(context.Response, ex);

            return context.Response;
        }
    }

    private static CommandInfo CreateCommand(string hyphenatedName, IBaseCommand command)
    {
        var argumentInfos = command.GetArguments()
            ?.Select(arg => new ArgumentInfo(
                name: arg.Name,
                description: arg.Description,
                value: string.Empty,
                defaultValue: arg is ArgumentBuilder<GlobalArguments> args ? args.DefaultValue : null,
                suggestedValues: arg is ArgumentBuilder<GlobalArguments> argsWithValues ? argsWithValues.SuggestedValues : null,
                required: arg.Required))
            .ToList();

        return new CommandInfo
        {
            Name = command.GetCommand().Name,
            Description = command.GetCommand().Description ?? string.Empty,
            Command = hyphenatedName.Replace('-', ' '),
            Arguments = argumentInfos,
        };
    }
}