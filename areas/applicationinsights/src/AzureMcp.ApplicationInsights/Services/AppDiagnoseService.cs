// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Data;
using Azure.Core;
using Azure.Monitor.Query;
using AzureMcp.ApplicationInsights.Models;
using AzureMcp.ApplicationInsights.Models.Query;
using AzureMcp.ApplicationInsights.Options;
using AzureMcp.Core.Options;
using AzureMcp.Core.Services.Azure;
using AzureMcp.Core.Services.Azure.Resource;

namespace AzureMcp.ApplicationInsights.Services
{
    public class AppDiagnoseService(IResourceResolverService resourceResolverService, IAppLogsQueryService appLogsQueryService) : BaseAzureService, IAppDiagnoseService
    {
        private readonly IResourceResolverService _resourceResolverService = resourceResolverService;
        private readonly IAppLogsQueryService _queryService = appLogsQueryService;

        public async Task<DistributedTraceResult> GetDistributedTrace(string subscription, string? resourceGroup, string? resourceName, string? resourceId, string traceId, string? spanId, DateTime startTime, DateTime endTime, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
        {
            ResourceIdentifier resolvedResource = await _resourceResolverService.ResolveResourceIdAsync(subscription, resourceGroup, "microsoft.insights/components", resourceName ?? resourceId!, tenant, retryPolicy);

            var client = await _queryService.CreateClientAsync(resolvedResource, tenant, retryPolicy);

            QueryTimeRange queryTimeRange = new QueryTimeRange(startTime, endTime);

            var kql = KQLQueryBuilder.GetDistributedTrace(traceId);

            var response = await client.QueryResourceAsync<DistributedTraceQueryResponse>(resolvedResource, kql, queryTimeRange);

            DistributedTraceGraphBuilder graphBuilder = new DistributedTraceGraphBuilder(traceId);

            List<SpanSummary> spans = new List<SpanSummary>();

            if (response != null)
            {
                DistributedTraceGraph graph = graphBuilder.AddSpans(response.Select(t => t.ToResponseModel())).Build();

                spans = graph.FilterSpansById(spanId);
            }

            return DistributedTraceResult.Create(traceId, spans);
        }

        public async Task<AppListTraceResult> ListDistributedTraces(string subscription, string? resourceGroup, string? resourceName, string? resourceId, string[] filters, string table, DateTime startTime, DateTime endTime, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
        {
            ResourceIdentifier resolvedResource = await _resourceResolverService.ResolveResourceIdAsync(subscription, resourceGroup, "microsoft.insights/components", resourceName ?? resourceId!, tenant, retryPolicy);

            var client = await _queryService.CreateClientAsync(resolvedResource, tenant, retryPolicy);

            QueryTimeRange queryTimeRange = new QueryTimeRange(startTime, endTime);

            var query = KQLQueryBuilder.ListTraces(table, filters);

            var response = await client.QueryResourceAsync<ListTraceQueryResponse>(resolvedResource, query, queryTimeRange);

            if (response == null || response.Count == 0)
            {
                return new AppListTraceResult
                {
                    Table = table,
                    Rows = new List<AppListTraceEntry>(),
                };
            }

            List<AppListTraceEntry> rows = response.Select(t => t.ToResponseModel()).ToList();

            return new AppListTraceResult
            {
                Table = table,
                Rows = rows
            };
        }

        public async Task<AppCorrelateTimeResult[]> CorrelateTimeSeries(string subscription, string? resourceGroup, string? resourceName, string? resourceId, List<AppCorrelateDataSet> dataSets, DateTime startTime, DateTime endTime, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
        {
            ResourceIdentifier resolvedResource = await _resourceResolverService.ResolveResourceIdAsync(subscription, resourceGroup, "microsoft.insights/components", resourceName ?? resourceId!, tenant, retryPolicy);

            var client = await _queryService.CreateClientAsync(resolvedResource, tenant, retryPolicy);

            // Convert the data sets into actual KQL queries...
            QueryTimeRange queryTimeRange = new QueryTimeRange(startTime, endTime);

            string interval = KQLQueryBuilder.GetKqlInterval(startTime, endTime);

            (string query, string description)[] queries = dataSets.Select(dataSet => KQLQueryBuilder.BuildTimeSeriesQuery(dataSet, interval, startTime, endTime)).ToArray();

            var result = await Task.WhenAll(queries.Select(q => ExecuteTimeSeriesQuery(resolvedResource, client, queryTimeRange, q.query, q.description, interval)));

            return result;
        }

        public async Task<List<AppImpactResult>> GetImpact(string subscription, string? resourceGroup, string? resourceName, string? resourceId, string[] filters, string table, DateTime startTime, DateTime endTime, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
        {
            ResourceIdentifier resolvedResource = await _resourceResolverService.ResolveResourceIdAsync(subscription, resourceGroup, "microsoft.insights/components", resourceName ?? resourceId!, tenant, retryPolicy);

            var client = await _queryService.CreateClientAsync(resolvedResource, tenant, retryPolicy);

            QueryTimeRange queryTimeRange = new QueryTimeRange(startTime, endTime);

            var query = KQLQueryBuilder.GetImpact(table, filters);

            var response = await client.QueryResourceAsync<ImpactQueryResponse>(resolvedResource, query, queryTimeRange);

            if (response == null || response.Count == 0)
            {
                return new List<AppImpactResult>();
            }

            List<AppImpactResult> results = response.Select(t => t.ToResponseModel()).ToList();

            return results;
        }

        private static async Task<AppCorrelateTimeResult> ExecuteTimeSeriesQuery(ResourceIdentifier resourceId, IAppLogsQueryClient client, QueryTimeRange timeRange, string query, string description, string interval)
        {
            var response = await client.QueryResourceAsync<TimeSeriesCorrelationResponse>(
                resourceId,
                query,
                timeRange);

            return new AppCorrelateTimeResult
            {
                TimeSeries = response.Select(t => t.ToResponseModel()).ToList(),
                Description = description,
                Start = timeRange.Start?.UtcDateTime ?? DateTime.MinValue,
                End = timeRange.End?.UtcDateTime ?? DateTime.MinValue,
                Interval = interval
            };
        }
    }
}
