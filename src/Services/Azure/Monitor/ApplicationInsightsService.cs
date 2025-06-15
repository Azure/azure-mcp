// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Nodes;
using System.Threading;
using Azure.Core;
using Azure.Monitor.Query;
using Azure.ResourceManager.ApplicationInsights;
using AzureMcp.Commands.Server.Tools;
using AzureMcp.Models.Monitor.ApplicationInsights;
using AzureMcp.Options;
using AzureMcp.Services.Interfaces;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Services.Azure.Monitor;

public class ApplicationInsightsService(ISubscriptionService subscriptionService, ITenantService tenantService, ILogsQueryService logsQueryService) : BaseAzureService(tenantService), IApplicationInsightsService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private readonly ILogsQueryService _logsQueryService = logsQueryService ?? throw new ArgumentNullException(nameof(logsQueryService));

    private static readonly StandardFields _standardFields = new StandardFields();
    // TODO: Add call to DT Indexer to get the full list of resource IDs.
    private const string DistributedTraceQueryTemplate = @"union requests, dependencies, exceptions, (availabilityResults | extend success=iff(success=='1', true, false))
| where timestamp > datetime(""{{startTime}}"") and timestamp < datetime(""{{endTime}}"")
| where operation_Id == ""{{traceId}}""
| project-away customMeasurements, _ResourceId, itemCount, client_Type, client_Model, client_OS, client_IP, client_City, client_StateOrProvince, client_CountryOrRegion, client_Browser, appId, appName, iKey, sdkVersion";

    private class StandardFields
    {
        public IEnumerable<string> Values => StandardFieldValues;

        public static List<string> StandardFieldValues = new List<string>();

        public string Id = AddValue("id");
        public string OperationParentId = AddValue("operation_ParentId");
        public string OperationName = AddValue("operation_Name");
        public string ResultCode = AddValue("resultCode");
        public string ItemType = AddValue("itemType");
        public string Success = AddValue("success");
        public string Timestamp = AddValue("timestamp");
        public string Duration = AddValue("duration");
        public string OperationId = AddValue("operation_Id");

        public StandardFields()
        {

        }

        private static string AddValue(string name)
        {
            StandardFieldValues.Add(name);
            return name;
        }
    }

    private enum InputType
    {
        Requests,
        Dependencies,
        AvailabilityResults
    }

    private enum IssueType
    {
        Errors,
        Latency
    }

    public async Task<List<JsonNode>> DiagnoseApplicationAsync(
        IMcpServer? mcpServer,
        string subscription,
        string appId,
        string symptoms,
        string? startTime = null,
        string? endTime = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription, appId, symptoms);

        // Calculate default times if not provided
        var end = string.IsNullOrEmpty(endTime)
            ? DateTimeOffset.UtcNow
            : DateTimeOffset.Parse(endTime);

        var start = string.IsNullOrEmpty(startTime)
            ? end.AddHours(-24)
            : DateTimeOffset.Parse(startTime);

        try
        {
            var (resourceId, _) = await GetApplicationInsightsInfo(appId, subscription, tenant, retryPolicy);

            (InputType inputType, IssueType issueType) = await GetSourceAndIssueAsync(mcpServer, symptoms, CancellationToken.None);

            // TODO: Implement the actual query logic to diagnose the application
            return new List<JsonNode>
            {
                new JsonObject
                {
                    ["inputType"] = inputType.ToString(),
                    ["issueType"] = issueType.ToString(),
                    ["resourceId"] = resourceId.ToString(),
                    ["startTime"] = start.UtcDateTime.ToString("O"),
                    ["endTime"] = end.UtcDateTime.ToString("O"),
                    ["symptoms"] = symptoms
                }
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error diagnosing application: {ex.Message}", ex);
        }
        
    }

    public async Task<DistributedTraceResult> GetDistributedTraceAsync(
        string subscription,
        string appId,
        string traceId,
        string spanId,
        string? startTime = null,
        string? endTime = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription, appId, traceId, spanId);

        // Calculate default times if not provided
        var end = string.IsNullOrEmpty(endTime)
            ? DateTimeOffset.UtcNow
            : DateTimeOffset.Parse(endTime);

        var start = string.IsNullOrEmpty(startTime)
            ? end.AddHours(-24)
            : DateTimeOffset.Parse(startTime);

        try
        {
            var (resourceId, _) = await GetApplicationInsightsInfo(appId, subscription, tenant, retryPolicy);
            var kql = DistributedTraceQueryTemplate
                .Replace("{{startTime}}", start.UtcDateTime.ToString("O"))
                .Replace("{{endTime}}", end.UtcDateTime.ToString("O"))
                .Replace("{{traceId}}", traceId);

            var response = await _logsQueryService.QueryResourceAsync(resourceId, kql, start, end, tenant, retryPolicy);
            
            // Create a result object to hold our trace data
            var result = new DistributedTraceResult
            {
                TraceId = traceId,
                Spans = new List<SpanDetails>()
            };

            List<SpanDetails> spans = new List<SpanDetails>();

            if (response != null)
            {
                // Dictionary to keep track of spans by ID and parent-child relationships
                var spanMap = new Dictionary<string, SpanDetails>();
                var parentChildRelationships = new Dictionary<string, List<string>>();

                // Process each row in the query results
                foreach (var row in response.Rows)
                {
                    var currentSpanId = GetStringValue(row, _standardFields.Id, string.Empty);
                    var parentSpanId = GetStringValue(row, _standardFields.OperationParentId, string.Empty);

                    var spanDetails = new SpanDetails
                    {
                        SpanId = currentSpanId,
                        ParentId = parentSpanId,
                        OperationName = GetStringValue(row, _standardFields.OperationName, string.Empty),
                        ResponseCode = GetStringValue(row, _standardFields.ResultCode, string.Empty),
                        ItemType = GetStringValue(row, _standardFields.ItemType, string.Empty),
                        IsSuccessful = GetStringValue(row, _standardFields.Success, "true").Equals("true", StringComparison.OrdinalIgnoreCase),
                        Properties = new List<KeyValuePair<string, string>>(),
                        ChildSpans = new List<SpanDetails>()
                    };

                    // Parse datetime fields when present
                    if (TryGetDateTime(row, _standardFields.Timestamp, out DateTime timestamp))
                    {
                        spanDetails.StartTime = timestamp;
                    }

                    // Try to get duration in milliseconds
                    if (TryGetDouble(row, _standardFields.Duration, out double durationMs))
                    {
                        spanDetails.Duration = TimeSpan.FromMilliseconds(durationMs);
                        spanDetails.EndTime = spanDetails.StartTime.Add(spanDetails.Duration);
                    }
                    else
                    {
                        // Default end time if duration isn't available
                        spanDetails.EndTime = spanDetails.StartTime;
                    }

                    // Add all other properties as key-value pairs
                    foreach (var column in row)
                    {
                        string columnName = column.Key;

                        if (!IsStandardField(columnName) && column.Value != null)
                        {
                            spanDetails.Properties.Add(new KeyValuePair<string, string>(
                                columnName,
                                column.Value?.ToString() ?? string.Empty
                            ));
                        }
                    }
                    // Add the span to our tracking map
                    spanMap[currentSpanId] = spanDetails;
                }
                  // Now we have all spans, we can build the hierarchy
                BuildSpanHierarchy(spanMap);
                
                // Filter to only include the specified span, its ancestors, and direct descendants
                if (spanMap.TryGetValue(spanId, out var targetSpan))
                {
                    // Find all ancestor spans (parents up to the root)
                    var ancestorSpans = FindAncestorSpans(spanMap, targetSpan);
                    
                    // The direct descendants are already in the ChildSpans property after building the hierarchy
                    var directDescendants = targetSpan.ChildSpans;
                    
                    // The result should include the target span, its ancestors, and direct descendants
                    var filteredSpans = new List<SpanDetails> { targetSpan };
                    filteredSpans.AddRange(ancestorSpans);
                    filteredSpans.AddRange(directDescendants);
                    
                    // Set the filtered spans to the result
                    result.Spans = filteredSpans;
                }
                else
                {
                    // If the specified span wasn't found, return an empty result
                    result.Spans = new List<SpanDetails>();
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving distributed trace: {ex.Message}", ex);
        }
    }

    private static bool SupportsSampling(IMcpServer? server)
    {
        return server?.ClientCapabilities?.Sampling != null;
    }

    private static async Task<(InputType, IssueType)> GetSourceAndIssueAsync(IMcpServer? server, string symptoms, CancellationToken cancellationToken)
    {
        if (server == null || !SupportsSampling(server))
        {
            // Default to investigating request failures if we don't have ability to sample
            return (InputType.Requests, IssueType.Errors);
        }

        // TODO: Progress notification?

        var samplingRequest = new CreateMessageRequestParams
        {
            Messages = [
                new SamplingMessage
                {
                    Role = Role.Assistant,
                    Content = new Content {
                        Type = "text",
                        Text = $$"""
                        Based on the symptoms below, determine the InputType and IssueType.

                        Symptoms
                        {{symptoms}}

                        InputType options (choose one that best matches the symptoms):
                        - Inbound - Inbound describes issues with requests being made to the application. For example, a user making a call to the application and getting an error.
                        - Outbound - Outbound describes issues with requests the application is making to external dependencies. For example, the application calls a SQL database.
                        - Availability - Availability describes issues with synthetic tests running against the application reporting failures. For example, the availability test for the health endpoint is failing.

                        IssueType options (choose one that best matches the symptoms):
                        - Slow - Slow describes issues where there is increased response time for an application or the application is behaving slower than expected.
                        - Failing - Failing describes issues where the application is experiencing errors.

                        Return the output in JSON with the following format:
                        ```json
                        {
                            "inputType": "Inbound|Outbound|Availability",
                            "issueType": "Slow|Failing"
                        }
                        ```
                        """
                    }
                }
            ]
        };

        try
        {
            var samplingResponse = await server.RequestSamplingAsync(samplingRequest, cancellationToken);
            var responseJson = samplingResponse.Content.Text?.Trim()?.Replace("```json", "").Replace("```", "");
            IssueType? issueType = null;
            InputType? inputType = null;
            Dictionary<string, object?> parameters = [];
            if (!string.IsNullOrEmpty(responseJson))
            {
                using var doc = JsonDocument.Parse(responseJson);
                var root = doc.RootElement;
                foreach (var property in root.EnumerateObject())
                {
                    if (property.NameEquals("inputType") && property.Value.ValueKind == JsonValueKind.String)
                    {
                        switch (property.Value.ToString())
                        {
                            case "Inbound":
                                inputType = InputType.Requests;
                                break;
                            case "Outbound":
                                inputType = InputType.Dependencies;
                                break;
                            case "Availability":
                                inputType = InputType.AvailabilityResults;
                                break;
                        }
                    }
                    if (property.NameEquals("issueType") && property.Value.ValueKind == JsonValueKind.String)
                    {
                        switch (property.Value.ToString())
                        {
                            case "Slow":
                                issueType = IssueType.Latency;
                                break;
                            case "Failing":
                                issueType = IssueType.Errors;
                                break;
                        }
                    }
                }
                
            }
            if (inputType == null || issueType == null)
            {
                throw new Exception($"Failed to understand the diagnostic flow based on the reported symptoms {symptoms}. {responseJson}.");
            }
            return (inputType.Value, issueType.Value);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to understand the symptoms: {ex}", ex);
        }
    }

    // Helper method to find all ancestor spans (parents, grandparents, etc.)
    private static List<SpanDetails> FindAncestorSpans(Dictionary<string, SpanDetails> spanMap, SpanDetails span)
    {
        var ancestors = new List<SpanDetails>();
        string currentParentId = span.ParentId;
        
        // Traverse up the chain until we reach a span with no parent or one that isn't in our map
        while (!string.IsNullOrEmpty(currentParentId) && spanMap.TryGetValue(currentParentId, out var parentSpan))
        {
            ancestors.Add(parentSpan);
            currentParentId = parentSpan.ParentId;
        }
        
        return ancestors;
    }

    // Helper method to build the span hierarchy based on parent-child relationships
    private static void BuildSpanHierarchy(Dictionary<string, SpanDetails> spanMap)
    {
        // Create a dictionary to track parent-child relationships
        var parentChildRelationships = new Dictionary<string, List<string>>();
        
        // Identify parent-child relationships from the span data
        foreach (var span in spanMap.Values)
        {
            // Find the parent span ID if available in properties
            string parentSpanId = span.ParentId;
            
            // If we found a parent ID, add this relationship
            if (!string.IsNullOrEmpty(parentSpanId) && spanMap.ContainsKey(parentSpanId))
            {
                if (!parentChildRelationships.TryGetValue(parentSpanId, out var children))
                {
                    children = new List<string>();
                    parentChildRelationships[parentSpanId] = children;
                }
                children.Add(span.SpanId);
            }
        }
        
        // Build the hierarchy based on identified relationships
        foreach (var parentId in parentChildRelationships.Keys)
        {
            if (spanMap.TryGetValue(parentId, out var parentSpan))
            {
                foreach (var childId in parentChildRelationships[parentId])
                {
                    if (spanMap.TryGetValue(childId, out var childSpan))
                    {
                        parentSpan.ChildSpans.Add(childSpan);
                    }
                }
            }
        }
    }

    // Helper method to get column indexes for faster lookup

    private static bool IsAppIdOrIkey(string app)
    {
        // Workspace IDs are GUIDs
        return Guid.TryParse(app, out _);
    }

    private async Task<(ResourceIdentifier id, string name)> GetApplicationInsightsInfo(
        string appId,
        string subscription,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        // If we're given an ID and need an ID, or given a name and need a name, return as is
        bool isId = IsAppIdOrIkey(appId);
        var resources = await ListApplicationInsightsResources(subscription, tenant, retryPolicy);

        // Find the workspace
        var matchingResource = resources.FirstOrDefault(w =>
            isId ? (w.AppId.Equals(appId, StringComparison.OrdinalIgnoreCase) || w.InstrumentationKey.Equals(appId, StringComparison.OrdinalIgnoreCase)) 
                 : w.Name.Equals(appId, StringComparison.OrdinalIgnoreCase));

        if (matchingResource == null)
        {
            throw new Exception($"Could not find Application Insights resource with {(isId ? "ID" : "name")} {appId}");
        }

        return (matchingResource.ResourceId, matchingResource.Name);
    }

    public async Task<List<ApplicationInsightsResourceInfo>> ListApplicationInsightsResources(
        string subscription,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription);

        try
        {
            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);

            var appInsightsResources = await subscriptionResource
                .GetApplicationInsightsComponentsAsync()
                .Select(resource => new ApplicationInsightsResourceInfo
                {
                    Name = resource.Data.Name,
                    ResourceId = resource.Id,
                    AppId = resource.Data.AppId,
                    InstrumentationKey = resource.Data.InstrumentationKey,
                })
                .ToListAsync()
                .ConfigureAwait(false);

            return appInsightsResources;
        }
        catch (Exception ex) when (ex is not ArgumentNullException)
        {
            throw new Exception($"Error retrieving Application Insights resources: {ex.Message}", ex);
        }
    }    // Helper methods for parsing query results
    private static string GetStringValue(Dictionary<string, object> row, string columnName, string defaultValue)
    {
        if (row.TryGetValue(columnName, out object? value) && value != null)
        {
            return value.ToString() ?? defaultValue;
        }
        return defaultValue;
    }

    private static bool TryGetDateTime(Dictionary<string, object> row, string columnName, out DateTime result)
    {
        result = DateTime.MinValue;
        
        if (!row.TryGetValue(columnName, out object? value) || value == null)
        {
            return false;
        }

        if (value is DateTime dateTime)
        {
            result = dateTime;
            return true;
        }

        return DateTime.TryParse(value.ToString(), out result);
    }

    private static bool TryGetDouble(Dictionary<string, object> row, string columnName, out double result)
    {
        result = 0;

        if (!row.TryGetValue(columnName, out object? value) || value == null)
        {
            return false;
        }

        if (value is double doubleVal)
        {
            result = doubleVal;
            return true;
        }

        return double.TryParse(value.ToString(), out result);
    }

    private static bool IsStandardField(string columnName)
    {
        return _standardFields.Values.Contains(columnName, StringComparer.OrdinalIgnoreCase);
    }
}
