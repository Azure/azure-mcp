// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.Developer.LoadTesting;
using AzureMcp.Models.LoadTesting.LoadTest;
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
    public async Task<List<LoadTestResource>> GetLoadTestsAsync(string subscriptionId, string? resourceGroup = null, string? loadTestName = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId);

        var credential = await GetCredential(tenant);
        var endpoint = $"{ARMEndpoint}/subscriptions/{subscriptionId}";

        if (!string.IsNullOrEmpty(resourceGroup))
        {
            endpoint += $"/resourceGroups/{resourceGroup}";
        }

        if (!string.IsNullOrEmpty(loadTestName))
        {
            endpoint += $"/providers/Microsoft.LoadTestService/loadTests/{loadTestName}?api-version={ControlPlaneApiVersion}";
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
        if (!string.IsNullOrEmpty(loadTestName))
        {
            var LoadTestResource = JsonConvert.DeserializeObject<LoadTestResource>(content);
            return LoadTestResource != null ? new List<LoadTestResource> { LoadTestResource } : new List<LoadTestResource>();
        }

        // Azure ARM list APIs return an object with a 'value' property containing the array
        using var doc = JsonDocument.Parse(content);
        if (!doc.RootElement.TryGetProperty("value", out var valueElement) || valueElement.ValueKind != JsonValueKind.Array)
        {
            throw new Exception($"Unexpected response format: missing 'value' array. Raw response: {content}");
        }
        return JsonConvert.DeserializeObject<List<LoadTestResource>>(valueElement.GetRawText()) ?? new List<LoadTestResource>();
    }

    public async Task<LoadTestRunResource> GetLoadTestRunAsync(string subscriptionId, string loadTestName, string testRunId, string? resourceGroup = null, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId, loadTestName, testRunId);
        var loadTestResource = await GetLoadTestsAsync(subscriptionId, resourceGroup, loadTestName, tenant, retryPolicy);
        if (loadTestResource.Count == 0)
        {
            throw new Exception($"Load Test '{loadTestName}' not found in subscription '{subscriptionId}' and resource group '{resourceGroup}'.");
        }
        var dataPlaneUri = loadTestResource[0].Properties?.DataPlaneUri;
        if (string.IsNullOrEmpty(dataPlaneUri))
        {
            throw new Exception($"Data Plane URI for Load Test '{loadTestName}' is not available.");
        }

        var credential = await GetCredential(tenant);
        var loadTestClient = new LoadTestRunClient(new Uri($"https://{dataPlaneUri}"), credential);

        var loadTestRunResponse = await loadTestClient.GetTestRunAsync(testRunId);
        if (loadTestRunResponse == null || loadTestRunResponse.IsError)
        {
            throw new Exception($"Failed to retrieve Azure Load Test Run: {loadTestRunResponse}");
        }

        var loadTestRun = loadTestRunResponse.Content.ToString();
        return JsonConvert.DeserializeObject<LoadTestRunResource>(loadTestRun);
    }
}
