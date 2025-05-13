// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Models;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.Tools;

[HiddenCommand]
public sealed class ToolsListCommand(ILogger<ToolsListCommand> logger) : BaseCommand()
{
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

            context.Response.Results = ResponseResult.Create(tools, ModelsJsonContext.Default.ListCommandInfo);
            return context.Response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception occurred processing tool.");
            HandleException(context.Response, ex);

            return context.Response;
        }
    }

    private static CommandInfo CreateCommand(string tokenizedName, IBaseCommand command)
    {
        var commandDetails = command.GetCommand();

        var argumentInfos = commandDetails.Options?
            .Where(arg => !arg.IsHidden)
            .Select(arg => new ArgumentInfo(
                name: arg.Name,
                description: arg.Description!,
                required: arg.IsRequired))
            .ToList();

        return new CommandInfo
        {
            Name = commandDetails.Name,
            Description = commandDetails.Description ?? string.Empty,
            Command = tokenizedName.Replace(CommandFactory.Separator, ' '),
            Arguments = argumentInfos,
        };
    }
}
