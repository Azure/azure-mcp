// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.ClientModel.Primitives;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure;
using Azure.Core;
using Azure.Data.Tables;
using Azure.Monitor.Query.Models;
using Azure.ResourceManager;
using Azure.ResourceManager.ApplicationInsights;
using Azure.ResourceManager.ApplicationInsights.Mocking;
using Azure.ResourceManager.Resources;
using AzureMcp.Commands.Monitor.ApplicationInsights;
using AzureMcp.Models;
using AzureMcp.Models.Command;
using AzureMcp.Models.Monitor;
using AzureMcp.Models.Monitor.ApplicationInsights;
using AzureMcp.Options;
using AzureMcp.Services.Azure.Monitor;
using AzureMcp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Commands.Monitor.ApplicationInsights;

public class AppTraceCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMcpServer _mcpServer;
    private readonly ILogger<AppTraceCommand> _logger;
    private readonly ISubscriptionService _subscriptionService;
    private readonly ITenantService _tenantService;
    private readonly ILogsQueryService _logsQueryService;
    private readonly AppTraceCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    private readonly MockableApplicationInsightsSubscriptionResource _listAppInsightsResourcesMock;

    public AppTraceCommandTests()
    {
        _logger = Substitute.For<ILogger<AppTraceCommand>>();
        _mcpServer = Substitute.For<IMcpServer>();
        _subscriptionService = Substitute.For<ISubscriptionService>();
        _tenantService = Substitute.For<ITenantService>();
        _logsQueryService = Substitute.For<ILogsQueryService>();

        _listAppInsightsResourcesMock = Substitute.For<MockableApplicationInsightsSubscriptionResource>();

        SubscriptionResource subscriptionResource = Substitute.For<SubscriptionResource>();

        subscriptionResource.GetCachedClient(Arg.Any<Func<ArmClient, MockableApplicationInsightsSubscriptionResource>>())
            .Returns(_listAppInsightsResourcesMock);

        _subscriptionService.GetSubscription(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromResult(subscriptionResource));

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton(_logger);
        serviceCollection.AddSingleton(_mcpServer);
        serviceCollection.AddSingleton(_subscriptionService);
        serviceCollection.AddSingleton(_tenantService);
        serviceCollection.AddSingleton(_logsQueryService);
        serviceCollection.AddSingleton<IApplicationInsightsService, ApplicationInsightsService>();

        _serviceProvider = serviceCollection.BuildServiceProvider();
        _context = new CommandContext(_serviceProvider);
        _command = new AppTraceCommand(_logger);
        _parser = new Parser(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_WithMissingRequiredOptions_ReturnsValidationError()
    {
        // Arrange - missing trace-id and span-id
        var parseResult = _parser.Parse("--subscription test-sub --app-id testapp");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(400, response.Status);
        Assert.Contains("Required option", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_EmptyLogsQueryResult_ReturnsEmptyTrace()
    {
        // Arrange
        var subscription = "test-sub";
        var appId = "matchingapp";
        var traceId = "trace123";
        var spanId = "span456";

        var resourceList = GetAppInsightsResourcesAsPageable(new List<ApplicationInsightsResourceInfo>()
        {
            new() {
                ResourceId = new ResourceIdentifier("/subscriptions/test-sub/resourceGroups/test/providers/microsoft.insights/components/testapp"),
                AppId = Guid.NewGuid().ToString(),
                Name = appId,
                InstrumentationKey = Guid.NewGuid().ToString()
            }
        });

        _listAppInsightsResourcesMock.GetApplicationInsightsComponentsAsync(Arg.Any<CancellationToken>()).Returns(resourceList);

        _logsQueryService.QueryResourceAsync(Arg.Any<ResourceIdentifier>(), Arg.Any<string>(), Arg.Any<DateTimeOffset>(), Arg.Any<DateTimeOffset>(), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromResult<LogsQueryTable?>(new LogsQueryTable()));

        var parseResult = _parser.Parse($"--subscription {subscription} --app-id {appId} --trace-id {traceId} --span-id {spanId}");
        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);
        // Assert
        Assert.Equal(200, response.Status);

        DistributedTraceResult? actual = GetResult(response.Results);

        Assert.Equal(traceId, actual?.TraceId);
        Assert.Equal(0, actual?.Spans.Count);
    }

    [Fact]
    public async Task ExecuteAsync_ServiceThrowsException_ReturnsValidationError()
    {
        // Arrange
        var subscription = "test-sub";
        var appId = "matchingapp";
        var traceId = "trace123";
        var spanId = "span456";

        var resourceList = GetAppInsightsResourcesAsPageable(new List<ApplicationInsightsResourceInfo>()
        {
            new() {
                ResourceId = new ResourceIdentifier("/subscriptions/test-sub/resourceGroups/test/providers/microsoft.insights/components/testapp"),
                AppId = Guid.NewGuid().ToString(),
                Name = appId,
                InstrumentationKey = Guid.NewGuid().ToString()
            }
        });

        _listAppInsightsResourcesMock.GetApplicationInsightsComponentsAsync(Arg.Any<CancellationToken>()).Returns(resourceList);

        _logsQueryService.When(s => s.QueryResourceAsync(Arg.Any<ResourceIdentifier>(), Arg.Any<string>(), Arg.Any<DateTimeOffset>(), Arg.Any<DateTimeOffset>(), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>()))
            .Throw(new Exception("Test exception"));

        var parseResult = _parser.Parse($"--subscription {subscription} --app-id {appId} --trace-id {traceId} --span-id {spanId}");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        // The test is failing because validation is returning 400 before we get to the service call
        // In a real implementation with valid parameters, this would be 500
        Assert.Contains("Test exception", response.Message);
        Assert.Equal(500, response.Status);
    }

    [Fact]
    public async Task ExecuteAsync_WithEmptySubscription_ReturnsValidationError()
    {
        // Arrange
        var subscription = "test-sub";
        var appId = "nonexistentapp";
        var traceId = "trace123";
        var spanId = "span456";

        _listAppInsightsResourcesMock.GetApplicationInsightsComponentsAsync(Arg.Any<CancellationToken>()).Returns(GetAppInsightsResourcesAsPageable(new List<ApplicationInsightsResourceInfo>()));

        var parseResult = _parser.Parse($"--subscription {subscription} --app-id {appId} --trace-id {traceId} --span-id {spanId}");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Error retrieving distributed trace: Could not find Application Insights resource with name nonexistentapp.", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_WithNonMatchingResource_ReturnsValidationError()
    {
        // Arrange
        var subscription = "test-sub";
        var appId = "nonexistentapp";
        var traceId = "trace123";
        var spanId = "span456";

        var result = GetAppInsightsResourcesAsPageable(new List<ApplicationInsightsResourceInfo>()
        {
            new() {
                ResourceId = new ResourceIdentifier("/subscriptions/test-sub/resourceGroups/test/providers/microsoft.insights/components/otherapp"),
                AppId = "otherapp",
                Name = "Other App",
                InstrumentationKey = "other-instrumentation-key"
            }
        });

        _listAppInsightsResourcesMock.GetApplicationInsightsComponentsAsync(Arg.Any<CancellationToken>()).Returns(result);

        var parseResult = _parser.Parse($"--subscription {subscription} --app-id {appId} --trace-id {traceId} --span-id {spanId}");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Error retrieving distributed trace: Could not find Application Insights resource with name nonexistentapp.", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_WithLogsQueryResult_BuildsSpanHierarchyCorrectly()
    {
        // Arrange
        var subscription = "test-sub";
        var appId = "testapp";
        var traceId = "trace123";
        var spanId = "span1"; // Root span

        var resourceList = GetAppInsightsResourcesAsPageable(new List<ApplicationInsightsResourceInfo>()
        {
            new() {
                ResourceId = new ResourceIdentifier("/subscriptions/test-sub/resourceGroups/test/providers/microsoft.insights/components/testapp"),
                AppId = Guid.NewGuid().ToString(),
                Name = appId,
                InstrumentationKey = Guid.NewGuid().ToString()
            }
        });

        _listAppInsightsResourcesMock.GetApplicationInsightsComponentsAsync(Arg.Any<CancellationToken>()).Returns(resourceList);

        // Create a mock query result with multiple spans in a hierarchy
        var mockTable = new LogsQueryTable();
        
        // Root span
        mockTable.Rows.Add(new Dictionary<string, object>
        {
            { "id", "span1" },
            { "operation_ParentId", "" },
            { "operation_Name", "Root Operation" },
            { "resultCode", "200" },
            { "itemType", "request" },
            { "success", "true" },
            { "timestamp", DateTime.UtcNow },
            { "duration", 1000.0 },
            { "operation_Id", traceId },
            { "custom_property", "custom value" }
        });

        // Child span 1
        mockTable.Rows.Add(new Dictionary<string, object>
        {
            { "id", "span2" },
            { "operation_ParentId", "span1" },
            { "operation_Name", "Child Operation 1" },
            { "resultCode", "200" },
            { "itemType", "dependency" },
            { "success", "true" },
            { "timestamp", DateTime.UtcNow },
            { "duration", 500.0 },
            { "operation_Id", traceId }
        });

        // Child span 2
        mockTable.Rows.Add(new Dictionary<string, object>
        {
            { "id", "span3" },
            { "operation_ParentId", "span1" },
            { "operation_Name", "Child Operation 2" },
            { "resultCode", "500" },
            { "itemType", "dependency" },
            { "success", "false" },
            { "timestamp", DateTime.UtcNow },
            { "duration", 250.0 },
            { "operation_Id", traceId }
        });

        _logsQueryService.QueryResourceAsync(Arg.Any<ResourceIdentifier>(), Arg.Any<string>(), Arg.Any<DateTimeOffset>(), Arg.Any<DateTimeOffset>(), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromResult<LogsQueryTable?>(mockTable));

        var parseResult = _parser.Parse($"--subscription {subscription} --app-id {appId} --trace-id {traceId} --span-id {spanId}");
        
        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);
        
        // Assert
        Assert.Equal(200, response.Status);
        
        DistributedTraceResult? actual = GetResult(response.Results);
        
        Assert.Equal(traceId, actual?.TraceId);
        Assert.Equal(3, actual?.Spans.Count);
        
        // Find span1 in the result spans
        var rootSpan = actual?.Spans.FirstOrDefault(s => s.SpanId == "span1");
        Assert.NotNull(rootSpan);
        Assert.Equal("Root Operation", rootSpan?.OperationName);
        Assert.Equal(2, rootSpan?.ChildSpans.Count);
        
        // Check that the children are correctly associated
        var childSpanIds = rootSpan?.ChildSpans.Select(c => c.SpanId).ToList();        Assert.NotNull(childSpanIds);
        Assert.Contains("span2", childSpanIds!);
        Assert.Contains("span3", childSpanIds!);
        
        // Check custom property was added
        var customProp = rootSpan?.Properties.FirstOrDefault(p => p.Key == "custom_property");
        Assert.NotNull(customProp);
        Assert.Equal("custom value", customProp?.Value);
    }

    [Fact]
    public async Task ExecuteAsync_WithNonRootSpanId_FiltersBySpecifiedSpan()
    {
        // Arrange
        var subscription = "test-sub";
        var appId = "testapp";
        var traceId = "trace123";
        var spanId = "span2"; // Middle span in the hierarchy

        var resourceList = GetAppInsightsResourcesAsPageable(new List<ApplicationInsightsResourceInfo>()
        {
            new() {
                ResourceId = new ResourceIdentifier("/subscriptions/test-sub/resourceGroups/test/providers/microsoft.insights/components/testapp"),
                AppId = Guid.NewGuid().ToString(),
                Name = appId,
                InstrumentationKey = Guid.NewGuid().ToString()
            }
        });

        _listAppInsightsResourcesMock.GetApplicationInsightsComponentsAsync(Arg.Any<CancellationToken>()).Returns(resourceList);

        // Create a deeper hierarchy: root -> middle -> leaf
        var mockTable = new LogsQueryTable();
        
        // Root span
        mockTable.Rows.Add(new Dictionary<string, object>
        {
            { "id", "span1" },
            { "operation_ParentId", "" },
            { "operation_Name", "Root Operation" },
            { "resultCode", "200" },
            { "itemType", "request" },
            { "success", "true" },
            { "timestamp", DateTime.UtcNow },
            { "duration", 1000.0 },
            { "operation_Id", traceId }
        });

        // Middle span (our target span)
        mockTable.Rows.Add(new Dictionary<string, object>
        {
            { "id", "span2" },
            { "operation_ParentId", "span1" },
            { "operation_Name", "Middle Operation" },
            { "resultCode", "200" },
            { "itemType", "dependency" },
            { "success", "true" },
            { "timestamp", DateTime.UtcNow },
            { "duration", 500.0 },
            { "operation_Id", traceId }
        });

        // Leaf span 1 (child of span2)
        mockTable.Rows.Add(new Dictionary<string, object>
        {
            { "id", "span3" },
            { "operation_ParentId", "span2" },
            { "operation_Name", "Leaf Operation 1" },
            { "resultCode", "200" },
            { "itemType", "dependency" },
            { "success", "true" },
            { "timestamp", DateTime.UtcNow },
            { "duration", 250.0 },
            { "operation_Id", traceId }
        });

        // Leaf span 2 (child of span2)
        mockTable.Rows.Add(new Dictionary<string, object>
        {
            { "id", "span4" },
            { "operation_ParentId", "span2" },
            { "operation_Name", "Leaf Operation 2" },
            { "resultCode", "200" },
            { "itemType", "dependency" },
            { "success", "true" },
            { "timestamp", DateTime.UtcNow },
            { "duration", 300.0 },
            { "operation_Id", traceId }
        });

        // Unrelated span (different parent)
        mockTable.Rows.Add(new Dictionary<string, object>
        {
            { "id", "span5" },
            { "operation_ParentId", "span1" },
            { "operation_Name", "Unrelated Operation" },
            { "resultCode", "200" },
            { "itemType", "dependency" },
            { "success", "true" },
            { "timestamp", DateTime.UtcNow },
            { "duration", 150.0 },
            { "operation_Id", traceId }
        });

        _logsQueryService.QueryResourceAsync(Arg.Any<ResourceIdentifier>(), Arg.Any<string>(), Arg.Any<DateTimeOffset>(), Arg.Any<DateTimeOffset>(), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromResult<LogsQueryTable?>(mockTable));

        var parseResult = _parser.Parse($"--subscription {subscription} --app-id {appId} --trace-id {traceId} --span-id {spanId}");
        
        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);
        
        // Assert
        Assert.Equal(200, response.Status);
        
        DistributedTraceResult? actual = GetResult(response.Results);
        
        Assert.Equal(traceId, actual?.TraceId);
        
        // Should include span2, its parent (span1), and its direct children (span3, span4)
        // But not span5 (which is not related to span2)
        Assert.Equal(4, actual?.Spans.Count);
        
        // Verify spans are included
        var spanIds = actual?.Spans.Select(s => s.SpanId).ToList();        Assert.NotNull(spanIds);
        Assert.Contains("span1", spanIds!); // Parent
        Assert.Contains("span2", spanIds!); // Target
        Assert.Contains("span3", spanIds!); // Child 1
        Assert.Contains("span4", spanIds!); // Child 2
        Assert.DoesNotContain("span5", spanIds!); // Unrelated span
    }

    [Fact]
    public async Task ExecuteAsync_WithSpanNotInResults_ReturnsEmptySpansList()
    {
        // Arrange
        var subscription = "test-sub";
        var appId = "testapp";
        var traceId = "trace123";
        var spanId = "nonexistent-span"; // Span ID that doesn't exist in the results

        var resourceList = GetAppInsightsResourcesAsPageable(new List<ApplicationInsightsResourceInfo>()
        {
            new() {
                ResourceId = new ResourceIdentifier("/subscriptions/test-sub/resourceGroups/test/providers/microsoft.insights/components/testapp"),
                AppId = Guid.NewGuid().ToString(),
                Name = appId,
                InstrumentationKey = Guid.NewGuid().ToString()
            }
        });

        _listAppInsightsResourcesMock.GetApplicationInsightsComponentsAsync(Arg.Any<CancellationToken>()).Returns(resourceList);

        // Create a mock query result with some spans
        var mockTable = new LogsQueryTable();
        
        mockTable.Rows.Add(new Dictionary<string, object>
        {
            { "id", "span1" },
            { "operation_ParentId", "" },
            { "operation_Name", "Root Operation" },
            { "resultCode", "200" },
            { "itemType", "request" },
            { "success", "true" },
            { "timestamp", DateTime.UtcNow },
            { "duration", 1000.0 },
            { "operation_Id", traceId }
        });

        _logsQueryService.QueryResourceAsync(Arg.Any<ResourceIdentifier>(), Arg.Any<string>(), Arg.Any<DateTimeOffset>(), Arg.Any<DateTimeOffset>(), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromResult<LogsQueryTable?>(mockTable));

        var parseResult = _parser.Parse($"--subscription {subscription} --app-id {appId} --trace-id {traceId} --span-id {spanId}");
        
        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);
        
        // Assert
        Assert.Equal(200, response.Status);
        
        DistributedTraceResult? actual = GetResult(response.Results);
        
        Assert.Equal(traceId, actual?.TraceId);
        Assert.NotNull(actual);
        Assert.Empty(actual!.Spans);
    }

    [Fact]
    public async Task ExecuteAsync_WithDifferentDataTypes_ParsesPropertiesCorrectly()
    {
        // Arrange
        var subscription = "test-sub";
        var appId = "testapp";
        var traceId = "trace123";
        var spanId = "span1";
        var testTimestamp = new DateTime(2023, 1, 1, 12, 0, 0, DateTimeKind.Utc);

        var resourceList = GetAppInsightsResourcesAsPageable(new List<ApplicationInsightsResourceInfo>()
        {
            new() {
                ResourceId = new ResourceIdentifier("/subscriptions/test-sub/resourceGroups/test/providers/microsoft.insights/components/testapp"),
                AppId = Guid.NewGuid().ToString(),
                Name = appId,
                InstrumentationKey = Guid.NewGuid().ToString()
            }
        });

        _listAppInsightsResourcesMock.GetApplicationInsightsComponentsAsync(Arg.Any<CancellationToken>()).Returns(resourceList);

        // Create a mock query result with different data types
        var mockTable = new LogsQueryTable();
        
        mockTable.Rows.Add(new Dictionary<string, object>
        {
            { "id", "span1" },
            { "operation_ParentId", "" },
            { "operation_Name", "Test Operation" },
            { "resultCode", 404 }, // Integer instead of string
            { "itemType", "request" },
            { "success", false }, // Boolean instead of string
            { "timestamp", testTimestamp }, // DateTime object
            { "duration", 1000.0 }, // Double for duration
            { "operation_Id", traceId },
            { "int_property", 42 },
            { "bool_property", true },
            { "null_property", DBNull.Value }
        });

        _logsQueryService.QueryResourceAsync(Arg.Any<ResourceIdentifier>(), Arg.Any<string>(), Arg.Any<DateTimeOffset>(), Arg.Any<DateTimeOffset>(), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromResult<LogsQueryTable?>(mockTable));

        var parseResult = _parser.Parse($"--subscription {subscription} --app-id {appId} --trace-id {traceId} --span-id {spanId}");
        
        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);
        
        // Assert
        Assert.Equal(200, response.Status);
        
        DistributedTraceResult? actual = GetResult(response.Results);
        
        Assert.Equal(traceId, actual?.TraceId);        Assert.NotNull(actual);
        Assert.Single(actual!.Spans);
        
        var span = actual!.Spans.First();        Assert.NotNull(span);
        Assert.Equal("span1", span.SpanId);
        Assert.Equal("Test Operation", span.OperationName);
        Assert.Equal("404", span.ResponseCode); // String conversion
        Assert.False(span.IsSuccessful); // Parsed correctly
        Assert.Equal(testTimestamp, span.StartTime);
        Assert.Equal(TimeSpan.FromMilliseconds(1000), span.Duration);
        
        // Check custom properties
        Assert.NotNull(span.Properties);
        Assert.Contains(span.Properties, p => p.Key == "int_property" && p.Value == "42");
        Assert.Contains(span.Properties, p => p.Key == "bool_property" && p.Value == "True");
        Assert.DoesNotContain(span.Properties, p => p.Key == "null_property");
    }

    private AsyncPageable<ApplicationInsightsComponentResource> GetAppInsightsResourcesAsPageable(List<ApplicationInsightsResourceInfo> resources)
    {
        List<ApplicationInsightsComponentResource> components = resources.Select(t =>
        {
            var result = Substitute.For<ApplicationInsightsComponentResource>();

            var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes($@"{{
                ""kind"": ""web"",
                ""location"": ""eastus"",
                ""name"": ""{t.Name}"",
                ""properties"": {{
                    ""AppId"": ""{t.AppId}"",
                    ""InstrumentationKey"": ""{t.InstrumentationKey}""
                }}
            }}"));

            IJsonModel<ApplicationInsightsComponentData> data = new ApplicationInsightsComponentData(AzureLocation.AustraliaCentral, "web");
            var d = data.Create(ref reader, new ModelReaderWriterOptions("W"));

            result.Data.Returns(d);
            result.Id.Returns(t.ResourceId);
            return result;
        }).ToList();
        var page = Page<ApplicationInsightsComponentResource>.FromValues(components, continuationToken: null, Substitute.For<Response>());
        return AsyncPageable<ApplicationInsightsComponentResource>.FromPages(new[] { page });
    }

    private DistributedTraceResult GetResult(ResponseResult? response)
    {
        string serialized = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true
        });

        JsonDocument document = JsonDocument.Parse(serialized);
        JsonElement root = document.RootElement;
        var actualResult = root.GetProperty("result");

        return JsonSerializer.Deserialize<DistributedTraceResult>(actualResult.GetRawText(), new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException("Deserialized result is null");
    }
}
