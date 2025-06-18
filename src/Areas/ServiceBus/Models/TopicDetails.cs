// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Messaging.ServiceBus.Administration;

namespace AzureMcp.Models.ServiceBus;

public class TopicDetails
{
    public TimeSpan DefaultMessageTimeToLive { get; set; }
    public bool EnablePartitioning { get; set; }
    public long? MaxMessageSizeInKilobytes { get; set; }
    public long MaxSizeInMegabytes { get; set; }
    public string? Name { get; set; }
    public long ScheduledMessageCount { get; set; }
    public long SizeInBytes { get; set; }
    public int SubscriptionCount { get; set; }
    public EntityStatus Status { get; set; }
}
