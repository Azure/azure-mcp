// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Search.Service;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Search.Service;

public sealed class ServiceListCommand(ILogger<ServiceListCommand> logger) : SubscriptionCommand<ServiceListArguments>()
{
    private const string _commandTitle = "List Azure AI Search Services";
    private readonly ILogger<ServiceListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        """
        List all Azure AI Search services in a subscription.

        Required arguments:
        - subscription
        """;

    public override string Title => _commandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindOptions(parseResult);

        try
        {
            var validationResult = Validate(parseResult.CommandResult);

            if (!validationResult.IsValid)
            {
                context.Response.Status = 400;
                context.Response.Message = validationResult.ErrorMessage!;
                return context.Response;
            }

            var searchService = context.GetService<ISearchService>();

            var services = await searchService.ListServices(
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy);

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

