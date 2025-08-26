// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.SignalR.Models;
using AzureMcp.SignalR.Options.Runtime;
using AzureMcp.SignalR.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.SignalR.Commands.Runtime;

/// <summary>
/// Lists Azure SignalR Service resources in the specified subscription.
/// </summary>
public sealed class RuntimeListCommand(ILogger<RuntimeListCommand> logger)
    : BaseSignalRCommand<SignalRListOptions>
{
    private const string CommandTitle = "List all Runtimes";
    private readonly ILogger<RuntimeListCommand> _logger = logger;
    protected override bool RequiresResourceGroup => false;

    public override string Name => "list";

    public override string Description =>
        """
        List all SignalR Runtime resources in a specified subscription. Returns an array of SignalR Runtime details.
        Required options:
        - subscription: The subscription ID or name
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

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
            var runtimes = await signalRService.ListRuntimesAsync(
                options.Subscription!,
                options.Tenant,
                options.AuthMethod,
                options.RetryPolicy);

            _logger.LogInformation("Found {Count} SignalR service(s) in subscription {SubscriptionId}",
                runtimes.Count(), options.Subscription);

            context.Response.Results = runtimes.Any()
                ? ResponseResult.Create(
                    new RuntimeListCommandResult(runtimes),
                    SignalRJsonContext.Default.RuntimeListCommandResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing SignalR services in subscription {SubscriptionId}",
                options.Subscription);
            HandleException(context, ex);
        }

        return context.Response;
    }

    public record RuntimeListCommandResult(IEnumerable<Models.Runtime> Runtimes);
}
