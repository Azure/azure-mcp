// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.ResourceManager;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;

namespace AzureMcp.Quota.Services.Util;

public class ComputeUsageChecker(TokenCredential credential, string subscriptionId) : AzureUsageChecker(credential, subscriptionId)
{
    public override async Task<List<UsageInfo>> GetUsageForLocationAsync(string location)
    {
        try
        {
            var subscription = ResourceClient.GetSubscriptionResource(new ResourceIdentifier($"/subscriptions/{SubscriptionId}"));
            var usages = subscription.GetUsagesAsync(location);
            var result = new List<UsageInfo>();

            await foreach (ComputeUsage item in usages)
            {
                result.Add(new UsageInfo(
                    Name: item.Name?.LocalizedValue ?? item.Name?.Value ?? string.Empty,
                    Limit: (int)item.Limit,
                    Used: (int)item.CurrentValue,
                    Unit: item.Unit.ToString()
                ));
            }

            return result;
        }
        catch (Exception error)
        {
            throw new Exception($"Error fetching compute quotas: {error.Message}");
        }
    }
}
