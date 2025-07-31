
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.Monitor.Query;
using AzureMcp.ApplicationInsights.Models;
using AzureMcp.ApplicationInsights.Models.Query;
using AzureMcp.ApplicationInsights.Services;
using AzureMcp.Core.Services.Azure.Resource;
using NSubstitute;
using Xunit;

namespace AzureMcp.ApplicationInsights.UnitTests.Services;

[Trait("Area", "ApplicationInsights")]
public class AppDiagnoseServiceTests
{
    private readonly IResourceResolverService _resourceResolverService = Substitute.For<IResourceResolverService>();
    private readonly IAppLogsQueryService _appLogsQueryService = Substitute.For<IAppLogsQueryService>();

    private readonly IAppDiagnoseService _sut;

    private readonly string _testSubscription = "test-subscription";
    private readonly string _testResourceGroup = "test-resource-group";
    private readonly string _testResourceName = "test-resource-name";

    private readonly ResourceIdentifier _testResourceId;

    private readonly IAppLogsQueryClient _appLogsQueryClient = Substitute.For<IAppLogsQueryClient>();

    public AppDiagnoseServiceTests()
    {
        _testResourceId = new ResourceIdentifier($"/subscriptions/{_testSubscription}/resourceGroups/{_testResourceGroup}/providers/Microsoft.Insights/components/{_testResourceName}");

        _resourceResolverService.ResolveResourceIdAsync(_testSubscription, _testResourceGroup, "microsoft.insights/components", _testResourceName, null, null)
            .Returns(_testResourceId);

        _appLogsQueryService.CreateClientAsync(_testResourceId, null, null)
            .Returns(_appLogsQueryClient);

        _sut = new AppDiagnoseService(_resourceResolverService, _appLogsQueryService);
    }

    [Fact]
    public async Task GetDistributedTrace_ReturnsDistributedTraceResult()
    {
        _appLogsQueryClient.QueryResourceAsync<DistributedTraceQueryResponse>(
            _testResourceId, Arg.Any<string>(), Arg.Any<QueryTimeRange>()
        ).Returns(new List<AppLogsQueryRow<DistributedTraceQueryResponse>>
        {
            new AppLogsQueryRow<DistributedTraceQueryResponse>
            {
                Data  = new DistributedTraceQueryResponse
                {
                    itemId = "span1ItemId",
                    itemType = "request",
                    operation_Name = "GET /api/values",
                    success = "True",
                    resultCode = "200",
                    timestamp = DateTime.Parse("2025-05-20T00:00:00Z"),
                    duration = 60,
                    id = "span1",
                    operation_ParentId = null
                },
                OtherColumns = new Dictionary<string, object?>
                {
                    { "http.method", "GET" },
                    { "http.url", "/api/values" }
                }
            },
            new AppLogsQueryRow<DistributedTraceQueryResponse>
            {
                Data  = new DistributedTraceQueryResponse
                {
                    itemId = "span3ItemId",
                    itemType = "exception",
                    problemId = "GETNullReferenceException",
                    timestamp = DateTime.Parse("2025-05-20T00:00:00Z"),
                    id = "span3",
                    operation_ParentId = "span1"
                },
                OtherColumns = new Dictionary<string, object?>
                {
                    { "exception.message", "Object reference not set to an instance of an object" }
                }
            }
        });

        var result = await _sut.GetDistributedTrace(
            _testSubscription,
            _testResourceGroup,
            _testResourceName,
            null,
            "test-trace-id",
            null,
            DateTime.UtcNow.AddHours(-1),
            DateTime.UtcNow,
            null,
            null
        );

        Assert.Equal("This represents a distributed trace. Parent/child relationships are represented by indentation. Columns: ItemId, ItemType, Name, Success, ResultCode, StartToEnd (milliseconds)", result.Description);
        Assert.Equal("""
            span1ItemId, request, GET /api/values, ✅, 200, 0->60
                span3ItemId, exception, GETNullReferenceException, ⚠️, , 60->60

            """, result.TraceDetails);
        Assert.Single(result.RelevantSpans);
        Assert.Equal("test-trace-id", result.TraceId);
    }

    [Fact]
    public async Task ListDistributedTraces_ReturnsEmpty()
    {
        _appLogsQueryClient.QueryResourceAsync<ListTraceQueryResponse>(
            _testResourceId, Arg.Any<string>(), Arg.Any<QueryTimeRange>()
        ).Returns(new List<AppLogsQueryRow<ListTraceQueryResponse>>());

        var result = await _sut.ListDistributedTraces(
            _testSubscription,
            _testResourceGroup,
            _testResourceName,
            null,
            new string[] { "success='False'"},
            "requests",
            DateTime.UtcNow.AddHours(-1),
            DateTime.UtcNow,
            null,
            null
        );

        Assert.Empty(result.Rows);
        Assert.Equal("requests", result.Table);
    }

