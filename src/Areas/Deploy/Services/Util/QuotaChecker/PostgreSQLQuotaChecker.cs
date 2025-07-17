using Areas.Server.Commands.Tools.DeployTools.Util;
using Azure.Core;

namespace Areas.Deploy.Services.Util;

public class PostgreSQLQuotaChecker(TokenCredential credential, string subscriptionId) : AzureQuotaChecker(credential, subscriptionId)
{
    public override async Task<List<QuotaInfo>> GetQuotaForLocationAsync(string location)
    {
        try
        {
            var requestUrl = $"https://management.azure.com/subscriptions/{SubscriptionId}/providers/Microsoft.DBforPostgreSQL/locations/{location}/resourceType/flexibleServers/usages?api-version=2023-06-01-preview";
            using var rawResponse = await GetQuotaByUrlAsync(requestUrl);

            if (rawResponse?.RootElement.TryGetProperty("value", out var valueElement) != true)
            {
                return [];
            }

            var result = new List<QuotaInfo>();
            foreach (var item in valueElement.EnumerateArray())
            {
                var name = string.Empty;
                var limit = 0;
                var used = 0;
                var unit = string.Empty;

                if (item.TryGetProperty("name", out var nameElement) && nameElement.TryGetProperty("value", out var nameValue))
                {
                    name = nameValue.GetStringSafe();
                }

                if (item.TryGetProperty("limit", out var limitElement))
                {
                    limit = limitElement.GetInt32();
                }

                if (item.TryGetProperty("currentValue", out var usedElement))
                {
                    used = usedElement.GetInt32();
                }

                if (item.TryGetProperty("unit", out var unitElement))
                {
                    unit = unitElement.GetStringSafe();
                }

                result.Add(new QuotaInfo(name, limit, used, unit));
            }

            return result;
        }
        catch (Exception error)
        {
            Console.WriteLine($"Error fetching PostgreSQL quotas: {error.Message}");
            return [];
        }
    }
}
