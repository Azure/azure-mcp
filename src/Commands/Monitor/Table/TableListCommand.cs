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

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindOptions(parseResult);

        try
        {
            var validationResult = Validate(parseResult.CommandResult);

            if (!validationResult.IsValid)
            {
                context.Response.Status = 400;
                context.Response.Message = validationResult.ErrorMessage!;
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

    protected override TableListArguments BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.TableType = parseResult.GetValueForOption(_tableTypeOption) ?? ArgumentDefinitions.Monitor.TableType.GetDefaultValue();
        args.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption) ?? ArgumentDefinitions.Common.ResourceGroup.GetDefaultValue();
        return args;
    }

    internal record TableListCommandResult(List<string> Tables);
}
