// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Monitor.Models;
using AzureMcp.Areas.Monitor.Options;
using AzureMcp.Areas.Monitor.Options.Metrics;
using AzureMcp.Areas.Monitor.Services;
using AzureMcp.Commands.Monitor;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Monitor.Commands.Metrics;

/// <summary>
/// Command for listing Azure Monitor metric namespaces
/// </summary>
public sealed class MetricsNamespacesCommand(ILogger<MetricsNamespacesCommand> logger)
    : BaseMetricsCommand<MetricsNamespacesOptions>
{
    private const string CommandTitle = "List Azure Monitor Metric Namespaces";
    private readonly ILogger<MetricsNamespacesCommand> _logger = logger;

    private readonly Option<int> _limitOption = MonitorOptionDefinitions.Metrics.NamespacesLimit;
    private readonly Option<string> _searchStringOption = MonitorOptionDefinitions.Metrics.SearchString;

    public override string Name => "namespaces";

    public override string Description =>
        $"""
        List available metric namespaces for an Azure resource. Returns the namespaces that contain metrics for the resource.
        Required options:
        - {_resourceNameOption.Name}: {_resourceNameOption.Description}
        Optional options:
        - {_resourceGroupOption.Name}: {_resourceGroupOption.Description}
        - {_resourceTypeOption.Name}: {_resourceTypeOption.Description}
        - {_limitOption.Name}: {_limitOption.Description}
        - {_searchStringOption.Name}: {_searchStringOption.Description}
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_limitOption);
        command.AddOption(_searchStringOption);
    }

    protected override MetricsNamespacesOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Limit = parseResult.GetValueForOption(_limitOption);
        options.SearchString = parseResult.GetValueForOption(_searchStringOption);
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
            var allResults = await service.ListMetricNamespacesAsync(
                options.Subscription!,
                options.ResourceGroup,
                options.ResourceType,
                options.ResourceName!,
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
                    status = $"Results truncated to {options.Limit} of {totalCount} metric namespaces. Use --search-string to filter results for more specific namespaces or increase --limit to see more results.";
                }
                else
                {
                    status = $"All {totalCount} metric namespaces returned.";
                }

                // Set results
                context.Response.Results = ResponseResult.Create(
                    new MetricsNamespacesCommandResult(limitedResults, status),
                    MonitorJsonContext.Default.MetricsNamespacesCommandResult);
            }
            else
            {
                context.Response.Results = null;
            }
        }
        catch (Exception ex)
        {            // Log error with all relevant context
            _logger.LogError(ex,
                "Error listing metric namespaces. ResourceGroup: {ResourceGroup}, ResourceType: {ResourceType}, ResourceName: {ResourceName}, Options: {@Options}",
                options.ResourceGroup, options.ResourceType, options.ResourceName, options);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    // Strongly-typed result record
    internal record MetricsNamespacesCommandResult(List<MetricNamespace> Results, string Status);
}
