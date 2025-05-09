// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using AzureMcp.Arguments;
using AzureMcp.Models.ServiceBus;
using AzureMcp.Services.Interfaces;

namespace AzureMcp.Services.Azure.ServiceBus;

public class ServiceBusService : BaseAzureService, IServiceBusService
{
    public async Task<QueueDetails> GetQueueDetails(
        string namespaceName,
        string queueName,
        string? tenantId = null,
        RetryPolicyArguments? retryPolicy = null)
    {
        var credential = await GetCredential(tenantId);
        var client = new ServiceBusAdministrationClient(namespaceName, credential);
        var runtimeProperties = (await client.GetQueueRuntimePropertiesAsync(queueName)).Value;
        var properties = (await client.GetQueueAsync(queueName)).Value;

        return new QueueDetails
        {
            Name = runtimeProperties.Name,
            LockDuration = properties.LockDuration,
            RequiresSession = properties.RequiresSession,
            DefaultMessageTimeToLive = properties.DefaultMessageTimeToLive,
            DeadLetteringOnMessageExpiration = properties.DeadLetteringOnMessageExpiration,
            MaxDeliveryCount = properties.MaxDeliveryCount,
            Status = properties.Status,
            ForwardTo = properties.ForwardTo,
            ForwardDeadLetteredMessagesTo = properties.ForwardDeadLetteredMessagesTo,
            EnablePartitioning = properties.EnablePartitioning,
            MaxMessageSizeInKilobytes = properties.MaxMessageSizeInKilobytes,
            MaxSizeInMegabytes = properties.MaxSizeInMegabytes,
            TotalMessageCount = runtimeProperties.TotalMessageCount,
            ActiveMessageCount = runtimeProperties.ActiveMessageCount,
            DeadLetterMessageCount = runtimeProperties.DeadLetterMessageCount,
            TransferMessageCount = runtimeProperties.TransferMessageCount,
            TransferDeadLetterMessageCount = runtimeProperties.TransferDeadLetterMessageCount,
            SizeInBytes = runtimeProperties.SizeInBytes,
        };
    }

    public async Task<List<ServiceBusReceivedMessage>> PeekQueueMessages(
        string namespaceName,
        string queueName,
        int maxMessages,
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
