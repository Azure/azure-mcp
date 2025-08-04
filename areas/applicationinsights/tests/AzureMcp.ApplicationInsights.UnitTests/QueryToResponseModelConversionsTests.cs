// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using Xunit;
using AzureMcp.ApplicationInsights.Models;
using AzureMcp.ApplicationInsights.Models.Query;
using AzureMcp.ApplicationInsights.Options;

namespace AzureMcp.ApplicationInsights.UnitTests;

[Trait("Area", "ApplicationInsights")]
public class QueryToResponseModelConversionsTests
{
    #region ListTraceQueryResponse → AppListTraceEntry Tests

    [Fact]
    public void ToResponseModel_ListTraceQueryResponse_WithValidData_MapsAllPropertiesCorrectly()
    {
        // Arrange
        var tracesJson = JsonSerializer.Serialize(new List<TraceIdEntry>
        {
            new() { TraceId = "trace1", SpanId = "span1" },
            new() { TraceId = "trace2", SpanId = "span2" }
        });

        var queryResponse = new ListTraceQueryResponse
        {
            problemId = "problem123",
            target = "target123",
            location = "East US",
            name = "test-operation",
            type = "request",
            operation_Name = "GET /api/test",
            resultCode = "200",
            traces = tracesJson
        };

        var row = new AppLogsQueryRow<ListTraceQueryResponse>
        {
            Data = queryResponse
        };

        // Act
        var result = row.ToResponseModel();

        // Assert
        Assert.Equal("problem123", result.ProblemId);
        Assert.Equal("target123", result.Target);
        Assert.Equal("East US", result.TestLocation);
        Assert.Equal("test-operation", result.TestName);
        Assert.Equal("request", result.Type);
        Assert.Equal("GET /api/test", result.OperationName);
        Assert.Equal("200", result.ResultCode);
        Assert.Equal(2, result.Traces.Count);
        Assert.Contains(result.Traces, t => t.TraceId == "trace1" && t.SpanId == "span1");
        Assert.Contains(result.Traces, t => t.TraceId == "trace2" && t.SpanId == "span2");
    }

    [Fact]
    public void ToResponseModel_ListTraceQueryResponse_WithNullTraces_ReturnsEmptyTracesList()
    {
        // Arrange
        var queryResponse = new ListTraceQueryResponse
        {
            problemId = "problem123",
            traces = null
        };

        var row = new AppLogsQueryRow<ListTraceQueryResponse>
        {
            Data = queryResponse
        };

        // Act
        var result = row.ToResponseModel();

        // Assert
        Assert.Empty(result.Traces);
    }

    [Fact]
    public void ToResponseModel_ListTraceQueryResponse_WithEmptyTracesJson_ReturnsEmptyTracesList()
    {
        // Arrange
        var queryResponse = new ListTraceQueryResponse
        {
            problemId = "problem123",
            traces = "[]"
        };

        var row = new AppLogsQueryRow<ListTraceQueryResponse>
        {
            Data = queryResponse
        };

        // Act
        var result = row.ToResponseModel();

        // Assert
        Assert.Empty(result.Traces);
    }

    [Fact]
    public void ToResponseModel_ListTraceQueryResponse_WithDuplicateTraces_RemovesDuplicates()
    {
        // Arrange
        var tracesJson = JsonSerializer.Serialize(new List<TraceIdEntry>
        {
            new() { TraceId = "trace1", SpanId = "span1" },
            new() { TraceId = "trace1", SpanId = "span1" }, // Duplicate
            new() { TraceId = "trace2", SpanId = "span2" }
        });

        var queryResponse = new ListTraceQueryResponse
        {
            problemId = "problem123",
            traces = tracesJson
        };

        var row = new AppLogsQueryRow<ListTraceQueryResponse>
        {
            Data = queryResponse
        };

        // Act
        var result = row.ToResponseModel();

        // Assert
        Assert.Equal(2, result.Traces.Count);
        Assert.Contains(result.Traces, t => t.TraceId == "trace1" && t.SpanId == "span1");
        Assert.Contains(result.Traces, t => t.TraceId == "trace2" && t.SpanId == "span2");
    }

