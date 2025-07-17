using System.Net.Http.Headers;
using System.Text.Json;
using Azure.Core;
using Azure.ResourceManager;
using AzureMcp.Services.Azure.Authentication;

namespace Areas.Deploy.Services.Util;

// For simplicity, we currently apply a single rule for all Azure resource providers:
//   - Any resource provider not listed in the enum is treated as having no quota limitations.
// Ideally, we'd differentiate between the following cases:
//   1. The resource provider has no quota limitations.
//   2. The resource provider has quota limitations but does not expose a quota API.
//   3. The resource provider exposes a quota API, but it's not yet supported by the checker.

public enum ResourceProvider
{
    CognitiveServices,
    Compute,
    Storage,
    ContainerApp,
    Network,
    MachineLearning,
    PostgreSQL,
    HDInsight,
    Search,
    ContainerInstance
}

public record QuotaInfo(
    string Name,
    int Limit,
    int Used,
    string? Unit = null,
    string? Description = null
);

public interface IQuotaChecker
{
    Task<List<QuotaInfo>> GetQuotaForLocationAsync(string location);
}

// Abstract base class for checking Azure quotas
public abstract class AzureQuotaChecker : IQuotaChecker
{
    protected readonly string SubscriptionId;
    protected readonly ArmClient ResourceClient;

    protected readonly TokenCredential Credential;
    private static readonly HttpClient HttpClient = new();

    protected AzureQuotaChecker(TokenCredential credential, string subscriptionId)
    {
        SubscriptionId = subscriptionId;
        Credential = credential ?? throw new ArgumentNullException(nameof(credential));
        ResourceClient = new ArmClient(credential, subscriptionId);
    }

    public abstract Task<List<QuotaInfo>> GetQuotaForLocationAsync(string location);

    protected async Task<JsonDocument?> GetQuotaByUrlAsync(string requestUrl)
    {
        try
        {
            var token = await Credential.GetTokenAsync(new TokenRequestContext(["https://management.azure.com/.default"]), CancellationToken.None);

            using var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await HttpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"HTTP error! status: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonDocument.Parse(content);
        }
        catch (Exception error)
        {
            Console.WriteLine($"Error fetching quotas directly: {error.Message}");
            return null;
        }
    }
}

// Factory function to create quota checkers
public static class QuotaCheckerFactory
{
    private static readonly Dictionary<string, ResourceProvider> ProviderMapping = new()
    {
        { "Microsoft.CognitiveServices", ResourceProvider.CognitiveServices },
        { "Microsoft.Compute", ResourceProvider.Compute },
        { "Microsoft.Storage", ResourceProvider.Storage },
        { "Microsoft.App", ResourceProvider.ContainerApp },
        { "Microsoft.Network", ResourceProvider.Network },
        { "Microsoft.MachineLearningServices", ResourceProvider.MachineLearning },
        { "Microsoft.DBforPostgreSQL", ResourceProvider.PostgreSQL },
        { "Microsoft.HDInsight", ResourceProvider.HDInsight },
        { "Microsoft.Search", ResourceProvider.Search },
        { "Microsoft.ContainerInstance", ResourceProvider.ContainerInstance }
    };

    public static IQuotaChecker CreateQuotaChecker(TokenCredential credential, string provider, string subscriptionId)
    {
        if (!ProviderMapping.TryGetValue(provider, out var resourceProvider))
        {
            throw new ArgumentException($"Unsupported resource provider: {provider}");
        }

        return resourceProvider switch
        {
            ResourceProvider.Compute => new ComputeQuotaChecker(credential, subscriptionId),
            ResourceProvider.CognitiveServices => new CognitiveServicesQuotaChecker(credential, subscriptionId),
            ResourceProvider.Storage => new StorageQuotaChecker(credential, subscriptionId),
            ResourceProvider.ContainerApp => new ContainerAppQuotaChecker(credential, subscriptionId),
            ResourceProvider.Network => new NetworkQuotaChecker(credential, subscriptionId),
            ResourceProvider.MachineLearning => new MachineLearningQuotaChecker(credential, subscriptionId),
            ResourceProvider.PostgreSQL => new PostgreSQLQuotaChecker(credential, subscriptionId),
            ResourceProvider.HDInsight => new HDInsightQuotaChecker(credential, subscriptionId),
            ResourceProvider.Search => new SearchQuotaChecker(credential, subscriptionId),
            ResourceProvider.ContainerInstance => new ContainerInstanceQuotaChecker(credential, subscriptionId),
            _ => throw new ArgumentException($"No implementation for provider: {provider}")
        };
    }
}

// Service to get Azure quota for a list of resource types
public static class AzureQuotaService
{
    public static async Task<Dictionary<string, List<QuotaInfo>>> GetAzureQuotaAsync(
        TokenCredential credential,
        List<string> resourceTypes,
        string subscriptionId,
        string location)
    {
        // Group resource types by provider to avoid duplicate processing
        var providerToResourceTypes = resourceTypes
            .GroupBy(rt => rt.Split('/')[0])
            .ToDictionary(g => g.Key, g => g.ToList());

        // Use Select to create tasks and await them all
        var quotaTasks = providerToResourceTypes.Select(async kvp =>
        {
            var (provider, resourceTypesForProvider) = (kvp.Key, kvp.Value);
            try
            {
                var quotaChecker = QuotaCheckerFactory.CreateQuotaChecker(credential, provider, subscriptionId);
                var quotaInfo = await quotaChecker.GetQuotaForLocationAsync(location);
                Console.WriteLine($"Quota info for provider {provider}: {quotaInfo.Count} items");

                return resourceTypesForProvider.Select(rt => new KeyValuePair<string, List<QuotaInfo>>(rt, quotaInfo));
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Unsupported resource provider", StringComparison.OrdinalIgnoreCase))
            {
                return resourceTypesForProvider.Select(rt => new KeyValuePair<string, List<QuotaInfo>>(rt, new List<QuotaInfo>(){
                    new QuotaInfo(rt, 0, 0, Description: "No Limit")
                }));
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error fetching quota for provider {provider}: {error.Message}");
                return resourceTypesForProvider.Select(rt => new KeyValuePair<string, List<QuotaInfo>>(rt, new List<QuotaInfo>()
                {
                    new QuotaInfo(rt, 0, 0, Description: error.Message)
                }));
            }
        });

        var results = await Task.WhenAll(quotaTasks);

        // Flatten the results into a single dictionary
        return results
            .SelectMany(i => i)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
}
