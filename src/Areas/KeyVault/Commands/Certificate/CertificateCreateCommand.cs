// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.KeyVault.Options;
using AzureMcp.Areas.KeyVault.Options.Certificate;
using AzureMcp.Areas.KeyVault.Services;
using AzureMcp.Commands.KeyVault;
using AzureMcp.Commands.Subscription;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.KeyVault.Commands.Certificate;

public sealed class CertificateCreateCommand(ILogger<CertificateCreateCommand> logger) : SubscriptionCommand<CertificateCreateOptions>
{
    private const string CommandTitle = "Create Key Vault Certificate";
    private readonly ILogger<CertificateCreateCommand> _logger = logger;
    private readonly Option<string> _vaultOption = KeyVaultOptionDefinitions.VaultName;
    private readonly Option<string> _certificateOption = KeyVaultOptionDefinitions.CertificateName;

    public override string Name => "create";

    public override string Title => CommandTitle;

    public override string Description =>
        """
        Creates a new certificate in an Azure Key Vault. This command creates a certificate with the specified name and
        the default policy in the given vault.
        """;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_vaultOption);
        command.AddOption(_certificateOption);
    }

    protected override CertificateCreateOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.VaultName = parseResult.GetValueForOption(_vaultOption);
        options.CertificateName = parseResult.GetValueForOption(_certificateOption);
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

            context.Activity?.WithSubscriptionTag(options);

            var keyVaultService = context.GetService<IKeyVaultService>();
            var operation = await keyVaultService.CreateCertificate(
                options.VaultName!,
                options.CertificateName!,
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new CertificateCreateCommandResult(
                    operation.Value.Name,
                    operation.Properties.Status,
                    operation.Properties.RequestId),
                KeyVaultJsonContext.Default.CertificateCreateCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating certificate {CertificateName} in vault {VaultName}", options.CertificateName, options.VaultName);
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record CertificateCreateCommandResult(string Name, string Status, string RequestId);
}
