// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.KeyVault.Options;
using AzureMcp.Areas.KeyVault.Options.Key;
using AzureMcp.Areas.KeyVault.Services;
using AzureMcp.Commands.KeyVault;
using AzureMcp.Commands.Subscription;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.KeyVault.Commands.Key;

public sealed class KeyCreateCommand(ILogger<KeyCreateCommand> logger) : SubscriptionCommand<KeyCreateOptions>
{
    private const string CommandTitle = "Create Key Vault Key";
    private readonly ILogger<KeyCreateCommand> _logger = logger;
    private readonly Option<string> _vaultOption = KeyVaultOptionDefinitions.VaultName;
    private readonly Option<string> _keyOption = KeyVaultOptionDefinitions.KeyName;
    private readonly Option<string> _keyTypeOption = KeyVaultOptionDefinitions.KeyType;

    public override string Name => "create";

    public override string Description =>
        """
        Create a new key in an Azure Key Vault. This command creates a key with the specified name and type
        in the given vault.

        Required arguments:
        - subscription
        - vault
        - key
        - key-type

        Key types:
        - RSA: RSA key pair
        - EC: Elliptic Curve key pair
        - OCT: ES cryptographic pair
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_vaultOption);
        command.AddOption(_keyOption);
        command.AddOption(_keyTypeOption);
    }

    protected override KeyCreateOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.VaultName = parseResult.GetValueForOption(_vaultOption);
        options.KeyName = parseResult.GetValueForOption(_keyOption);
        options.KeyType = parseResult.GetValueForOption(_keyTypeOption);
        return options;
    }

    [McpServerTool(Destructive = false, ReadOnly = false, Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            var service = context.GetService<IKeyVaultService>();
            var key = await service.CreateKey(
                options.VaultName!,
                options.KeyName!,
                options.KeyType!,
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new KeyCreateCommandResult(key.Name, key.KeyType.ToString(), key.Properties.Enabled, key.Properties.NotBefore, key.Properties.ExpiresOn, key.Properties.CreatedOn, key.Properties.UpdatedOn),
                KeyVaultJsonContext.Default.KeyCreateCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating key {KeyName} in vault {VaultName}", options.KeyName, options.VaultName);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record KeyCreateCommandResult(string Name, string KeyType, bool? Enabled, DateTimeOffset? NotBefore, DateTimeOffset? ExpiresOn, DateTimeOffset? CreatedOn, DateTimeOffset? UpdatedOn);
}
