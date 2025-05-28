// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Monitor;
using AzureMcp.Models.Argument;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Monitor.Table;

public sealed class TableListCommand(ILogger<TableListCommand> logger) : BaseMonitorCommand<TableListArguments>()
{
    private const string _commandTitle = "List Log Analytics Tables";
    private readonly ILogger<TableListCommand> _logger = logger;
    private readonly Option<string> _tableTypeOption = ArgumentDefinitions.Monitor.TableType;

    public override string Name => "list";

    public override string Description =>
        $"""
        List all tables in a Log Analytics workspace. Requires {ArgumentDefinitions.Monitor.WorkspaceIdOrName}.
        Returns table names and schemas that can be used for constructing KQL queries.
        """;

    public override string Title => _commandTitle;

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

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);

        try
        {
            if (!context.Validate(parseResult))

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
        var defaultValue = ArgumentDefinitions.Monitor.TableType.GetDefaultValue() ?? "CustomLog";
        return ArgumentBuilder<TableListArguments>
            .Create(ArgumentDefinitions.Monitor.TableType.Name, ArgumentDefinitions.Monitor.TableType.Description!)
            .WithValueAccessor(args => args.TableType ?? defaultValue)
            .WithDefaultValue(defaultValue)
            .WithIsRequired(ArgumentDefinitions.Monitor.TableType.IsRequired);
    }

    protected override TableListArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.TableType = parseResult.GetValueForOption(_tableTypeOption) ?? ArgumentDefinitions.Monitor.TableType.GetDefaultValue();
        args.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption) ?? ArgumentDefinitions.Common.ResourceGroup.GetDefaultValue();
        return args;
    }

    internal record TableListCommandResult(List<string> Tables);
}
