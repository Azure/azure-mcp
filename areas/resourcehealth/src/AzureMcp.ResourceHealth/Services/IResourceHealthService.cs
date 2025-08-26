// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Options;
using AzureMcp.ResourceHealth.Models;

namespace AzureMcp.ResourceHealth.Services;

public interface IResourceHealthService
{
    /// <summary>
    /// Gets the current availability status of the specified Azure resource.
    /// </summary>
    /// <param name="resourceId">The Azure resource ID</param>
    /// <param name="retryPolicy">Optional retry policy configuration</param>
    /// <returns>The availability status of the resource</returns>
    /// <exception cref="Exception">When the service request fails</exception>
    Task<AvailabilityStatus> GetAvailabilityStatusAsync(
        string resourceId,
        RetryPolicyOptions? retryPolicy = null);

    /// <summary>
    /// Lists availability statuses for all resources in a subscription or resource group.
    /// </summary>
    /// <param name="subscription">The subscription ID or name</param>
    /// <param name="resourceGroup">Optional resource group name to filter results</param>
    /// <param name="tenant">Optional tenant ID</param>
    /// <param name="retryPolicy">Optional retry policy configuration</param>
    /// <returns>List of availability statuses for resources</returns>
    /// <exception cref="Exception">When the service request fails</exception>
    Task<List<AvailabilityStatus>> ListAvailabilityStatusesAsync(
        string subscription,
        string? resourceGroup = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);

    /// <summary>
    /// Lists service health events affecting Azure services and subscriptions.
    /// </summary>
    /// <param name="subscription">The subscription ID or name</param>
    /// <param name="filter">Optional filter expression</param>
    /// <param name="eventType">Optional event type filter</param>
    /// <param name="status">Optional status filter</param>
    /// <param name="trackingId">Optional tracking ID filter</param>
    /// <param name="queryStartTime">Optional start time filter</param>
    /// <param name="queryEndTime">Optional end time filter</param>
    /// <param name="top">Optional maximum number of results</param>
    /// <param name="tenant">Optional tenant ID</param>
    /// <param name="retryPolicy">Optional retry policy configuration</param>
    /// <returns>List of service health events</returns>
    /// <exception cref="Exception">When the service request fails</exception>
    Task<List<ServiceHealthEvent>> ListServiceHealthEventsAsync(
        string subscription,
        string? filter = null,
        string? eventType = null,
        string? status = null,
        string? trackingId = null,
        string? queryStartTime = null,
        string? queryEndTime = null,
        int? top = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);

    /// <summary>
    /// Gets historical availability events for a specific Azure resource.
    /// </summary>
    /// <param name="resourceId">The Azure resource ID to get events for</param>
    /// <param name="filter">Optional filter expression</param>
    /// <param name="queryStartTime">Optional start time filter</param>
    /// <param name="queryEndTime">Optional end time filter</param>
    /// <param name="top">Optional maximum number of results</param>
    /// <param name="expand">Optional comma-separated list of properties to expand</param>
    /// <param name="retryPolicy">Optional retry policy configuration</param>
    /// <returns>List of resource health events</returns>
    /// <exception cref="Exception">When the service request fails</exception>
    Task<List<ServiceHealthEvent>> GetResourceEventsAsync(
        string resourceId,
        string? filter = null,
        string? queryStartTime = null,
        string? queryEndTime = null,
        int? top = null,
        string? expand = null,
        RetryPolicyOptions? retryPolicy = null);
}
