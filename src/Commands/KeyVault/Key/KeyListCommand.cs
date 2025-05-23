// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.KeyVault.Key;
using AzureMcp.Models.Argument;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.KeyVault.Key;

public sealed class KeyListCommand(ILogger<KeyListCommand> logger) : SubscriptionCommand<KeyListArgument>
{
    private const string _commandTitle = "List Key Vault Keys";
    private readonly ILogger<KeyListCommand> _logger = logger;
    private readonly Option<string> _vaultOption = ArgumentDefinitions.KeyVault.VaultName;
    private readonly Option<bool> _includeManagedKeysOption = ArgumentDefinitions.KeyVault.IncludeManagedKeys;

    public override string Name => "list";

    public override string Description =>
        """
        List all keys in an Azure Key Vault. This command retrieves and displays the names of all keys
        stored in the specified vault.

        Required arguments:
        - subscription
        - vault
        """;

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_vaultOption);
        command.AddOption(_includeManagedKeysOption);
    }

    protected override KeyListArgument BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.VaultName = parseResult.GetValueForOption(_vaultOption);
        args.IncludeManagedKeys = parseResult.GetValueForOption(_includeManagedKeysOption);
        return args;
    }

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

            var keyVaultService = context.GetService<IKeyVaultService>();
            var keys = await keyVaultService.ListKeys(
                args.VaultName!,
                args.IncludeManagedKeys,
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = keys?.Count > 0 ?
                ResponseResult.Create(
                    new KeyListCommandResult(keys),
                    KeyVaultJsonContext.Default.KeyListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing keys from vault {VaultName}.", args.VaultName);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record KeyListCommandResult(List<string> Keys);
}
