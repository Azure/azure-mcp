// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using Azure.Security.KeyVault.Keys;
using AzureMcp.Arguments.KeyVault.Key;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.KeyVault.Key;

public sealed class KeyGetCommand(ILogger<KeyGetCommand> logger) : SubscriptionCommand<KeyGetArguments>
{
    private const string _commandTitle = "Get Key Vault Key";
    private readonly ILogger<KeyGetCommand> _logger = logger;
    private readonly Option<string> _vaultOption = ArgumentDefinitions.KeyVault.VaultName.ToOption();
    private readonly Option<string> _keyOption = ArgumentDefinitions.KeyVault.KeyName.ToOption();

    public override string Name => "get";

    public override string Description =>
        """
        Get a key from an Azure Key Vault. This command retrieves and displays details
        about a specific key in the specified vault.

        Required arguments:
        - subscription
        - vault
        - key
        """;

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_vaultOption);
        command.AddOption(_keyOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateVaultArgument());
        AddArgument(CreateKeyArgument());
    }

    private static ArgumentBuilder<KeyGetArguments> CreateVaultArgument() =>
        ArgumentBuilder<KeyGetArguments>
            .Create(ArgumentDefinitions.KeyVault.VaultName.Name, ArgumentDefinitions.KeyVault.VaultName.Description)
            .WithValueAccessor(args => args.VaultName ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.KeyVault.VaultName.Required);

    private static ArgumentBuilder<KeyGetArguments> CreateKeyArgument() =>
        ArgumentBuilder<KeyGetArguments>
            .Create(ArgumentDefinitions.KeyVault.KeyName.Name, ArgumentDefinitions.KeyVault.KeyName.Description)
            .WithValueAccessor(args => args.KeyName ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.KeyVault.KeyName.Required);

    protected override KeyGetArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.VaultName = parseResult.GetValueForOption(_vaultOption);
        args.KeyName = parseResult.GetValueForOption(_keyOption);
        return args;
    }

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);

        try
        {
            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            var service = context.GetService<IKeyVaultService>();
            var key = await service.GetKey(
                args.VaultName!,
                args.KeyName!,
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new KeyGetCommandResult(key.Name, key.KeyType.ToString(), key.Properties.Enabled, key.Properties.NotBefore, key.Properties.ExpiresOn, key.Properties.CreatedOn, key.Properties.UpdatedOn),
                KeyVaultJsonContext.Default.KeyGetCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting key {KeyName} from vault {VaultName}", args.KeyName, args.VaultName);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record KeyGetCommandResult(string Name, string KeyType, bool? Enabled, DateTimeOffset? NotBefore, DateTimeOffset? ExpiresOn, DateTimeOffset? CreatedOn, DateTimeOffset? UpdatedOn);
}
