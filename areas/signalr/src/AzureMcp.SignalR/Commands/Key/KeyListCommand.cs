// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.SignalR.Models;
using AzureMcp.SignalR.Options;
using AzureMcp.SignalR.Options.Key;
using AzureMcp.SignalR.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.SignalR.Commands.Key;

/// <summary>
/// Lists access keys for an Azure SignalR Service.
/// </summary>
public sealed class KeyListCommand(ILogger<KeyListCommand> logger)
    : BaseSignalRCommand<KeyListOptions>
{
    private const string CommandTitle = "List Access Keys";
    private readonly ILogger<KeyListCommand> _logger = logger;

    private static readonly Option<string> _signalRNameOption = SignalROptionDefinitions.SignalRName;

    public override string Name => "list";

    public override string Description =>
        """
        List access keys for a SignalR service. This command retrieves and displays the primary and secondary
        access keys and connection strings for the specified SignalR service.
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

    protected override KeyListOptions BindOptions(ParseResult parseResult)
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
            var keys = await signalRService.ListKeysAsync(
                options.Subscription!,
                options.ResourceGroup!,
                options.SignalRName!,
                options.Tenant,
                options.AuthMethod,
                options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new KeyListCommandResult(keys),
                SignalRJsonContext.Default.KeyListCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing SignalR access keys");
            HandleException(context, ex);
        }

        return context.Response;
    }

    public record KeyListCommandResult(SignalRKeyModel Keys);
}
