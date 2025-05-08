using Azure.Messaging.ServiceBus;
using AzureMcp.Arguments;

namespace AzureMcp.Services.Interfaces;

public interface IServiceBusService
{
    /// <summary>
    /// Peeks messages from a Service Bus queue without removing them.
    /// </summary>
    /// <param name="namespaceName">The Service Bus namespace name</param>
    /// <param name="queueName">The queue name to peek messages from</param>
    /// <param name="maxMessages">Maximum number of messages to peek (default: 1)</param>
    /// <param name="subscription">Subscription ID or name</param>
    /// <param name="tenantId">Optional tenant ID</param>
    /// <param name="retryPolicy">Optional retry policy</param>
    /// <returns>List of peeked messages</returns>
    /// <exception cref="RequestFailedException">When the service request fails</exception>
    Task<List<ServiceBusReceivedMessage>> PeekQueueMessages(
        string namespaceName,
        string queueName,
        int maxMessages,
        string subscription,
        string? tenantId = null,
        RetryPolicyArguments? retryPolicy = null);
}
