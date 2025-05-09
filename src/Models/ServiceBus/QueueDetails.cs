using Azure.Messaging.ServiceBus.Administration;

namespace AzureMcp.Models.ServiceBus
{
    public class QueueDetails
    {
        public string Name { get; set; } = string.Empty;

        public TimeSpan LockDuration { get; set; }

        public bool RequiresSession { get; set; }

        public TimeSpan DefaultMessageTimeToLive { get; set; }

        public bool DeadLetteringOnMessageExpiration { get; set; }

        public int MaxDeliveryCount { get; set; }

        public EntityStatus Status { get; set; }

        public string ForwardTo { get; set; } = string.Empty;

        public string ForwardDeadLetteredMessagesTo { get; set; } = string.Empty;

        public bool EnablePartitioning { get; set; }

        public long? MaxMessageSizeInKilobytes { get; set; }

        public long MaxSizeInMegabytes { get; set; }

        public long TotalMessageCount { get; set; }

        public long ActiveMessageCount { get; set; }

        public long DeadLetterMessageCount { get; set; }

        public long ScheduledMessageCount { get; set; }

        public long TransferMessageCount { get; set; }

        public long TransferDeadLetterMessageCount { get; set; }

        public long SizeInBytes { get; set; }
    }
}
