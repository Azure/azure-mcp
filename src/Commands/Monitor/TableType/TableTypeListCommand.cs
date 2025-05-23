using AzureMcp.Arguments.Monitor.TableType;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Monitor.TableType;

public sealed class TableTypeListCommand(ILogger<TableTypeListCommand> logger) : BaseMonitorCommand<TableTypeListArguments>()
{
    private const string _commandTitle = "List Log Analytics Table Types";
    private readonly ILogger<TableTypeListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        "List available table types in a Log Analytics workspace. Returns table type names.";

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_resourceGroupOption); // inherited from base
    }

    protected override TableTypeListArguments BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        return args;
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
