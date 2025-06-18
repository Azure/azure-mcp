// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Monitor.Commands;
using AzureMcp.Areas.Monitor.Options;
using AzureMcp.Areas.Monitor.Services;
using AzureMcp.Models.Option;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Monitor.Commands.Log;

public sealed class LogQueryCommand(ILogger<LogQueryCommand> logger) : BaseMonitorCommand<LogQueryOptions>()
{
    private const string CommandTitle = "Query Log Analytics Workspace";
    private readonly ILogger<LogQueryCommand> _logger = logger;
    private readonly Option<string> _tableNameOption = MonitorOptionDefinitions.TableName;
    private readonly Option<string> _queryOption = MonitorOptionDefinitions.Query;
    private readonly Option<int> _hoursOption = MonitorOptionDefinitions.Hours;
    private readonly Option<int> _limitOption = MonitorOptionDefinitions.Limit;

    public override string Name => "query";

    public override string Description =>
        $"""
        Execute a KQL query against a Log Analytics workspace. Requires {MonitorOptionDefinitions.WorkspaceIdOrName}
        and resource group. Optional {MonitorOptionDefinitions.HoursName}
        (default: {MonitorOptionDefinitions.Hours.GetDefaultValue()}) and {MonitorOptionDefinitions.LimitName}
        (default: {MonitorOptionDefinitions.Limit.GetDefaultValue()}) parameters.
        The {MonitorOptionDefinitions.QueryTextName} parameter accepts KQL syntax.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_tableNameOption);
        command.AddOption(_queryOption);
        command.AddOption(_hoursOption);
        command.AddOption(_limitOption);
        command.AddOption(_resourceGroupOption);
    }

    [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
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
            var results = await monitorService.QueryLogs(
                options.Subscription!,
                options.Workspace!,
                options.Query!,
                options.TableName!,
                options.Hours,
                options.Limit,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(results, JsonSourceGenerationContext.Default.ListJsonNode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing log query command.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    protected override LogQueryOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.TableName = parseResult.GetValueForOption(_tableNameOption);
        options.Query = parseResult.GetValueForOption(_queryOption);
        options.Hours = parseResult.GetValueForOption(_hoursOption);
        options.Limit = parseResult.GetValueForOption(_limitOption);
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        return options;
    }
}
