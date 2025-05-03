// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Arguments.Monitor;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.Monitor.Table;

public sealed class TableListCommand(ILogger<TableListCommand> logger) : BaseMonitorCommand<TableListArguments>()
{
    private readonly ILogger<TableListCommand> _logger = logger;
    private readonly Option<string> _tableTypeOption = ArgumentDefinitions.Monitor.TableType.ToOption();

    protected override string GetCommandName() => "list";

    protected override string GetCommandDescription() =>
        $"""
        List all tables in a Log Analytics workspace. Requires {ArgumentDefinitions.Monitor.WorkspaceIdOrName}.
        Returns table names and schemas that can be used for constructing KQL queries.
        """;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_tableTypeOption);
        command.AddOption(_resourceGroupOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateTableTypeArgument());
        AddArgument(CreateResourceGroupArgument());
    }

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);

        try
        {
            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            var monitorService = context.GetService<IMonitorService>();
            var tables = await monitorService.ListTables(
                args.Subscription!,
                args.ResourceGroup!,
                args.Workspace!,
                args.TableType,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = tables?.Count > 0 ?
                ResponseResult.Create(new TableListCommandResult(tables), MonitorJsonContext.Default.TableListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing tables.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    private static ArgumentBuilder<TableListArguments> CreateTableTypeArgument()
    {
        var defaultValue = ArgumentDefinitions.Monitor.TableType.DefaultValue ?? "CustomLog";
        return ArgumentBuilder<TableListArguments>
            .Create(ArgumentDefinitions.Monitor.TableType.Name, ArgumentDefinitions.Monitor.TableType.Description)
            .WithValueAccessor(args => args.TableType ?? defaultValue)
            .WithDefaultValue(defaultValue)
            .WithIsRequired(ArgumentDefinitions.Monitor.TableType.Required);
    }

    protected override TableListArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.TableType = parseResult.GetValueForOption(_tableTypeOption) ?? ArgumentDefinitions.Monitor.TableType.DefaultValue;
        args.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption) ?? ArgumentDefinitions.Common.ResourceGroup.DefaultValue;
        return args;
    }

    internal record TableListCommandResult(List<string> Tables);
}
