// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.ApplicationInsights.Models;
using AzureMcp.ApplicationInsights.Options;
using AzureMcp.ApplicationInsights.Services;
using AzureMcp.Core.Commands;
using Microsoft.Extensions.Logging;

namespace AzureMcp.ApplicationInsights.Commands;

public sealed class AppCorrelateTimeCommand(ILogger<AppCorrelateTimeCommand> logger)
    : BaseAppCommand<AppCorrelateTimeOptions>
{
    private const string CommandTitle = "App correlate time-series";
    private readonly ILogger<AppCorrelateTimeCommand> _logger = logger;

    public override string Name => "correlate-time";
    public override string Description =>
        $$"""
        Perform a time-series correlation analysis based on a user-reported symptom on an Application Insights resource.

        This tool takes one or more data sets. Each data set consists of a table, filters and splitBy dimensions. The tool will
        construct time series of each data set split by the splitBy dimensions, then correlate the time series to find
        the most likely causes of the symptom.

        Example data sets:

        Determine which result code is contributing to the symptom:
        [
            {
                "table": "requests",
                "filters": [
                    "success=\"false\""
                 ],
                "splitBy": "resultCode"
            }
        ]

        Determine whether a specific exception is contributing to 500 errors:
        [
            {
                "table": "requests",
                "filters": [
                    "resultCode=\"500\""
                ]
            },
            {
                "table": "exceptions",
                "filters": [
                    "type=\"System.InvalidOperationException\""
                ]
            }
        ]

        Determine which operation name is contributing to slow performance:
        [
            {
                "table": "requests",
                "splitBy": "operation_Name",
                "aggregation": "Average"
            },
            {
                "table": "requests",
                "splitBy": "operation_Name",
                "aggregation": "95thPercentile"
            }
        ]

        Use this tool for investigating issues with Application Insights resources.
        Required options:
        - {{_resourceNameOption.Name}}: {{_resourceNameOption.Description}} or {{_resourceIdOption.Name}}: {{_resourceIdOption.Description}}
        - {{_symptomOption.Name}}: {{_symptomOption.Description}}
        - {{_dataSetsOption.Name}}: {{_dataSetsOption.Description}}
        Optional options:
        - {{_resourceGroupOption.Name}}: {{_resourceGroupOption.Description}}
        - {{_startTimeOption.Name}}: {{_startTimeOption.Description}}
        - {{_endTimeOption.Name}}: {{_endTimeOption.Description}}
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    // Define options from OptionDefinitions
    private readonly Option<string> _symptomOption = ApplicationInsightsOptionDefinitions.Symptom;
    private readonly Option<string> _startTimeOption = ApplicationInsightsOptionDefinitions.StartTime;
    private readonly Option<string> _endTimeOption = ApplicationInsightsOptionDefinitions.EndTime;
    private readonly Option<AppCorrelateDataSetParseResult> _dataSetsOption = ApplicationInsightsOptionDefinitions.DataSets;

    protected override void RegisterOptions(Command command)
    {
        command.AddOption(_symptomOption);
        command.AddOption(_startTimeOption);
        command.AddOption(_endTimeOption);
        command.AddOption(_dataSetsOption);
        base.RegisterOptions(command);
    }

    protected override AppCorrelateTimeOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Symptom = parseResult.GetValueForOption(_symptomOption);
        options.StartTime = DateTimeOffset.Parse(parseResult.GetValueForOption(_startTimeOption)!).UtcDateTime;
        options.EndTime = DateTimeOffset.Parse(parseResult.GetValueForOption(_endTimeOption)!).UtcDateTime;
        options.DataSets = parseResult.GetValueForOption(_dataSetsOption)!.DataSets;
        return options;
    }

    public override ValidationResult Validate(CommandResult commandResult, CommandResponse? commandResponse = null)
    {
        var result = base.Validate(commandResult, commandResponse);

        if (result.IsValid)
        {
            var dataSets = commandResult.GetValueForOption(_dataSetsOption);

            if (!dataSets!.IsValid)
            {
                result.IsValid = false;
                result.ErrorMessage = dataSets.ErrorMessage ?? "Invalid data sets provided.";
                if (commandResponse != null)
                {
                    commandResponse.Status = 400;
                    commandResponse.Message = result.ErrorMessage;
                }
            }

            if (!DateTime.TryParse(commandResult.GetValueForOption(_startTimeOption), out DateTime startTime) ||
                !DateTime.TryParse(commandResult.GetValueForOption(_endTimeOption), out DateTime endTime) ||
                startTime >= endTime)
            {
                result.IsValid = false;
                result.ErrorMessage = $"Invalid time range specified. Ensure that --{_startTimeOption.Name} is before --{_endTimeOption.Name} and that --{_startTimeOption.Name} and --{_endTimeOption.Name} are valid dates in ISO format.";
                if (commandResponse != null)
                {
                    commandResponse.Status = 400;
                    commandResponse.Message = result.ErrorMessage;
                }
            }
        }

        return result;
    }

    [McpServerTool(
        Destructive = false,
        ReadOnly = true,
        Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        if (!Validate(parseResult.CommandResult, context.Response).IsValid)
        {
            return context.Response;
        }

        var options = BindOptions(parseResult);

        try
        {
            var service = context.GetService<IAppDiagnoseService>();

            var result = await service.CorrelateTimeSeries(
                options.Subscription!,
                options.ResourceGroup,
                options.ResourceName,
                options.ResourceId,
                options.DataSets!,
                options.StartTime,
                options.EndTime,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = result?.Length > 0 ?
                ResponseResult.Create(
                    new AppCorrelateCommandResult(result),
                    ApplicationInsightsJsonContext.Default.AppCorrelateCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            // Log error with all relevant context
            _logger.LogError(ex,
                "Error in {Name}. Subscription: {Subscription}, ResourceGroup: {ResourceGroup}, ResourceName: {ResourceName}, Options: {@Options}",
                Name, options.Subscription, options.ResourceGroup, options.ResourceName, options);
            HandleException(context, ex);
        }

        return context.Response;
    }

    public record AppCorrelateCommandResult(AppCorrelateTimeResult[]? Result);
}
