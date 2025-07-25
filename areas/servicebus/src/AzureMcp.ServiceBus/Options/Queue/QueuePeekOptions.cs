// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.ServiceBus.Options.Queue;

public class QueuePeekOptions : BaseQueueOptions
{
    /// <summary>
    /// Maximum number of messages to peek from queue.
    /// </summary>
    public int? MaxMessages { get; set; }
}
