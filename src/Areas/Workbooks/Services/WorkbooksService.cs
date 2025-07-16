// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Workbooks.Models;
using AzureMcp.Options;
using Microsoft.Extensions.Logging;
using Azure.Core;

namespace AzureMcp.Areas.Workbooks.Services;
using AzureMcp.Models.Identity;

using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using AzureMcp.Services.Azure;
using Azure.ResourceManager.ApplicationInsights;
using Azure.ResourceManager.ApplicationInsights.Models;
using AzureMcp.Services.Azure.Subscription;

public class WorkbooksService(ISubscriptionService _subscriptionService, ILogger<WorkbooksService> logger) : BaseAzureService, IWorkbooksService
{
    private readonly ILogger<WorkbooksService> _logger = logger;

    private static string? ConvertTagsToString(IDictionary<string, string>? tags)
    {
        return tags?.Count > 0 ? string.Join(", ", tags.Select(kvp => $"{kvp.Key}={kvp.Value}")) : null;
    }

    public async Task<List<WorkbookInfo>> ListWorkbooks(string? subscriptionId = null, string? resourceGroupName = null, RetryPolicyOptions? retryPolicy = null)
    {
        if (string.IsNullOrEmpty(resourceGroupName))
        {
            throw new ArgumentException("Resource group name is required", nameof(resourceGroupName));
        }

        var subscriptionResource = await _subscriptionService.GetSubscription(subscriptionId ?? "", null, retryPolicy) ?? throw new Exception($"Subscription '{subscriptionId}' not found");

        var workbookInfos = new List<WorkbookInfo>();

        // Only search in the specified resource group
        var resourceGroupResource = await subscriptionResource.GetResourceGroups().GetAsync(resourceGroupName);
        if (resourceGroupResource?.Value != null)
        {
            var workbookCollection = resourceGroupResource.Value.GetApplicationInsightsWorkbooks();
            
            await foreach (ApplicationInsightsWorkbookResource workbook in workbookCollection.GetAllAsync("workbook"))
            {
                _logger.LogInformation("Full workbook object: {@Workbook}", workbook.Data);
                _logger.LogInformation("Workbook Name: {Name}, ID: {Id}, Location: {Location}, Category: {Category}", 
                    workbook.Data.Name, workbook.Id, workbook.Data.Location, workbook.Data.Category);

                workbookInfos.Add(new WorkbookInfo(
                    WorkbookId: workbook.Id?.ToString() ?? "",
                    WorkbookDisplayName: workbook.Data.DisplayName ?? "Unknown",
                    Description: workbook.Data.Description,
                    Category: workbook.Data.Category,
                    Location: workbook.Data.Location.ToString(),
                    Kind: workbook.Data.Kind?.ToString(),
                    Tags: ConvertTagsToString(workbook.Data.Tags),
                    SerializedData: workbook.Data.SerializedData,
                    Version: workbook.Data.Version,
                    TimeModified: workbook.Data.ModifiedOn,
                    UserId: workbook.Data.UserId,
                    SourceId: workbook.Data.SourceId
                ));
            }
        }
        else
        {
            throw new ArgumentException($"Resource group '{resourceGroupName}' not found in subscription '{subscriptionId}'", nameof(resourceGroupName));
        }
        
        return workbookInfos;
    }

