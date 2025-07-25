// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Messaging.ServiceBus;
using AzureMcp.Core.Commands.Subscription;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.ServiceBus.Commands;
using AzureMcp.ServiceBus.Models;
using AzureMcp.ServiceBus.Options;
using AzureMcp.ServiceBus.Options.Topic;
using AzureMcp.ServiceBus.Services;

namespace AzureMcp.ServiceBus.Commands.Topic;

public sealed class SubscriptionDetailsCommand : SubscriptionCommand<SubscriptionDetailsOptions>
{
    private const string CommandTitle = "Get Service Bus Topic Subscription Details";
    private readonly Option<string> _namespaceOption = ServiceBusOptionDefinitions.Namespace;
    private readonly Option<string> _topicOption = ServiceBusOptionDefinitions.Topic;
    private readonly Option<string> _subscriptionNameOption = ServiceBusOptionDefinitions.Subscription;

    public override string Name => "details";

    public override string Description =>
        """
        Get details about a Service Bus subscription. Returns subscription runtime properties including message counts, delivery settings, and other metadata.

        Required arguments:
        - namespace: The fully qualified Service Bus namespace host name. (This is usually in the form <namespace>.servicebus.windows.net)
        - topic-name: Topic name containing the subscription
        - subscription-name: Name of the subscription to get details for
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_namespaceOption);
        command.AddOption(_topicOption);
        command.AddOption(_subscriptionNameOption);
    }



    protected override SubscriptionDetailsOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Namespace = parseResult.GetValueForOption(_namespaceOption);
        options.TopicName = parseResult.GetValueForOption(_topicOption);
        options.SubscriptionName = parseResult.GetValueForOption(_subscriptionNameOption);
        return options;
    }

    [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            context.Activity?.WithSubscriptionTag(options);

            var service = context.GetService<IServiceBusService>();
            var details = await service.GetSubscriptionDetails(
                options.Namespace!,
                options.TopicName!,
                options.SubscriptionName!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new SubscriptionDetailsCommandResult(details),
                ServiceBusJsonContext.Default.SubscriptionDetailsCommandResult);
        }
        catch (Exception ex)
        {
            HandleException(context, ex);
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