    [Fact]
    public async Task ListDistributedTraces_ReturnsResults()
    {
        _appLogsQueryClient.QueryResourceAsync<ListTraceQueryResponse>(
            _testResourceId, Arg.Any<string>(), Arg.Any<QueryTimeRange>()
        ).Returns(new List<AppLogsQueryRow<ListTraceQueryResponse>>
        {
            new AppLogsQueryRow<ListTraceQueryResponse>
            {
                Data = new ListTraceQueryResponse
                {
                    name = "GET /api/values",
                    operation_Name = "GET /api/values",
                    resultCode = "200",
                    type = "requests"
                }
            }
        });

        var result = await _sut.ListDistributedTraces(
            _testSubscription,
            _testResourceGroup,
            _testResourceName,
            null,
            new string[] { "success='False'" },
            "requests",
            DateTime.UtcNow.AddHours(-1),
            DateTime.UtcNow,
            null,
            null
        );

        Assert.Single(result.Rows);
        Assert.Contains(result.Rows, r => r.OperationName == "GET /api/values" && r.ResultCode == "200");
        Assert.Equal("requests", result.Table);
    }

    [Fact]
    public async Task CorrelateTimeSeries_ReturnsEmpty()
    {
        _appLogsQueryClient.QueryResourceAsync<TimeSeriesCorrelationResponse>(
            _testResourceId, Arg.Any<string>(), Arg.Any<QueryTimeRange>()
        ).Returns(new List<AppLogsQueryRow<TimeSeriesCorrelationResponse>>());

        var dataSets = new List<AppCorrelateDataSet>
        {
            new AppCorrelateDataSet
            {
                Table = "requests",
                Filters = new[] { "success=\"false\"" },
                SplitBy = "resultCode",
                Aggregation = "Count"
            }
        };

        var result = await _sut.CorrelateTimeSeries(
            _testSubscription,
            _testResourceGroup,
            _testResourceName,
            null,
            dataSets,
            DateTime.UtcNow.AddHours(-1),
            DateTime.UtcNow,
            null,
            null
        );

        Assert.Single(result);
        Assert.Empty(result[0].TimeSeries);
        Assert.NotEmpty(result[0].Description);
        Assert.NotEmpty(result[0].Interval);
    }

    [Fact]
    public async Task CorrelateTimeSeries_ReturnsResults()
    {
        _appLogsQueryClient.QueryResourceAsync<TimeSeriesCorrelationResponse>(
            _testResourceId, Arg.Any<string>(), Arg.Any<QueryTimeRange>()
        ).Returns(new List<AppLogsQueryRow<TimeSeriesCorrelationResponse>>
        {
            new AppLogsQueryRow<TimeSeriesCorrelationResponse>
            {
                Data = new TimeSeriesCorrelationResponse
                {
                    split = "500",
                    Value = "[1.0, 2.0, 3.0]"
                }
            },
            new AppLogsQueryRow<TimeSeriesCorrelationResponse>
            {
                Data = new TimeSeriesCorrelationResponse
                {
                    split = "404",
                    Value = "[0.5, 1.5, 2.5]"
                }
            }
        });

        var dataSets = new List<AppCorrelateDataSet>
        {
            new AppCorrelateDataSet
            {
                Table = "requests",
                Filters = new[] { "success=\"false\"" },
                SplitBy = "resultCode",
                Aggregation = "Count"
            }
        };

        var result = await _sut.CorrelateTimeSeries(
            _testSubscription,
            _testResourceGroup,
            _testResourceName,
            null,
            dataSets,
            DateTime.UtcNow.AddHours(-1),
            DateTime.UtcNow,
            null,
            null
        );

        Assert.Single(result);
        Assert.Equal(2, result[0].TimeSeries.Count);
        Assert.Contains(result[0].TimeSeries, ts => ts.Label == "500" && ts.Data.SequenceEqual(new[] { 1.0, 2.0, 3.0 }));
        Assert.Contains(result[0].TimeSeries, ts => ts.Label == "404" && ts.Data.SequenceEqual(new[] { 0.5, 1.5, 2.5 }));
    }

