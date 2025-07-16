// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Workbooks.Models;
using AzureMcp.Options;

namespace AzureMcp.Areas.Workbooks.Services;

public interface IWorkbooksService
{
    Task<List<WorkbookInfo>> ListWorkbooks(string? subscriptionId = null, string? resourceGroupName = null, RetryPolicyOptions? retryPolicy = null);
    Task<WorkbookInfo?> GetWorkbook(string workbookId, RetryPolicyOptions? retryPolicy = null);
    Task<WorkbookInfo?> UpdateWorkbook(string workbookId, string? title = null, string? serializedContent = null, RetryPolicyOptions? retryPolicy = null);
    Task<WorkbookInfo?> CreateWorkbook(string subscriptionId, string resourceGroupName, string title, string serializedData, string sourceId, RetryPolicyOptions? retryPolicy = null);
    Task<bool> DeleteWorkbook(string workbookId, RetryPolicyOptions? retryPolicy = null);
}