    [Fact]
    public void ToResponseModel_ListTraceQueryResponse_WithNullProperties_HandlesNullsGracefully()
    {
        // Arrange
        var queryResponse = new ListTraceQueryResponse
        {
            problemId = null,
            target = null,
            location = null,
            name = null,
            type = null,
            operation_Name = null,
            resultCode = null,
            traces = "[]"
        };

        var row = new AppLogsQueryRow<ListTraceQueryResponse>
        {
            Data = queryResponse
        };

        // Act
        var result = row.ToResponseModel();

        // Assert
        Assert.Null(result.ProblemId);
        Assert.Null(result.Target);
        Assert.Null(result.TestLocation);
        Assert.Null(result.TestName);
        Assert.Null(result.Type);
        Assert.Null(result.OperationName);
        Assert.Null(result.ResultCode);
        Assert.Empty(result.Traces);
    }

    #endregion

    #region DistributedTraceQueryResponse → SpanSummary Tests

    [Fact]
    public void ToResponseModel_DistributedTraceQueryResponse_WithCompleteData_MapsAllPropertiesCorrectly()
    {
        // Arrange
        var timestamp = new DateTimeOffset(2024, 1, 15, 10, 30, 0, TimeSpan.Zero);
        var duration = 1500.0; // 1.5 seconds in milliseconds

        var queryResponse = new DistributedTraceQueryResponse
        {
            id = "span123",
            operation_ParentId = "parent456",
            operation_Name = "GET /api/users",
            resultCode = "200",
            itemType = "request",
            success = "true",
            timestamp = timestamp,
            duration = duration,
            operation_Id = "operation789",
            itemId = "item001",
            problemId = "problem123",
            type = "HTTP",
            name = "GetUsers",
            target = "api.example.com"
        };

        var otherColumns = new Dictionary<string, object?>
        {
            { "customProperty1", "value1" },
            { "customProperty2", 42 },
            { "customProperty3", null }
        };

        var row = new AppLogsQueryRow<DistributedTraceQueryResponse>
        {
            Data = queryResponse,
            OtherColumns = otherColumns
        };

        // Act
        var result = row.ToResponseModel();

        // Assert
        Assert.Equal("span123", result.SpanId);
        Assert.Equal("parent456", result.ParentId);
        Assert.Equal("item001", result.ItemId);
        Assert.Equal("200", result.ResponseCode);
        Assert.Equal("request", result.ItemType);
        Assert.True(result.IsSuccessful);
        Assert.Equal(TimeSpan.FromMilliseconds(1500), result.Duration);
        Assert.Equal(timestamp.UtcDateTime.Subtract(TimeSpan.FromMilliseconds(1500)), result.StartTime);
        Assert.Equal(timestamp.UtcDateTime, result.EndTime);
        Assert.Equal("api.example.com", result.Name); // target takes priority
        Assert.Empty(result.ChildSpans);
        
        // Check properties mapping
        Assert.Equal(2, result.Properties.Count);
        Assert.Contains(result.Properties, p => p.Key == "customProperty1" && p.Value == "value1");
        Assert.Contains(result.Properties, p => p.Key == "customProperty2" && p.Value == "42");
    }

    [Fact]
    public void ToResponseModel_DistributedTraceQueryResponse_WithNamePriority_UsesCorrectNameOrder()
    {
        // Test target priority
        var queryResponse1 = new DistributedTraceQueryResponse
        {
            target = "target-name",
            problemId = "problem-name",
            operation_Name = "operation-name",
            name = "basic-name"
        };

        var row1 = new AppLogsQueryRow<DistributedTraceQueryResponse> { Data = queryResponse1 };
        var result1 = row1.ToResponseModel();
        Assert.Equal("target-name", result1.Name);

        // Test problemId priority when target is null
        var queryResponse2 = new DistributedTraceQueryResponse
        {
            target = null,
            problemId = "problem-name",
            operation_Name = "operation-name",
            name = "basic-name"
        };

        var row2 = new AppLogsQueryRow<DistributedTraceQueryResponse> { Data = queryResponse2 };
        var result2 = row2.ToResponseModel();
        Assert.Equal("problem-name", result2.Name);

        // Test operation_Name priority when target and problemId are null
        var queryResponse3 = new DistributedTraceQueryResponse
        {
            target = null,
            problemId = null,
            operation_Name = "operation-name",
            name = "basic-name"
        };

        var row3 = new AppLogsQueryRow<DistributedTraceQueryResponse> { Data = queryResponse3 };
        var result3 = row3.ToResponseModel();
        Assert.Equal("operation-name", result3.Name);

        // Test name fallback when others are null
        var queryResponse4 = new DistributedTraceQueryResponse
        {
            target = null,
            problemId = null,
            operation_Name = null,
            name = "basic-name"
        };

        var row4 = new AppLogsQueryRow<DistributedTraceQueryResponse> { Data = queryResponse4 };
        var result4 = row4.ToResponseModel();
        Assert.Equal("basic-name", result4.Name);
    }

