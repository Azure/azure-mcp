using Azure.Core;
using Azure.ResourceManager.MachineLearning;

namespace AzureMcp.Areas.Quota.Services.Util;

public class MachineLearningUsageChecker(TokenCredential credential, string subscriptionId) : AzureUsageChecker(credential, subscriptionId)
{
    public override async Task<List<UsageInfo>> GetUsageForLocationAsync(string location)
    {
        try
        {
            var subscription = ResourceClient.GetSubscriptionResource(new ResourceIdentifier($"/subscriptions/{SubscriptionId}"));
            var usages = subscription.GetMachineLearningUsagesAsync(location);
            var result = new List<UsageInfo>();

            await foreach (var item in usages)
            {
                result.Add(new UsageInfo(
                    Name: item.Name?.Value ?? string.Empty,
                    Limit: (int)(item.Limit ?? 0),
                    Used: (int)(item.CurrentValue ?? 0),
                    Unit: item.Unit.ToString()
                ));
            }

            return result;
        }
        catch (Exception error)
        {
            throw new Exception($"Error fetching Machine Learning Services quotas: {error.Message}");
        }
    }
}
