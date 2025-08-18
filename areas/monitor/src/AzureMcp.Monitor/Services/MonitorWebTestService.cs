// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.RegularExpressions;
using Azure;
using Azure.Core;
using Azure.ResourceManager.ApplicationInsights;
using Azure.ResourceManager.ApplicationInsights.Models;
using AzureMcp.Core.Options;
using AzureMcp.Core.Services.Azure;
using AzureMcp.Core.Services.Azure.ResourceGroup;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.Models.Monitor.WebTests;

namespace AzureMcp.Monitor.Services;

public class MonitorWebTestService(ISubscriptionService subscriptionService, ITenantService tenantService, IResourceGroupService resourceGroupService)
    : BaseAzureService(tenantService), IMonitorWebTestService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private readonly IResourceGroupService _resourceGroupService = resourceGroupService ?? throw new ArgumentNullException(nameof(resourceGroupService));

    public async Task<List<WebTestSummaryInfo>> ListWebTests(
        string subscription,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription);

        try
        {
            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);

            var webTests = await subscriptionResource
                .GetApplicationInsightsWebTestsAsync()
                .Select(webTest => new WebTestSummaryInfo
                {
                    ResourceName = webTest.Data.Name,
                    Location = webTest.Data.Location,
                    ResourceGroup = webTest.Id.ResourceGroupName,
                    Kind = webTest.Data.WebTestKind?.ToString(),
                    AppInsightsComponentId = GetAppInsightsComponentIdFromWebTestData(webTest.Data)
                })
                .ToListAsync()
                .ConfigureAwait(false);

            return webTests;
        }
        catch (Exception ex) when (ex is not ArgumentNullException)
        {
            throw new Exception($"Error retrieving web tests: {ex.Message}", ex);
        }
    }

    public async Task<List<WebTestSummaryInfo>> ListWebTests(
        string subscription,
        string resourceGroup,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription);

        try
        {
            var resourceGroupResource = await _resourceGroupService.GetResourceGroupResource(subscription, resourceGroup, tenant, retryPolicy) ??
                throw new Exception($"Resource group {resourceGroup} not found in subscription {subscription}");

            var webTests = await resourceGroupResource
                .GetApplicationInsightsWebTests()
                .GetAllAsync()
                .Select(webTest => new WebTestSummaryInfo
                {
                    ResourceName = webTest.Data.Name,
                    Location = webTest.Data.Location,
                    ResourceGroup = webTest.Id.ResourceGroupName,
                    Kind = webTest.Data.WebTestKind?.ToString(),
                    AppInsightsComponentId = GetAppInsightsComponentIdFromWebTestData(webTest.Data)
                })
                .ToListAsync()
                .ConfigureAwait(false);

            return webTests;
        }
        catch (Exception ex) when (ex is not ArgumentNullException)
        {
            throw new Exception($"Error retrieving web tests: {ex.Message}", ex);
        }
    }
    public async Task<WebTestDetailedInfo> GetWebTest(
      string subscription,
      string resourceGroup,
      string resourceName,
      string? tenant = null,
      RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription, resourceGroup, resourceName);

        try
        {
            var resourceGroupResource = await _resourceGroupService.GetResourceGroupResource(subscription, resourceGroup, tenant, retryPolicy) ??
                throw new Exception($"Resource group {resourceGroup} not found in subscription {subscription}");

            var webTest = await resourceGroupResource.GetApplicationInsightsWebTestAsync(resourceName).ConfigureAwait(false);
            if (webTest == null || !webTest.HasValue || !webTest.Value!.HasData)
            {
                throw new Exception($"Error retrieving details for web test '{resourceName}'");
            }

            if (webTest.Value.Data.WebTestKind == WebTestKind.Ping || webTest.Value.Data.WebTestKind == WebTestKind.MultiStep)
            {
                throw new NotSupportedException($"Web test '{resourceName}' is of type '{webTest.Value.Data.WebTestKind}', which has been deprecated and is not supported by this command.");
            }

            var webTestData = webTest.Value.Data;
            var webTestId = webTest.Value.Id;

            var parsedHeaders = ParseHeadersFromRawResponse(webTest.GetRawResponse());
            PatchRequestWithCorrectedHeaders(webTestData.Request, parsedHeaders);

            return new WebTestDetailedInfo
            {
                ResourceName = webTestData.Name,
                Location = webTestData.Location.ToString(),
                Locations = webTestData.Locations
                    .Where(x => x.Location != null)
                    .Select(x => x.Location.ToString())
                    .Cast<string>()
                    .ToList(),
                ResourceGroup = webTestId.ResourceGroupName,
                Id = webTestId.ToString(),
                Kind = webTestData.WebTestKind?.ToString(),
                WebTestName = webTestData.WebTestName,
                IsEnabled = webTestData.IsEnabled,
                SyntheticMonitorId = webTestData.SyntheticMonitorId,
                FrequencyInSeconds = webTestData.FrequencyInSeconds,
                TimeoutInSeconds = webTestData.TimeoutInSeconds,
                IsRetryEnabled = webTestData.IsRetryEnabled,
                Request = webTestData.Request,
                ValidationRules = webTestData.ValidationRules,
                AppInsightsComponentId = GetAppInsightsComponentIdFromWebTestData(webTestData)
            };
        }
        catch (Exception ex) when (ex is not ArgumentNullException)
        {
            throw new Exception($"Error retrieving web test {resourceName}: {ex.Message}", ex);
        }
    }

    public async Task<WebTestDetailedInfo> CreateOrUpdateWebTest(
        string subscription,
        string resourceGroup,
        string resourceName,
        string appInsightsComponentId,
        string location,
        string[] locations,
        string requestUrl,
        string? webTestName = null,
        string? description = null,
        bool enabled = true,
        int? expectedStatusCode = null,
        bool? followRedirects = false,
        int? frequencyInSeconds = null,
        IReadOnlyDictionary<string, string>? headers = null,
        string? httpVerb = "get",
        bool? ignoreStatusCode = false,
        bool? parseRequests = false,
        string? requestBody = null,
        bool? retryEnabled = false,
        bool? sslCheck = false,
        int? sslLifetimeCheckInDays = null,
        int? timeoutInSeconds = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription, resourceGroup, resourceName);

        try
        {
            var resourceGroupResource = await _resourceGroupService.GetResourceGroupResource(subscription, resourceGroup, tenant, retryPolicy) ??
                throw new Exception($"Resource group {resourceGroup} not found in subscription {subscription}");

            var webTestData = new ApplicationInsightsWebTestData(new(location))
            {
                WebTestName = webTestName ?? resourceName,
                SyntheticMonitorId = resourceName,
                WebTestKind = WebTestKind.Standard,
                IsEnabled = enabled,
                FrequencyInSeconds = frequencyInSeconds,
                TimeoutInSeconds = timeoutInSeconds,
                IsRetryEnabled = retryEnabled,
                Description = description,
                Request = new WebTestRequest
                {
                    RequestUri = new Uri(requestUrl),
                    HttpVerb = (httpVerb ?? HttpMethod.Get.ToString()).ToUpperInvariant(),
                    RequestBody = requestBody,
                    FollowRedirects = followRedirects,
                    ParseDependentRequests = parseRequests
                },
                ValidationRules = new WebTestValidationRules
                {
                    ExpectedHttpStatusCode = expectedStatusCode,
                    IgnoreHttpStatusCode = ignoreStatusCode,
                    CheckSsl = sslCheck,
                    SslCertRemainingLifetimeCheck = sslCheck.HasValue && sslCheck.Value ? sslLifetimeCheckInDays : null
                },
            };

            webTestData.Tags.Add($"hidden-link:{appInsightsComponentId}", "Resource");

            foreach (var webTestLocation in locations)
            {
                webTestData.Locations.Add(new WebTestGeolocation
                {
                    Location = new AzureLocation(webTestLocation)
                });
            }

            if (headers != null)
            {
                foreach (var headerKey in headers.Keys)
                {
                    webTestData.Request.Headers.Add(new WebTestRequestHeaderField()
                    {
                        HeaderFieldName = headerKey,
                        HeaderFieldValue = headers[headerKey]
                    });
                }
            }

            var webTestArmResource = await resourceGroupResource.GetApplicationInsightsWebTests()
                .CreateOrUpdateAsync(
                    WaitUntil.Completed,
                    resourceName,
                    webTestData
                ).ConfigureAwait(false);

            if (webTestArmResource == null || !webTestArmResource.HasCompleted || !webTestArmResource.HasValue)
            {
                throw new Exception($"Error creating web test resource with name '{resourceName}' in resource group '{resourceGroup}'");
            }

            var createdWebTest = webTestArmResource.Value.Data;

            var parsedHeaders = ParseHeadersFromRawResponse(webTestArmResource.GetRawResponse());
            PatchRequestWithCorrectedHeaders(createdWebTest.Request, parsedHeaders);

            return new WebTestDetailedInfo
            {
                ResourceName = createdWebTest.Name,
                Location = createdWebTest.Location.ToString(),
                Locations = createdWebTest.Locations
                    .Where(x => x.Location != null)
                    .Select(x => x.Location.ToString())
                    .Cast<string>()
                    .ToList(),
                ResourceGroup = webTestArmResource.Value.Id.ResourceGroupName,
                Id = webTestArmResource.Value.Id.ToString(),
                Kind = createdWebTest.WebTestKind?.ToString(),
                WebTestName = createdWebTest.WebTestName,
                IsEnabled = createdWebTest.IsEnabled,
                SyntheticMonitorId = createdWebTest.SyntheticMonitorId,
                FrequencyInSeconds = createdWebTest.FrequencyInSeconds,
                TimeoutInSeconds = createdWebTest.TimeoutInSeconds,
                IsRetryEnabled = createdWebTest.IsRetryEnabled,
                Request = createdWebTest.Request,
                ValidationRules = createdWebTest.ValidationRules,
                AppInsightsComponentId = GetAppInsightsComponentIdFromWebTestData(createdWebTest)
            };
        }
        catch (Exception ex) when (ex is not ArgumentNullException)
        {
            throw new Exception($"Error creating web test {resourceName}: {ex.Message}", ex);
        }
    }

    private List<WebTestRequestHeaderField> ParseHeadersFromRawResponse(Response response)
    {
        using (var document = JsonDocument.Parse(response.Content))
        {
            var root = document.RootElement;
            var headers = new List<WebTestRequestHeaderField>();

            if (root.TryGetProperty("properties", out JsonElement properties) &&
                properties.TryGetProperty("Request", out JsonElement request) &&
                request.TryGetProperty("Headers", out JsonElement headersArray) &&
                headersArray.ValueKind == JsonValueKind.Array)
            {
                foreach (var header in headersArray.EnumerateArray())
                {
                    if (header.TryGetProperty("Key", out JsonElement keyElement) &&
                        header.TryGetProperty("Value", out JsonElement valueElement))
                    {
                        string key = keyElement.GetString()!;
                        string value = valueElement.GetString()!;
                        headers.Add(new WebTestRequestHeaderField
                        {
                            HeaderFieldName = key,
                            HeaderFieldValue = value
                        });
                    }
                }
            }

            return headers;
        }
    }

    private void PatchRequestWithCorrectedHeaders(WebTestRequest request, List<WebTestRequestHeaderField> headers)
    {
        request.Headers.Clear();
        foreach (var header in headers)
        {
            request.Headers.Add(header);
        }
    }

    private string? GetAppInsightsComponentIdFromWebTestData(ApplicationInsightsWebTestData webTest)
    {
        if (webTest.Tags == null || webTest.Tags.Count == 0)
        {
            return null;
        }

        var hiddenLinkMatch = webTest.Tags.Keys.Select(x => AppInsightsComponentHiddenLinkTagRegex.Match(x)).FirstOrDefault(match => match.Success);
        return hiddenLinkMatch?.Groups[1].Value;
    }

    private readonly Regex AppInsightsComponentHiddenLinkTagRegex = new Regex("^hidden-link:(\\/subscriptions\\/[^\\/]+\\/resourceGroups\\/[^\\/]+\\/providers\\/microsoft\\.insights\\/components\\/[^\\/]+)$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
}