    [Fact]
    public void ToResponseModel_DistributedTraceQueryResponse_WithNullTimestamp_UsesDefaultDateTime()
    {
        // Arrange
        var queryResponse = new DistributedTraceQueryResponse
        {
            id = "span123",
            timestamp = null,
            duration = 1000.0
        };

        var row = new AppLogsQueryRow<DistributedTraceQueryResponse>
        {
            Data = queryResponse
        };

        // Act
        var result = row.ToResponseModel();

        // Assert
        Assert.Equal(DateTime.MinValue, result.EndTime);
        Assert.Equal(DateTime.MinValue, result.StartTime);
    }

    [Fact]
    public void ToResponseModel_DistributedTraceQueryResponse_WithNullDuration_UsesZeroDuration()
    {
        // Arrange
        var timestamp = new DateTimeOffset(2024, 1, 15, 10, 30, 0, TimeSpan.Zero);
        var queryResponse = new DistributedTraceQueryResponse
        {
            id = "span123",
            timestamp = timestamp,
            duration = null
        };

        var row = new AppLogsQueryRow<DistributedTraceQueryResponse>
        {
            Data = queryResponse
        };

        // Act
        var result = row.ToResponseModel();

        // Assert
        Assert.Equal(TimeSpan.Zero, result.Duration);
        Assert.Equal(timestamp.UtcDateTime, result.StartTime);
        Assert.Equal(timestamp.UtcDateTime, result.EndTime);
    }

    [Fact]
    public void ToResponseModel_DistributedTraceQueryResponse_WithSuccessString_ParsesCorrectly()
    {
        // Test "true"
        var queryResponse1 = new DistributedTraceQueryResponse { success = "true" };
        var row1 = new AppLogsQueryRow<DistributedTraceQueryResponse> { Data = queryResponse1 };
        var result1 = row1.ToResponseModel();
        Assert.True(result1.IsSuccessful);

        // Test "false"
        var queryResponse2 = new DistributedTraceQueryResponse { success = "false" };
        var row2 = new AppLogsQueryRow<DistributedTraceQueryResponse> { Data = queryResponse2 };
        var result2 = row2.ToResponseModel();
        Assert.False(result2.IsSuccessful);

        // Test null or empty
        var queryResponse3 = new DistributedTraceQueryResponse { success = null };
        var row3 = new AppLogsQueryRow<DistributedTraceQueryResponse> { Data = queryResponse3 };
        var result3 = row3.ToResponseModel();
        Assert.Null(result3.IsSuccessful);

        var queryResponse4 = new DistributedTraceQueryResponse { success = "" };
        var row4 = new AppLogsQueryRow<DistributedTraceQueryResponse> { Data = queryResponse4 };
        var result4 = row4.ToResponseModel();
        Assert.Null(result4.IsSuccessful);
    }

    #endregion

    #region ImpactQueryResponse → AppImpactResult Tests

    [Fact]
    public void ToResponseModel_ImpactQueryResponse_WithValidData_MapsAllPropertiesCorrectly()
    {
        // Arrange
        var queryResponse = new ImpactQueryResponse
        {
            cloud_RoleName = "web-app",
            ImpactedInstances = 5,
            TotalInstances = 10,
            ImpactedRequests = 150,
            TotalRequests = 1000,
            ImpactedRequestsPercent = 15.0,
            ImpactedInstancePercent = 50.0
        };

        var row = new AppLogsQueryRow<ImpactQueryResponse>
        {
            Data = queryResponse
        };

        // Act
        var result = row.ToResponseModel();

        // Assert
        Assert.Equal("web-app", result.CloudRoleName);
        Assert.Equal(5, result.ImpactedInstances);
        Assert.Equal(10, result.TotalInstances);
        Assert.Equal(150, result.ImpactedCount);
        Assert.Equal(1000, result.TotalCount);
        Assert.Equal(15.0, result.ImpactedCountPercent);
        Assert.Equal(50.0, result.ImpactedInstancePercent);
    }

