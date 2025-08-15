// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.Monitor.Options;
using AzureMcp.Monitor.Services;
using Microsoft.Extensions.Logging;
using System.Text.Json.Nodes;

namespace AzureMcp.Monitor.Commands.Ingestion;

public sealed class IngestionStatusCheckCommand(ILogger<IngestionStatusCheckCommand> logger) : BaseMonitorCommand<IngestionStatusCheckOptions>()
{
    private const string CommandTitle = "Check Status of Azure Monitor Log Ingestion Operations";
    private readonly ILogger<IngestionStatusCheckCommand> _logger = logger;
    private readonly Option<string> _dataCollectionRuleOption = MonitorOptionDefinitions.Ingestion.DataCollectionRule;
    private readonly Option<string> _operationIdOption = MonitorOptionDefinitions.Ingestion.OperationId;

    public override string Name => "azmcp-monitor-ingestion-status-check";

    public override string Description => """
        Check the status of Azure Monitor log ingestion operations for a specific data collection rule or operation ID.
        
        Requires {WorkspaceOptionDefinitions.WorkspaceIdOrName} and {MonitorOptionDefinitions.DataCollectionRuleName} parameters.
        Optional {MonitorOptionDefinitions.OperationIdName} parameter to check specific operation status.
        Returns ingestion operation status and details.
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_dataCollectionRuleOption);
        command.AddOption(_operationIdOption);
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
            var result = await monitorService.CheckIngestionStatus(
                options.Workspace!,
                options.DataCollectionRule!,
                options.OperationId,
                options.Tenant,
                options.RetryPolicy);

            var commandResult = new IngestionStatusCheckCommandResult(result.Status, result.Message, result.Details);
            context.Response.Results = ResponseResult.Create(commandResult, MonitorJsonContext.Default.IngestionStatusCheckCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking ingestion status for DCR {DataCollectionRule}", options.DataCollectionRule);
            HandleException(context, ex);
        }

        return context.Response;
    }

    protected override IngestionStatusCheckOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.DataCollectionRule = parseResult.GetValueForOption(_dataCollectionRuleOption);
        options.OperationId = parseResult.GetValueForOption(_operationIdOption);
        return options;
    }

    internal record IngestionStatusCheckCommandResult(string Status, string Message, JsonNode? Details);
}
