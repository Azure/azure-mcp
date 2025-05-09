// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Messaging.ServiceBus;
using AzureMcp.Arguments;
using AzureMcp.Services.Interfaces;

namespace AzureMcp.Services.Azure.ServiceBus;

public class ServiceBusService : BaseAzureService, IServiceBusService
{
    public async Task<List<ServiceBusReceivedMessage>> PeekQueueMessages(
        string namespaceName,
        string queueName,
        int maxMessages,
        string subscription,
        string? tenantId = null,
        RetryPolicyArguments? retryPolicy = null)
    {
        var credential = await GetCredential(tenantId);

        await using (var client = new ServiceBusClient(namespaceName, credential))
        await using (var receiver = client.CreateReceiver(queueName))
        {
            var messages = await receiver.PeekMessagesAsync(maxMessages);

            return messages.ToList();
        }
    }

    public async Task<List<ServiceBusReceivedMessage>> PeekSubscriptionMessages(
        string namespaceName,
        string topicName,
        string subscriptionName,
        int maxMessages,
        string subscription,
        string? tenantId = null,
        RetryPolicyArguments? retryPolicy = null)
    {
        var credential = await GetCredential(tenantId);

        await using (var client = new ServiceBusClient(namespaceName, credential))
        await using (var receiver = client.CreateReceiver(topicName, subscriptionName))
        {
            var messages = await receiver.PeekMessagesAsync(maxMessages);

            return messages.ToList();
        }
    }
}
