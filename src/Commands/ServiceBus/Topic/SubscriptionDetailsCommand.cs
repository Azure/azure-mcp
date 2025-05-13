// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using Azure.Messaging.ServiceBus;
using AzureMcp.Arguments.ServiceBus.Topic;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Models.ServiceBus;
using AzureMcp.Services.Interfaces;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.ServiceBus.Topic;

public sealed class SubscriptionDetailsCommand : SubscriptionCommand<SubscriptionDetailsArguments>
{
    private readonly Option<string> _namespaceOption = ArgumentDefinitions.ServiceBus.Namespace.ToOption();
    private readonly Option<string> _topicOption = ArgumentDefinitions.ServiceBus.Topic.ToOption();
    private readonly Option<string> _subscriptionNameOption = ArgumentDefinitions.ServiceBus.Subscription.ToOption();

    protected override string GetCommandName() => "details";

    protected override string GetCommandDescription() =>
        """
        Get details about a Service Bus subscription. Returns subscription runtime properties including message counts, delivery settings, and other metadata.

        Required arguments:
        - namespace: Service Bus namespace name. (This is usually in the form <namespace>.servicebus.windows.net)
        - topic-name: Topic name containing the subscription
        - subscription-name: Name of the subscription to get details for
        """;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_namespaceOption);
        command.AddOption(_topicOption);
        command.AddOption(_subscriptionNameOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateSubscriptionNameArgument());
        AddArgument(CreateTopicNameArgument());
        AddArgument(CreateNamespaceArgument());
    }

    protected override SubscriptionDetailsArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Namespace = parseResult.GetValueForOption(_namespaceOption);
        args.TopicName = parseResult.GetValueForOption(_topicOption);
        args.SubscriptionName = parseResult.GetValueForOption(_subscriptionNameOption);
        return args;
    }

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);

        try
        {
            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            var service = context.GetService<IServiceBusService>();
            var details = await service.GetSubscriptionDetails(
                args.Namespace!,
                args.TopicName!,
                args.SubscriptionName!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new SubscriptionDetailsCommandResult(details),
                ServiceBusJsonContext.Default.SubscriptionDetailsCommandResult);
        }
        catch (Exception ex)
        {
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        ServiceBusException exception when exception.Reason == ServiceBusFailureReason.MessagingEntityNotFound =>
            $"Topic or subscription not found. Please check the topic and subscription names and try again.",
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        ServiceBusException sbEx when sbEx.Reason == ServiceBusFailureReason.MessagingEntityNotFound => 404,
        _ => base.GetStatusCode(ex)
    };

    private static ArgumentBuilder<SubscriptionDetailsArguments> CreateTopicNameArgument()
    {
        return ArgumentBuilder<SubscriptionDetailsArguments>
            .Create(ArgumentDefinitions.ServiceBus.Topic.Name, ArgumentDefinitions.ServiceBus.Topic.Description)
            .WithValueAccessor(args => args.TopicName ?? string.Empty)
            .WithIsRequired(true);
    }

    private static ArgumentBuilder<SubscriptionDetailsArguments> CreateSubscriptionNameArgument()
    {
        return ArgumentBuilder<SubscriptionDetailsArguments>
            .Create(ArgumentDefinitions.ServiceBus.Subscription.Name, ArgumentDefinitions.ServiceBus.Subscription.Description)
            .WithValueAccessor(args => args.SubscriptionName ?? string.Empty)
            .WithIsRequired(true);
    }

    private static ArgumentBuilder<SubscriptionDetailsArguments> CreateNamespaceArgument()
    {
        return ArgumentBuilder<SubscriptionDetailsArguments>
            .Create(ArgumentDefinitions.ServiceBus.Namespace.Name, ArgumentDefinitions.ServiceBus.Namespace.Description)
            .WithValueAccessor(args => args.Namespace ?? string.Empty)
            .WithIsRequired(true);
    }

    internal record SubscriptionDetailsCommandResult(SubscriptionDetails SubscriptionDetails);
}
