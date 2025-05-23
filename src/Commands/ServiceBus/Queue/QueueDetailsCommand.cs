// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Messaging.ServiceBus;
using AzureMcp.Arguments.ServiceBus.Queue;
using AzureMcp.Models.Argument;
using AzureMcp.Models.ServiceBus;
using AzureMcp.Services.Interfaces;

namespace AzureMcp.Commands.ServiceBus.Queue;

public sealed class QueueDetailsCommand : SubscriptionCommand<BaseQueueArguments>
{
    private const string _commandTitle = "Get Service Bus Queue Details";
    private readonly Option<string> _queueOption = ArgumentDefinitions.ServiceBus.Queue;
    private readonly Option<string> _namespaceOption = ArgumentDefinitions.ServiceBus.Namespace;

    public override string Name => "details";

    public override string Description =>
        """
        Get details about a Service Bus queue. Returns queue properties and runtime information. Properties returned include
        lock duration, max message size, queue size, creation date, status, current message counts, etc.

        Required arguments:
        - namespace: The fully qualified Service Bus namespace host name. (This is usually in the form <namespace>.servicebus.windows.net)
        - queue-name: Queue name to get details and runtime information for.
        """;

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_namespaceOption);
        command.AddOption(_queueOption);
    }

    protected override BaseQueueArguments BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.Name = parseResult.GetValueForOption(_queueOption);
        args.Namespace = parseResult.GetValueForOption(_namespaceOption);
        return args;
    }

    [McpServerTool(Destructive = false, ReadOnly = true)]
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
            var details = await service.GetQueueDetails(
                args.Namespace!,
                args.Name!,
                args.Tenant,
                args.RetryPolicy);

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
