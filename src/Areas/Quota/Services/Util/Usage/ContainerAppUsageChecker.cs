using Azure.Core;
using Azure.ResourceManager.AppContainers;
using Azure.ResourceManager.AppContainers.Models;

namespace AzureMcp.Areas.Quota.Services.Util;

public class ContainerAppUsageChecker(TokenCredential credential, string subscriptionId) : AzureUsageChecker(credential, subscriptionId)
{
    public override async Task<List<UsageInfo>> GetQuotaForLocationAsync(string location)
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
            throw new Exception($"Error fetching Container Apps quotas: {error.Message}");
        }
    }
}
