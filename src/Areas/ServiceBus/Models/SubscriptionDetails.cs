// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.ServiceBus.Models;

public class SubscriptionDetails
{
    public long ActiveMessageCount { get; set; }
    public bool DeadLetteringOnMessageExpiration { get; set; }
    public long DeadLetterMessageCount { get; set; }
    public bool EnableBatchedOperations { get; set; }
    public string? ForwardDeadLetteredMessagesTo { get; set; }
    public string? ForwardTo { get; set; }
    public TimeSpan LockDuration { get; set; }
    public int MaxDeliveryCount { get; set; }
    public bool RequiresSession { get; set; }
    public string? SubscriptionName { get; set; }
    public string? TopicName { get; set; }
    public long TotalMessageCount { get; set; }
    public long TransferDeadLetterMessageCount { get; set; }
    public long TransferMessageCount { get; set; }
}
