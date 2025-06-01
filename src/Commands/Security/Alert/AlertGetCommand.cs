// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Options.Security.Alert;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Security.Alert;

/// <summary>
/// Command to get a security alert by ID.
/// </summary>
public sealed class AlertGetCommand(ILogger<AlertGetCommand> logger) : SubscriptionCommand<AlertGetOptions>()
{
    private const string _commandTitle = "Get Security Alert";
    private readonly ILogger<AlertGetCommand> _logger = logger;
    private readonly Option<string> _alertIdOption = OptionDefinitions.Security.SystemAlertId;

    public override string Name => "get";

    public override string Description =>
        """
        Get a security alert by ID from Defender for Cloud. This command retrieves detailed information
        about a specific security alert, including its severity, status, entities, and remediation steps.
        Supports searching within a specific resource group or across an entire subscription.
        """;

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_alertIdOption);
    }
    protected override AlertGetOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.SystemAlertId = parseResult.GetValueForOption(_alertIdOption)!;
        return options;
    }

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            var securityService = context.GetService<ISecurityService>();            // Get the subscription ID to use
            var subscriptionId = options.Subscription;
            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                context.Response.Status = 400;
                context.Response.Message = "No subscription ID specified";
                return context.Response;
            }
            _logger.LogInformation("Retrieving security alert {AlertId} from subscription {SubscriptionId}", options.SystemAlertId, subscriptionId);

            var alerts = await securityService.GetAlertAsync(
                subscriptionId,
                options.SystemAlertId,
                options.ResourceGroup);

            if (alerts.Count == 0)
            {
                context.Response.Status = 404;
                context.Response.Message = $"Security alert '{options.SystemAlertId}' not found";
                return context.Response;
            }

            // Return the raw JSON data for maximum flexibility
            context.Response.Results = alerts.Count > 0
                ? ResponseResult.Create(alerts, JsonSourceGenerationContext.Default.ListJsonElement)
                : null;
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogError(ex, "Access denied when retrieving security alert {AlertId}", options.SystemAlertId);
            context.Response.Status = 403;
            context.Response.Message = ex.Message;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve security alert {AlertId}", options.SystemAlertId);
            context.Response.Status = 500;
            context.Response.Message = $"Failed to retrieve security alert: {ex.Message}";
        }
        return context.Response;
    }
}
