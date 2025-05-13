using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Arguments.Monitor.TableType;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.Monitor.TableType;

public sealed class TableTypeListCommand(ILogger<TableTypeListCommand> logger) : BaseMonitorCommand<TableTypeListArguments>()
{
    private readonly ILogger<TableTypeListCommand> _logger = logger;

    protected override string GetCommandName() => "list";

    protected override string GetCommandDescription() =>
        "List available table types in a Log Analytics workspace. Returns table type names.";

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_resourceGroupOption); // inherited from base
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
    }

    protected override TableTypeListArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        return args;
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
            var tableTypes = await monitorService.ListTableTypes(
                args.Subscription!,
                args.ResourceGroup!,
                args.Workspace!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = tableTypes?.Count > 0 ?
                ResponseResult.Create<TableTypeListCommandResult>(
                    new TableTypeListCommandResult(tableTypes),
                    MonitorJsonContext.Default.TableTypeListCommandResult // Changed to match the expected type
                ) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing table types.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record TableTypeListCommandResult(List<string> TableTypes);
}
