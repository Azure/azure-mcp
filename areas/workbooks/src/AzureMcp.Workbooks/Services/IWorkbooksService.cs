// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Options;
using AzureMcp.Workbooks.Models;

namespace AzureMcp.Workbooks.Services;

public interface IWorkbooksService
{
    Task<List<WorkbookInfo>> ListWorkbooks(string subscription, string resourceGroupName, WorkbookFilters? filters = null, RetryPolicyOptions? retryPolicy = null, string? tenant = null);
    Task<WorkbookInfo?> CreateWorkbook(string subscription, string resourceGroupName, string displayName, string serializedData, string sourceId, RetryPolicyOptions? retryPolicy = null, string? tenant = null);
    Task<WorkbookInfo?> GetWorkbook(string workbookId, RetryPolicyOptions? retryPolicy = null, string? tenant = null);
    Task<WorkbookInfo?> UpdateWorkbook(string workbookId, string? displayName = null, string? serializedContent = null, RetryPolicyOptions? retryPolicy = null, string? tenant = null);
    Task<bool> DeleteWorkbook(string workbookId, RetryPolicyOptions? retryPolicy = null, string? tenant = null);
}
