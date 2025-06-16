// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using AzureMcp.Commands.Monitor.ApplicationInsights;
using AzureMcp.Models;
using AzureMcp.Models.Monitor;
using AzureMcp.Models.Option;
using AzureMcp.Options.Monitor.ApplicationInsights;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Monitor.ApplicationInsights;

public sealed class AppDiagnoseCommand(ILogger<AppDiagnoseCommand> logger) 
    : BaseApplicationInsightsCommand<AppDiagnoseOptions>
{
    private const string _commandTitle = "Diagnose App with Application Insights";
    private readonly ILogger<AppDiagnoseCommand> _logger = logger;

    private readonly Option<string> _startTimeOption = OptionDefinitions.Monitor.StartTime;
    private readonly Option<string> _endTimeOption = OptionDefinitions.Monitor.EndTime;
    private readonly Option<string> _symptomsOption = OptionDefinitions.Monitor.Symptoms;

    public override string Name => "diagnose";

    public override string Description =>
        $"""
        Diagnose an issue with an application using Application Insights. This tool can be used to diagnose issues such as slow performance, failing requests, or availability problems.
        Returns a list of detected issues with trace and span IDs for drilling into distributed traces to uncover the root cause.
        Required options:
        - --{OptionDefinitions.Monitor.AppIdName}: {OptionDefinitions.Monitor.AppId.Description}
        - --{OptionDefinitions.Monitor.SymptomsName}: {OptionDefinitions.Monitor.Symptoms.Description}
        Optional parameters:
        - --{OptionDefinitions.Monitor.StartTimeName}: {OptionDefinitions.Monitor.StartTime.Description}
        - --{OptionDefinitions.Monitor.EndTimeName}: {OptionDefinitions.Monitor.EndTime.Description}
        """;

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_startTimeOption);
        command.AddOption(_endTimeOption);
        command.AddOption(_symptomsOption);
    }    
    
    protected override AppDiagnoseOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.StartTime = parseResult.GetValueForOption(_startTimeOption);
        options.EndTime = parseResult.GetValueForOption(_endTimeOption);
        options.Symptoms = parseResult.GetValueForOption(_symptomsOption);
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
            var diagnosticResults = await service.DiagnoseApplicationAsync(
                context.Server,
                options.Subscription!,
                options.AppId!,
                options.Symptoms!,
                options.StartTime,
                options.EndTime,
                options.Tenant,
                options.RetryPolicy);

            // Set the response
            context.Response.Results = diagnosticResults.Count > 0 ?
                ResponseResult.Create(new AppDiagnoseCommandResult(diagnosticResults), MonitorJsonContext.Default.AppDiagnoseCommandResult) :
                null;
            return context.Response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error diagnosing Application Insights application: {AppId}", options.AppId);
            HandleException(context.Response, ex);
            return context.Response;
        }
    }

    // Result model for the command
    internal record AppDiagnoseCommandResult(List<JsonNode> Results);
}
