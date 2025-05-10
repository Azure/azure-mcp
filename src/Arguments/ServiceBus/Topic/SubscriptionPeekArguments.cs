// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Arguments.ServiceBus.Subscription;

public class SubscriptionPeekArguments : BaseTopicArguments
{
    /// <summary>
    /// Name of the subscription.
    /// </summary>
    public string? SubscriptionName { get; set; }

    /// <summary>
    /// Maximum number of messages to peek from subscription.
    /// </summary>
    public int? MaxMessages { get; set; }
}
