// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.ServiceBus.Subscription;

namespace AzureMcp.Arguments.ServiceBus.Topic;

public class SubscriptionDetailsArguments : BaseTopicArguments
{
    /// <summary>
    /// Name of the subscription.
    /// </summary>
    public string? SubscriptionName { get; set; }
}
