// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Quota.Services.Util;

namespace AzureMcp.Areas.Quota.Services;

public interface IQuotaService
{
    Task<Dictionary<string, List<QuotaInfo>>> GetAzureQuotaAsync(
        List<string> resourceTypes,
        string subscriptionId,
        string location);
}
