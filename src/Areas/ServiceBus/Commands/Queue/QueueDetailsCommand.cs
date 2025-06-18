// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Messaging.ServiceBus;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Models.ServiceBus;
using AzureMcp.Options.ServiceBus.Queue;
using AzureMcp.Services.Interfaces;

namespace AzureMcp.Commands.ServiceBus.Queue;

public sealed class QueueDetailsCommand : SubscriptionCommand<BaseQueueOptions>
{
    private const string CommandTitle = "Get Service Bus Queue Details";
    private readonly Option<string> _queueOption = OptionDefinitions.ServiceBus.Queue;
    private readonly Option<string> _namespaceOption = OptionDefinitions.ServiceBus.Namespace;

    public override string Name => "details";

    public override string Description =>
        """
        Get details about a Service Bus queue. Returns queue properties and runtime information. Properties returned include
        lock duration, max message size, queue size, creation date, status, current message counts, etc.

        Required arguments:
        - namespace: The fully qualified Service Bus namespace host name. (This is usually in the form <namespace>.servicebus.windows.net)
        - queue-name: Queue name to get details and runtime information for.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_namespaceOption);
        command.AddOption(_queueOption);
    }

    protected override BaseQueueOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Name = parseResult.GetValueForOption(_queueOption);
        options.Namespace = parseResult.GetValueForOption(_namespaceOption);
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

            var service = context.GetService<IServiceBusService>();
            var details = await service.GetQueueDetails(
                options.Namespace!,
                options.Name!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new QueueDetailsCommandResult(details),
                ServiceBusJsonContext.Default.QueueDetailsCommandResult);
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

    internal record QueueDetailsCommandResult(QueueDetails QueueDetails);
}
