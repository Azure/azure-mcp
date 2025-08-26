// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.ResourceHealth.Models;
using AzureMcp.ResourceHealth.Options.ServiceHealthEvents;
using AzureMcp.ResourceHealth.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.ResourceHealth.Commands.ServiceHealthEvents;

/// <summary>
/// Lists service health events affecting Azure services and subscriptions.
/// </summary>
public sealed class ServiceHealthEventsListCommand(ILogger<ServiceHealthEventsListCommand> logger)
    : BaseResourceHealthCommand<ServiceHealthEventsListOptions>()
{
    private const string CommandTitle = "List Service Health Events";
    private readonly ILogger<ServiceHealthEventsListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        """
        Get service health events affecting Azure services and subscriptions.
        Provides information about service issues, planned maintenance, health advisories, and security advisories.
        Helps identify current and past incidents that may affect your Azure resources.
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(ResourceHealthOptionDefinitions.Filter);
        command.AddOption(ResourceHealthOptionDefinitions.EventType);
        command.AddOption(ResourceHealthOptionDefinitions.Status);
        command.AddOption(ResourceHealthOptionDefinitions.TrackingId);
        command.AddOption(ResourceHealthOptionDefinitions.QueryStartTime);
        command.AddOption(ResourceHealthOptionDefinitions.QueryEndTime);
        command.AddOption(ResourceHealthOptionDefinitions.Top);
    }

    protected override ServiceHealthEventsListOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Filter = parseResult.GetValueForOption(ResourceHealthOptionDefinitions.Filter);
        options.EventType = parseResult.GetValueForOption(ResourceHealthOptionDefinitions.EventType);
        options.Status = parseResult.GetValueForOption(ResourceHealthOptionDefinitions.Status);
        options.TrackingId = parseResult.GetValueForOption(ResourceHealthOptionDefinitions.TrackingId);
        options.QueryStartTime = parseResult.GetValueForOption(ResourceHealthOptionDefinitions.QueryStartTime);
        options.QueryEndTime = parseResult.GetValueForOption(ResourceHealthOptionDefinitions.QueryEndTime);
        options.Top = parseResult.GetValueForOption(ResourceHealthOptionDefinitions.Top);
        return options;
    }

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            var resourceHealthService = context.GetService<IResourceHealthService>() ??
                throw new InvalidOperationException("Resource Health service is not available.");

            var events = await resourceHealthService.ListServiceHealthEventsAsync(
                options.Subscription!,
                options.Filter,
                options.EventType,
                options.Status,
                options.TrackingId,
                options.QueryStartTime,
                options.QueryEndTime,
                options.Top,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = events?.Count > 0
                ? ResponseResult.Create(
                    new ServiceHealthEventsListCommandResult(events),
                    ResourceHealthJsonContext.Default.ServiceHealthEventsListCommandResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list service health events for subscription {Subscription}",
                options.Subscription);
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record ServiceHealthEventsListCommandResult(List<ServiceHealthEvent> Events);
}
