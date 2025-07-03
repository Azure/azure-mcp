// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using AzureMcp.Models.LoadTesting.LoadTest;
using AzureMcp.Models.LoadTesting.LoadTestResource;
using AzureMcp.Models.LoadTesting.LoadTestRun;
using AzureMcp.Options;

namespace AzureMcp.Services.Interfaces;

public interface ILoadTestingService
{
    Task<List<TestResource>> GetLoadTestResourcesAsync(string subscriptionId, string? resourceGroup = null, string? testResourceName = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    Task<TestRun> GetLoadTestRunAsync(string subscriptionId, string testResourceName, string testRunId, string? resourceGroup = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    Task<TestRun> CreateOrUpdateLoadTestRunAsync(string subscriptionId, string testResourceName, string testId, string? testRunId = null, string? oldTestRunId = null, string? resourceGroup = null, string? tenant = null, string? displayName = null, string? description = null, bool? debugMode = false, RetryPolicyOptions? retryPolicy = null);
    Task<List<TestRun>> GetLoadTestRunsFromTestIdAsync(string subscriptionId, string testResourceName, string testId, string? resourceGroup = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    Task<Test> GetTestAsync(string subscriptionId, string testResourceName, string testId, string? resourceGroup = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
}
