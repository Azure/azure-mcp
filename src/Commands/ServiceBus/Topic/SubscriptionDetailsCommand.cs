// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Messaging.ServiceBus;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Models.ServiceBus;
using AzureMcp.Options.ServiceBus.Topic;
using AzureMcp.Services.Interfaces;

namespace AzureMcp.Commands.ServiceBus.Topic;

public sealed class SubscriptionDetailsCommand : SubscriptionCommand<SubscriptionDetailsOptions>
{
    private const string _commandTitle = "Get Service Bus Topic Subscription Details";
    private readonly Option<string> _namespaceOption = OptionDefinitions.ServiceBus.Namespace;
    private readonly Option<string> _topicOption = OptionDefinitions.ServiceBus.Topic;
    private readonly Option<string> _subscriptionNameOption = OptionDefinitions.ServiceBus.Subscription;

    public override string Name => "details";

    public override string Description =>
        """
        Get details about a Service Bus subscription. Returns subscription runtime properties including message counts, delivery settings, and other metadata.

        Required arguments:
        - namespace: The fully qualified Service Bus namespace host name. (This is usually in the form <namespace>.servicebus.windows.net)
        - topic-name: Topic name containing the subscription
        - subscription-name: Name of the subscription to get details for
        """;

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_namespaceOption);
        command.AddOption(_topicOption);
        command.AddOption(_subscriptionNameOption);
    }



    protected override SubscriptionDetailsOptions BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.Namespace = parseResult.GetValueForOption(_namespaceOption);
        args.TopicName = parseResult.GetValueForOption(_topicOption);
        args.SubscriptionName = parseResult.GetValueForOption(_subscriptionNameOption);
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

    internal record SubscriptionDetailsCommandResult(SubscriptionDetails SubscriptionDetails);
}
