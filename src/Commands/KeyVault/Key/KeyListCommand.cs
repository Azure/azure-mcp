// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Options.KeyVault.Key;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.KeyVault.Key;

public sealed class KeyListCommand(ILogger<KeyListCommand> logger) : SubscriptionCommand<KeyListOptions>
{
    private const string CommandTitle = "List Key Vault Keys";
    private readonly ILogger<KeyListCommand> _logger = logger;
    private readonly Option<string> _vaultOption = OptionDefinitions.KeyVault.VaultName;
    private readonly Option<bool> _includeManagedKeysOption = OptionDefinitions.KeyVault.IncludeManagedKeys;

    public override string Name => "list";

    public override string Description =>
        """
        List all keys in an Azure Key Vault. This command retrieves and displays the names of all keys
        stored in the specified vault.

        Required arguments:
        - subscription
        - vault
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_vaultOption);
        command.AddOption(_includeManagedKeysOption);
    }

    protected override KeyListOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.VaultName = parseResult.GetValueForOption(_vaultOption);
        options.IncludeManagedKeys = parseResult.GetValueForOption(_includeManagedKeysOption);
        return options;
    }

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

            var keyVaultService = context.GetService<IKeyVaultService>();
            var keys = await keyVaultService.ListKeys(
                options.VaultName!,
                options.IncludeManagedKeys,
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = keys?.Count > 0 ?
                ResponseResult.Create(
                    new KeyListCommandResult(keys),
                    KeyVaultJsonContext.Default.KeyListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing keys from vault {VaultName}.", options.VaultName);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record KeyListCommandResult(List<string> Keys);
}