    [Fact]
    public async Task CorrelateTimeSeries_HandlesMultipleDataSets()
    {
        _appLogsQueryClient.QueryResourceAsync<TimeSeriesCorrelationResponse>(
            _testResourceId, Arg.Any<string>(), Arg.Any<QueryTimeRange>()
        ).Returns(new List<AppLogsQueryRow<TimeSeriesCorrelationResponse>>
        {
            new AppLogsQueryRow<TimeSeriesCorrelationResponse>
            {
                Data = new TimeSeriesCorrelationResponse
                {
                    split = "requests",
                    Value = "[10.0, 20.0, 30.0]"
                }
            }
        });

        var dataSets = new List<AppCorrelateDataSet>
        {
            new AppCorrelateDataSet
            {
                Table = "requests",
                Filters = new[] { "success=\"false\"" },
                SplitBy = "resultCode"
            },
            new AppCorrelateDataSet
            {
                Table = "dependencies",
                Filters = new[] { "type=\"SQL\"" },
                Aggregation = "Average"
            }
        };

        var result = await _sut.CorrelateTimeSeries(
            _testSubscription,
            _testResourceGroup,
            _testResourceName,
            null,
            dataSets,
            DateTime.UtcNow.AddHours(-1),
            DateTime.UtcNow,
            null,
            null
        );

        Assert.Equal(2, result.Length);
        Assert.All(result, r => Assert.NotEmpty(r.Description));
        Assert.All(result, r => Assert.NotEmpty(r.Interval));
    }

    [Fact]
    public async Task GetImpact_ReturnsEmpty()
    {
        _appLogsQueryClient.QueryResourceAsync<ImpactQueryResponse>(
            _testResourceId, Arg.Any<string>(), Arg.Any<QueryTimeRange>()
        ).Returns(new List<AppLogsQueryRow<ImpactQueryResponse>>());

        var result = await _sut.GetImpact(
            _testSubscription,
            _testResourceGroup,
            _testResourceName,
            null,
            new string[] { "resultCode=\"500\"" },
            "requests",
            DateTime.UtcNow.AddHours(-1),
            DateTime.UtcNow,
            null,
            null
        );

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetImpact_ReturnsResults()
    {
        _appLogsQueryClient.QueryResourceAsync<ImpactQueryResponse>(
            _testResourceId, Arg.Any<string>(), Arg.Any<QueryTimeRange>()
        ).Returns(new List<AppLogsQueryRow<ImpactQueryResponse>>
        {
            new AppLogsQueryRow<ImpactQueryResponse>
            {
                Data = new ImpactQueryResponse
                {
                    cloud_RoleName = "web-service-1",
                    ImpactedInstances = 5,
                    TotalInstances = 10,
                    ImpactedRequests = 100,
                    TotalRequests = 1000,
                    ImpactedRequestsPercent = 10.0,
                    ImpactedInstancePercent = 50.0
                }
            },
            new AppLogsQueryRow<ImpactQueryResponse>
            {
                Data = new ImpactQueryResponse
                {
                    cloud_RoleName = "api-service-1",
                    ImpactedInstances = 2,
                    TotalInstances = 8,
                    ImpactedRequests = 50,
                    TotalRequests = 800,
                    ImpactedRequestsPercent = 6.25,
                    ImpactedInstancePercent = 25.0
                }
            }
        });

        var result = await _sut.GetImpact(
            _testSubscription,
            _testResourceGroup,
            _testResourceName,
            null,
            new string[] { "resultCode=\"500\"" },
            "requests",
            DateTime.UtcNow.AddHours(-1),
            DateTime.UtcNow,
            null,
            null
        );

        Assert.Equal(2, result.Count);
        
        var webService = result.First(r => r.CloudRoleName == "web-service-1");
        Assert.Equal(5, webService.ImpactedInstances);
        Assert.Equal(10, webService.TotalInstances);
        Assert.Equal(100, webService.ImpactedCount);
        Assert.Equal(1000, webService.TotalCount);
        Assert.Equal(10.0, webService.ImpactedCountPercent);
        Assert.Equal(50.0, webService.ImpactedInstancePercent);

        var apiService = result.First(r => r.CloudRoleName == "api-service-1");
        Assert.Equal(2, apiService.ImpactedInstances);
        Assert.Equal(8, apiService.TotalInstances);
        Assert.Equal(50, apiService.ImpactedCount);
        Assert.Equal(800, apiService.TotalCount);
        Assert.Equal(6.25, apiService.ImpactedCountPercent);
        Assert.Equal(25.0, apiService.ImpactedInstancePercent);
    }

    [Fact]
    public async Task GetImpact_HandlesNullResponse()
    {
        _appLogsQueryClient.QueryResourceAsync<ImpactQueryResponse>(
            _testResourceId, Arg.Any<string>(), Arg.Any<QueryTimeRange>()
        ).Returns((List<AppLogsQueryRow<ImpactQueryResponse>>)null!);

        var result = await _sut.GetImpact(
            _testSubscription,
            _testResourceGroup,
            _testResourceName,
            null,
            new string[] { "resultCode=\"500\"" },
            "requests",
            DateTime.UtcNow.AddHours(-1),
            DateTime.UtcNow,
            null,
            null
        );

        Assert.Empty(result);
    }
}
