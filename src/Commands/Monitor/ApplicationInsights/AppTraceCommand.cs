// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Monitor.ApplicationInsights;
using AzureMcp.Models.Option;
using AzureMcp.Options.Monitor.ApplicationInsights;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Monitor.ApplicationInsights;

public sealed class AppTraceCommand(ILogger<AppTraceCommand> logger)
    : BaseApplicationInsightsCommand<AppTraceOptions>
{
    private const string _commandTitle = "Get Distributed Trace from Application Insights";
    private readonly ILogger<AppTraceCommand> _logger = logger;

    // Define options from OptionDefinitions
    private readonly Option<string> _traceIdOption = OptionDefinitions.Monitor.TraceId;
    private readonly Option<string> _spanIdOption = OptionDefinitions.Monitor.SpanId;
    private readonly Option<string> _startTimeOption = OptionDefinitions.Monitor.StartTime;
    private readonly Option<string> _endTimeOption = OptionDefinitions.Monitor.EndTime;

    public override string Name => "trace";

    public override string Description =>
        $"""
        Get a distributed trace from Application Insights by trace ID and span ID. This command retrieves detailed span information including timing, dependencies, and properties.
        Returns the hierarchical trace structure with all spans and their relationships.
        Required options:
        - {OptionDefinitions.Monitor.AppIdName}: {OptionDefinitions.Monitor.AppId.Description}
        - {OptionDefinitions.Monitor.TraceIdName}: {OptionDefinitions.Monitor.TraceId.Description}
        - {OptionDefinitions.Monitor.SpanIdName}: {OptionDefinitions.Monitor.SpanId.Description}
        Optional parameters:
        - {OptionDefinitions.Monitor.StartTimeName}: {OptionDefinitions.Monitor.StartTime.Description}
        - {OptionDefinitions.Monitor.EndTimeName}: {OptionDefinitions.Monitor.EndTime.Description}
        """;

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_traceIdOption);
        command.AddOption(_spanIdOption);
        command.AddOption(_startTimeOption);
        command.AddOption(_endTimeOption);
    }

    protected override AppTraceOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.TraceId = parseResult.GetValueForOption(_traceIdOption);
        options.SpanId = parseResult.GetValueForOption(_spanIdOption);
        options.StartTime = parseResult.GetValueForOption(_startTimeOption);
        options.EndTime = parseResult.GetValueForOption(_endTimeOption);
        return options;
    }

    [McpServerTool(
        Destructive = false,
        ReadOnly = true,
        Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            // Required validation step
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            // Get the Application Insights service from DI
            var service = context.GetService<IApplicationInsightsService>();

            // Execute the command and get the results
            var traceResult = await service.GetDistributedTraceAsync(
                options.Subscription!,
                options.AppId!,
                options.TraceId!,
                options.SpanId!,
                options.StartTime,
                options.EndTime,
                options.Tenant,
                options.RetryPolicy);

            // Set the response
            context.Response.Results = ResponseResult.Create(
                new AppTraceCommandResult(traceResult),
                MonitorJsonContext.Default.AppTraceCommandResult);
            return context.Response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving distributed trace: TraceId={TraceId}, SpanId={SpanId}, AppId={AppId}",
                options.TraceId, options.SpanId, options.AppId);
            HandleException(context.Response, ex);
            return context.Response;
        }
    }

    // Define specialized error handling if needed
    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        InvalidOperationException invEx when invEx.Message.Contains("Could not find Application Insights") => 
            $"The specified Application Insights resource was not found. Please check the AppId and your permissions.",
        _ => base.GetErrorMessage(ex)
    };

    // Result model for the command
    internal record AppTraceCommandResult(DistributedTraceResult Result);
}
