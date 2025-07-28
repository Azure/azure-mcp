using Azure.Core;
using Azure.ResourceManager.Network;

namespace AzureMcp.Areas.Quota.Services.Util;

public class NetworkUsageChecker(TokenCredential credential, string subscriptionId) : AzureUsageChecker(credential, subscriptionId)
{
    public override async Task<List<UsageInfo>> GetUsageForLocationAsync(string location)
    {
        try
        {
            var subscription = ResourceClient.GetSubscriptionResource(new ResourceIdentifier($"/subscriptions/{SubscriptionId}"));
            var usages = subscription.GetUsagesAsync(location);
            var result = new List<UsageInfo>();

            await foreach (var item in usages)
            {
                result.Add(new UsageInfo(
                    Name: item.Name?.Value ?? string.Empty,
                    Limit: (int)(item.Limit),
                    Used: (int)(item.CurrentValue),
                    Unit: item.Unit.ToString()
                ));
            }

            return result;
        }
        catch (Exception error)
        {
            throw new Exception($"Error fetching network quotas: {error.Message}");
        }
    }
}
