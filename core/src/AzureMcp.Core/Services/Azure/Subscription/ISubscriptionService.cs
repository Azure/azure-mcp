// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.Resources;
using AzureMcp.Core.Options;

namespace AzureMcp.Core.Services.Azure.Subscription;

public interface ISubscriptionService
{
    Task<List<SubscriptionData>> GetSubscriptions(string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    Task<SubscriptionResource> GetSubscription(string subscription, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    bool IsSubscriptionId(string subscription, string? tenant = null);
    Task<string> GetSubscriptionIdByName(string subscriptionName, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    Task<string> GetSubscriptionNameById(string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
}
