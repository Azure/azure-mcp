// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Subscription;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.CommandLine.Parsing;

namespace AzureMcp.Commands.Subscription;

public sealed class SubscriptionListCommand : GlobalCommand<SubscriptionListArguments>
{
    private readonly ILogger<SubscriptionListCommand> _logger;

    public SubscriptionListCommand(ILogger<SubscriptionListCommand> logger) : base()
    {
        _logger = logger;
    }

    protected override string GetCommandName() => "list";

    protected override string GetCommandDescription() =>
        $"""
        List all Azure subscriptions accessible to your account. Optionally specify {ArgumentDefinitions.Common.TenantName}
        and {ArgumentDefinitions.Common.AuthMethodName}. Results include subscription names and IDs, returned as a JSON array.
        """;

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult commandOptions)
    {
        var args = BindArguments(commandOptions);

        try
        {
            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            var subscriptionService = context.GetService<ISubscriptionService>();
            var subscriptions = await subscriptionService.GetSubscriptions(args.Tenant,
                args.RetryPolicy);

            context.Response.Results = subscriptions?.Count > 0 ? new { subscriptions } : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing subscriptions.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
}