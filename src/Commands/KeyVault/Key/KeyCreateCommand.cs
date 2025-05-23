// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.KeyVault.Key;
using AzureMcp.Models.Argument;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.KeyVault.Key;

public sealed class KeyCreateCommand(ILogger<KeyCreateCommand> logger) : SubscriptionCommand<KeyCreateArguments>
{
    private const string _commandTitle = "Create Key Vault Key";
    private readonly ILogger<KeyCreateCommand> _logger = logger;
    private readonly Option<string> _vaultOption = ArgumentDefinitions.KeyVault.VaultName;
    private readonly Option<string> _keyOption = ArgumentDefinitions.KeyVault.KeyName;
    private readonly Option<string> _keyTypeOption = ArgumentDefinitions.KeyVault.KeyType;

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

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_vaultOption);
        command.AddOption(_keyOption);
        command.AddOption(_keyTypeOption);
    }

    protected override KeyCreateArguments BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.VaultName = parseResult.GetValueForOption(_vaultOption);
        args.KeyName = parseResult.GetValueForOption(_keyOption);
        args.KeyType = parseResult.GetValueForOption(_keyTypeOption);
        return args;
    }

    [McpServerTool(Destructive = false, ReadOnly = false, Title = _commandTitle)]
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

            var service = context.GetService<IKeyVaultService>();
            var key = await service.CreateKey(
                args.VaultName!,
                args.KeyName!,
                args.KeyType!,
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new KeyCreateCommandResult(key.Name, key.KeyType.ToString(), key.Properties.Enabled, key.Properties.NotBefore, key.Properties.ExpiresOn, key.Properties.CreatedOn, key.Properties.UpdatedOn),
                KeyVaultJsonContext.Default.KeyCreateCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating key {KeyName} in vault {VaultName}", args.KeyName, args.VaultName);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record KeyCreateCommandResult(string Name, string KeyType, bool? Enabled, DateTimeOffset? NotBefore, DateTimeOffset? ExpiresOn, DateTimeOffset? CreatedOn, DateTimeOffset? UpdatedOn);
}
