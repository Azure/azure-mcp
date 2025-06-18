// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Commands.Subscription;
using AzureMcp.Options.Cosmos;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Cosmos;

public sealed class AccountListCommand(ILogger<AccountListCommand> logger) : SubscriptionCommand<AccountListOptions>()
{
    private const string CommandTitle = "List Cosmos DB Accounts";
    private readonly ILogger<AccountListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        """
        List all Cosmos DB accounts in a subscription. This command retrieves and displays all Cosmos DB accounts
        available in the specified subscription. Results include account names and are returned as a JSON array.
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

            var cosmosService = context.GetService<ICosmosService>();
            var accounts = await cosmosService.GetCosmosAccounts(
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = accounts?.Count > 0 ?
                ResponseResult.Create(
                    new AccountListCommandResult(accounts),
                    CosmosJsonContext.Default.AccountListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred fetching Cosmos accounts.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record AccountListCommandResult(List<string> Accounts);
}
