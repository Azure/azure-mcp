// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.LoadTesting;
using AzureMcp.Options;

namespace AzureMcp.Services.Interfaces;

public interface ILoadTestingService
{
    Task<List<LoadTestResource>> GetLoadTestsAsync(string subscriptionId, string? resourceGroup = null, string? loadTestName = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null);

}
