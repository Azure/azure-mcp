// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Options.ServiceBus.Subscription;

public class BaseTopicOptions : SubscriptionOptions
{
    /// <summary>
    /// Service Bus namespace.
    /// </summary>
    public string? Namespace { get; set; }

    /// <summary>
    /// Name of the topic.
    /// </summary>
    public string? TopicName { get; set; }
}
