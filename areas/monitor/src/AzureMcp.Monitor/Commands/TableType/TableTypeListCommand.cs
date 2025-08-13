// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.Monitor.Options.TableType;
using AzureMcp.Monitor.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Monitor.Commands.TableType;

public sealed class TableTypeListCommand(ILogger<TableTypeListCommand> logger) : BaseMonitorCommand<TableTypeListOptions>()
{
    private const string CommandTitle = "List Log Analytics Table Types";
    private readonly ILogger<TableTypeListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        "List available table types in a Log Analytics workspace. Returns table type names.";

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_resourceGroupOption); // inherited from base
    }

    protected override TableTypeListOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        return options;
    }

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            var monitorService = context.GetService<IMonitorService>();
            var tableTypes = await monitorService.ListTableTypes(
                options.Subscription!,
                options.ResourceGroup!,
                options.Workspace!,
                options.Tenant,
                options.RetryPolicy);

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
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record TableTypeListCommandResult(List<string> TableTypes);
}
