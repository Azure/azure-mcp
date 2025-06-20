// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Subscription.Commands;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Subscription;

public class SubscriptionSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        // Create Subscription command group
        var subscription = new CommandGroup("subscription", "Azure subscription operations - Commands for listing and managing Azure subscriptions accessible to your account.");
        rootGroup.AddSubGroup(subscription);

        // Register Subscription commands
        subscription.AddCommand("list", new SubscriptionListCommand(loggerFactory.CreateLogger<SubscriptionListCommand>()));
    }
}
