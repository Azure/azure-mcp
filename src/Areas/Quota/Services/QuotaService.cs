// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using AzureMcp.Areas.Quota.Services.Util;
using AzureMcp.Services.Azure;

namespace AzureMcp.Areas.Quota.Services;

public class QuotaService() : BaseAzureService, IQuotaService
{
    public async Task<Dictionary<string, List<QuotaInfo>>> GetAzureQuotaAsync(
        List<string> resourceTypes,
        string subscriptionId,
        string location)
    {
        TokenCredential credential = await GetCredential();
        Dictionary<string, List<QuotaInfo>> quotaByResourceTypes = await AzureQuotaService.GetAzureQuotaAsync(
            credential,
            resourceTypes,
            subscriptionId,
            location
            );
        return quotaByResourceTypes;
    }
}
