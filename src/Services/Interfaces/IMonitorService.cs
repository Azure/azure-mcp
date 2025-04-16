// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments;
using AzureMcp.Models.Monitor;
using System.Text.Json;

namespace AzureMcp.Services.Interfaces;

public interface IMonitorService
{
    Task<List<JsonDocument>> QueryWorkspace(
        string subscription,
        string workspace,
        string query,
        int timeSpanDays = 1,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null);

    Task<List<string>> ListTables(
        string subscription,
        string resourceGroup,
        string workspace, string? tableType = "CustomLog",
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null);

    Task<List<WorkspaceInfo>> ListWorkspaces(
        string subscription,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null);

    Task<object> QueryLogs(
        string subscription,
        string workspace,
        string query,
        string table,
        int? hours = 24, int? limit = 20,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null);
}