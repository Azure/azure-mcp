// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.AzureSignalR.Commands;
using AzureMcp.AzureSignalR.Options.SignalR;
using AzureMcp.AzureSignalR.Services;
using AzureMcp.AzureSignalR.Models;
using Microsoft.Extensions.Logging;

namespace AzureMcp.AzureSignalR.Commands.SignalR;

/// <summary>
/// Lists Azure SignalR Service resources in the specified subscription.
/// </summary>
public sealed class SignalRServiceListCommand(ILogger<SignalRServiceListCommand> logger)
    : BaseAzureSignalRCommand<SignalRListOptions>
{
    private const string CommandTitle = "List SignalR Services";
    private readonly ILogger<SignalRServiceListCommand> _logger = logger;
    protected override bool RequiresResourceGroup => false;

    public override string Name => "list";

    public override string Description =>
        """
        List all SignalR Service resources in a specified subscription. Returns an array of SignalR Service details.
        Required options:
        - --subscription: The subscription ID or name
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

            var signalRService = context.GetService<IAzureSignalRService>() ?? throw new InvalidOperationException("SignalR service is not available.");
            var signalRServices = await signalRService.ListSignalRServicesAsync(
                options.Subscription!,
                options.Tenant,
                options.AuthMethod,
                options.RetryPolicy);

            _logger.LogInformation("Found {Count} SignalR service(s) in subscription {SubscriptionId}", signalRServices.Count(), options.Subscription);

            context.Response.Results = signalRServices.Any() ?
                ResponseResult.Create(
                    new SignalRServiceListCommandResult(signalRServices),
                    AzureSignalRJsonContext.Default.SignalRServiceListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing SignalR services in subscription {SubscriptionId}", options.Subscription);
            HandleException(context, ex);
        }

        return context.Response;
    }

    public record SignalRServiceListCommandResult(IEnumerable<SignalRServiceModel> SignalRServices);
}
