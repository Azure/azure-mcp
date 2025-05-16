// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Reflection;
using Azure.ResourceManager.Resources;

namespace AzureMcp.Tests.Helpers;

public static class TestSubscriptionData
{
    private static readonly ConstructorInfo? Constructor = typeof(SubscriptionData).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault();

    public static SubscriptionData Create(string subscriptionId, string displayName)
    {
        if (Constructor == null)
        {
            throw new InvalidOperationException("Could not find constructor for SubscriptionData");
        }

        // Create subscription data using reflection
        var data = (SubscriptionData)Constructor.Invoke(Array.Empty<object>());

        // Use reflection to set the read-only properties
        typeof(SubscriptionData)
            .GetProperty(nameof(SubscriptionData.SubscriptionId))!
            .SetValue(data, subscriptionId);

        typeof(SubscriptionData)
            .GetProperty(nameof(SubscriptionData.DisplayName))!
            .SetValue(data, displayName);

        return data;
    }
}
