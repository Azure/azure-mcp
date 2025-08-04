// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.ApplicationInsights.Models;
using AzureMcp.ApplicationInsights.Options;
using AzureMcp.ApplicationInsights.Services;
using AzureMcp.Core.Commands;
using Microsoft.Extensions.Logging;

namespace AzureMcp.ApplicationInsights.Commands;

public sealed class AppImpactCommand(ILogger<AppImpactCommand> logger)
    : BaseAppCommand<AppImpactOptions>
{
    private readonly ILogger<AppImpactCommand> _logger = logger;

    private const string CommandTitle = "App get impact";

    public override string Name => "get-impact";

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    // Define options from OptionDefinitions
    private readonly Option<string> _tableOption = ApplicationInsightsOptionDefinitions.Table;
    private readonly Option<string[]> _filtersOption = ApplicationInsightsOptionDefinitions.Filters;
    private readonly Option<string> _startTimeOption = ApplicationInsightsOptionDefinitions.StartTime;
    private readonly Option<string> _endTimeOption = ApplicationInsightsOptionDefinitions.EndTime;

    public override string Description =>
        $$"""
        Evaluate the distribution and impact of an issue impacting an application.

        This tool is useful for understanding how many instances are impacted and what the failure rates are.

        You can use this to validate how widespread an issue is, or to determine the impact of a specific error code or type of dependency.

        Example usage:
        Determine how many instances and the overall failure rate caused by requests with a 500 result code:
        "table": "requests",
        "filters": ["resultCode=\"500\""]

        Determine how many instances and the overall failure rate caused by Azure Blob storage 500 errors:
        "table": "dependencies",
        "filters": ["type=\"Azure Blob\"", "resultCode=\"500\""]

        Use this tool for investigating issues with Application Insights resources.

        Required options:
        - {{_resourceNameOption.Name}}: {{_resourceNameOption.Description}} or {{_resourceIdOption.Name}}: {{_resourceIdOption.Description}}
        - {{_tableOption.Name}}: {{_tableOption.Description}}
        Optional options:
        - {{_filtersOption.Name}}: {{_filtersOption.Description}}
        - {{_resourceGroupOption.Name}}: {{_resourceGroupOption.Description}}
        - {{_startTimeOption.Name}}: {{_startTimeOption.Description}}
        - {{_endTimeOption.Name}}: {{_endTimeOption.Description}}
        """;

    protected override void RegisterOptions(Command command)
    {
        command.AddOption(_tableOption);
        command.AddOption(_startTimeOption);
        command.AddOption(_endTimeOption);
        command.AddOption(_filtersOption);
        base.RegisterOptions(command);
    }

    protected override AppImpactOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Table = parseResult.GetValueForOption(_tableOption);
        options.StartTime = DateTimeOffset.Parse(parseResult.GetValueForOption(_startTimeOption)!).UtcDateTime;
        options.EndTime = DateTimeOffset.Parse(parseResult.GetValueForOption(_endTimeOption)!).UtcDateTime;
        options.Filters = parseResult.GetValueForOption(_filtersOption) ?? Array.Empty<string>();
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

            if (result.IsValid)
            {
                var table = commandResult.GetValueForOption(_tableOption)?.ToLowerInvariant();

                if (table != "dependencies" && table != "requests")
                {
                    result.IsValid = false;
                    result.ErrorMessage = $"Invalid table specified. Valid options are: dependencies and requests.";
                    if (commandResponse != null)
                    {
                        commandResponse.Status = 400;
                        commandResponse.Message = result.ErrorMessage;
                    }
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

            var result = await service.GetImpact(
                options.Subscription!,
                options.ResourceGroup,
                options.ResourceName,
                options.ResourceId,
                options.Filters,
                options.Table!,
                options.StartTime,
                options.EndTime,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(new AppImpactCommandResult(result), ApplicationInsightsJsonContext.Default.AppImpactCommandResult);
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
    public record AppImpactCommandResult(List<AppImpactResult>? Result);
}
