using Azure.Core;
using AzureMcp.Models.LoadTesting;
using AzureMcp.Options;
using Azure.Developer.LoadTesting;
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

    // public async Task<LoadTestRun?> GetLoadTestRunsAsync(string subscription, string testRunId, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    // {
    //     ValidateRequiredParameters(subscription, testRunId);

    //     var credential = await GetCredential(tenant);
    //     var endpoint = $"{ARMEndpoint}/subscriptions/{subscriptionId}";

    //     if (!string.IsNullOrEmpty(resourceGroup))
    //     {
    //         endpoint += $"/resourceGroups/{resourceGroup}";
    //     }

    //     endpoint += $"/providers/Microsoft.LoadTestService/loadTests/{loadTestName}?api-version={ControlPlaneApiVersion}";

    //     var client = new LoadTestRunClient();
    //     var token = (await credential.GetTokenAsync(new TokenRequestContext(new[] { "https://management.azure.com/.default" }), CancellationToken.None)).Token;
    //     client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    //     client.DefaultRequestHeaders.Add("x-ms-client-request-id", Guid.NewGuid().ToString());
}
