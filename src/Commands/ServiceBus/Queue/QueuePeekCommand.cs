// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json.Serialization.Metadata;
using Azure.Messaging.ServiceBus;
using AzureMcp.Arguments.ServiceBus.Queue;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.ServiceBus.Queue;

public sealed class QueuePeekCommand : SubscriptionCommand<QueuePeekArguments>
{
    private readonly Option<string> _queueOption;
    private readonly Option<int> _maxMessagesOption;
    private readonly Option<string> _namespaceOption;

    public QueuePeekCommand() : base()
    {
        _queueOption = ArgumentDefinitions.ServiceBus.Queue.ToOption();
        _maxMessagesOption = ArgumentDefinitions.ServiceBus.MaxMessages.ToOption();
        _namespaceOption = ArgumentDefinitions.ServiceBus.Namespace.ToOption();
    }

    protected override string GetCommandName() => "peek";

    protected override string GetCommandDescription() =>
        """
        Peek messages from a Service Bus queue without removing them.
        Returns message content, properties, and metadata.
        Messages remain in the queue after peeking.
        
        Required arguments:
        - namespace: Service Bus namespace name
        - queue: Queue name to peek messages from
        """;

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
                args.Subscription!,
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
