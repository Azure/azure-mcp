// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Arguments.ServiceBus.Queue;

public class QueuePeekArguments : BaseQueueArguments
{
    /// <summary>
    /// Maximum number of messages to peek from queue.
    /// </summary>
    public int? MaxMessages { get; set; }
}
