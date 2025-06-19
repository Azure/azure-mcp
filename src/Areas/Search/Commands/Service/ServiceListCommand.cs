// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Search.Options.Service;
using AzureMcp.Areas.Search.Services;
using AzureMcp.Commands.Subscription;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Search.Commands.Service;

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

            context.Response.Results = services?.Count > 0 ?
                ResponseResult.Create(
                    new ServiceListCommandResult(services),
                    SearchJsonContext.Default.ServiceListCommandResult) : null;
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

