// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.AzureSignalR.Commands;
using AzureMcp.AzureSignalR.Options;
using AzureMcp.AzureSignalR.Options.Certificate;
using AzureMcp.AzureSignalR.Services;
using AzureMcp.AzureSignalR.Models;
using Microsoft.Extensions.Logging;
using System.CommandLine;

namespace AzureMcp.AzureSignalR.Commands.Certificate;

/// <summary>
/// Shows details of a custom certificate in an Azure SignalR Service.
/// </summary>
public sealed class CertificateShowCommand(ILogger<CertificateShowCommand> logger)
    : BaseAzureSignalRCommand<CertificateShowOptions>
{
    private const string CommandTitle = "Show SignalR Certificate";
    private readonly ILogger<CertificateShowCommand> _logger = logger;

    private readonly Option<string> _signalRNameOption = AzureSignalROptionDefinitions.SignalRName;
    private readonly Option<string> _certificateNameOption = AzureSignalROptionDefinitions.CertificateName;

    public override string Name => "show";

    public override string Description =>
        """
        Show details of a custom certificate in an Azure SignalR Service. Returns certificate information including Key Vault configuration and provisioning state.
        Required options:
        - --subscription: The subscription ID or name
        - --resource-group: The resource group name
        - --signalr-name: The SignalR service name
        - --certificate-name: The certificate name
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new()
    {
        Destructive = false,
        ReadOnly = true
    };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_signalRNameOption);
        command.AddOption(_certificateNameOption);
    }

    protected override CertificateShowOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.SignalRName = parseResult.GetValueForOption(_signalRNameOption);
        options.CertificateName = parseResult.GetValueForOption(_certificateNameOption);
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

            var signalRService = context.GetService<IAzureSignalRService>() ?? throw new InvalidOperationException("SignalR service is not available.");
            var certificate = await signalRService.GetCertificateAsync(
                options.Subscription!,
                options.ResourceGroup!,
                options.SignalRName!,
                options.CertificateName!,
                options.Tenant,
                options.AuthMethod,
                options.RetryPolicy);

            if (certificate == null)
            {
                context.Response.Status = 404;
                context.Response.Message = $"Certificate '{options.CertificateName}' not found in SignalR service '{options.SignalRName}'.";
                return context.Response;
            }

            _logger.LogInformation("Retrieved certificate {CertificateName} from SignalR service {SignalRName}",
                options.CertificateName, options.SignalRName);

            context.Response.Results = ResponseResult.Create(
                new CertificateShowCommandResult(certificate),
                AzureSignalRJsonContext.Default.CertificateShowCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error showing certificate {CertificateName} in SignalR service {SignalRName}. Options: {@Options}",
                options.CertificateName, options.SignalRName, options);
            HandleException(context, ex);
        }

        return context.Response;
    }

    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        Azure.RequestFailedException reqEx when reqEx.Status == 404 =>
            "Certificate or SignalR service not found. Verify the certificate name, SignalR service name, and resource group are correct.",
        Azure.RequestFailedException reqEx when reqEx.Status == 403 =>
            $"Authorization failed accessing the SignalR certificate. Ensure you have appropriate permissions. Details: {reqEx.Message}",
        Azure.RequestFailedException reqEx => reqEx.Message,
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        Azure.RequestFailedException reqEx => reqEx.Status,
        _ => base.GetStatusCode(ex)
    };

    public record CertificateShowCommandResult(SignalRCertificateModel Certificate);
}
