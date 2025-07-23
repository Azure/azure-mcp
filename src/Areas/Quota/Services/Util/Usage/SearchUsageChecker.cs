using Azure.Core;
using Azure.ResourceManager.Search;
using Azure.ResourceManager.Search.Models;

namespace AzureMcp.Areas.Quota.Services.Util;

public class SearchUsageChecker(TokenCredential credential, string subscriptionId) : AzureUsageChecker(credential, subscriptionId)
{
    public override async Task<List<UsageInfo>> GetQuotaForLocationAsync(string location)
    {
        try
        {
            var subscription = ResourceClient.GetSubscriptionResource(new ResourceIdentifier($"/subscriptions/{SubscriptionId}"));
            var usages = subscription.GetUsagesBySubscriptionAsync(location);
            var result = new List<UsageInfo>();

            await foreach (QuotaUsageResult item in usages)
            {
                result.Add(new UsageInfo(
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
            throw new Exception($"Error fetching Search quotas: {error.Message}");
        }
    }
}
