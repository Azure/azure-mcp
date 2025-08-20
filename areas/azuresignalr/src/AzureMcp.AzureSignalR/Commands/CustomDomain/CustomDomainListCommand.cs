// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.AzureSignalR.Options.CustomDomain;
using AzureMcp.AzureSignalR.Services;
using AzureMcp.AzureSignalR.Models;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using AzureMcp.AzureSignalR.Options;

namespace AzureMcp.AzureSignalR.Commands.CustomDomain;

public sealed class CustomDomainListCommand(ILogger<CustomDomainListCommand> logger)
    : BaseAzureSignalRCommand<CustomDomainListOptions>
{
    private const string CommandTitle = "List Custom Domains";
    private readonly ILogger<CustomDomainListCommand> _logger = logger;

    private static readonly Option<string> _signalRNameOption = AzureSignalROptionDefinitions.SignalRName;

    public override string Name => "list";

    public override string Description =>
        """
        List all custom domains for a SignalR service. This command retrieves and displays all custom domains
        configured for the specified SignalR service. Results include domain names, provisioning states, and
        associated certificates.
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_signalRNameOption);
    }

    protected override CustomDomainListOptions BindOptions(ParseResult parseResult)
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
            var customDomains = await signalRService.ListCustomDomainsAsync(
                options.Subscription!,
                options.ResourceGroup!,
                options.SignalRName!,
                options.Tenant,
                options.AuthMethod,
                options.RetryPolicy);

            var customDomainsList = customDomains.ToList();
            context.Response.Results = customDomainsList.Count > 0 ?
                ResponseResult.Create(
                    new CustomDomainListCommandResult(customDomainsList),
                    AzureSignalRJsonContext.Default.CustomDomainListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing SignalR custom domains");
            HandleException(context, ex);
        }

        return context.Response;
    }

    public record CustomDomainListCommandResult(List<SignalRCustomDomainModel> CustomDomains);
}
