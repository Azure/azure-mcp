using Azure.Core;
using Azure.ResourceManager.Network;

namespace Areas.Deploy.Services.Util;

public class NetworkQuotaChecker(TokenCredential credential, string subscriptionId) : AzureQuotaChecker(credential, subscriptionId)
{
    public override async Task<List<QuotaInfo>> GetQuotaForLocationAsync(string location)
    {
        try
        {
            var subscription = ResourceClient.GetSubscriptionResource(new ResourceIdentifier($"/subscriptions/{SubscriptionId}"));
            var usages = subscription.GetUsagesAsync(location);
            var result = new List<QuotaInfo>();

            await foreach (var item in usages)
            {
                result.Add(new QuotaInfo(
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
            Console.WriteLine($"Error fetching network quotas: {error.Message}");
            return [];
        }
    }
}
