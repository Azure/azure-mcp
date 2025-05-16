// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using Azure.Messaging.ServiceBus;
using AzureMcp.Arguments.ServiceBus.Subscription;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.ServiceBus.Topic;

public sealed class SubscriptionPeekCommand : SubscriptionCommand<SubscriptionPeekArguments>
{
    private const string _commandTitle = "Peek Messages from Service Bus Topic Subscription";
    private readonly Option<string> _topicOption = ArgumentDefinitions.ServiceBus.Topic.ToOption();
    private readonly Option<string> _subscriptionNameOption = ArgumentDefinitions.ServiceBus.Subscription.ToOption();
    private readonly Option<int> _maxMessagesOption = ArgumentDefinitions.ServiceBus.MaxMessages.ToOption();
    private readonly Option<string> _namespaceOption = ArgumentDefinitions.ServiceBus.Namespace.ToOption();

    public override string Name => "peek";

    public override string Description =>
        """
        Peek messages from a Service Bus subscription without removing them.  Message browsing, or peeking, enables a
        Service Bus client to enumerate all messages in a subscription, for diagnostic and debugging purposes.
        The peek operation returns active, locked, and deferred messages in the subscription.

        Returns message content, properties, and metadata.  Messages remain in the subscription after peeking.

        Required arguments:
        - namespace: The fully qualified Service Bus namespace host name. (This is usually in the form <namespace>.servicebus.windows.net)
        - topic-name: Topic name containing the subscription
        - subscription-name: Subscription name to peek messages from
        """;

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_namespaceOption);
        command.AddOption(_topicOption);
        command.AddOption(_subscriptionNameOption);
        command.AddOption(_maxMessagesOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateSubscriptionNameArgument());
        AddArgument(CreateTopicNameArgument());
        AddArgument(CreateNamespaceArgument());
        AddArgument(CreateMaxMessageArgument());
    }

    protected override SubscriptionPeekArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.SubscriptionName = parseResult.GetValueForOption(_subscriptionNameOption);
        args.TopicName = parseResult.GetValueForOption(_topicOption);
        args.Namespace = parseResult.GetValueForOption(_namespaceOption);
        args.MaxMessages = parseResult.GetValueForOption(_maxMessagesOption);
        return args;
    }

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
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
            var messages = await service.PeekSubscriptionMessages(
                args.Namespace!,
                args.TopicName!,
                args.SubscriptionName!,
                args.MaxMessages ?? 1,
                args.Tenant,
                args.RetryPolicy);

            var peekedMessages = messages ?? new List<ServiceBusReceivedMessage>();

            context.Response.Results = ResponseResult.Create(
                new SubscriptionPeekCommandResult(peekedMessages),
                ServiceBusJsonContext.Default.SubscriptionPeekCommandResult);
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
            $"Subscription not found. Please check the topic and subscription name and try again.",
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        ServiceBusException sbEx when sbEx.Reason == ServiceBusFailureReason.MessagingEntityNotFound => 404,
        _ => base.GetStatusCode(ex)
    };

    private static ArgumentBuilder<SubscriptionPeekArguments> CreateTopicNameArgument()
    {
        return ArgumentBuilder<SubscriptionPeekArguments>
            .Create(ArgumentDefinitions.ServiceBus.Topic.Name, ArgumentDefinitions.ServiceBus.Topic.Description)
            .WithValueAccessor(args => args.TopicName ?? string.Empty)
            .WithIsRequired(true);
    }

    private static ArgumentBuilder<SubscriptionPeekArguments> CreateSubscriptionNameArgument()
    {
        return ArgumentBuilder<SubscriptionPeekArguments>
            .Create(ArgumentDefinitions.ServiceBus.Subscription.Name, ArgumentDefinitions.ServiceBus.Subscription.Description)
            .WithValueAccessor(args => args.SubscriptionName ?? string.Empty)
            .WithIsRequired(true);
    }

    private static ArgumentBuilder<SubscriptionPeekArguments> CreateNamespaceArgument()
    {
        return ArgumentBuilder<SubscriptionPeekArguments>
            .Create(ArgumentDefinitions.ServiceBus.Namespace.Name, ArgumentDefinitions.ServiceBus.Namespace.Description)
            .WithValueAccessor(args => args.Namespace ?? string.Empty)
            .WithIsRequired(true);
    }

    private static ArgumentBuilder<SubscriptionPeekArguments> CreateMaxMessageArgument()
    {
        return ArgumentBuilder<SubscriptionPeekArguments>
            .Create(ArgumentDefinitions.ServiceBus.MaxMessages.Name, ArgumentDefinitions.ServiceBus.MaxMessages.Description)
            .WithValueAccessor(args => args.MaxMessages?.ToString() ?? "1")
            .WithIsRequired(false);
    }

    internal record SubscriptionPeekCommandResult(List<ServiceBusReceivedMessage> Messages);
}
