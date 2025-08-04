using AzureMcp.ApplicationInsights.Models;
using AzureMcp.ApplicationInsights.Options;
using AzureMcp.ApplicationInsights.Services;
using AzureMcp.Core.Commands;
using Microsoft.Extensions.Logging;

namespace AzureMcp.ApplicationInsights.Commands
{
    public sealed class AppListTraceCommand(ILogger<AppListTraceCommand> logger)
    : BaseAppCommand<AppListTraceOptions>
    {
        private readonly ILogger<AppListTraceCommand> _logger = logger;

        private const string CommandTitle = "App list traces";

        public override string Name => "list-traces";

        // Define options from OptionDefinitions
        private readonly Option<string> _tableOption = ApplicationInsightsOptionDefinitions.Table;
        private readonly Option<string[]> _filtersOption = ApplicationInsightsOptionDefinitions.Filters;
        private readonly Option<string> _startTimeOption = ApplicationInsightsOptionDefinitions.StartTime;
        private readonly Option<string> _endTimeOption = ApplicationInsightsOptionDefinitions.EndTime;

        public override string Description =>
            $$"""
            List the most relevant traces from an Application Insights table.

            This tool is useful for correlating errors and dependencies to specific transactions in an application.

            Returns a list of traceIds and spanIds that can be further explored for each operation.

            Example usage:
            Filter to dependency failures
            "table": "dependencies",
            "filters": ["success=\"false\""]

            Filter to request failures with 500 code
            "table": "requests",
            "filters": ["success=\"false\"", "resultCode=\"500\""]

            Filter to requests slower than 95th percentile (use start and end time filters to filter to the duration spike). Any percentile is valid (e.g. 99p is also valid)
            "table": "requests",
            "filters": ["duration=\"95p\""],
            "start-time":"start of spike (ISO date)",
            "end-time":"end of spike (ISO date)"

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

        public override string Title => CommandTitle;

        public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

        protected override void RegisterOptions(Command command)
        {
            command.AddOption(_tableOption);
            command.AddOption(_startTimeOption);
            command.AddOption(_endTimeOption);
            command.AddOption(_filtersOption);
            base.RegisterOptions(command);
        }

        protected override AppListTraceOptions BindOptions(ParseResult parseResult)
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

                    if (table != "exceptions" && table != "dependencies" && table != "availabilityresults" && table != "requests")
                    {
                        result.IsValid = false;
                        result.ErrorMessage = $"Invalid table specified. Valid options are: exceptions, dependencies, availabilityResults, requests.";
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

                var result = await service.ListDistributedTraces(
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

                context.Response.Results = result != null ?
                    ResponseResult.Create(new AppListTraceCommandResult(result), ApplicationInsightsJsonContext.Default.AppListTraceCommandResult) : null;
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

        public record AppListTraceCommandResult(AppListTraceResult? Result);
    }
}
