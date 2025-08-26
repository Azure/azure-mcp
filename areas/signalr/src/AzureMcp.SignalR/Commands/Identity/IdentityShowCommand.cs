// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.SignalR.Models;
using AzureMcp.SignalR.Options;
using AzureMcp.SignalR.Options.Identity;
using AzureMcp.SignalR.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.SignalR.Commands.Identity;

/// <summary>
/// Shows the managed identity configuration of an Azure SignalR Service.
/// </summary>
public sealed class IdentityShowCommand(ILogger<IdentityShowCommand> logger)
    : BaseSignalRCommand<IdentityShowOptions>
{
    private const string CommandTitle = "Show Identity Configuration";
    private readonly ILogger<IdentityShowCommand> _logger = logger;

    private static readonly Option<string> _signalRNameOption = SignalROptionDefinitions.SignalRName;

    public override string Name => "show";

    public override string Description =>
        """
        Show the managed identity configuration of an Azure SignalR Service. Returns identity information
        including type (SystemAssigned, UserAssigned, or both), principal ID, tenant ID, and any
        user-assigned identities associated with the service.
        Required options:
        - subscription: The subscription ID or name
        - resource-group: The resource group name
        - signalr-name: The SignalR service name
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_signalRNameOption);
    }

    protected override IdentityShowOptions BindOptions(ParseResult parseResult)
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
            var identity = await signalRService.GetSignalRIdentityAsync(
                options.Subscription!,
                options.ResourceGroup!,
                options.SignalRName!,
                options.Tenant,
                options.AuthMethod,
                options.RetryPolicy);

            context.Response.Results = identity is null ?
                null: ResponseResult.Create(
                new IdentityShowCommandResult(identity),
                SignalRJsonContext.Default.IdentityShowCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred showing SignalR identity");
            HandleException(context, ex);
        }

        return context.Response;
    }

    public record IdentityShowCommandResult(Models.Identity Identity);
}
