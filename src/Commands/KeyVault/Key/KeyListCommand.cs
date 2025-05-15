// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Arguments.KeyVault;
using AzureMcp.Arguments.KeyVault.Key;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.KeyVault.Key;

public sealed class KeyListCommand(ILogger<KeyListCommand> logger) : SubscriptionCommand<KeyListArgument>
{
    private readonly ILogger<KeyListCommand> _logger = logger;
    private readonly Option<string> _vaultOption = ArgumentDefinitions.KeyVault.VaultName.ToOption();
    private readonly Option<bool> _includeManagedKeysOption = ArgumentDefinitions.KeyVault.IncludeManagedKeys.ToOption();

    protected override string GetCommandName() => "list";

    protected override string GetCommandDescription() =>
        """
        List all keys in an Azure Key Vault. This command retrieves and displays the names of all keys
        stored in the specified vault.

        Required arguments:
        - subscription
        - vault
        """;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_vaultOption);
        command.AddOption(_includeManagedKeysOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateVaultArgument());
        AddArgument(CreateIncludeManagedArgument());
    }

    private ArgumentBuilder<KeyListArgument> CreateIncludeManagedArgument() =>
        ArgumentBuilder<KeyListArgument>
            .Create(ArgumentDefinitions.KeyVault.IncludeManagedKeys.Name, ArgumentDefinitions.KeyVault.IncludeManagedKeys.Description)
            .WithValueAccessor(args => args.IncludeManagedKeys.ToString())
            .WithIsRequired(ArgumentDefinitions.KeyVault.IncludeManagedKeys.Required);

    private static ArgumentBuilder<KeyListArgument> CreateVaultArgument() =>
        ArgumentBuilder<KeyListArgument>
            .Create(ArgumentDefinitions.KeyVault.VaultName.Name, ArgumentDefinitions.KeyVault.VaultName.Description)
            .WithValueAccessor(args => args.VaultName ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.KeyVault.VaultName.Required);

    protected override KeyListArgument BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.VaultName = parseResult.GetValueForOption(_vaultOption);
        args.IncludeManagedKeys = parseResult.GetValueForOption(_includeManagedKeysOption);
        return args;
    }

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);

        try
        {
            if (!await ProcessArguments(context, args))
            {
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
