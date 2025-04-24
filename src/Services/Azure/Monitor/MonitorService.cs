// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using Azure.Monitor.Query;
using Azure.ResourceManager.OperationalInsights;
using AzureMcp.Arguments;
using AzureMcp.Models.Monitor;
using AzureMcp.Services.Interfaces;
using System.Text.Json;

namespace AzureMcp.Services.Azure.Monitor;

public class MonitorService(ISubscriptionService subscriptionService, IResourceGroupService resourceGroupService)
    : BaseAzureService, IMonitorService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private readonly IResourceGroupService _resourceGroupService = resourceGroupService ?? throw new ArgumentNullException(nameof(resourceGroupService));

    private const string TablePlaceholder = "{tableName}";

    private static readonly Dictionary<string, string> PredefinedQueries = new()
    {
        ["recent"] = """
            {tableName}
            | order by TimeGenerated desc
            """,
        ["errors"] = """
            {tableName}
            | where Level == "ERROR"
            | order by TimeGenerated desc
            """
    };

    public async Task<List<JsonDocument>> QueryWorkspace(
        string subscription,
        string workspace,
        string query,
        int timeSpanDays = 1,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription, workspace, query);

        var credential = await GetCredential(tenant);
        var options = AddDefaultPolicies(new LogsQueryClientOptions());

        if (retryPolicy != null)
        {
            options.Retry.Delay = TimeSpan.FromSeconds(retryPolicy.DelaySeconds);
            options.Retry.MaxDelay = TimeSpan.FromSeconds(retryPolicy.MaxDelaySeconds);
            options.Retry.MaxRetries = retryPolicy.MaxRetries;
            options.Retry.Mode = retryPolicy.Mode;
            options.Retry.NetworkTimeout = TimeSpan.FromSeconds(retryPolicy.NetworkTimeoutSeconds);
        }
        var client = new LogsQueryClient(credential, options);

        try
        {
            var (workspaceId, _) = await GetWorkspaceInfo(workspace, subscription, tenant, retryPolicy);

            var response = await client.QueryWorkspaceAsync(
                workspaceId,
                query,
                new QueryTimeRange(TimeSpan.FromDays(timeSpanDays))
            );

            var results = new List<JsonDocument>();
            if (response.Value.Table != null)
            {
                var rows = response.Value.Table.Rows;
                var columns = response.Value.Table.Columns;

                if (rows != null && columns != null && rows.Any())
                {
                    foreach (var row in rows)
                    {
                        var rowDict = new Dictionary<string, object>();
                        for (int i = 0; i < columns.Count; i++)
                        {
                            rowDict[columns[i].Name] = row[i];
                        }
                        results.Add(JsonDocument.Parse(JsonSerializer.Serialize(rowDict)));
                    }
                }
            }
            return results;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error querying workspace: {ex.Message}", ex);
        }
    }

    public async Task<List<string>> ListTables(
        string subscription,
        string resourceGroup,
        string workspace,
        string? tableType = "CustomLog",
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription, resourceGroup, workspace);

        try
        {
            var (_, resolvedWorkspaceName) = await GetWorkspaceInfo(workspace, subscription, tenant, retryPolicy);

            var resourceGroupResource = await _resourceGroupService.GetResourceGroupResource(subscription, resourceGroup, tenant, retryPolicy) ??
                throw new Exception($"Resource group {resourceGroup} not found in subscription {subscription}");
            var workspaceResponse = await resourceGroupResource.GetOperationalInsightsWorkspaceAsync(resolvedWorkspaceName)
                .ConfigureAwait(false);

            if (workspaceResponse?.Value == null)
            {
                throw new Exception($"Workspace {resolvedWorkspaceName} not found in resource group {resourceGroup}");
            }

            var workspaceResource = workspaceResponse.Value;
            var tableOperations = workspaceResource.GetOperationalInsightsTables();
            var tables = await tableOperations.GetAllAsync()
                .ToListAsync()
                .ConfigureAwait(false);

            return [.. tables
                .Where(table => string.IsNullOrEmpty(tableType) || table.Data.Schema.TableType.ToString() == tableType)
                .Select(table => table.Data.Name)
                .OrderBy(name => name)];
        }
        catch (Exception ex)
        {
            throw new Exception($"Error listing tables: {ex.Message}", ex);
        }
    }

    public async Task<List<WorkspaceInfo>> ListWorkspaces(
        string subscription,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription);

        try
        {
            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);

            var workspaces = await subscriptionResource
                .GetOperationalInsightsWorkspacesAsync()
                .Select(workspace => new WorkspaceInfo
                {
                    Name = workspace.Data.Name,
                    CustomerId = workspace.Data.CustomerId?.ToString() ?? string.Empty,
                })
                .ToListAsync()
                .ConfigureAwait(false);

            return workspaces;
        }
        catch (Exception ex) when (ex is not ArgumentNullException)
        {
            throw new Exception($"Error retrieving Log Analytics workspaces: {ex.Message}", ex);
        }
    }

    public async Task<object> QueryLogs(
        string subscription,
        string workspace,
        string query,
        string table,
        int? hours = 24,
        int? limit = 20,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription, workspace, table);

        // Get the workspace ID regardless of what was passed
        var (workspaceId, _) = await GetWorkspaceInfo(workspace, subscription, tenant, retryPolicy);

        // Check if the query is a predefined query name
        if (!string.IsNullOrEmpty(query) && PredefinedQueries.ContainsKey(query.Trim().ToLower()))
        {
            query = PredefinedQueries[query.Trim().ToLower()];
            // Replace table placeholder with actual table name
            query = query.Replace(TablePlaceholder, table);
        }

        ValidateRequiredParameters(query);

        // Add limit to query if specified and not already present
        if (limit.HasValue && !query.Contains("limit", StringComparison.CurrentCultureIgnoreCase))
        {
            query = $"{query}\n| limit {limit}";
        }

        try
        {
            // Convert hours to days for QueryWorkspace
            double days = (hours ?? 24) / 24.0;

            // Call QueryWorkspace with the prepared query
            var jsonResults = await QueryWorkspace(
                subscription,
                workspaceId,
                query,
                (int)Math.Ceiling(days), // Round up to ensure we cover the full time range
                tenant,
                retryPolicy
            );

            // Convert JsonDocument results to Dictionary<string, object>
            var results = new List<Dictionary<string, object?>>();
            foreach (var jsonDoc in jsonResults)
            {
                var dict = new Dictionary<string, object?>();
                foreach (var property in jsonDoc.RootElement.EnumerateObject())
                {
                    dict[property.Name] = GetValueFromJsonElement(property.Value);
                }
                results.Add(dict);
            }

            // Return the list as an object to match the interface
            return results;
        }
        catch (Exception ex)
        {
            // Provide a more specific error message based on the exception type
            string errorMessage = ex switch
            {
                RequestFailedException rfe => $"Azure request failed: {rfe.Status} - {rfe.Message}",
                TimeoutException => "The query timed out. Try simplifying your query or reducing the time range.",
                _ => $"Error querying logs: {ex.Message}"
            };

            throw new Exception(errorMessage, ex);
        }
    }

    // Helper method to convert JsonElement to appropriate .NET types
    private static object? GetValueFromJsonElement(JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.String:
                return element.GetString() ?? string.Empty; // Return empty string instead of null
            case JsonValueKind.Number:
                if (element.TryGetInt64(out long longValue))
                    return longValue;
                return element.GetDouble();
            case JsonValueKind.True:
                return true;
            case JsonValueKind.False:
                return false;
            case JsonValueKind.Null:
                return null;
            case JsonValueKind.Object:
                var obj = new Dictionary<string, object?>();
                foreach (var property in element.EnumerateObject())
                {
                    obj[property.Name] = GetValueFromJsonElement(property.Value);
                }
                return obj;
            case JsonValueKind.Array:
                var array = new List<object?>();
                foreach (var item in element.EnumerateArray())
                {
                    array.Add(GetValueFromJsonElement(item));
                }
                return array;
            default:
                return null;
        }
    }

    private static bool IsWorkspaceId(string workspace)
    {
        // Workspace IDs are GUIDs
        return Guid.TryParse(workspace, out _);
    }

    private async Task<(string id, string name)> GetWorkspaceInfo(
        string workspace,
        string subscription,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null)
    {
        // If we're given an ID and need an ID, or given a name and need a name, return as is
        bool isId = IsWorkspaceId(workspace);
        var workspaces = await ListWorkspaces(subscription, tenant, retryPolicy);

        // Find the workspace
        var matchingWorkspace = workspaces.FirstOrDefault(w =>
            isId ? w.CustomerId.Equals(workspace, StringComparison.OrdinalIgnoreCase)
                : w.Name.Equals(workspace, StringComparison.OrdinalIgnoreCase));

        if (matchingWorkspace == null)
        {
            throw new Exception($"Could not find workspace with {(isId ? "ID" : "name")} {workspace}");
        }

        return (matchingWorkspace.CustomerId, matchingWorkspace.Name);
    }
}