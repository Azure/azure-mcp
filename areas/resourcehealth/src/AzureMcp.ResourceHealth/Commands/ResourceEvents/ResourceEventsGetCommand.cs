// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Core.Commands;
using AzureMcp.ResourceHealth.Options.ResourceEvents;
using AzureMcp.ResourceHealth.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.ResourceHealth.Commands.ResourceEvents;

public sealed class ResourceEventsGetCommand(ILogger<ResourceEventsGetCommand> logger)
    : BaseResourceHealthCommand<ResourceEventsGetOptions>
{
    private const string CommandTitle = "Get historical availability events for a specific Azure resource";

    private readonly ILogger<ResourceEventsGetCommand> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly Option<string> _resourceIdOption = ResourceHealthOptionDefinitions.ResourceId;
    private readonly Option<string> _filterOption = ResourceHealthOptionDefinitions.Filter;
    private readonly Option<string> _queryStartTimeOption = ResourceHealthOptionDefinitions.QueryStartTime;
    private readonly Option<string> _queryEndTimeOption = ResourceHealthOptionDefinitions.QueryEndTime;
    private readonly Option<int?> _topOption = ResourceHealthOptionDefinitions.Top;
    private readonly Option<string> _expandOption = ResourceHealthOptionDefinitions.Expand;

    public override string Name => "get";

    public override string Description =>
        $"""
        Get historical availability events for a specific Azure resource to track health changes over time.
        Provides detailed information about resource availability events, status changes, and impact details.
        Equivalent to Azure Resource Health events API for single resources.
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_resourceIdOption);
        command.AddOption(_filterOption);
        command.AddOption(_queryStartTimeOption);
        command.AddOption(_queryEndTimeOption);
        command.AddOption(_topOption);
        command.AddOption(_expandOption);
    }

    protected override ResourceEventsGetOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);

        options.ResourceId = parseResult.GetValueForOption(_resourceIdOption) ?? throw new ArgumentException("Resource ID is required.");
        options.Filter = parseResult.GetValueForOption(_filterOption);
        options.QueryStartTime = parseResult.GetValueForOption(_queryStartTimeOption);
        options.QueryEndTime = parseResult.GetValueForOption(_queryEndTimeOption);
        options.Top = parseResult.GetValueForOption(_topOption);
        options.Expand = parseResult.GetValueForOption(_expandOption);

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

            _logger.LogDebug("Getting resource events for resource ID: {ResourceId}", options.ResourceId);

            var resourceHealthService = context.GetService<IResourceHealthService>() ??
                throw new InvalidOperationException("Resource Health service is not registered in the dependency injection container.");

            var events = await resourceHealthService.GetResourceEventsAsync(
                options.ResourceId,
                options.Filter,
                options.QueryStartTime,
                options.QueryEndTime,
                options.Top,
                options.Expand,
                options.RetryPolicy);

            _logger.LogDebug("Retrieved {Count} resource events", events.Count);

            var result = new ResourceEventsGetCommandResult(events);
            context.Response.Results = ResponseResult.Create(result, ResourceHealthJsonContext.Default.ResourceEventsGetCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting resource events for resource ID '{ResourceId}'", options.ResourceId);
            HandleException(context, ex);
        }

        return context.Response;
    }

    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        ArgumentException argEx => $"Invalid argument: {argEx.Message}",
        HttpRequestException httpEx => $"Request failed: {httpEx.Message}",
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        ArgumentException => 400,
        HttpRequestException httpEx when httpEx.Message.Contains("404") => 404,
        HttpRequestException httpEx when httpEx.Message.Contains("403") => 403,
        HttpRequestException httpEx when httpEx.Message.Contains("401") => 401,
        _ => base.GetStatusCode(ex)
    };

    internal record ResourceEventsGetCommandResult(List<Models.ServiceHealthEvent> Events);
}
