// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.SignalR.Models;
using AzureMcp.SignalR.Options;
using AzureMcp.SignalR.Options.CustomCertificate;
using AzureMcp.SignalR.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.SignalR.Commands.CustomCertificate;

/// <summary>
/// Shows details of a custom certificate in an Azure SignalR Service.
/// </summary>
public sealed class CustomCertificateShowCommand(ILogger<CustomCertificateShowCommand> logger)
    : BaseSignalRCommand<CustomCertificateShowOptions>
{
    private const string CommandTitle = "Show Certificate";
    private readonly ILogger<CustomCertificateShowCommand> _logger = logger;

    private readonly Option<string> _signalRNameOption = SignalROptionDefinitions.SignalRName;
    private readonly Option<string> _certificateNameOption = SignalROptionDefinitions.CertificateName;

    public override string Name => "show";

    public override string Description =>
        """
        Show details of a custom certificate in an Azure SignalR Service. Returns certificate information including Key Vault configuration and provisioning state.
        Required options:
        - --subscription: The subscription ID or name
        - --resource-group: The resource group name
        - --signalr-name: The SignalR service name
        - --name: The certificate name
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_signalRNameOption);
        command.AddOption(_certificateNameOption);
    }

    protected override CustomCertificateShowOptions BindOptions(ParseResult parseResult)
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

            var signalRService = context.GetService<ISignalRService>() ??
                                 throw new InvalidOperationException("SignalR service is not available.");
            var certificate = await signalRService.GetCustomCertificateAsync(
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
                context.Response.Message =
                    $"Certificate '{options.CertificateName}' not found in SignalR service '{options.SignalRName}'.";
                return context.Response;
            }

            _logger.LogInformation("Retrieved certificate {CertificateName} from SignalR service {SignalRName}",
                options.CertificateName, options.SignalRName);

            context.Response.Results = ResponseResult.Create(
                new CertificateShowCommandResult(certificate),
                SignalRJsonContext.Default.CertificateShowCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error showing certificate {CertificateName} in SignalR service {SignalRName}. Options: {@Options}",
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

    public record CertificateShowCommandResult(SignalRCustomCertificateModel CustomCertificate);
}