    public async Task<WorkbookInfo?> GetWorkbook(string workbookId, RetryPolicyOptions? retryPolicy = null)
    {
        if (string.IsNullOrEmpty(workbookId))
        {
            throw new ArgumentException("Workbook ID is required", nameof(workbookId));
        }

        try
        {
            // Parse the workbook resource ID to get subscription info
            var workbookResourceId = new ResourceIdentifier(workbookId);
            var armClient = await CreateArmClientAsync(null, retryPolicy);

            // Get the workbook resource
            var workbookResource = armClient.GetApplicationInsightsWorkbookResource(workbookResourceId);

            if (workbookResource == null)
            {
                _logger.LogWarning("Workbook with ID {WorkbookId} not found", workbookId);
                return null;
            }

            // Get the workbook data
            var workbookResponse = await workbookResource.GetAsync(true);
            var workbook = workbookResponse.Value;

            if (workbook?.Data == null)
            {
                _logger.LogWarning("Workbook data is null for ID {WorkbookId}", workbookId);
                return null;
            }

            _logger.LogInformation("Retrieved workbook details for ID: {WorkbookId}", workbookId);

            return new WorkbookInfo(
                WorkbookId: workbook.Id?.ToString() ?? workbookId,
                WorkbookDisplayName: workbook.Data.DisplayName ?? "Unknown",
                Description: workbook.Data.Description,
                Category: workbook.Data.Category,
                Location: workbook.Data.Location.ToString(),
                Kind: workbook.Data.Kind?.ToString(),
                Tags: ConvertTagsToString(workbook.Data.Tags),
                SerializedData: workbook.Data.SerializedData,
                Version: workbook.Data.Version,
                TimeModified: workbook.Data.ModifiedOn,
                UserId: workbook.Data.UserId,
                SourceId: workbook.Data.SourceId
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving workbook with ID: {WorkbookId}", workbookId);
            throw;
        }
    }

    public async Task<WorkbookInfo?> UpdateWorkbook(string workbookId, string? title = null, string? serializedContent = null, RetryPolicyOptions? retryPolicy = null)
    {
        if (string.IsNullOrEmpty(workbookId))
        {
            throw new ArgumentException("Workbook ID is required", nameof(workbookId));
        }

        if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(serializedContent))
        {
            throw new ArgumentException("At least one property (title or serializedContent) must be provided for update");
        }

        try
        {
            // Get ARM client
            var armClient = await CreateArmClientAsync(null, retryPolicy);

            // Parse the workbook resource ID to get the workbook directly
            var workbookResourceId = new ResourceIdentifier(workbookId);
            var workbookResource = armClient.GetApplicationInsightsWorkbookResource(workbookResourceId);

            if (workbookResource == null)
            {
                _logger.LogWarning("Workbook with ID {WorkbookId} not found", workbookId);
                return null;
            }

            // Get the current workbook data
            var workbookResponse = await workbookResource.GetAsync();
            var workbook = workbookResponse.Value;

            if (workbook?.Data == null)
            {
                _logger.LogWarning("Workbook data is null for ID {WorkbookId}", workbookId);
                return null;
            }

            // Create a patch document with the updated properties
            var patchData = new ApplicationInsightsWorkbookPatch();

            if (!string.IsNullOrEmpty(title))
            {
                patchData.DisplayName = title;
            }

            if (!string.IsNullOrEmpty(serializedContent))
            {
                patchData.SerializedData = serializedContent;
            }

            patchData.Kind = "shared";

            // Update the workbook
            var updateResponse = await workbookResource.UpdateAsync(patchData);
            var updatedWorkbook = updateResponse.Value;

            _logger.LogInformation("Successfully updated workbook with ID: {WorkbookId}", workbookId);

            return new WorkbookInfo(
                WorkbookId: updatedWorkbook.Id?.ToString() ?? workbookId,
                WorkbookDisplayName: updatedWorkbook.Data.DisplayName ?? "Unknown",
                Description: updatedWorkbook.Data.Description,
                Category: updatedWorkbook.Data.Category,
                Location: updatedWorkbook.Data.Location.ToString(),
                Kind: updatedWorkbook.Data.Kind?.ToString(),
                Tags: ConvertTagsToString(updatedWorkbook.Data.Tags),
                SerializedData: updatedWorkbook.Data.SerializedData,
                Version: updatedWorkbook.Data.Version,
                TimeModified: updatedWorkbook.Data.ModifiedOn,
                UserId: updatedWorkbook.Data.UserId,
                SourceId: updatedWorkbook.Data.SourceId
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating workbook with ID: {WorkbookId}", workbookId);
            throw;
        }
    }

    public async Task<WorkbookInfo?> CreateWorkbook(string subscriptionId, string resourceGroupName, string title, string serializedData, string sourceId, RetryPolicyOptions? retryPolicy = null)
    {
        if (string.IsNullOrEmpty(subscriptionId))
        {
            throw new ArgumentException("Subscription ID is required", nameof(subscriptionId));
        }

        if (string.IsNullOrEmpty(resourceGroupName))
        {
            throw new ArgumentException("Resource group name is required", nameof(resourceGroupName));
        }

        if (string.IsNullOrEmpty(title))
        {
            throw new ArgumentException("Title is required", nameof(title));
        }

        if (string.IsNullOrEmpty(serializedData))
        {
            throw new ArgumentException("Serialized data is required", nameof(serializedData));
        }

        if (string.IsNullOrEmpty(sourceId))
        {
            throw new ArgumentException("Source ID is required", nameof(sourceId));
        }

        try
        {
            // Get the subscription resource
            var subscriptionResource = await _subscriptionService.GetSubscription(subscriptionId, null, retryPolicy) ?? throw new Exception($"Subscription '{subscriptionId}' not found");

            // Get the resource group
            var resourceGroupResource = await subscriptionResource.GetResourceGroups().GetAsync(resourceGroupName);
            if (resourceGroupResource?.Value == null)
            {
                throw new ArgumentException($"Resource group '{resourceGroupName}' not found in subscription '{subscriptionId}'", nameof(resourceGroupName));
            }

            // Create the workbook data
            var workbookData = new ApplicationInsightsWorkbookData(resourceGroupResource.Value.Data.Location)
            {
                DisplayName = title,
                SerializedData = serializedData,
                Category = "workbook",
                Kind = "shared",
                SourceId = new ResourceIdentifier(sourceId)
            };

            // Generate a unique name for the workbook
            var workbookName = Guid.NewGuid().ToString();

            // Create the workbook
            var workbookCollection = resourceGroupResource.Value.GetApplicationInsightsWorkbooks();
            var createOperation = await workbookCollection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, workbookName, workbookData);
            var createdWorkbook = createOperation.Value;

            _logger.LogInformation("Successfully created workbook with name: {WorkbookName} in resource group: {ResourceGroup}", workbookName, resourceGroupName);

            return new WorkbookInfo(
                WorkbookId: createdWorkbook.Id?.ToString() ?? "",
                WorkbookDisplayName: createdWorkbook.Data.DisplayName ?? title,
                Description: createdWorkbook.Data.Description,
                Category: createdWorkbook.Data.Category,
                Location: createdWorkbook.Data.Location.ToString(),
                Kind: createdWorkbook.Data.Kind?.ToString(),
                Tags: ConvertTagsToString(createdWorkbook.Data.Tags),
                SerializedData: createdWorkbook.Data.SerializedData,
                Version: createdWorkbook.Data.Version,
                TimeModified: createdWorkbook.Data.ModifiedOn,
                UserId: createdWorkbook.Data.UserId,
                SourceId: createdWorkbook.Data.SourceId
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating workbook '{Title}' in resource group '{ResourceGroup}'", title, resourceGroupName);
            throw;
        }
    }

    public async Task<bool> DeleteWorkbook(string workbookId, RetryPolicyOptions? retryPolicy = null)
    {
        if (string.IsNullOrEmpty(workbookId))
        {
            throw new ArgumentException("Workbook ID is required", nameof(workbookId));
        }

        try
        {
            // Get ARM client
            var armClient = await CreateArmClientAsync(null, retryPolicy);

            // Parse the workbook resource ID to get the workbook directly
            var workbookResourceId = new ResourceIdentifier(workbookId);
            var workbookResource = armClient.GetApplicationInsightsWorkbookResource(workbookResourceId);

            // Delete the workbook
            var response = await workbookResource.DeleteAsync(Azure.WaitUntil.Completed);

            _logger.LogInformation("Successfully deleted workbook with ID: {WorkbookId}", workbookId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting workbook with ID: {WorkbookId}", workbookId);
            throw;
        }
    }
}
