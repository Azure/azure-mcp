// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Support.Commands.Ticket;
using AzureMcp.Areas.Support.Services;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Support;

public class SupportSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ISupportFilterProcessor, SupportFilterProcessor>();
        services.AddSingleton<ISupportService, SupportService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        // Create Support command group
        var support = new CommandGroup("support", "Support operations - Commands for managing and viewing Azure support tickets and cases.");
        rootGroup.AddSubGroup(support);

        // Create Support Ticket subgroup
        var ticket = new CommandGroup("ticket", "Support ticket operations - Commands for listing and managing Azure support tickets in your subscription.");
        support.AddSubGroup(ticket);

        // Register ticket commands
        ticket.AddCommand("list", new TicketListCommand(loggerFactory.CreateLogger<TicketListCommand>()));
    }
}
