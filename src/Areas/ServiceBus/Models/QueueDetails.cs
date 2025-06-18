// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Messaging.ServiceBus.Administration;

namespace AzureMcp.Areas.ServiceBus.Models;

public class QueueDetails
{
    public long ActiveMessageCount { get; set; }
    public bool DeadLetteringOnMessageExpiration { get; set; }
    public long DeadLetterMessageCount { get; set; }
    public TimeSpan DefaultMessageTimeToLive { get; set; }
    public bool EnablePartitioning { get; set; }
    public string? ForwardDeadLetteredMessagesTo { get; set; }
    public string? ForwardTo { get; set; }
    public TimeSpan LockDuration { get; set; }
    public int MaxDeliveryCount { get; set; }
    public long? MaxMessageSizeInKilobytes { get; set; }
    public long MaxSizeInMegabytes { get; set; }
    public string? Name { get; set; }
    public bool RequiresSession { get; set; }
    public long ScheduledMessageCount { get; set; }
    public long SizeInBytes { get; set; }
    public EntityStatus Status { get; set; }
    public long TotalMessageCount { get; set; }
    public long TransferDeadLetterMessageCount { get; set; }
    public long TransferMessageCount { get; set; }
}
