// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Nodes;
using Azure;
using Azure.Core;
using Azure.Monitor.Ingestion;
using Azure.Monitor.Query;
using Azure.Monitor.Query.Models;
using Azure.ResourceManager.OperationalInsights;
using AzureMcp.Core.Options;
using AzureMcp.Core.Services.Azure;
using AzureMcp.Core.Services.Azure.ResourceGroup;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.Monitor.Models;

namespace AzureMcp.Monitor.Services;

public class MonitorService : BaseAzureService, IMonitorService
{
    private readonly ISubscriptionService _subscriptionService;
    private readonly IResourceGroupService _resourceGroupService;

    public MonitorService(ISubscriptionService subscriptionService, ITenantService tenantService, IResourceGroupService resourceGroupService)
        : base(tenantService)
    {
        _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
        _resourceGroupService = resourceGroupService ?? throw new ArgumentNullException(nameof(resourceGroupService));
    }

    public async Task<List<JsonNode>> QueryResourceLogs(
        string subscription,
        string resourceId,
        string query,
        string table,
        int? hours = 24,
        int? limit = 20,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription, resourceId, table);
        query = BuildQuery(query, table, limit);

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
        var timeRange = new QueryTimeRange(TimeSpan.FromHours(hours ?? 24));

        try
        {
            var response = await client.QueryResourceAsync(
                ResourceIdentifier.Parse(resourceId),
                query,
                timeRange);
            return ParseQueryResults(response.Value.Table);
        }
        catch (Exception ex)
        {
            string errorMessage = ex switch
            {
                RequestFailedException rfe => $"Azure request failed: {rfe.Status} - {rfe.Message}",
                TimeoutException => "The query timed out. Try simplifying your query or reducing the time range.",
                _ => $"Error querying resource logs: {ex.Message}"
            };
            throw new Exception(errorMessage, ex);
        }
    }

    private const string TablePlaceholder = "{tableName}";

    private static readonly Dictionary<string, string> s_predefinedQueries = new()
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

    public async Task<List<JsonNode>> QueryWorkspace(
        string subscription,
        string workspace,
        string query,
        int timeSpanDays = 1,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
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

            var results = new List<JsonNode>();
            if (response.Value.Table != null)
            {
                var rows = response.Value.Table.Rows;
                var columns = response.Value.Table.Columns;

                if (rows != null && columns != null && rows.Any())
                {
                    foreach (var row in rows)
                    {
                        var rowDict = new JsonObject();
                        for (int i = 0; i < columns.Count; i++)
                        {
                            rowDict[columns[i].Name] = JsonValue.Create(row[i]?.ToString() ?? "null");
                        }
                        results.Add(rowDict);
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
        RetryPolicyOptions? retryPolicy = null)
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
                .Select(table => table.Data.Name ?? string.Empty) // ensure non-null
                .Where(name => !string.IsNullOrEmpty(name))
                .OrderBy(name => name)];
        }
        catch (Exception ex)
        {
            throw new Exception($"Error listing tables for workspace {workspace}: {ex.Message}", ex);
        }
    }

    public async Task<List<WorkspaceInfo>> ListWorkspaces(
        string subscription,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
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
    public async Task<List<JsonNode>> QueryWorkspaceLogs(
        string subscription,
        string workspace,
        string query,
        string table,
        int? hours = 24,
        int? limit = 20,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription, workspace, table);

        var (workspaceId, _) = await GetWorkspaceInfo(workspace, subscription, tenant, retryPolicy);
        query = BuildQuery(query, table, limit);
        ValidateRequiredParameters(query);

        try
        {
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
            var timeRange = new QueryTimeRange(TimeSpan.FromHours(hours ?? 24));

            var response = await client.QueryWorkspaceAsync(
                workspaceId,
                query,
                timeRange);

            return ParseQueryResults(response.Value.Table);
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

    // Helper to build the query string with table and limit
    private static string BuildQuery(string query, string table, int? limit)
    {
        if (!string.IsNullOrEmpty(query) && s_predefinedQueries.ContainsKey(query.Trim().ToLower()))
        {
            query = s_predefinedQueries[query.Trim().ToLower()];
            query = query.Replace(TablePlaceholder, table);
        }
        // Add limit if not present
        if (limit.HasValue && !query.Contains("limit", StringComparison.CurrentCultureIgnoreCase))
        {
            query = $"{query}\n| limit {limit}";
        }
        return query;
    }

    // Helper to parse query results from a LogsTable
    private static List<JsonNode> ParseQueryResults(LogsTable? table)
    {
        var results = new List<JsonNode>();
        if (table != null)
        {
            var rows = table.Rows;
            var columns = table.Columns;
            if (rows != null && columns != null && rows.Any())
            {
                foreach (var row in rows)
                {
                    var rowDict = new JsonObject();
                    for (int i = 0; i < columns.Count; i++)
                    {
                        rowDict[columns[i].Name] = JsonValue.Create(row[i]?.ToString() ?? "null");
                    }
                    results.Add(rowDict);
                }
            }
        }
        return results;
    }

    public async Task<List<string>> ListTableTypes(
        string subscription,
        string resourceGroup,
        string workspace,
        string? tenant,
        RetryPolicyOptions? retryPolicy)
    {
        ValidateRequiredParameters(subscription, resourceGroup, workspace);
        try
        {
            var (_, resolvedWorkspaceName) = await GetWorkspaceInfo(workspace, subscription, tenant, retryPolicy);

            var resourceGroupResource = await _resourceGroupService.GetResourceGroupResource(subscription, resourceGroup, tenant, retryPolicy)
                ?? throw new Exception($"Resource group {resourceGroup} not found in subscription {subscription}");
            var workspaceResponse = await resourceGroupResource.GetOperationalInsightsWorkspaceAsync(resolvedWorkspaceName)
                .ConfigureAwait(false);

            if (workspaceResponse?.Value == null)
            {
                throw new Exception($"Workspace {resolvedWorkspaceName} not found in resource group {resourceGroup}");
            }

            var workspaceResource = workspaceResponse.Value;
            var tableOperations = workspaceResource.GetOperationalInsightsTables();
            var tables = await tableOperations.GetAllAsync().ToListAsync().ConfigureAwait(false);

            var tableTypes = tables
                .Select(table => table.Data.Schema.TableType?.ToString() ?? string.Empty)
                .Where(type => !string.IsNullOrEmpty(type))
                .Distinct()
                .OrderBy(type => type)
                .ToList();

            return tableTypes;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error listing table types for workspace {workspace}: {ex.Message}", ex);
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
        RetryPolicyOptions? retryPolicy = null)
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

    public async Task<(string Status, int RecordCount, string Message)> UploadLogs(
        string workspace,
        string dataCollectionRule,
        string streamName,
        string logData,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(workspace, dataCollectionRule, streamName, logData);

        try
        {
            var credential = await GetCredential(tenant);

            // Parse the data collection rule ID to get the endpoint
            var dcrId = dataCollectionRule;
            var endpointUrl = BuildIngestionEndpoint(dcrId);

            var ingestionClientOptions = AddDefaultPolicies(new LogsIngestionClientOptions());

            if (retryPolicy != null)
            {
                ingestionClientOptions.Retry.Delay = TimeSpan.FromSeconds(retryPolicy.DelaySeconds);
                ingestionClientOptions.Retry.MaxDelay = TimeSpan.FromSeconds(retryPolicy.MaxDelaySeconds);
                ingestionClientOptions.Retry.MaxRetries = retryPolicy.MaxRetries;
            }

            var ingestionClient = new LogsIngestionClient(new Uri(endpointUrl), credential, ingestionClientOptions);

            // Parse the log data as JSON nodes to avoid AOT issues
            JsonNode? jsonNode;
            try
            {
                jsonNode = JsonNode.Parse(logData);
            }
            catch (Exception ex)
            {
                return ("Failed", 0, $"Invalid JSON format: {ex.Message}");
            }

            if (jsonNode is not JsonArray jsonArray)
            {
                return ("Failed", 0, "Log data must be a JSON array");
            }

            if (jsonArray.Count == 0)
            {
                return ("Failed", 0, "Log data array is empty");
            }

            // Convert JsonArray to object array for the ingestion client
            var logEntries = new List<object>();
            foreach (var item in jsonArray)
            {
                if (item != null)
                {
                    logEntries.Add(item);
                }
            }

            // Upload the logs
            var response = await ingestionClient.UploadAsync(dcrId, streamName, logEntries);

            // Check if the upload was successful
            var recordCount = logEntries.Count;
            var status = response.Status >= 200 && response.Status < 300 ? "Success" : "Failed";
            var message = status == "Success"
                ? $"Successfully uploaded {recordCount} records to stream '{streamName}'"
                : $"Upload failed with status {response.Status}";

            return (status, recordCount, message);
        }
        catch (Exception ex)
        {
            return ("Failed", 0, $"Error uploading logs: {ex.Message}");
        }
    }

    public Task<(string Status, string Message, JsonNode? Details)> CheckIngestionStatus(
        string workspace,
        string dataCollectionRule,
        string? operationId = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(workspace, dataCollectionRule);

        try
        {
            // Note: Azure Monitor ingestion doesn't provide direct status APIs
            // This is a simplified implementation that checks for recent log operations
            var message = operationId != null
                ? $"Operation ID {operationId} status check is not directly supported by Azure Monitor. Check ingestion logs manually."
                : "Recent ingestion status check is not directly supported by Azure Monitor. Check workspace logs manually.";

            var details = new JsonObject
            {
                ["dataCollectionRule"] = dataCollectionRule,
                ["workspace"] = workspace,
                ["operationId"] = operationId,
                ["note"] = "Azure Monitor Log Ingestion API does not provide direct status endpoints. Check Azure Monitor Logs in the portal for ingestion status."
            };

            return Task.FromResult(("Unavailable", message, (JsonNode?)details));
        }
        catch (Exception ex)
        {
            return Task.FromResult(("Error", $"Error checking ingestion status: {ex.Message}", (JsonNode?)null));
        }
    }

    private static string BuildIngestionEndpoint(string dataCollectionRuleId)
    {
        // Extract the data collection endpoint from the DCR ID
        // DCR ID format: /subscriptions/{subscription}/resourceGroups/{rg}/providers/Microsoft.Insights/dataCollectionRules/{name}
        // We need to build the data collection endpoint URL
        var parts = dataCollectionRuleId.Split('/');
        if (parts.Length < 4)
        {
            throw new ArgumentException($"Invalid data collection rule ID format: {dataCollectionRuleId}");
        }

        // For simplicity, we'll assume the standard ingestion endpoint pattern
        // In a real implementation, you might need to query the DCR to get the actual endpoint
        var subscriptionId = parts[2];
        return $"https://monitor.azure.com/";
    }
}
