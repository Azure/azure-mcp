using Azure.Core;
using Azure.ResourceManager.HDInsight;
using Azure.ResourceManager.HDInsight.Models;

namespace AzureMcp.Areas.Quota.Services.Util;

public class HDInsightUsageChecker(TokenCredential credential, string subscriptionId) : AzureUsageChecker(credential, subscriptionId)
{
    public override async Task<List<UsageInfo>> GetQuotaForLocationAsync(string location)
    {
        try
        {
            var subscription = ResourceClient.GetSubscriptionResource(new ResourceIdentifier($"/subscriptions/{SubscriptionId}"));
            var usages = subscription.GetHDInsightUsagesAsync(location);
            var result = new List<UsageInfo>();

            await foreach (HDInsightUsage item in usages)
            {
                result.Add(new UsageInfo(
                     Name: item.Name?.LocalizedValue ?? item.Name?.Value ?? string.Empty,
                     Limit: (int)(item.Limit ?? 0),
                     Used: (int)(item.CurrentValue ?? 0),
                     Unit: item.Unit.ToString()
                 ));
            }
            return result;
        }
        catch (Exception error)
        {
            throw new Exception($"Error fetching HDInsight quotas: {error.Message}");
        }
    }
}
