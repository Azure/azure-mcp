// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json.Nodes;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using AzureMcp.Monitor.Commands.Ingestion;
using AzureMcp.Monitor.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Monitor.UnitTests.Commands.Ingestion;

[Trait("Area", "Monitor")]
public sealed class IngestionStatusCheckCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMonitorService _monitorService;
    private readonly ILogger<IngestionStatusCheckCommand> _logger;
    private readonly IngestionStatusCheckCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    private const string _knownWorkspace = "knownWorkspace";
    private const string _knownSubscription = "knownSubscription";
    private const string _knownDataCollectionRule = "/subscriptions/12345678-1234-1234-1234-123456789012/resourceGroups/rg-test/providers/Microsoft.Insights/dataCollectionRules/dcr-test";
    private const string _knownOperationId = "test-operation-123";
    private const string _knownTenant = "knownTenant";

    public IngestionStatusCheckCommandTests()
    {
        _monitorService = Substitute.For<IMonitorService>();
        _logger = Substitute.For<ILogger<IngestionStatusCheckCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_monitorService);
        _serviceProvider = collection.BuildServiceProvider();

        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Theory]
    [InlineData($"--subscription {_knownSubscription} --workspace {_knownWorkspace} --data-collection-rule {_knownDataCollectionRule}", true)]
    [InlineData($"--subscription {_knownSubscription} --workspace {_knownWorkspace} --data-collection-rule {_knownDataCollectionRule} --operation-id {_knownOperationId}", true)]
    [InlineData($"--subscription {_knownSubscription} --workspace {_knownWorkspace} --data-collection-rule {_knownDataCollectionRule} --tenant {_knownTenant}", true)]
    [InlineData($"--subscription {_knownSubscription} --workspace {_knownWorkspace}", false)] // missing data-collection-rule
    [InlineData($"--subscription {_knownSubscription}", false)] // missing workspace and data-collection-rule
    [InlineData("", false)] // missing all parameters
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            var details = new JsonObject
            {
                ["dataCollectionRule"] = _knownDataCollectionRule,
                ["workspace"] = _knownWorkspace,
                ["note"] = "Azure Monitor Log Ingestion API does not provide direct status endpoints"
            };

            var mockResult = ("Unavailable", "Status check not directly supported by Azure Monitor", details);
            _monitorService.CheckIngestionStatus(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<RetryPolicyOptions>())
                .Returns(mockResult);
        }

        // Act
        var response = await _command.ExecuteAsync(_context, _parser.Parse(args));

        // Assert
        Assert.Equal(shouldSucceed ? 200 : 400, response.Status);
        if (shouldSucceed)
        {
            Assert.NotNull(response.Results);
            Assert.Equal("Success", response.Message);
        }
        else
        {
            Assert.True(response.Status >= 400);
        }
    }

    [Fact]
    public async Task ExecuteAsync_ServiceThrowsException_ShouldReturnError()
    {
        // Arrange
        _monitorService.CheckIngestionStatus(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromException<(string, string, JsonNode?)>(new Exception("Test service error")));

        var args = $"--subscription {_knownSubscription} --workspace {_knownWorkspace} --data-collection-rule {_knownDataCollectionRule}";

        // Act
        var response = await _command.ExecuteAsync(_context, _parser.Parse(args));

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Test service error", response.Message);
    }

    [Fact]
    public void IngestionStatusCheckCommand_Properties_ShouldBeCorrect()
    {
        // Arrange & Act
        var name = _command.Name;
        var title = _command.Title;
        var description = _command.Description;
        var metadata = _command.Metadata;

        // Assert
        Assert.Equal("azmcp-monitor-ingestion-status-check", name);
        Assert.Equal("Check Status of Azure Monitor Log Ingestion Operations", title);
        Assert.Contains("Check the status of Azure Monitor log ingestion operations", description);
        Assert.False(metadata.Destructive);
        Assert.True(metadata.ReadOnly);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidOperationId_ShouldCallServiceWithCorrectParameters()
    {
        // Arrange
        var args = $"--subscription {_knownSubscription} --workspace {_knownWorkspace} --data-collection-rule {_knownDataCollectionRule} --operation-id {_knownOperationId}";
        
        var details = new JsonObject { ["operationId"] = _knownOperationId };
        var mockResult = ("Unavailable", "Operation ID status check not supported", details);
        
        _monitorService.CheckIngestionStatus(
            _knownWorkspace,
            _knownDataCollectionRule,
            _knownOperationId,
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(mockResult);

        // Act
        var response = await _command.ExecuteAsync(_context, _parser.Parse(args));

        // Assert
        Assert.Equal(200, response.Status);
        await _monitorService.Received(1).CheckIngestionStatus(
            _knownWorkspace,
            _knownDataCollectionRule,
            _knownOperationId,
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_WithoutOperationId_ShouldCallServiceWithNullOperationId()
    {
        // Arrange
        var args = $"--subscription {_knownSubscription} --workspace {_knownWorkspace} --data-collection-rule {_knownDataCollectionRule}";
        
        var details = new JsonObject { ["note"] = "No operation ID provided" };
        var mockResult = ("Unavailable", "Recent ingestion status check not supported", details);
        
        _monitorService.CheckIngestionStatus(
            _knownWorkspace,
            _knownDataCollectionRule,
            null,
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(mockResult);

        // Act
        var response = await _command.ExecuteAsync(_context, _parser.Parse(args));

        // Assert
        Assert.Equal(200, response.Status);
        await _monitorService.Received(1).CheckIngestionStatus(
            _knownWorkspace,
            _knownDataCollectionRule,
            null,
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>());
    }
}
