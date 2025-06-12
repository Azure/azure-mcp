// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using AzureMcp.Models.LoadTesting.LoadTest;
using AzureMcp.Models.LoadTesting.LoadTestRun;
using AzureMcp.Options;

namespace AzureMcp.Services.Interfaces;

public interface ILoadTestingService
{
    Task<List<LoadTestResource>> GetLoadTestsAsync(string subscriptionId, string? resourceGroup = null, string? loadTestName = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    Task<LoadTestRunResource> GetLoadTestRunAsync(string subscriptionId, string loadTestName, string testRunId, string? resourceGroup = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    Task<LoadTestRunResource> CreateLoadTestRunAsync(string subscriptionId, string loadTestName, string testId, string? testRunId = null, string? resourceGroup = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
}
