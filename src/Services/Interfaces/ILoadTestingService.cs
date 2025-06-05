// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.LoadTesting;
using AzureMcp.Options;

namespace AzureMcp.Services.Interfaces;

public interface ILoadTestingService
{
    Task<List<LoadTestResource>> GetLoadTestsForSubscriptionAsync(string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null);

}
