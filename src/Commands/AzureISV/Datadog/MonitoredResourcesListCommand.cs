// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Arguments.AzureISV.Datadog;
using AzureMcp.Commands.AzureISV.Datadog;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Datadog.MonitoredResources;

public sealed class MonitoredResourcesListCommand(ILogger<MonitoredResourcesListCommand> logger) : SubscriptionCommand<MonitoredResourcesListArguments>()
{
    private readonly ILogger<MonitoredResourcesListCommand> _logger = logger;

    protected override string GetCommandName() => "list";

    protected override string GetCommandDescription() =>
    $"""
    List monitored resources in Datadog for a datadog resource taken as input from the user. 
    """;

    private readonly Option<string> _datadogResourceOption = ArgumentDefinitions.Datadog.DatadogResource.ToOption();

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_datadogResourceOption);
        command.AddOption(_resourceGroupOption);
    }
    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateDatadogResourceArgument());
        AddArgument(CreateResourceGroupArgument());
    }

    protected override MonitoredResourcesListArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.DatadogResource = parseResult.GetValueForOption(_datadogResourceOption);
        args.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        return args;
    }

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);

        try
        {
            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            var service = context.GetService<IDatadogService>();
            List<string> results = await service.ListMonitoredResources(
                args.ResourceGroup!,
                args.Subscription!,
                args.DatadogResource!);

            context.Response.Results = results?.Count > 0 ?
                ResponseResult.Create(new MonitoredResourcesListResult(results),
                DatadogJsonContext.Default.MonitoredResourcesListResult) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while executing the command.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    private static ArgumentBuilder<MonitoredResourcesListArguments> CreateDatadogResourceArgument() =>
        ArgumentBuilder<MonitoredResourcesListArguments>
            .Create(ArgumentDefinitions.Datadog.DatadogResource.Name, ArgumentDefinitions.Datadog.DatadogResource.Description)
            .WithValueAccessor(args => args.DatadogResource ?? string.Empty)
            .WithIsRequired(true);

    internal record MonitoredResourcesListResult(List<string> resources);
}
