// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Models.Option;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.Monitor.Options;
using AzureMcp.Monitor.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Monitor.Commands.Table;

public sealed class TableListCommand(ILogger<TableListCommand> logger) : BaseMonitorCommand<TableListOptions>()
{
    private const string CommandTitle = "List Log Analytics Tables";
    private readonly ILogger<TableListCommand> _logger = logger;
    private readonly Option<string> _tableTypeOption = MonitorOptionDefinitions.TableType;

    public override string Name => "list";

    public override string Description =>
        $"""
        List all tables in a Log Analytics workspace. Requires {WorkspaceOptionDefinitions.WorkspaceIdOrName}.
        Returns table names and schemas that can be used for constructing KQL queries.
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_tableTypeOption);
        command.AddOption(_resourceGroupOption);
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

            context.Activity?.WithSubscriptionTag(options);

            var monitorService = context.GetService<IMonitorService>();
            var tables = await monitorService.ListTables(
                options.Subscription!,
                options.ResourceGroup!,
                options.Workspace!,
                options.TableType,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = tables?.Count > 0 ?
                ResponseResult.Create(new TableListCommandResult(tables), MonitorJsonContext.Default.TableListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing tables.");
            HandleException(context, ex);
        }

        return context.Response;
    }

    protected override TableListOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.TableType = parseResult.GetValueForOption(_tableTypeOption) ?? MonitorOptionDefinitions.TableType.GetDefaultValue();
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption) ?? OptionDefinitions.Common.ResourceGroup.GetDefaultValue();
        return options;
    }

    internal record TableListCommandResult(List<string> Tables);
}
