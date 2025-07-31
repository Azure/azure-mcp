// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Options;

namespace AzureMcp.ServiceBus.Options.Topic;

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
