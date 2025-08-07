// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.ServiceBus.Commands.Queue;
using AzureMcp.ServiceBus.Commands.Topic;
using AzureMcp.ServiceBus.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.ServiceBus;

public class ServiceBusSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IServiceBusService, ServiceBusService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var serviceBus = new CommandGroup("servicebus", "Service Bus operations - Commands for managing Azure Service Bus messaging infrastructure including queues, topics, and subscriptions for reliable asynchronous communication and enterprise application integration. Use this tool when you need to manage message queues for point-to-point communication, configure topics and subscriptions for publish-subscribe messaging patterns, monitor message processing, or set up enterprise messaging scenarios for decoupled application architectures. This tool supports reliable messaging, dead letter handling, and enterprise integration patterns. Do not use this tool for real-time communication, direct API calls, database operations, or simple HTTP-based messaging - Service Bus is designed for asynchronous, reliable messaging between distributed applications and services.");
        rootGroup.AddSubGroup(serviceBus);

        var queue = new CommandGroup("queue", "Queue operations - Commands for using Azure Service Bus queues.");
        // queue.AddCommand("peek", new QueuePeekCommand());
        queue.AddCommand("details", new QueueDetailsCommand(
            loggerFactory.CreateLogger<QueueDetailsCommand>()));

        var topic = new CommandGroup("topic", "Topic operations - Commands for using Azure Service Bus topics and subscriptions.");
        topic.AddCommand("details", new TopicDetailsCommand(
            loggerFactory.CreateLogger<TopicDetailsCommand>()));

        var subscription = new CommandGroup("subscription", "Subscription operations - Commands for using subscriptions within a Service Bus topic.");
        // subscription.AddCommand("peek", new SubscriptionPeekCommand());
        subscription.AddCommand("details", new SubscriptionDetailsCommand(
            loggerFactory.CreateLogger<SubscriptionDetailsCommand>()));

        serviceBus.AddSubGroup(queue);
        serviceBus.AddSubGroup(topic);

        topic.AddSubGroup(subscription);
    }
}
