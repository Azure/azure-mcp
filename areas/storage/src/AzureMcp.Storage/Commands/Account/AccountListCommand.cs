// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Commands.Subscription;
using AzureMcp.Core.Models.Option;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.Storage.Commands;
using AzureMcp.Storage.Options.Account;
using AzureMcp.Storage.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Storage.Commands.Account;

public sealed class AccountListCommand(ILogger<AccountListCommand> logger) : SubscriptionCommand<AccountListOptions>()
{
    private const string CommandTitle = "List Storage Accounts";
    private readonly ILogger<AccountListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        $"""
        List all Storage accounts in a subscription. This command retrieves all Storage accounts available
        in the specified {OptionDefinitions.Common.SubscriptionName}. Results include account names and are
        returned as a JSON array.
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            context.Activity?.WithSubscriptionTag(options);

            var storageService = context.GetService<IStorageService>();
            var accounts = await storageService.GetStorageAccounts(
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = accounts?.Count > 0
                ? ResponseResult.Create(new Result(accounts), StorageJsonContext.Default.AccountListCommandResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing storage accounts");
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record Result(List<string> Accounts);
}
