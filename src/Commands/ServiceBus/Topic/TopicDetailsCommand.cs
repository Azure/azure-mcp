// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Messaging.ServiceBus;
using AzureMcp.Arguments.ServiceBus.Subscription;
using AzureMcp.Models.Argument;
using AzureMcp.Models.ServiceBus;
using AzureMcp.Services.Interfaces;

namespace AzureMcp.Commands.ServiceBus.Topic;

public sealed class TopicDetailsCommand : SubscriptionCommand<BaseTopicArguments>
{
    private const string _commandTitle = "Get Service Bus Topic Details";
    private readonly Option<string> _topicOption = ArgumentDefinitions.ServiceBus.Topic;
    private readonly Option<string> _namespaceOption = ArgumentDefinitions.ServiceBus.Namespace;

    public override string Name => "details";

    public override string Description =>
        """
        Get details about a Service Bus topic. Returns topic properties and runtime information. Properties returned include
        number of subscriptions, max message size, max topic size, number of scheduled messages, etc.

        Required arguments:
        - namespace: The fully qualified Service Bus namespace host name. (This is usually in the form <namespace>.servicebus.windows.net)
        - topic-name: Topic name to get information about.
        """;

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_namespaceOption);
        command.AddOption(_topicOption);
    }



    protected override BaseTopicArguments BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.TopicName = parseResult.GetValueForOption(_topicOption);
        args.Namespace = parseResult.GetValueForOption(_namespaceOption);
        return args;
    }

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindOptions(parseResult);

        try
        {
            var validationResult = Validate(parseResult.CommandResult);

            if (!validationResult.IsValid)
            {
                context.Response.Status = 400;
                context.Response.Message = validationResult.ErrorMessage!;
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

    internal record TopicDetailsCommandResult(TopicDetails TopicDetails);
}
