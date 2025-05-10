// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using Azure.Messaging.ServiceBus;
using AzureMcp.Arguments.ServiceBus.Subscription;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Models.ServiceBus;
using AzureMcp.Services.Interfaces;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.ServiceBus.Topic;

public sealed class TopicDetailsCommand : SubscriptionCommand<BaseTopicArguments>
{
    private readonly Option<string> _topicOption = ArgumentDefinitions.ServiceBus.Topic.ToOption();
    private readonly Option<string> _namespaceOption = ArgumentDefinitions.ServiceBus.Namespace.ToOption();

    protected override string GetCommandName() => "details";

    protected override string GetCommandDescription() =>
        """
        Get details about a Service Bus topic. Returns topic properties and runtime information. Properties returned include
        number of subscriptions, max message size, max topic size, number of scheduled messages, etc.

        Required arguments:
        - namespace: Service Bus namespace name. (This is usually in the form <namespace>.servicebus.windows.net)
        - topic-name: Topic name to get information about.
        """;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_namespaceOption);
        command.AddOption(_topicOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateTopicNameArgument());
        AddArgument(CreateNamespaceArgument());
    }

    protected override BaseTopicArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.TopicName = parseResult.GetValueForOption(_topicOption);
        args.Namespace = parseResult.GetValueForOption(_namespaceOption);
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
            var details = await service.GetTopicDetails(
                args.Namespace!,
                args.TopicName!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new TopicDetailsCommandResult(details),
                ServiceBusJsonContext.Default.TopicDetailsCommandResult);
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

    private static ArgumentBuilder<SubscriptionPeekArguments> CreateNamespaceArgument()
    {
        return ArgumentBuilder<SubscriptionPeekArguments>
            .Create(ArgumentDefinitions.ServiceBus.Namespace.Name, ArgumentDefinitions.ServiceBus.Namespace.Description)
            .WithValueAccessor(args => args.Namespace ?? string.Empty)
            .WithIsRequired(true);
    }

    internal record TopicDetailsCommandResult(TopicDetails TopicDetails);
}
