// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using Azure.ResourceManager.AppService;
using AzureMcp.Arguments;
using AzureMcp.Services.Interfaces;

namespace AzureMcp.Services.Azure.Cosmos;

public class AppServiceService(ISubscriptionService subscriptionService, ITenantService tenantService)
    : BaseAzureService(tenantService), IAppServiceService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));

    public async Task<List<string>> ListAppServicePlans (string subscriptionId, string? tenant = null, RetryPolicyArguments? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId);

        var subscription = await _subscriptionService.GetSubscription(subscriptionId, tenant, retryPolicy);
        var appServicePlans = subscription.GetAppServicePlans(false).Select(p => p.Data.Name).ToList();
        return (List<string>)appServicePlans;
    }

}