// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using Microsoft.Extensions.Logging;
using AzureMcp.SignalR.Models;
using AzureMcp.SignalR.Options;
using AzureMcp.SignalR.Options.CustomDomain;
using AzureMcp.SignalR.Services;

namespace AzureMcp.SignalR.Commands.CustomDomain;

/// <summary>
/// Shows details of a custom domain in an Azure SignalR Service.
/// </summary>
public sealed class CustomDomainShowCommand(ILogger<CustomDomainShowCommand> logger)
    : BaseSignalRCommand<CustomDomainShowOptions>
{
    private const string CommandTitle = "Show Custom Domain";
    private readonly ILogger<CustomDomainShowCommand> _logger = logger;

    private static readonly Option<string> _signalRNameOption = SignalROptionDefinitions.SignalRName;

    private static readonly Option<string> _customDomainNameOption = SignalROptionDefinitions.CustomDomainName;

    public override string Name => "show";

    public override string Description =>
        """
        Show details of a custom domain in an Azure SignalR Service. Returns custom domain information including
        domain name, provisioning state, and associated certificate details.
        Required options:
        - --subscription: The subscription ID or name
        - --resource-group: The resource group name
        - --signalr-name: The SignalR service name
        - --name: The custom domain name
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_signalRNameOption);
        command.AddOption(_customDomainNameOption);
    }

    protected override CustomDomainShowOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.SignalRName = parseResult.GetValueForOption(_signalRNameOption);
        options.CustomDomainName = parseResult.GetValueForOption(_customDomainNameOption);
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

            var signalRService = context.GetService<ISignalRService>();
            var customDomain = await signalRService.GetCustomDomainAsync(
                options.Subscription!,
                options.ResourceGroup!,
                options.SignalRName!,
                options.CustomDomainName!,
                options.Tenant,
                options.AuthMethod,
                options.RetryPolicy);

            if (customDomain == null)
            {
                context.Response.Status = 404;
                context.Response.Message =
                    $"Custom domain '{options.CustomDomainName}' not found in SignalR service '{options.SignalRName}'.";
                return context.Response;
            }

            context.Response.Results = ResponseResult.Create(
                new CustomDomainShowCommandResult(customDomain),
                SignalRJsonContext.Default.CustomDomainShowCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred showing SignalR custom domain");
            HandleException(context, ex);
        }

        return context.Response;
    }

    public record CustomDomainShowCommandResult(SignalRCustomDomainModel CustomDomain);
}
