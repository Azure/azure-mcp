// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.ApplicationInsights.Models;
using AzureMcp.ApplicationInsights.Options;
using AzureMcp.ApplicationInsights.Services;
using AzureMcp.Core.Commands;
using Microsoft.Extensions.Logging;

namespace AzureMcp.ApplicationInsights.Commands;

public sealed class AppGetTraceCommand(ILogger<AppGetTraceCommand> logger)
    : BaseAppCommand<AppGetTraceOptions>
{
    private readonly ILogger<AppGetTraceCommand> _logger = logger;

    private const string CommandTitle = "App get distributed trace";

    public override string Name => "get-trace";

    // Define options from OptionDefinitions
    private readonly Option<string> _traceIdOption = ApplicationInsightsOptionDefinitions.TraceId;
    private readonly Option<string> _spanIdOption = ApplicationInsightsOptionDefinitions.SpanId;
    private readonly Option<string> _startTimeOption = ApplicationInsightsOptionDefinitions.StartTime;
    private readonly Option<string> _endTimeOption = ApplicationInsightsOptionDefinitions.EndTime;

    public override string Description =>
        $$"""
        Retrieve the distributed trace for an application based on the TraceId and SpanId.

        This tool is useful for identifying the root cause of problems in an application.
        and can be used to retrieve the errors, dependency calls and other information about a specific transaction.

        Use this tool for investigating issues with Application Insights resources.
        Required options:
        - {{_resourceNameOption.Name}}: {{_resourceNameOption.Description}} or {{_resourceIdOption.Name}}: {{_resourceIdOption.Description}}
        - {{_traceIdOption.Name}}: {{_traceIdOption.Description}}
        Optional options:
        - {{_spanIdOption.Name}}: {{_spanIdOption.Description}}
        - {{_resourceGroupOption.Name}}: {{_resourceGroupOption.Description}}
        - {{_startTimeOption.Name}}: {{_startTimeOption.Description}}
        - {{_endTimeOption.Name}}: {{_endTimeOption.Description}}
        """;
    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };


    protected override void RegisterOptions(Command command)
    {
        command.AddOption(_traceIdOption);
        command.AddOption(_startTimeOption);
        command.AddOption(_endTimeOption);
        command.AddOption(_spanIdOption);
        base.RegisterOptions(command);
    }

    protected override AppGetTraceOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.TraceId = parseResult.GetValueForOption(_traceIdOption);
        options.StartTime = DateTimeOffset.Parse(parseResult.GetValueForOption(_startTimeOption)!).UtcDateTime;
        options.EndTime = DateTimeOffset.Parse(parseResult.GetValueForOption(_endTimeOption)!).UtcDateTime;
        options.SpanId = parseResult.GetValueForOption(_spanIdOption);
        return options;
    }

    public override ValidationResult Validate(CommandResult commandResult, CommandResponse? commandResponse = null)
    {
        var result = base.Validate(commandResult, commandResponse);

        if (result.IsValid)
        {
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

            var result = await service.GetDistributedTrace(
                options.Subscription!,
                options.ResourceGroup,
                options.ResourceName,
                options.ResourceId,
                options.TraceId!,
                options.SpanId,
                options.StartTime,
                options.EndTime,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = result != null ?
                    ResponseResult.Create(new AppGetTraceCommandResult(result), ApplicationInsightsJsonContext.Default.AppGetTraceCommandResult) : null;
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

    public record AppGetTraceCommandResult(DistributedTraceResult? Result);
}
