using Azure.Core;
using AzureMcp.Models.LoadTesting;
using AzureMcp.Options;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Interfaces;
using Newtonsoft.Json;

namespace AzureMcp.Services.Azure.LoadTesting;

public class LoadTestingService(ISubscriptionService subscriptionService) : BaseAzureService, ILoadTestingService
{

    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private const string ARMEndpoint = "https://management.azure.com";
    private const string ControlPlaneApiVersion = "2022-12-01";
    public async Task<List<LoadTestResource>> GetLoadTestsForSubscriptionAsync(string subscriptionId, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId);

        var credential = await GetCredential(tenant);
        var endpoint = $"{ARMEndpoint}/subscriptions/{subscriptionId}/providers/Microsoft.LoadTestService/loadtests?api-version={ControlPlaneApiVersion}";

        var client = new HttpClient();
        var token = (await credential.GetTokenAsync(new TokenRequestContext(new[] { "https://management.azure.com/.default" }), CancellationToken.None)).Token;
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        client.DefaultRequestHeaders.Add("x-ms-client-request-id", Guid.NewGuid().ToString());
        // TODO: Log CID or get it from somewhere
        client.DefaultRequestHeaders.Add("User-Agent", UserAgent);

        var response = await client.GetAsync(endpoint);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to retrieve Azure Load Testing resources: {response.ReasonPhrase}");
        }
        var content = await response.Content.ReadAsStringAsync();

        // Azure ARM list APIs return an object with a 'value' property containing the array
        using var doc = JsonDocument.Parse(content);
        if (!doc.RootElement.TryGetProperty("value", out var valueElement) || valueElement.ValueKind != JsonValueKind.Array)
        {
            throw new Exception($"Unexpected response format: missing 'value' array. Raw response: {content}");
        }
        var loadTestingResources = JsonConvert.DeserializeObject<List<LoadTestResource>>(valueElement.GetRawText());
        return loadTestingResources ?? new List<LoadTestResource>();
    }
}
