// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Options.KeyVault.Secret;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.KeyVault.Secret;

public sealed class SecretGetCommand(ILogger<SecretGetCommand> logger, IKeyVaultService keyVaultService) : SubscriptionCommand<SecretGetOptions>
{
    private const string _commandTitle = "Get Key Vault Secret";
    private readonly ILogger<SecretGetCommand> _logger = logger;
    private readonly IKeyVaultService _keyVaultService = keyVaultService;
    private readonly Option<string> _vaultOption = OptionDefinitions.KeyVault.VaultName;
    private readonly Option<string> _secretOption = OptionDefinitions.KeyVault.SecretName;

    public override string Name => "get";

    public override string Title => _commandTitle;

    public override string Description =>
        """
        Gets a secret from an Azure Key Vault. This command retrieves and displays the value
        of a specific secret from the specified vault.

        Required arguments:
        - subscription
        - vault
        - secret
        """;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_vaultOption);
        command.AddOption(_secretOption);
    }

    protected override SecretGetOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.VaultName = parseResult.GetValueForOption(_vaultOption);
        options.SecretName = parseResult.GetValueForOption(_secretOption);
        return options;
    }

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            var result = await _keyVaultService.GetSecret(
                options.VaultName!,
                options.SecretName!,
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new SecretGetCommandResult(options.SecretName!, result),
                KeyVaultJsonContext.Default.SecretGetCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting secret {SecretName} from vault {VaultName}", options.SecretName, options.VaultName);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record SecretGetCommandResult(string Name, string Value);
}
