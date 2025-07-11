// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics;
using AzureMcp.Options;

namespace AzureMcp.Services.Telemetry
{
    internal static class TelemetryActivityExtensions
    {
        public static Activity? WithSubscriptionTag(this Activity? activity, SubscriptionOptions subscriptionOptions)
        {
            return activity?.AddTag(TelemetryConstants.TagName.SubscriptionGuid, subscriptionOptions.Subscription);
        }
    }
}
