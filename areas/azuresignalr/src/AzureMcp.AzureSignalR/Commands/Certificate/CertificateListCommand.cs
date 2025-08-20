// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.AzureSignalR.Options.Certificate;
using AzureMcp.AzureSignalR.Services;
using AzureMcp.AzureSignalR.Models;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using AzureMcp.AzureSignalR.Options;

namespace AzureMcp.AzureSignalR.Commands.Certificate;

public sealed class CertificateListCommand(ILogger<CertificateListCommand> logger)
    : BaseAzureSignalRCommand<CertificateListOptions>
{
    private const string CommandTitle = "List Certificates";
    private readonly ILogger<CertificateListCommand> _logger = logger;

    private static readonly Option<string> _signalRNameOption = AzureSignalROptionDefinitions.SignalRName;

    public override string Name => "list";

    public override string Description =>
        """
        List all custom certificates for a SignalR service. This command retrieves and displays all custom certificates
        configured for the specified SignalR service. Results include certificate names, provisioning states, and
        Key Vault information.
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_signalRNameOption);
    }

    protected override CertificateListOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.SignalRName = parseResult.GetValueForOption(_signalRNameOption);
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

            var signalRService = context.GetService<IAzureSignalRService>();
            var certificates = await signalRService.ListCertificatesAsync(
                options.Subscription!,
                options.ResourceGroup!,
                options.SignalRName!,
                options.Tenant,
                options.AuthMethod,
                options.RetryPolicy);

            var certificatesList = certificates.ToList();
            context.Response.Results = certificatesList.Count > 0 ?
                ResponseResult.Create(
                    new CertificateListCommandResult(certificatesList),
                    AzureSignalRJsonContext.Default.CertificateListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing SignalR certificates");
            HandleException(context, ex);
        }

        return context.Response;
    }

    public record CertificateListCommandResult(List<SignalRCertificateModel> Certificates);
}
