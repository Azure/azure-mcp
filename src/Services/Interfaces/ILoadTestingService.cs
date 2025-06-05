// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Options;
using AzureMcp.Models.LoadTesting;

namespace AzureMcp.Services.Interfaces;

public interface ILoadTestingService
{
    Task<List<LoadTestResource>> GetLoadTestsForSubscriptionAsync(string subscriptionId, string tenant, RetryPolicyOptions? retryPolicy = null);

}