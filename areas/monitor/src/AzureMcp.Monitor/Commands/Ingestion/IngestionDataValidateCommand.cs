// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Nodes;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Commands.Subscription;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.Monitor.Options;
using AzureMcp.Monitor.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Monitor.Commands.Ingestion;

public sealed class IngestionDataValidateCommand(ILogger<IngestionDataValidateCommand> logger) : SubscriptionCommand<IngestionDataValidateOptions>()
{
    private const string CommandTitle = "Validate Log Data for Azure Monitor Ingestion";
    private readonly ILogger<IngestionDataValidateCommand> _logger = logger;
    private readonly Option<string> _dataCollectionRuleOption = MonitorOptionDefinitions.Ingestion.DataCollectionRule;
    private readonly Option<string> _logDataOption = MonitorOptionDefinitions.Ingestion.LogData;

    public override string Name => "data-validate";

    public override string Description =>
        $"""
        Validate custom log data before uploading to Azure Monitor workspaces. This tool checks JSON format, structure requirements, and Azure Monitor compliance to prevent ingestion errors. 
        Requires {MonitorOptionDefinitions.DataCollectionRuleName} and {MonitorOptionDefinitions.LogDataName} parameters.
        Returns validation status, error details, and recommendations for data correction.
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_dataCollectionRuleOption);
        command.AddOption(_logDataOption);

        // Add validation for required options
        command.AddValidator(result =>
        {
            var dataCollectionRule = result.GetValueForOption(_dataCollectionRuleOption);
            var logData = result.GetValueForOption(_logDataOption);

            var missingOptions = new List<string>();

            if (string.IsNullOrEmpty(dataCollectionRule))
            {
                missingOptions.Add("--data-collection-rule");
            }

            if (string.IsNullOrEmpty(logData))
            {
                missingOptions.Add("--log-data");
            }

            if (missingOptions.Count > 0)
            {
                result.ErrorMessage = $"Missing required options: {string.Join(", ", missingOptions)}";
            }
        });
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
            var result = await monitorService.ValidateLogData(
                options.DataCollectionRule!,
                options.LogData!,
                options.Tenant,
                options.RetryPolicy);

            var commandResult = new IngestionDataValidateCommandResult(result.Status, result.Message, result.ValidationResults);
            context.Response.Results = ResponseResult.Create(commandResult, MonitorJsonContext.Default.IngestionDataValidateCommandResult);

            // Set the response message from the service result
            context.Response.Message = result.Message;

            // Set status based on validation result - keep 200 for all validation results since the command succeeded
            // The actual validation status is in the result object
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing ingestion data validation command for DCR {DataCollectionRule}",
                options.DataCollectionRule);
            HandleException(context, ex);
        }

        return context.Response;
    }

    protected override IngestionDataValidateOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult); // This calls the base class which sets retry policy
        options.DataCollectionRule = parseResult.GetValueForOption(_dataCollectionRuleOption);
        options.LogData = parseResult.GetValueForOption(_logDataOption);
        return options;
    }

    internal record IngestionDataValidateCommandResult(string Status, string Message, JsonNode? ValidationResults);
}
