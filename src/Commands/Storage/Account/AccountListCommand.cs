// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Storage.Account;
using AzureMcp.Models.Argument;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Storage.Account;

public sealed class AccountListCommand(ILogger<AccountListCommand> logger) : SubscriptionCommand<AccountListArguments>()
{
    private const string _commandTitle = "List Storage Accounts";
    private readonly ILogger<AccountListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        $"""
        List all Storage accounts in a subscription. This command retrieves all Storage accounts available
        in the specified {ArgumentDefinitions.Common.SubscriptionName}. Results include account names and are
        returned as a JSON array.
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

            var storageService = context.GetService<IStorageService>();
            var accounts = await storageService.GetStorageAccounts(
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = accounts?.Count > 0
                ? ResponseResult.Create(new Result(accounts), StorageJsonContext.Default.AccountListCommandResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing storage accounts");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record Result(List<string> Accounts);
}
