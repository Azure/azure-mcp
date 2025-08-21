// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using Microsoft.Extensions.Logging;
using AzureMcp.SignalR.Models;
using AzureMcp.SignalR.Options;
using AzureMcp.SignalR.Options.SignalR;
using AzureMcp.SignalR.Services;

namespace AzureMcp.SignalR.Commands.SignalR;

/// <summary>
/// Shows details of an Azure SignalR Service.
/// </summary>
public sealed class SignalRShowCommand(ILogger<SignalRShowCommand> logger)
    : BaseSignalRCommand<SignalRShowOptions>
{
    private const string CommandTitle = "Show Service Details";
    private readonly ILogger<SignalRShowCommand> _logger = logger;

    private static readonly Option<string> _signalRNameOption = SignalROptionDefinitions.SignalRName;

    public override string Name => "show";

    public override string Description =>
        """
        Show details of an Azure SignalR Service. Returns service information including location, SKU,
        provisioning state, hostname, and port configuration.
        Required options:
        - --subscription: The subscription ID or name
        - --resource-group: The resource group name
        - --signalr-name: The SignalR service name
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_signalRNameOption);
    }

    protected override SignalRShowOptions BindOptions(ParseResult parseResult)
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

            var signalRService = context.GetService<ISignalRService>();
            var service = await signalRService.GetSignalRServiceAsync(
                options.Subscription!,
                options.ResourceGroup!,
                options.SignalRName!,
                options.Tenant,
                options.AuthMethod,
                options.RetryPolicy);

            if (service == null)
            {
                context.Response.Status = 404;
                context.Response.Message =
                    $"SignalR service '{options.SignalRName}' not found in resource group '{options.ResourceGroup}'.";
                return context.Response;
            }

            context.Response.Results = ResponseResult.Create(
                new SignalRShowCommandResult(service),
                SignalRJsonContext.Default.SignalRShowCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred showing SignalR service");
            HandleException(context, ex);
        }

        return context.Response;
    }

    public record SignalRShowCommandResult(SignalRServiceModel Service);
}
