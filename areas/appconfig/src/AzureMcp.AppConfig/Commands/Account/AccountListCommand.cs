// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.AppConfig.Commands;
using AzureMcp.AppConfig.Models;
using AzureMcp.AppConfig.Options.Account;
using AzureMcp.AppConfig.Services;
using AzureMcp.Core.Commands.Subscription;
using AzureMcp.Core.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.AppConfig.Commands.Account;

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

            context.Activity?.WithSubscriptionTag(options);

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
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record AccountListCommandResult(List<AppConfigurationAccount> Accounts);
}
