// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.AppConfig.Models;
using AzureMcp.Areas.AppConfig.Options.Account;
using AzureMcp.Areas.AppConfig.Services;
using AzureMcp.Commands.AppConfig;
using AzureMcp.Commands.Subscription;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.AppConfig.Commands.Account;

public sealed class AccountListCommand(ILogger<AccountListCommand> logger) : SubscriptionCommand<AccountListOptions>()
{
    private const string CommandTitle = "List App Configuration Stores";
    private readonly ILogger<AccountListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        """
        List all App Configuration stores in a subscription. This command retrieves and displays all App Configuration
        stores available in the specified subscription. Results include store names returned as a JSON array.
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

            var appConfigService = context.GetService<IAppConfigService>();
            var accounts = await appConfigService.GetAppConfigAccounts(
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = accounts?.Count > 0 ?
                ResponseResult.Create(
                    new AccountListCommandResult(accounts),
                    AppConfigJsonContext.Default.AccountListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing accounts.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record AccountListCommandResult(List<AppConfigurationAccount> Accounts);
}
