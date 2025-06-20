// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Commands.Subscription;
using AzureMcp.Options.Search.Service;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Search.Service;

public sealed class ServiceListCommand(ILogger<ServiceListCommand> logger) : SubscriptionCommand<ServiceListOptions>()
{
    private const string CommandTitle = "List Azure AI Search Services";
    private readonly ILogger<ServiceListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        """
        List all Azure AI Search services in a subscription.

        Required arguments:
        - subscription
        """;

    public override string Title => CommandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            var searchService = context.GetService<ISearchService>();

            var services = await searchService.ListServices(
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = CreateListResult(
                services,
                serviceList => new ServiceListCommandResult(serviceList),
                SearchJsonContext.Default.ServiceListCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing search services");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal sealed record ServiceListCommandResult(List<string> Services);
}

