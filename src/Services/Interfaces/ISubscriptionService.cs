// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.Resources;
using AzureMcp.Arguments;
using AzureMcp.Models.Argument;

namespace AzureMcp.Services.Interfaces;

public interface ISubscriptionService
{
    Task<List<ArgumentOption>> GetSubscriptions(string? tenant = null, RetryPolicyArguments? retryPolicy = null);
    Task<SubscriptionResource> GetSubscription(string subscription, string? tenant = null, RetryPolicyArguments? retryPolicy = null);
    bool IsSubscriptionId(string subscription, string? tenant = null);
    Task<string> GetSubscriptionIdByName(string subscriptionName, string? tenant = null, RetryPolicyArguments? retryPolicy = null);
    Task<string> GetSubscriptionNameById(string subscriptionId, string? tenant = null, RetryPolicyArguments? retryPolicy = null);
}
