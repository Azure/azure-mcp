using Azure.Core;
using Azure.ResourceManager;
using Azure.ResourceManager.Storage;
using Azure.ResourceManager.Storage.Models;

namespace AzureMcp.Areas.Quota.Services.Util;

public class StorageUsageChecker(TokenCredential credential, string subscriptionId) : AzureUsageChecker(credential, subscriptionId)
{
    public override async Task<List<UsageInfo>> GetQuotaForLocationAsync(string location)
    {
        try
        {
            var subscription = ResourceClient.GetSubscriptionResource(new ResourceIdentifier($"/subscriptions/{SubscriptionId}"));
            var usages = subscription.GetUsagesByLocationAsync(location);
            var result = new List<UsageInfo>();

            await foreach (var item in usages)
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
            throw new Exception($"Error fetching storage quotas: {error.Message}");
        }
    }
}
