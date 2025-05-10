// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Arguments.ServiceBus.Queue
{
    public class BaseQueueArguments : SubscriptionArguments
    {
        /// <summary>
        /// Service Bus namespace.
        /// </summary>
        public string? Namespace { get; set; }

        /// <summary>
        /// Name of the queue.
        /// </summary>
        public string? Name { get; set; }
    }
}
