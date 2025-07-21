// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using AzureMcp.Areas.LoadTesting.Models.LoadTest;
using AzureMcp.Areas.LoadTesting.Models.LoadTestingResource;
using AzureMcp.Areas.LoadTesting.Models.LoadTestRun;
using AzureMcp.Options;

namespace AzureMcp.Areas.LoadTesting.Services;

public interface ILoadTestingService
{
    Task<TestResource> CreateOrUpdateLoadTestingResourceAsync(string subscription, string resourceGroup, string? testResourceName = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    Task<List<TestResource>> GetLoadTestResourcesAsync(string subscription, string? resourceGroup = null, string? testResourceName = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    Task<TestRun> GetLoadTestRunAsync(string subscription, string testResourceName, string testRunId, string? resourceGroup = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    Task<TestRun> CreateOrUpdateLoadTestRunAsync(string subscription, string testResourceName, string testId, string? testRunId = null, string? oldTestRunId = null, string? resourceGroup = null, string? tenant = null, string? displayName = null, string? description = null, bool? debugMode = false, RetryPolicyOptions? retryPolicy = null);
    Task<List<TestRun>> GetLoadTestRunsFromTestIdAsync(string subscription, string testResourceName, string testId, string? resourceGroup = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    Task<Test> GetTestAsync(string subscription, string testResourceName, string testId, string? resourceGroup = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    Task<Test> CreateTestAsync(string subscription, string testResourceName, string testId, string? resourceGroup = null,
        string? displayName = null, string? description = null,
        int? duration = 20, int? virtualUsers = 50, int? rampUpTime = 1, string? endpointUrl = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
}
