// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.ApplicationInsights.Models;
using AzureMcp.Core.Options;

namespace AzureMcp.ApplicationInsights.Services
{
    public interface IAppDiagnoseService
    {
        Task<AppCorrelateTimeResult[]> CorrelateTimeSeries(string subscription,
            string? resourceGroup,
            string? resourceName,
            string? resourceId,
            List<AppCorrelateDataSet> dataSets,
            DateTime startTime,
            DateTime endTime,
            string? tenant = null,
            RetryPolicyOptions? retryPolicy = null);

        Task<DistributedTraceResult> GetDistributedTrace(
            string subscription,
            string? resourceGroup,
            string? resourceName,
            string? resourceId,
            string traceId,
            string? spanId,
           DateTime startTime,
           DateTime endTime,
           string? tenant = null,
           RetryPolicyOptions? retryPolicy = null);

        Task<AppListTraceResult> ListDistributedTraces(
            string subscription,
            string? resourceGroup,
            string? resourceName,
            string? resourceId,
            string[] filters,
            string table,
           DateTime startTime,
           DateTime endTime,
           string? tenant = null,
           RetryPolicyOptions? retryPolicy = null);

        Task<List<AppImpactResult>> GetImpact(
            string subscription,
            string? resourceGroup,
            string? resourceName,
            string? resourceId,
            string[] filters,
            string table,
           DateTime startTime,
           DateTime endTime,
           string? tenant = null,
           RetryPolicyOptions? retryPolicy = null);
    }
}
