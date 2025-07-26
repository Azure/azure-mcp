// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Commands.Subscription;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.KeyVault.Commands;
using AzureMcp.KeyVault.Options;
using AzureMcp.KeyVault.Options.Key;
using AzureMcp.KeyVault.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.KeyVault.Commands.Key;

public sealed class KeyListCommand(ILogger<KeyListCommand> logger) : SubscriptionCommand<KeyListOptions>
{
    private const string CommandTitle = "List Key Vault Keys";
    private readonly ILogger<KeyListCommand> _logger = logger;
    private readonly Option<string> _vaultOption = KeyVaultOptionDefinitions.VaultName;
    private readonly Option<bool> _includeManagedKeysOption = KeyVaultOptionDefinitions.IncludeManagedKeys;

    public override string Name => "list";

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    public override string Description =>
        """
        List all keys in an Azure Key Vault. This command retrieves and displays the names of all keys
        stored in the specified vault.
        """;

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
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record KeyListCommandResult(List<string> Keys);
}
