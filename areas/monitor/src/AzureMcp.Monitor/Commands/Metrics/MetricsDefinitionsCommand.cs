// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Monitor.Commands;
using AzureMcp.Monitor.Models;
using AzureMcp.Monitor.Options;
using AzureMcp.Monitor.Options.Metrics;
using AzureMcp.Monitor.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Monitor.Commands.Metrics;

/// <summary>
/// Command for listing Azure Monitor metric definitions
/// </summary>
public sealed class MetricsDefinitionsCommand(ILogger<MetricsDefinitionsCommand> logger)
    : BaseMetricsCommand<MetricsDefinitionsOptions>
{
    private const string CommandTitle = "List Azure Monitor Metric Definitions";
    private readonly ILogger<MetricsDefinitionsCommand> _logger = logger;

    private readonly Option<string> _metricNamespaceOption = MonitorOptionDefinitions.Metrics.MetricNamespaceOptional;
    private readonly Option<string> _searchStringOption = MonitorOptionDefinitions.Metrics.SearchString;
    private readonly Option<int> _limitOption = MonitorOptionDefinitions.Metrics.DefinitionsLimit;

    public override string Name => "definitions";

    public override string Description =>
        $"""
        List available metric definitions for an Azure resource. Returns metadata about the metrics available for the resource.
        Required options:
        - {_resourceNameOption.Name}: {_resourceNameOption.Description}
        Optional options:
        - {_optionalResourceGroupOption.Name}: {_optionalResourceGroupOption.Description}
        - {_resourceTypeOption.Name}: {_resourceTypeOption.Description}
        - {_metricNamespaceOption.Name}: {_metricNamespaceOption.Description}
        - {_searchStringOption.Name}: {_searchStringOption.Description}
        - {_limitOption.Name}: {_limitOption.Description}
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_metricNamespaceOption);
        command.AddOption(_searchStringOption);
        command.AddOption(_limitOption);
    }

    protected override MetricsDefinitionsOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.MetricNamespace = parseResult.GetValueForOption(_metricNamespaceOption);
        options.SearchString = parseResult.GetValueForOption(_searchStringOption);
        options.Limit = parseResult.GetValueForOption(_limitOption);
        return options;
    }

    [McpServerTool(
        Destructive = false,
        ReadOnly = true,
        Title = CommandTitle)]
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

            // Get the metrics service from DI
            var service = context.GetService<IMonitorMetricsService>();
            // Call service operation with required parameters
            var allResults = await service.ListMetricDefinitionsAsync(
                options.Subscription!,
                options.ResourceGroup,
                options.ResourceType,
                options.ResourceName!,
                options.MetricNamespace,
                options.SearchString,
                options.Tenant,
                options.RetryPolicy);

            if (allResults?.Count > 0)
            {
                // Apply limiting and determine status
                var totalCount = allResults.Count;
                var limitedResults = allResults.Take(options.Limit).ToList();
                var isTruncated = totalCount > options.Limit;

                string status;
                if (isTruncated)
                {
                    status = $"Results truncated to {options.Limit} of {totalCount} metric definitions. Use --search-string to filter results for more specific metrics or increase --limit to see more results.";
                }
                else
                {
                    status = $"All {totalCount} metric definitions returned.";
                }

                // Set response message and results
                context.Response.Message = status;
                context.Response.Results = ResponseResult.Create(
                    new MetricsDefinitionsCommandResult(limitedResults, status),
                    MonitorJsonContext.Default.MetricsDefinitionsCommandResult);
            }
            else
            {
                context.Response.Results = null;
            }
        }
        catch (Exception ex)
        {            // Log error with all relevant context
            _logger.LogError(ex,
                "Error listing metric definitions. ResourceGroup: {ResourceGroup}, ResourceType: {ResourceType}, ResourceName: {ResourceName}, MetricNamespace: {MetricNamespace}, Options: {@Options}",
                options.ResourceGroup, options.ResourceType, options.ResourceName, options.MetricNamespace, options);
            HandleException(context, ex);
        }

        return context.Response;
    }

    // Strongly-typed result record
    internal record MetricsDefinitionsCommandResult(List<MetricDefinition> Results, string Status);
}
