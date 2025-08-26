// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Net.Http;
using System.Text.Json;
using Azure;
using Azure.Core;
using Azure.ResourceManager;
using Azure.ResourceManager.ResourceHealth;
using AzureMcp.Core.Options;
using AzureMcp.Core.Services.Azure;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.ResourceHealth.Commands;
using AzureMcp.ResourceHealth.Models;

namespace AzureMcp.ResourceHealth.Services;

public class ResourceHealthService(ISubscriptionService subscriptionService, ITenantService tenantService)
    : BaseAzureService(tenantService), IResourceHealthService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));

    public async Task<AvailabilityStatus> GetAvailabilityStatusAsync(
        string resourceId,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(resourceId);

        try
        {
            var armClient = await CreateArmClientAsync(null, retryPolicy);

            // Create ResourceIdentifier from the resource ID string
            var resourceIdentifier = new ResourceIdentifier(resourceId);

            // Call the Azure ResourceHealth API to get current availability status
            var response = await armClient.GetAvailabilityStatusAsync(
                resourceIdentifier,
                cancellationToken: default);

            var availabilityStatus = response.Value;
            var properties = availabilityStatus.Properties;

            // Map Azure SDK response to our model
            return new AvailabilityStatus
            {
                ResourceId = resourceId,
                AvailabilityState = properties.AvailabilityState?.ToString(),
                Summary = properties.Summary,
                DetailedStatus = properties.DetailedStatus,
                ReasonType = properties.ReasonType,
                OccurredTime = properties.OccuredOn, // Note: "OccuredOn" property name is misspelled in Azure SDK
                ReportedTime = properties.ReportedOn,
                CauseType = properties.HealthEventCause,
                RootCauseAttributionTime = properties.RootCauseAttributionOn?.ToString("O"),
                Category = properties.HealthEventCategory,
                Title = properties.Title,
                Location = availabilityStatus.Location?.Name ?? "Unknown"
            };
        }
        catch (RequestFailedException ex)
        {
            throw new Exception($"Failed to get availability status for resource '{resourceId}': {ex.Message}", ex);
        }
    }

    public async Task<List<AvailabilityStatus>> ListAvailabilityStatusesAsync(
        string subscription,
        string? resourceGroup = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription);

        try
        {
            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);

            // Get all availability statuses from the subscription
            var availabilityStatuses = new List<AvailabilityStatus>();

            foreach (var status in subscriptionResource.GetAvailabilityStatusesBySubscription())
            {
                var properties = status.Properties;

                // If resource group filter is specified, check if the resource belongs to that group
                if (!string.IsNullOrWhiteSpace(resourceGroup))
                {
                    var resourceId = status.Id?.ToString(); // Convert ResourceIdentifier to string
                    if (!string.IsNullOrEmpty(resourceId) && !resourceId.Contains($"/resourceGroups/{resourceGroup}/", StringComparison.OrdinalIgnoreCase))
                    {
                        continue; // Skip resources not in the specified resource group
                    }
                }

                availabilityStatuses.Add(new AvailabilityStatus
                {
                    ResourceId = status.Id?.ToString() ?? status.Name, // Use Id first, fallback to Name
                    AvailabilityState = properties.AvailabilityState?.ToString(),
                    Summary = properties.Summary,
                    DetailedStatus = properties.DetailedStatus,
                    ReasonType = properties.ReasonType,
                    OccurredTime = properties.OccuredOn, // Note: "OccuredOn" property name is misspelled in Azure SDK
                    ReportedTime = properties.ReportedOn,
                    CauseType = properties.HealthEventCause,
                    RootCauseAttributionTime = properties.RootCauseAttributionOn?.ToString("O"),
                    Category = properties.HealthEventCategory,
                    Title = properties.Title,
                    Location = status.Location?.Name ?? "Unknown"
                });
            }

            return availabilityStatuses;
        }
        catch (RequestFailedException ex)
        {
            throw new Exception($"Failed to list availability statuses for subscription '{subscription}'{(resourceGroup != null ? $" and resource group '{resourceGroup}'" : "")}: {ex.Message}", ex);
        }
    }

    public async Task<List<ServiceHealthEvent>> ListServiceHealthEventsAsync(
        string subscription,
        string? filter = null,
        string? eventType = null,
        string? status = null,
        string? trackingId = null,
        string? queryStartTime = null,
        string? queryEndTime = null,
        int? top = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription);

        try
        {
            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);

            // Get access token for Azure Management API
            var tokenCredential = await GetCredential(tenant);
            var tokenRequestContext = new TokenRequestContext(["https://management.azure.com/.default"]);
            var accessToken = await tokenCredential.GetTokenAsync(tokenRequestContext, default);

            // Build the Service Health REST API URL - using latest API version
            string apiVersion = "2025-05-01";
            string baseUrl = $"https://management.azure.com/subscriptions/{subscriptionResource.Data.SubscriptionId}/providers/Microsoft.ResourceHealth/events";
            string fullUrl = $"{baseUrl}?api-version={apiVersion}";

            // Build query parameters - avoid complex OData filters which cause InvalidODataQueryOptions
            var queryParams = new List<string>();

            // Add queryStartTime parameter (this is supported directly, not as a filter)
            if (!string.IsNullOrEmpty(queryStartTime))
            {
                queryParams.Add($"queryStartTime={Uri.EscapeDataString(queryStartTime)}");
            }

            // Add basic filter if provided by user (simpler approach)
            if (!string.IsNullOrEmpty(filter))
            {
                queryParams.Add($"$filter={Uri.EscapeDataString(filter)}");
            }
            else
            {
                // Build simple filter for supported parameters
                var filterParts = new List<string>();

                // Only add event type filter if no user filter provided
                if (!string.IsNullOrEmpty(eventType))
                {
                    filterParts.Add($"properties/eventType eq '{eventType}'");
                }

                // Only add status filter if no user filter provided  
                if (!string.IsNullOrEmpty(status))
                {
                    filterParts.Add($"properties/status eq '{status}'");
                }

                // Add combined filter if we have filter parts
                if (filterParts.Count > 0)
                {
                    string combinedFilter = string.Join(" and ", filterParts);
                    queryParams.Add($"$filter={Uri.EscapeDataString(combinedFilter)}");
                }
            }

            // Add additional query parameters to URL
            if (queryParams.Count > 0)
            {
                fullUrl += "&" + string.Join("&", queryParams);
            }

            // Add top parameter
            if (top.HasValue)
            {
                var topValue = Math.Min(top.Value, 1000);
                fullUrl += $"&$top={topValue}"; // Cap at 1000 as per API limits
            }

            // Make the REST API call
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken.Token);
            httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent);

            var response = await httpClient.GetAsync(fullUrl);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Response status code does not indicate success: {(int)response.StatusCode} ({response.StatusCode}). Error details: {errorContent}");
            }

            var jsonContent = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize(jsonContent, ResourceHealthJsonContext.Default.ServiceHealthEventsResponse);

            var serviceHealthEvents = new List<ServiceHealthEvent>();

            if (apiResponse?.Value != null)
            {
                foreach (var eventData in apiResponse.Value)
                {
                    var properties = eventData.Properties;
                    if (properties == null)
                        continue;

                    serviceHealthEvents.Add(new ServiceHealthEvent
                    {
                        Id = eventData.Id,
                        Name = eventData.Name,
                        Title = properties.Title,
                        EventType = properties.EventType,
                        Status = properties.Status,
                        Level = properties.Level ?? properties.EventLevel, // Use EventLevel if Level is not available
                        Summary = properties.Summary,
                        Description = properties.Description,
                        TrackingId = properties.TrackingId,
                        StartTime = properties.ImpactStartTime,
                        EndTime = properties.ImpactMitigationTime,
                        LastUpdateTime = properties.LastUpdateTime,
                        SubscriptionId = subscriptionResource.Data.SubscriptionId,
                        AffectedServices = properties.Impact?.Select(i => i.ImpactedService).Where(s => !string.IsNullOrEmpty(s)).Cast<string>().ToList() ?? new List<string>(),
                        AffectedRegions = properties.Impact?.SelectMany(i => i.ImpactedRegions?.Select(r => r.ImpactedRegion) ?? Enumerable.Empty<string?>()).Where(r => !string.IsNullOrEmpty(r)).Cast<string>().Distinct().ToList() ?? new List<string>(),
                        AffectedResourceTypes = new List<string>() // This field is not in the new API structure
                    });
                }
            }

            return serviceHealthEvents;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Failed to retrieve service health events from Azure API: {ex.Message}", ex);
        }
        catch (JsonException ex)
        {
            throw new Exception($"Failed to parse service health events response: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to list service health events for subscription '{subscription}': {ex.Message}", ex);
        }
    }

    public async Task<List<ServiceHealthEvent>> GetResourceEventsAsync(
        string resourceId,
        string? filter = null,
        string? queryStartTime = null,
        string? queryEndTime = null,
        int? top = null,
        string? expand = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(resourceId);

        try
        {
            // Get access token for Azure Management API
            var tokenCredential = await GetCredential(null);
            var tokenRequestContext = new TokenRequestContext(["https://management.azure.com/.default"]);
            var accessToken = await tokenCredential.GetTokenAsync(tokenRequestContext, default);

            // Build the API URL - Events - List By Single Resource
            var apiVersion = "2025-05-01";
            var baseUrl = $"https://management.azure.com{resourceId}/providers/Microsoft.ResourceHealth/events";

            // Build query parameters
            var queryParams = new List<string> { $"api-version={apiVersion}" };

            if (!string.IsNullOrWhiteSpace(filter))
            {
                queryParams.Add($"$filter={Uri.EscapeDataString(filter)}");
            }

            if (top.HasValue)
            {
                var topValue = Math.Min(top.Value, 1000); // Cap at 1000 as per API limits
                queryParams.Add($"$top={topValue}");
            }

            if (!string.IsNullOrWhiteSpace(expand))
            {
                queryParams.Add($"$expand={Uri.EscapeDataString(expand)}");
            }

            var fullUrl = $"{baseUrl}?{string.Join("&", queryParams)}";

            // Make the REST API call
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken.Token);
            httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent);

            var response = await httpClient.GetAsync(fullUrl);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Response status code does not indicate success: {(int)response.StatusCode} ({response.StatusCode}). Error details: {errorContent}");
            }

            var jsonContent = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize(jsonContent, ResourceHealthJsonContext.Default.ServiceHealthEventsResponse);

            var serviceHealthEvents = new List<ServiceHealthEvent>();

            if (apiResponse?.Value != null)
            {
                foreach (var eventData in apiResponse.Value)
                {
                    var properties = eventData.Properties;
                    if (properties == null)
                        continue;

                    serviceHealthEvents.Add(new ServiceHealthEvent
                    {
                        Id = eventData.Id,
                        Name = eventData.Name,
                        Title = properties.Title,
                        EventType = properties.EventType,
                        Status = properties.Status,
                        Level = properties.Level ?? properties.EventLevel, // Use EventLevel if Level is not available
                        Summary = properties.Summary,
                        Description = properties.Description,
                        TrackingId = properties.TrackingId,
                        StartTime = properties.ImpactStartTime,
                        EndTime = properties.ImpactMitigationTime,
                        LastUpdateTime = properties.LastUpdateTime,
                        AffectedServices = properties.Impact?.Select(i => i.ImpactedService).Where(s => !string.IsNullOrEmpty(s)).Cast<string>().ToList() ?? new List<string>(),
                        AffectedRegions = properties.Impact?.SelectMany(i => i.ImpactedRegions?.Select(r => r.ImpactedRegion) ?? Enumerable.Empty<string?>()).Where(r => !string.IsNullOrEmpty(r)).Cast<string>().Distinct().ToList() ?? new List<string>(),
                        AffectedResourceTypes = new List<string>() // This field is not available in the resource events API
                    });
                }
            }

            return serviceHealthEvents;
        }
        catch (JsonException ex)
        {
            throw new Exception($"Failed to parse resource events response: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to get resource events for resource '{resourceId}': {ex.Message}", ex);
        }
    }
}
