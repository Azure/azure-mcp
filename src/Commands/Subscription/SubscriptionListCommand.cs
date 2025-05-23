// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.Resources;
using AzureMcp.Arguments.Subscription;
using AzureMcp.Models.Argument;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Subscription;

public sealed class SubscriptionListCommand(ILogger<SubscriptionListCommand> logger) : GlobalCommand<SubscriptionListArguments>()
{
    private const string _commandTitle = "List Azure Subscriptions";
    private readonly ILogger<SubscriptionListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        $"""
        List all Azure subscriptions accessible to your account. Optionally specify {ArgumentDefinitions.Common.TenantName}
        and {ArgumentDefinitions.Common.AuthMethodName}. Results include subscription names and IDs, returned as a JSON array.
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

            var subscriptionService = context.GetService<ISubscriptionService>();
            var subscriptions = await subscriptionService.GetSubscriptions(args.Tenant, args.RetryPolicy);

            context.Response.Results = subscriptions?.Count > 0
                ? ResponseResult.Create(
                    new SubscriptionListCommandResult(subscriptions),
                    SubscriptionJsonContext.Default.SubscriptionListCommandResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing subscriptions.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record SubscriptionListCommandResult(List<SubscriptionData> Subscriptions);
}
