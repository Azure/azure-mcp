// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Storage.Account;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.CommandLine.Parsing;

namespace AzureMcp.Commands.Storage.Account;

public sealed class AccountListCommand : SubscriptionCommand<AccountListArguments>
{
    private readonly ILogger<AccountListCommand> _logger;

    public AccountListCommand(ILogger<AccountListCommand> logger) : base()
    {
        _logger = logger;
    }

    protected override string GetCommandName() => "list";

    protected override string GetCommandDescription() =>
        $"""
        List all Storage accounts in a subscription. This command retrieves all Storage accounts available
        in the specified {ArgumentDefinitions.Common.SubscriptionName}. Results include account names and are 
        returned as a JSON array.
        """;

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);

        try
        {
            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            var storageService = context.GetService<IStorageService>();
            var accounts = await storageService.GetStorageAccounts(
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = accounts?.Count > 0 ? new { accounts } : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing storage accounts");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
}