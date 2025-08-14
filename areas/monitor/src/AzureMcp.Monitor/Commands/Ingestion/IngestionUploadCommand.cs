// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.Monitor.Options;
using AzureMcp.Monitor.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Monitor.Commands.Ingestion;

public sealed class IngestionUploadCommand(ILogger<IngestionUploadCommand> logger) : BaseMonitorCommand<IngestionUploadOptions>()
{
    private const string CommandTitle = "Upload Custom Log Data to Azure Monitor";
    private readonly ILogger<IngestionUploadCommand> _logger = logger;
    private readonly Option<string> _dataCollectionRuleOption = MonitorOptionDefinitions.Ingestion.DataCollectionRule;
    private readonly Option<string> _logDataOption = MonitorOptionDefinitions.Ingestion.LogData;
    private readonly Option<string> _streamNameOption = MonitorOptionDefinitions.Ingestion.StreamName;

    public override string Name => "upload";

    public override string Description =>
        $"""
        Upload custom log data to Azure Monitor workspaces using data collection rules. This tool sends structured log data to Azure Monitor with schema validation and transformation support for custom monitoring scenarios. 
        Requires {WorkspaceOptionDefinitions.WorkspaceIdOrName}, {MonitorOptionDefinitions.DataCollectionRuleName}, {MonitorOptionDefinitions.StreamNameName}, and {MonitorOptionDefinitions.LogDataName} parameters.
        Returns ingestion operation status and record count.
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = false };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_dataCollectionRuleOption);
        command.AddOption(_logDataOption);
        command.AddOption(_streamNameOption);
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
            var result = await monitorService.UploadLogs(
                options.Workspace!,
                options.DataCollectionRule!,
                options.StreamName!,
                options.LogData!,
                options.Tenant,
                options.RetryPolicy);

            var commandResult = new IngestionUploadCommandResult(result.Status, result.RecordCount, result.Message);
            context.Response.Results = ResponseResult.Create(commandResult, MonitorJsonContext.Default.IngestionUploadCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing ingestion upload command for workspace {Workspace} with DCR {DataCollectionRule}",
                options.Workspace, options.DataCollectionRule);
            HandleException(context, ex);
        }

        return context.Response;
    }

    protected override IngestionUploadOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.DataCollectionRule = parseResult.GetValueForOption(_dataCollectionRuleOption);
        options.LogData = parseResult.GetValueForOption(_logDataOption);
        options.StreamName = parseResult.GetValueForOption(_streamNameOption);
        return options;
    }

    internal record IngestionUploadCommandResult(string Status, int RecordCount, string Message);
}
