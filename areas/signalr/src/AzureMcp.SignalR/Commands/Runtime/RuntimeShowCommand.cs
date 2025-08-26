// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.SignalR.Models;
using AzureMcp.SignalR.Options;
using AzureMcp.SignalR.Options.Runtime;
using AzureMcp.SignalR.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.SignalR.Commands.Runtime;

/// <summary>
/// Shows details of an Azure SignalR Service.
/// </summary>
public sealed class RuntimeShowCommand(ILogger<RuntimeShowCommand> logger)
    : BaseSignalRCommand<SignalRShowOptions>
{
    private const string CommandTitle = "Show Service Details";
    private readonly ILogger<RuntimeShowCommand> _logger = logger;

    private static readonly Option<string> _signalRNameOption = SignalROptionDefinitions.SignalRName;

    public override string Name => "show";

    public override string Description =>
        """
        Show details of an Azure SignalR Runtime. Returns runtime information including location, SKU,
        provisioning state, hostname, and port configuration.
        Required options:
        - subscription: The subscription ID or name
        - resource-group: The resource group name
        - signalr-name: The SignalR runtime name
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
            var runtime = await signalRService.GetRuntimeAsync(
                options.Subscription!,
                options.ResourceGroup!,
                options.SignalRName!,
                options.Tenant,
                options.AuthMethod,
                options.RetryPolicy);

            context.Response.Results = runtime is null ?
                null: ResponseResult.Create(
                new RuntimeShowCommandResult(runtime),
                SignalRJsonContext.Default.RuntimeShowCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred showing SignalR service");
            HandleException(context, ex);
        }

        return context.Response;
    }

    public record RuntimeShowCommandResult(Models.Runtime Runtime);
}
