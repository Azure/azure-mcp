using Azure.Core;
using Azure.ResourceManager.Search;
using Azure.ResourceManager.Search.Models;

namespace Areas.Deploy.Services.Util;

public class SearchQuotaChecker(TokenCredential credential, string subscriptionId) : AzureQuotaChecker(credential, subscriptionId)
{
    public override async Task<List<QuotaInfo>> GetQuotaForLocationAsync(string location)
    {
        try
        {
            var subscription = ResourceClient.GetSubscriptionResource(new ResourceIdentifier($"/subscriptions/{SubscriptionId}"));
            var usages = subscription.GetUsagesBySubscriptionAsync(location);
            var result = new List<QuotaInfo>();

            await foreach (QuotaUsageResult item in usages)
            {
                result.Add(new QuotaInfo(
                    Name: item.Name?.Value ?? string.Empty,
                    Limit: item.Limit ?? 0,
                    Used: item.CurrentValue ?? 0,
                    Unit: item.Unit.ToString()
                ));
            }

            return result;
        }
        catch (Exception error)
        {
            Console.WriteLine($"Error fetching Search quotas: {error.Message}");
            return [];
        }
    }
}
