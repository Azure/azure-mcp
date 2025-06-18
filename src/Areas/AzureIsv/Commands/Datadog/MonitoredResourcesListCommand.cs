// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Command;
using AzureMcp.Models.Option;
using AzureMcp.Options.AzureIsv.Datadog;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.AzureIsv.Datadog.MonitoredResources;

public sealed class MonitoredResourcesListCommand(ILogger<MonitoredResourcesListCommand> logger) : SubscriptionCommand<MonitoredResourcesListOptions>
{
    private const string _commandTitle = "List Monitored Resources in a Datadog Monitor";
    private readonly ILogger<MonitoredResourcesListCommand> _logger = logger;
    private readonly Option<string> _datadogResourceOption = OptionDefinitions.Datadog.DatadogResourceName;

    public override string Name => "list";

    public override string Description =>
        """
        List monitored resources in Datadog for a datadog resource taken as input from the user. 
        This command retrieves all monitored azure resources available. Requires `datadog-resource`, `resource-group` and `subscription`.
        Result is a list of monitored resources as a JSON array.
        """;

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_datadogResourceOption);
        command.AddOption(_resourceGroupOption);
    }

    protected override MonitoredResourcesListOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.DatadogResource = parseResult.GetValueForOption(_datadogResourceOption);
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        return options;
    }

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);
        try
        {
            var service = context.GetService<IDatadogService>();
            List<string> results = await service.ListMonitoredResources(
                options.ResourceGroup!,
                options.Subscription!,
                options.DatadogResource!);
            context.Response.Results = results?.Count > 0
                ? ResponseResult.Create(new MonitoredResourcesListResult(results), DatadogJsonContext.Default.MonitoredResourcesListResult)
                : ResponseResult.Create(new MonitoredResourcesListResult([
                    "No monitored resources found for the specified Datadog resource."]), DatadogJsonContext.Default.MonitoredResourcesListResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while executing the command.");
            context.Response.Status = 500;
            context.Response.Message = ex.Message;
        }
        return context.Response;
    }

    internal record MonitoredResourcesListResult(List<string> resources);
}
