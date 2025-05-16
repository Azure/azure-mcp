// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using Azure.Messaging.ServiceBus;
using AzureMcp.Arguments.ServiceBus.Queue;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.ServiceBus.Queue;

public sealed class QueuePeekCommand : SubscriptionCommand<QueuePeekArguments>
{
    private const string _commandTitle = "Peek Messages from Service Bus Queue";
    private readonly Option<string> _queueOption = ArgumentDefinitions.ServiceBus.Queue.ToOption();
    private readonly Option<int> _maxMessagesOption = ArgumentDefinitions.ServiceBus.MaxMessages.ToOption();
    private readonly Option<string> _namespaceOption = ArgumentDefinitions.ServiceBus.Namespace.ToOption();

    public override string Name => "peek";

    public override string Description =>
        """
        Peek messages from a Service Bus queue without removing them.  Message browsing, or peeking, enables a
        Service Bus client to enumerate all messages in a queue, for diagnostic and debugging purposes.
        The peek operation returns active, locked, deferred, and scheduled messages in the queue.

        Returns message content, properties, and metadata.  Messages remain in the queue after peeking.

        Required arguments:
        - namespace: The fully qualified Service Bus namespace host name. (This is usually in the form <namespace>.servicebus.windows.net)
        - queue: Queue name to peek messages from
        """;

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_namespaceOption);
        command.AddOption(_queueOption);
        command.AddOption(_maxMessagesOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateQueueArgument());
        AddArgument(CreateNamespaceArgument());
        AddArgument(CreateMaxMessageArgument());
    }

    protected override QueuePeekArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Name = parseResult.GetValueForOption(_queueOption);
        args.Namespace = parseResult.GetValueForOption(_namespaceOption);
        args.MaxMessages = parseResult.GetValueForOption(_maxMessagesOption);
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
            var messages = await service.PeekQueueMessages(
                args.Namespace!,
                args.Name!,
                args.MaxMessages ?? 1,
                args.Tenant,
                args.RetryPolicy);

            var peekedMessages = messages ?? new List<ServiceBusReceivedMessage>();

            context.Response.Results = ResponseResult.Create(
                new QueuePeekCommandResult(peekedMessages),
                ServiceBusJsonContext.Default.QueuePeekCommandResult);
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
            $"Queue not found. Please check the queue name and try again.",
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        ServiceBusException sbEx when sbEx.Reason == ServiceBusFailureReason.MessagingEntityNotFound => 404,
        _ => base.GetStatusCode(ex)
    };

    private static ArgumentBuilder<QueuePeekArguments> CreateQueueArgument()
    {
        return ArgumentBuilder<QueuePeekArguments>
            .Create(ArgumentDefinitions.ServiceBus.Queue.Name, ArgumentDefinitions.ServiceBus.Queue.Description)
            .WithValueAccessor(args => args.Name ?? string.Empty)
            .WithIsRequired(true);
    }

    private static ArgumentBuilder<QueuePeekArguments> CreateNamespaceArgument()
    {
        return ArgumentBuilder<QueuePeekArguments>
            .Create(ArgumentDefinitions.ServiceBus.Namespace.Name, ArgumentDefinitions.ServiceBus.Namespace.Description)
            .WithValueAccessor(args => args.Namespace ?? string.Empty)
            .WithIsRequired(true);
    }

    private static ArgumentBuilder<QueuePeekArguments> CreateMaxMessageArgument()
    {
        return ArgumentBuilder<QueuePeekArguments>
            .Create(ArgumentDefinitions.ServiceBus.Namespace.Name, ArgumentDefinitions.ServiceBus.Namespace.Description)
            .WithValueAccessor(args => args.MaxMessages?.ToString() ?? "1")
            .WithIsRequired(true);
    }

    internal record QueuePeekCommandResult(List<ServiceBusReceivedMessage> Messages);
}
