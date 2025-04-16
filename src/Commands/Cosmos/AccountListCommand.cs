// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Cosmos;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.CommandLine.Parsing;

namespace AzureMcp.Commands.Cosmos;

public sealed class AccountListCommand : SubscriptionCommand<AccountListArguments>
{
    private readonly ILogger<AccountListCommand> _logger;

    public AccountListCommand(ILogger<AccountListCommand> logger) : base()
    {
        _logger = logger;
    }

    protected override string GetCommandName() => "list";

    protected override string GetCommandDescription() =>
        """
        List all Cosmos DB accounts in a subscription. This command retrieves and displays all Cosmos DB accounts 
        available in the specified subscription. Results include account names and are returned as a JSON array.
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

            var cosmosService = context.GetService<ICosmosService>();
            var accounts = await cosmosService.GetCosmosAccounts(
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = accounts?.Count > 0 ?
                new { accounts } :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred fetching Cosmos accounts.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
}