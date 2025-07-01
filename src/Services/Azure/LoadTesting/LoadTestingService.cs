// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.Developer.LoadTesting;
using AzureMcp.Models.LoadTesting.LoadTestResource;
using AzureMcp.Models.LoadTesting.LoadTestRun;
using AzureMcp.Options;
using AzureMcp.Services.Interfaces;
using Newtonsoft.Json;

namespace AzureMcp.Services.Azure.LoadTesting;

public class LoadTestingService(ISubscriptionService subscriptionService) : BaseAzureService, ILoadTestingService
{

    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private const string ARMEndpoint = "https://management.azure.com";
    private const string ControlPlaneApiVersion = "2022-12-01";
    public async Task<List<TestResource>> GetLoadTestResourcesAsync(string subscriptionId, string? resourceGroup = null, string? testResourceName = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId);

        var credential = await GetCredential(tenant);
        var endpoint = $"{ARMEndpoint}/subscriptions/{subscriptionId}";

        if (!string.IsNullOrEmpty(resourceGroup))
        {
            endpoint += $"/resourceGroups/{resourceGroup}";
        }

        if (!string.IsNullOrEmpty(testResourceName))
        {
            endpoint += $"/providers/Microsoft.LoadTestService/loadTests/{testResourceName}?api-version={ControlPlaneApiVersion}";
        }
        else
        {
            endpoint += $"/providers/Microsoft.LoadTestService/loadTests?api-version={ControlPlaneApiVersion}";
        }

        var client = new HttpClient();
        var token = (await credential.GetTokenAsync(new TokenRequestContext(new[] { "https://management.azure.com/.default" }), CancellationToken.None)).Token;
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        client.DefaultRequestHeaders.Add("x-ms-client-request-id", Guid.NewGuid().ToString());
        client.DefaultRequestHeaders.Add("User-Agent", UserAgent);

        var response = await client.GetAsync(endpoint);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to retrieve Azure Load Testing resources: {response.ReasonPhrase}");
        }
        var content = await response.Content.ReadAsStringAsync();
        if (!string.IsNullOrEmpty(testResourceName))
        {
            var TestResource = JsonConvert.DeserializeObject<TestResource>(content);
            return TestResource != null ? new List<TestResource> { TestResource } : new List<TestResource>();
        }

        // Azure ARM list APIs return an object with a 'value' property containing the array
        using var doc = JsonDocument.Parse(content);
        if (!doc.RootElement.TryGetProperty("value", out var valueElement) || valueElement.ValueKind != JsonValueKind.Array)
        {
            throw new Exception($"Unexpected response format: missing 'value' array. Raw response: {content}");
        }
        return JsonConvert.DeserializeObject<List<TestResource>>(valueElement.GetRawText()) ?? new List<TestResource>();
    }

    public async Task<TestRun> GetLoadTestRunAsync(string subscriptionId, string testResourceName, string testRunId, string? resourceGroup = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId, testResourceName, testRunId);
        var loadTestResource = await GetLoadTestResourcesAsync(subscriptionId, resourceGroup, testResourceName, tenant, retryPolicy);
        if (loadTestResource.Count == 0)
        {
            throw new Exception($"Load Test '{testResourceName}' not found in subscription '{subscriptionId}' and resource group '{resourceGroup}'.");
        }
        var dataPlaneUri = loadTestResource[0].Properties?.DataPlaneUri;
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

        var loadTestRun = loadTestRunResponse.Content.ToString();
        return JsonConvert.DeserializeObject<TestRun>(loadTestRun) ?? new TestRun();
    }

    public async Task<List<TestRun>> GetLoadTestRunsFromTestIdAsync(string subscriptionId, string testResourceName, string testId, string? resourceGroup = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId, testResourceName, testId);
        var loadTestResource = await GetLoadTestResourcesAsync(subscriptionId, resourceGroup, testResourceName, tenant, retryPolicy);
        if (loadTestResource.Count == 0)
        {
            throw new Exception($"Load Test '{testResourceName}' not found in subscription '{subscriptionId}' and resource group '{resourceGroup}'.");
        }
        var dataPlaneUri = loadTestResource[0].Properties?.DataPlaneUri;
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
            var testRun = JsonConvert.DeserializeObject<TestRun>(binaryData.ToString());
            if (testRun != null)
            {
                testRuns.Add(testRun);
            }
        }

        if (testRuns.Count == 0)
        {
            throw new Exception($"No test runs found for test ID '{testId}' in Load Test '{testResourceName}'.");
        }

        Console.WriteLine($"Retrieved {JsonConvert.SerializeObject(testRuns)} test runs for test ID '{testId}' in Load Test '{testResourceName}'.");
        return testRuns;
    }

    public async Task<TestRun> CreateLoadTestRunAsync(string subscriptionId, string testResourceName, string testId, string? testRunId = null, string? resourceGroup = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId, testResourceName, testRunId);
        var loadTestResource = await GetLoadTestResourcesAsync(subscriptionId, resourceGroup, testResourceName, tenant, retryPolicy);
        if (loadTestResource.Count == 0)
        {
            throw new Exception($"Load Test '{testResourceName}' not found in subscription '{subscriptionId}' and resource group '{resourceGroup}'.");
        }
        var dataPlaneUri = loadTestResource[0].Properties?.DataPlaneUri;
        if (string.IsNullOrEmpty(dataPlaneUri))
        {
            throw new Exception($"Data Plane URI for Load Test '{testResourceName}' is not available.");
        }

        var credential = await GetCredential(tenant);
        var loadTestClient = new LoadTestRunClient(new Uri($"https://{dataPlaneUri}"), credential);

        TestRunCreateRequest createRequest = new TestRunCreateRequest
        {
            TestId = testId,
            DisplayName = "TestRun_" + DateTime.UtcNow.ToString("dd/MM/yyyy") + "_" + DateTime.UtcNow.ToString("HH:mm:ss"),
        };

        RequestContent content = RequestContent.Create(JsonConvert.SerializeObject(createRequest));
        var loadTestRunResponse = await loadTestClient.BeginTestRunAsync(0, testRunId, content);
        if (loadTestRunResponse == null)
        {
            throw new Exception($"Failed to retrieve Azure Load Test Run: {loadTestRunResponse}");
        }

        var loadTestRun = loadTestRunResponse.Value.ToString();
        return JsonConvert.DeserializeObject<TestRun>(loadTestRun) ?? new TestRun();
    }
}