    [Fact]
    public void ToResponseModel_ImpactQueryResponse_WithNullCloudRoleName_DefaultsToUnknown()
    {
        // Arrange
        var queryResponse = new ImpactQueryResponse
        {
            cloud_RoleName = null,
            ImpactedInstances = 0,
            TotalInstances = 0,
            ImpactedRequests = 0,
            TotalRequests = 0,
            ImpactedRequestsPercent = 0.0,
            ImpactedInstancePercent = 0.0
        };

        var row = new AppLogsQueryRow<ImpactQueryResponse>
        {
            Data = queryResponse
        };

        // Act
        var result = row.ToResponseModel();

        // Assert
        Assert.Equal("Unknown", result.CloudRoleName);
    }

    [Fact]
    public void ToResponseModel_ImpactQueryResponse_WithDefaultValues_MapsCorrectly()
    {
        // Arrange
        var queryResponse = new ImpactQueryResponse(); // Uses default values from class

        var row = new AppLogsQueryRow<ImpactQueryResponse>
        {
            Data = queryResponse
        };

        // Act
        var result = row.ToResponseModel();

        // Assert
        Assert.Equal("Unknown", result.CloudRoleName);
        Assert.Equal(0, result.ImpactedInstances);
        Assert.Equal(0, result.TotalInstances);
        Assert.Equal(0, result.ImpactedCount);
        Assert.Equal(0, result.TotalCount);
        Assert.Equal(0.0, result.ImpactedCountPercent);
        Assert.Equal(0.0, result.ImpactedInstancePercent);
    }

    #endregion

    #region TimeSeriesCorrelationResponse → AppCorrelateTimeSeries Tests

    [Fact]
    public void ToResponseModel_TimeSeriesCorrelationResponse_WithValidData_MapsCorrectly()
    {
        // Arrange
        var timeSeriesData = new double[] { 1.5, 2.3, 4.7, 3.1, 2.8 };
        var valueJson = JsonSerializer.Serialize(timeSeriesData);

        var queryResponse = new TimeSeriesCorrelationResponse
        {
            split = "ErrorCode=500",
            Value = valueJson
        };

        var row = new AppLogsQueryRow<TimeSeriesCorrelationResponse>
        {
            Data = queryResponse
        };

        // Act
        var result = row.ToResponseModel();

        // Assert
        Assert.Equal("ErrorCode=500", result.Label);
        Assert.Equal(5, result.Data.Length);
        Assert.Equal(1.5, result.Data[0]);
        Assert.Equal(2.3, result.Data[1]);
        Assert.Equal(4.7, result.Data[2]);
        Assert.Equal(3.1, result.Data[3]);
        Assert.Equal(2.8, result.Data[4]);
    }

    [Fact]
    public void ToResponseModel_TimeSeriesCorrelationResponse_WithNullSplit_DefaultsToUnknown()
    {
        // Arrange
        var timeSeriesData = new double[] { 1.0, 2.0 };
        var valueJson = JsonSerializer.Serialize(timeSeriesData);

        var queryResponse = new TimeSeriesCorrelationResponse
        {
            split = null,
            Value = valueJson
        };

        var row = new AppLogsQueryRow<TimeSeriesCorrelationResponse>
        {
            Data = queryResponse
        };

        // Act
        var result = row.ToResponseModel();

        // Assert
        Assert.Equal("Unknown", result.Label);
        Assert.Equal(2, result.Data.Length);
    }

    [Fact]
    public void ToResponseModel_TimeSeriesCorrelationResponse_WithNullValue_ReturnsEmptyArray()
    {
        // Arrange
        var queryResponse = new TimeSeriesCorrelationResponse
        {
            split = "test-split",
            Value = null
        };

        var row = new AppLogsQueryRow<TimeSeriesCorrelationResponse>
        {
            Data = queryResponse
        };

        // Act
        var result = row.ToResponseModel();

        // Assert
        Assert.Equal("test-split", result.Label);
        Assert.Empty(result.Data);
    }

    [Fact]
    public void ToResponseModel_TimeSeriesCorrelationResponse_WithEmptyArray_ReturnsEmptyArray()
    {
        // Arrange
        var valueJson = JsonSerializer.Serialize(new double[0]);

        var queryResponse = new TimeSeriesCorrelationResponse
        {
            split = "test-split",
            Value = valueJson
        };

        var row = new AppLogsQueryRow<TimeSeriesCorrelationResponse>
        {
            Data = queryResponse
        };

        // Act
        var result = row.ToResponseModel();

        // Assert
        Assert.Equal("test-split", result.Label);
        Assert.Empty(result.Data);
    }

    #endregion
}
