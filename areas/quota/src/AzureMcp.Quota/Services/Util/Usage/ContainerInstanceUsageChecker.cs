// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.ResourceManager.ContainerInstance;
using Azure.ResourceManager.ContainerInstance.Models;

namespace AzureMcp.Quota.Services.Util;

public class ContainerInstanceUsageChecker(TokenCredential credential, string subscriptionId) : AzureUsageChecker(credential, subscriptionId)
{
    public override async Task<List<UsageInfo>> GetUsageForLocationAsync(string location)
    {
        try
        {
            var subscription = ResourceClient.GetSubscriptionResource(new ResourceIdentifier($"/subscriptions/{SubscriptionId}"));
            var usages = subscription.GetUsagesWithLocationAsync(location);

            var result = new List<UsageInfo>();
            await foreach (ContainerInstanceUsage item in usages)
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
            throw new Exception($"Error fetching Container Instance quotas: {error.Message}");
        }
    }
}
