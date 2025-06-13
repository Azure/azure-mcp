// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Nodes;
using AzureMcp.Models.Monitor.ApplicationInsights;
using AzureMcp.Options;

namespace AzureMcp.Services.Interfaces;

public interface IApplicationInsightsService
{
    /// <summary>
    /// Diagnoses application issues by analyzing telemetry data
    /// </summary>
    /// <param name="mcpServer">MCP server instance (if present), used for sampling</param>
    /// <param name="subscription">The current subscription</param>
    /// <param name="appId">The Application Insights identifier (instrumentation key, app ID, or resource name)</param>
    /// <param name="startTime">The optional start time in ISO format</param>
    /// <param name="endTime">The optional end time in ISO format</param>
    /// <param name="symptoms">Description of the symptoms to diagnose</param>
    /// <param name="retryPolicy">Optional retry policy parameters for the request</param>
    /// <param name="tenant">Optional tenant ID for multi-tenant scenarios</param>
    /// <returns>A list of diagnostic results with trace and span information</returns>
    Task<List<JsonNode>> DiagnoseApplicationAsync(
        IMcpServer? mcpServer,
        string subscription,
        string appId,
        string symptoms,
        string? startTime = null,
        string? endTime = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);

    /// <summary>
    /// Gets a distributed trace by trace ID and span ID
    /// </summary>
    /// <param name="tenant">Optional tenant ID for multi-tenant scenarios</param>
    /// <param name="retryPolicy">Optional retry policy parameters for the request</param>
    /// <param name="subscription"> The current subscription</param>
    /// <param name="appId">The Application Insights resource ID</param>
    /// <param name="traceId">The trace ID to retrieve</param>
    /// <param name="spanId">The span ID to retrieve</param>
    /// <param name="startTime">The optional start time in ISO format</param>
    /// <param name="endTime">The optional end time in ISO format</param>
    /// <returns>The trace details object</returns>
    Task<DistributedTraceResult> GetDistributedTraceAsync(
        string subscription,
        string appId,
        string traceId,
        string spanId,
        string? startTime = null,
        string? endTime = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);
}
