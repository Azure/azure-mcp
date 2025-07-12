// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Support.Models;
using AzureMcp.Areas.Support.Options;
using AzureMcp.Areas.Support.Options.Ticket;
using AzureMcp.Areas.Support.Services;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Support.Commands.Ticket;

public sealed class TicketListCommand(ILogger<TicketListCommand> logger) 
    : BaseSupportCommand<TicketListOptions>(logger)
{
    private const string CommandTitle = "List Support Tickets";
    
    public override string Name => "list";

    public override string Description =>
        """
        List and filter Azure support tickets in your subscription. You can filter by status, severity, 
        or search terms to find specific tickets. Returns an array of support ticket objects with details 
        including ticket ID, title, status, severity, and contact information.
        """;

    public override string Title => CommandTitle;
    
    internal record TicketListCommandResult(List<SupportTicket> Tickets);

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(SupportOptionDefinitions.Filter);
        command.AddOption(SupportOptionDefinitions.Top);
    }

    protected override TicketListOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Filter = parseResult.GetValueForOption(SupportOptionDefinitions.Filter);
        options.Top = parseResult.GetValueForOption(SupportOptionDefinitions.Top);
        return options;
    }

    [McpServerTool(
        Destructive = false,
        ReadOnly = true,
        Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            _logger.LogInformation("Listing support tickets for subscription {Subscription}", options.Subscription);
            
            // Add subscription information for telemetry
            context.Activity?.WithSubscriptionTag(options);

            var supportService = context.GetService<ISupportService>();
            
            var tickets = await supportService.ListSupportTickets(
                options.Subscription!,
                options.Filter,
                options.Top,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = tickets?.Count > 0
                ? ResponseResult.Create(
                    new TicketListCommandResult(tickets),
                    SupportJsonContext.Default.TicketListCommandResult)
                : null;
            
            _logger.LogInformation("Successfully listed {Count} support tickets", tickets?.Count ?? 0);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list support tickets for subscription {Subscription}", options.Subscription);
            HandleException(context, ex);
        }

        return context.Response;
    }

    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        UnauthorizedAccessException => "You don't have permission to access support tickets in this subscription. Ensure you have the appropriate role assignments.",
        ArgumentException argEx when argEx.ParamName == "subscription" => "Invalid subscription ID or name provided.",
        ArgumentException argEx when argEx.Message.Contains("OData filter") => $"Invalid filter: {argEx.Message}",
        TimeoutException => "Request timed out while retrieving support tickets. Try again or use a smaller page size.",
        InvalidOperationException when ex.Message.Contains("not found") => "Subscription not found or you don't have access to it.",
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        UnauthorizedAccessException => 403,
        ArgumentException => 400,
        TimeoutException => 408,
        InvalidOperationException when ex.Message.Contains("not found") => 404,
        _ => base.GetStatusCode(ex)
    };
}
