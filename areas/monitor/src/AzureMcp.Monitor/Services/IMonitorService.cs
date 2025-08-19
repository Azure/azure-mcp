// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Nodes;
using AzureMcp.Core.Options;
using AzureMcp.Monitor.Models;

namespace AzureMcp.Monitor.Services;

public interface IMonitorService
{
    Task<List<JsonNode>> QueryResourceLogs(
        string subscription,
        string resourceId,
        string query,
        string table,
        int? hours = 24,
        int? limit = 20,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<List<JsonNode>> QueryWorkspace(
        string subscription,
        string workspace,
        string query,
        int timeSpanDays = 1,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<List<string>> ListTables(
        string subscription,
        string resourceGroup,
        string workspace, string? tableType = "CustomLog",
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<List<WorkspaceInfo>> ListWorkspaces(
        string subscription,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<List<JsonNode>> QueryWorkspaceLogs(
        string subscription,
        string workspace,
        string query,
        string table,
        int? hours = 24, int? limit = 20,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<List<string>> ListTableTypes(
        string subscription,
        string resourceGroup,
        string workspace,
        string? tenant,
        RetryPolicyOptions? retryPolicy);

    Task<(string Status, int RecordCount, string Message)> UploadLogs(
        string workspace,
        string dataCollectionRule,
        string streamName,
        string logData,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<(string Status, string Message, JsonNode? Details)> CheckIngestionStatus(
        string workspace,
        string dataCollectionRule,
        string? operationId = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<(string Status, string Message, JsonNode? ValidationResults)> ValidateLogData(
        string dataCollectionRule,
        string logData,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);
}
