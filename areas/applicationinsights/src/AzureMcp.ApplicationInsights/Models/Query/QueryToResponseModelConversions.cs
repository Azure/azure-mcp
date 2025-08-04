// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.ApplicationInsights.Options;

namespace AzureMcp.ApplicationInsights.Models.Query;

public static class QueryToResponseModelConversions
{
    public static AppListTraceEntry ToResponseModel(this AppLogsQueryRow<ListTraceQueryResponse> row)
    {
        return new AppListTraceEntry
        {
            ProblemId = row.Data.problemId,
            Target = row.Data.target,
            TestLocation = row.Data.location,
            TestName = row.Data.name,
            Type = row.Data.type,
            OperationName = row.Data.operation_Name,
            ResultCode = row.Data.resultCode,
            Traces = string.IsNullOrEmpty(row.Data.traces) ? new List<TraceIdEntry>() : (JsonSerializer.Deserialize(row.Data.traces!, ApplicationInsightsJsonContext.Default.ListTraceIdEntry) ?? new List<TraceIdEntry>()).Distinct().ToList()
        };
    }

    public static SpanSummary ToResponseModel(this AppLogsQueryRow<DistributedTraceQueryResponse> row)
    {
        DateTime? end = row.Data.timestamp?.UtcDateTime;

        string? target = row.Data.target;
        string? problemId = row.Data.problemId;
        string? operationName = row.Data.operation_Name;
        string? name = row.Data.name;
        TimeSpan? duration = TimeSpan.FromMilliseconds(row.Data.duration ?? 0);

        var spanDetails = new SpanSummary
        {
            SpanId = row.Data.id,
            ParentId = row.Data.operation_ParentId,
            ItemId = row.Data.itemId,
            ResponseCode = row.Data.resultCode,
            ItemType = row.Data.itemType,
            IsSuccessful = !string.IsNullOrEmpty(row.Data.success) ? bool.Parse(row.Data.success!) : null,
            ChildSpans = new List<SpanSummary>(),
            Properties = row.OtherColumns.Where(t => t.Key != null && t.Value != null).Select(t => new KeyValuePair<string, string>(t.Key, t.Value?.ToString()!)).ToList(),
            Name = target ?? problemId ?? operationName ?? name,
            Duration = duration,
            StartTime = end.HasValue ? end.Value.Subtract(duration ?? TimeSpan.Zero) : default,
            EndTime = end ?? default,
        };

        return spanDetails;
    }

    public static AppImpactResult ToResponseModel(this AppLogsQueryRow<ImpactQueryResponse> row)
    {
        return new AppImpactResult
        {
            CloudRoleName = row.Data.cloud_RoleName ?? "Unknown",
            ImpactedInstances = (int)row.Data.ImpactedInstances,
            TotalInstances = (int)row.Data.TotalInstances,
            ImpactedCount = (int)row.Data.ImpactedRequests,
            TotalCount = (int)row.Data.TotalRequests,
            ImpactedCountPercent = row.Data.ImpactedRequestsPercent,
            ImpactedInstancePercent = row.Data.ImpactedInstancePercent
        };
    }

    public static AppCorrelateTimeSeries ToResponseModel(this AppLogsQueryRow<TimeSeriesCorrelationResponse> row)
    {
        string split = row.Data.split ?? "Unknown";
        double[]? value = row.Data.Value != null ? JsonSerializer.Deserialize(row.Data.Value?.ToString()!, ApplicationInsightsJsonContext.Default.DoubleArray) : Array.Empty<double>();

        return new AppCorrelateTimeSeries
        {
            Data = value ?? Array.Empty<double>(),
            Label = split
        };
    }
}
