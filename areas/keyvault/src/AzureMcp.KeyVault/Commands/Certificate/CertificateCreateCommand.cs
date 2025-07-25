// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands.Subscription;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.KeyVault.Commands;
using AzureMcp.KeyVault.Options;
using AzureMcp.KeyVault.Options.Certificate;
using AzureMcp.KeyVault.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.KeyVault.Commands.Certificate;

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
        Creates a new certificate in an Azure Key Vault. This command creates a certificate with the specified name in
        the given vault using the default certificate policy.
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

            // Wait for the certificate operation to complete
            var completedOperation = await operation.WaitForCompletionAsync();
            var certificate = completedOperation.Value;

            context.Response.Results = ResponseResult.Create(
                new CertificateCreateCommandResult(
                    certificate.Name,
                    certificate.Id,
                    certificate.KeyId,
                    certificate.SecretId,
                    Convert.ToBase64String(certificate.Cer),
                    certificate.Properties.X509ThumbprintString,
                    certificate.Properties.Enabled,
                    certificate.Properties.NotBefore,
                    certificate.Properties.ExpiresOn,
                    certificate.Properties.CreatedOn,
                    certificate.Properties.UpdatedOn,
                    certificate.Policy.Subject,
                    certificate.Policy.IssuerName),
                KeyVaultJsonContext.Default.CertificateCreateCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating certificate {CertificateName} in vault {VaultName}", options.CertificateName, options.VaultName);
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record CertificateCreateCommandResult(string Name, Uri Id, Uri KeyId, Uri SecretId, string Cer, string Thumbprint, bool? Enabled, DateTimeOffset? NotBefore, DateTimeOffset? ExpiresOn, DateTimeOffset? CreatedOn, DateTimeOffset? UpdatedOn, string Subject, string IssuerName);
}
