// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using Azure;
using Azure.Core;
using Azure.Developer.LoadTesting;
using Azure.ResourceManager;
using Azure.ResourceManager.LoadTesting;
using Azure.ResourceManager.Resources;
using AzureMcp.Areas.LoadTesting.Commands;
using AzureMcp.Areas.LoadTesting.Models.LoadTest;
using AzureMcp.Areas.LoadTesting.Models.LoadTestingResource;
using AzureMcp.Areas.LoadTesting.Models.LoadTestRun;
using AzureMcp.Options;
using AzureMcp.Services.Azure;

namespace AzureMcp.Areas.LoadTesting.Services;

public class LoadTestingService() : BaseAzureService, ILoadTestingService
{

    /* Load Testing Resource APIs Start */
    public async Task<List<TestResource>> GetLoadTestResourcesAsync(string subscriptionId, string? resourceGroup = null, string? testResourceName = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId);

        var credential = await GetCredential(tenant);

        var client = new ArmClient(credential);
        if (!string.IsNullOrEmpty(testResourceName))
        {
            var resourceId = LoadTestingResource.CreateResourceIdentifier(subscriptionId, resourceGroup, testResourceName);
            var response = await client.GetLoadTestingResource(resourceId).GetAsync();

            if (response == null)
            {
                throw new Exception($"Failed to retrieve Azure Load Testing resources: {response}");
            }
            return new List<TestResource>
            {
                new TestResource
                {
                    Id = response.Value.Data.Id!,
                    Name = response.Value.Data.Name,
                    Location = response.Value.Data.Location,
                    DataPlaneUri = response.Value.Data.DataPlaneUri,
                    ProvisioningState = response.Value.Data.ProvisioningState?.ToString(),
                }
            };
        }
        else
        {
            var rgResource = ResourceGroupResource.CreateResourceIdentifier(subscriptionId, resourceGroup);
            var response = client.GetResourceGroupResource(rgResource).GetLoadTestingResources().ToList();

            if (response == null || response.Count == 0)
            {
                throw new Exception($"Failed to retrieve Azure Load Testing resources: {response}");
            }
            var loadTestResources = new List<TestResource>();
            foreach (var resource in response)
            {
                loadTestResources.Add(new TestResource
                {
                    Id = resource.Data.Id!,
                    Name = resource.Data.Name,
                    Location = resource.Data.Location,
                    DataPlaneUri = resource.Data.DataPlaneUri,
                    ProvisioningState = resource.Data.ProvisioningState?.ToString(),
                });
            }
            return loadTestResources;
        }
    }

    public async Task<TestResource> CreateOrUpdateLoadTestingResourceAsync(string subscriptionId, string resourceGroup, string? testResourceName = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId, resourceGroup);

        var credential = await GetCredential(tenant);

        var client = new ArmClient(credential);
        var rgResource = client.GetResourceGroupResource(ResourceGroupResource.CreateResourceIdentifier(subscriptionId, resourceGroup));
        if (testResourceName == null)
        {
            testResourceName = "TestRun_" + DateTime.UtcNow.ToString("dd-MM-yyyy") + "_" + DateTime.UtcNow.ToString("HH-mm-ss");
        }
        var location = rgResource.Get().Value.Data.Location;
        var response = await rgResource.GetLoadTestingResources().CreateOrUpdateAsync(WaitUntil.Completed, testResourceName, new LoadTestingResourceData(location));
        if (response == null || response.Value == null)
        {
            throw new Exception($"Failed to create or update Azure Load Testing resource: {response}");
        }

        return new TestResource
        {
            Id = response.Value.Data.Id!,
            Name = response.Value.Data.Name,
            Location = response.Value.Data.Location,
            DataPlaneUri = response.Value.Data.DataPlaneUri,
            ProvisioningState = response.Value.Data.ProvisioningState?.ToString(),
        };
    }
    /* Load Testing Resource APIs End */

    /* Load Testing Run APIs Start */
    public async Task<TestRun> GetLoadTestRunAsync(string subscriptionId, string testResourceName, string testRunId, string? resourceGroup = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId, testResourceName, testRunId);
        var loadTestResource = await GetLoadTestResourcesAsync(subscriptionId, resourceGroup, testResourceName, tenant, retryPolicy);
        if (loadTestResource == null)
        {
            throw new Exception($"Load Test '{testResourceName}' not found in subscription '{subscriptionId}' and resource group '{resourceGroup}'.");
        }
        var dataPlaneUri = loadTestResource[0]?.DataPlaneUri;
        if (string.IsNullOrEmpty(dataPlaneUri))
        {
            throw new Exception($"Data Plane URI for Load Test '{testResourceName}' is not available.");
        }

        var credential = await GetCredential(tenant);
        var loadTestClient = new LoadTestRunClient(new Uri($"https://{dataPlaneUri}"), credential);

        var loadTestRunResponse = await loadTestClient.GetTestRunAsync(testRunId);
        if (loadTestRunResponse == null || loadTestRunResponse.IsError)
        {
            throw new Exception($"Failed to retrieve Azure Load Test Run: {loadTestRunResponse}");
        }

        var loadTestRun = loadTestRunResponse.Content;
        return JsonSerializer.Deserialize(loadTestRun, LoadTestJsonContext.Default.TestRun) ?? new TestRun();
    }

    public async Task<List<TestRun>> GetLoadTestRunsFromTestIdAsync(string subscriptionId, string testResourceName, string testId, string? resourceGroup = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId, testResourceName, testId);
        var loadTestResource = await GetLoadTestResourcesAsync(subscriptionId, resourceGroup, testResourceName, tenant, retryPolicy);
        if (loadTestResource == null)
        {
            throw new Exception($"Load Test '{testResourceName}' not found in subscription '{subscriptionId}' and resource group '{resourceGroup}'.");
        }
        var dataPlaneUri = loadTestResource[0]?.DataPlaneUri;
        if (string.IsNullOrEmpty(dataPlaneUri))
        {
            throw new Exception($"Data Plane URI for Load Test '{testResourceName}' is not available.");
        }

        var credential = await GetCredential(tenant);
        var loadTestClient = new LoadTestRunClient(new Uri($"https://{dataPlaneUri}"), credential);

        var loadTestRunResponse = loadTestClient.GetTestRunsAsync(testId: testId);
        if (loadTestRunResponse == null)
        {
            throw new Exception($"Failed to retrieve Azure Load Test Run: {loadTestRunResponse}");
        }

        var testRuns = new List<TestRun>();
        await foreach (var binaryData in loadTestRunResponse)
        {
            var testRun = JsonSerializer.Deserialize<TestRun>(binaryData.ToString(), LoadTestJsonContext.Default.TestRun);
            if (testRun != null)
            {
                testRuns.Add(testRun);
            }
        }

        if (testRuns.Count == 0)
        {
            throw new Exception($"No test runs found for test ID '{testId}' in Load Test '{testResourceName}'.");
        }
        return testRuns;
    }

    public async Task<TestRun> CreateOrUpdateLoadTestRunAsync(string subscriptionId, string testResourceName, string testId, string? testRunId = null, string? oldTestRunId = null, string? resourceGroup = null, string? tenant = null, string? displayName = null, string? description = null, bool? debugMode = false, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId, testResourceName, testRunId);
        var loadTestResource = await GetLoadTestResourcesAsync(subscriptionId, resourceGroup, testResourceName, tenant, retryPolicy);
        if (loadTestResource == null)
        {
            throw new Exception($"Load Test '{testResourceName}' not found in subscription '{subscriptionId}' and resource group '{resourceGroup}'.");
        }
        var dataPlaneUri = loadTestResource[0]?.DataPlaneUri;
        if (string.IsNullOrEmpty(dataPlaneUri))
        {
            throw new Exception($"Data Plane URI for Load Test '{testResourceName}' is not available.");
        }

        var credential = await GetCredential(tenant);
        var loadTestClient = new LoadTestRunClient(new Uri($"https://{dataPlaneUri}"), credential);

        TestRunRequest requestBody = new TestRunRequest
        {
            TestId = testId,
            DisplayName = displayName ?? "TestRun_" + DateTime.UtcNow.ToString("dd/MM/yyyy") + "_" + DateTime.UtcNow.ToString("HH:mm:ss"),
            Description = description,
            DebugLogsEnabled = debugMode ?? false,
            RequestDataLevel = debugMode == true ? RequestDataLevel.ERRORS : RequestDataLevel.NONE,
        };

        var loadTestRunResponse = await loadTestClient.BeginTestRunAsync(0, testRunId, RequestContent.Create(JsonSerializer.Serialize(requestBody, LoadTestJsonContext.Default.TestRunRequest)), oldTestRunId: oldTestRunId);
        if (loadTestRunResponse == null)
        {
            throw new Exception($"Failed to retrieve Azure Load Test Run: {loadTestRunResponse}");
        }

        var loadTestRun = loadTestRunResponse.WaitForCompletionAsync().Result.Value.ToString();
        return JsonSerializer.Deserialize<TestRun>(loadTestRun, LoadTestJsonContext.Default.TestRun) ?? new TestRun();
    }
    /* Load Test Run APIs End */

    /* Load Test APIs Start */
    public async Task<Test> GetTestAsync(string subscriptionId, string testResourceName, string testId, string? resourceGroup = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId, testResourceName, testId);
        var loadTestResource = await GetLoadTestResourcesAsync(subscriptionId, resourceGroup, testResourceName, tenant, retryPolicy);
        if (loadTestResource == null)
        {
            throw new Exception($"Load Test '{testResourceName}' not found in subscription '{subscriptionId}' and resource group '{resourceGroup}'.");
        }
        var dataPlaneUri = loadTestResource[0]?.DataPlaneUri;
        if (string.IsNullOrEmpty(dataPlaneUri))
        {
            throw new Exception($"Data Plane URI for Load Test '{testResourceName}' is not available.");
        }

        var credential = await GetCredential(tenant);
        var loadTestClient = new LoadTestAdministrationClient(new Uri($"https://{dataPlaneUri}"), credential);

        var loadTestResponse = await loadTestClient.GetTestAsync(testId);
        if (loadTestResponse == null || loadTestResponse.IsError)
        {
            throw new Exception($"Failed to retrieve Azure Load Test: {loadTestResponse}");
        }

        var loadTest = loadTestResponse.Content.ToString();
        return JsonSerializer.Deserialize(loadTest, LoadTestJsonContext.Default.Test) ?? new Test();
    }
    public async Task<Test> CreateTestAsync(string subscriptionId, string testResourceName, string testId, string? resourceGroup = null,
        string? displayName = null, string? description = null,
        int? duration = 20, int? virtualUsers = 50, int? rampUpTime = 1, string? endpointUrl = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId, testResourceName, testId);
        var loadTestResource = await GetLoadTestResourcesAsync(subscriptionId, resourceGroup, testResourceName, tenant, retryPolicy);
        if (loadTestResource == null)
        {
            throw new Exception($"Load Test '{testResourceName}' not found in subscription '{subscriptionId}' and resource group '{resourceGroup}'.");
        }
        var dataPlaneUri = loadTestResource[0]?.DataPlaneUri;
        if (string.IsNullOrEmpty(dataPlaneUri))
        {
            throw new Exception($"Data Plane URI for Load Test '{testResourceName}' is not available.");
        }

        var credential = await GetCredential(tenant);
        var loadTestClient = new LoadTestAdministrationClient(new Uri($"https://{dataPlaneUri}"), credential);
        OptionalLoadTestConfig optionalLoadTestConfig = new OptionalLoadTestConfig
        {
            Duration = (duration ?? 20) * 60, // Convert minutes to seconds
            EndpointUrl = endpointUrl ?? "https://example.com",
            RampUpTime = (rampUpTime ?? 1) * 60, // Convert minutes to seconds
            VirtualUsers = virtualUsers ?? 50,
        };
        TestRequestPayload testRequestPayload = new TestRequestPayload
        {
            TestId = testId,
            DisplayName = displayName ?? "Test_" + DateTime.UtcNow.ToString("dd/MM/yyyy") + "_" + DateTime.UtcNow.ToString("HH:mm:ss"),
            Description = description,
            LoadTestConfiguration = new LoadTestConfiguration
            {
                OptionalLoadTestConfig = optionalLoadTestConfig,
                QuickStartTest = true, // Set to true for quick start tests (URL BASIC Test)
            }
        };

        var loadTestResponse = await loadTestClient.CreateOrUpdateTestAsync(testId, RequestContent.Create(JsonSerializer.Serialize(testRequestPayload, LoadTestJsonContext.Default.TestRequestPayload)));
        if (loadTestResponse == null || loadTestResponse.IsError)
        {
            throw new Exception($"Failed to create Azure Load Test: {loadTestResponse}");
        }

        var loadTest = loadTestResponse.Content.ToString();
        return JsonSerializer.Deserialize(loadTest, LoadTestJsonContext.Default.Test) ?? new Test();
    }

    /* Load Test APIs End */
}
