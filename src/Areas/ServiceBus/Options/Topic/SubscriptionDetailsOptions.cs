// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Options.ServiceBus.Subscription;

namespace AzureMcp.Options.ServiceBus.Topic;

public class SubscriptionDetailsOptions : BaseTopicOptions
{
    /// <summary>
    /// Name of the subscription.
    /// </summary>
    public string? SubscriptionName { get; set; }
}
