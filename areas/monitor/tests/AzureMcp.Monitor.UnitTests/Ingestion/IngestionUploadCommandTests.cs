// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using AzureMcp.Monitor.Commands.Ingestion;
using AzureMcp.Monitor.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Monitor.UnitTests.Ingestion;

[Trait("Area", "Monitor")]
public sealed class IngestionUploadCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMonitorService _monitorService;
    private readonly ILogger<IngestionUploadCommand> _logger;
    private readonly IngestionUploadCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    private const string _knownIngestionEndpoint = "https://myendpoint-abcd.eastus-1.ingest.monitor.azure.com";
    private const string _knownSubscription = "knownSubscription";
    private const string _knownDataCollectionRule = "/subscriptions/12345678-1234-1234-1234-123456789012/resourceGroups/rg-test/providers/Microsoft.Insights/dataCollectionRules/dcr-test";
    private const string _knownStreamName = "Custom-MyStream";
    private const string _knownLogData = @"[{""TimeGenerated"": ""2023-01-01T12:00:00Z"", ""Message"": ""Test log entry"", ""Level"": ""Info""}]";
    private const string _knownTenant = "knownTenant";

    public IngestionUploadCommandTests()
    {
        _monitorService = Substitute.For<IMonitorService>();
        _logger = Substitute.For<ILogger<IngestionUploadCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_monitorService);
        _serviceProvider = collection.BuildServiceProvider();

        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Theory]
    [InlineData($"--subscription {_knownSubscription} --ingestion-endpoint {_knownIngestionEndpoint} --data-collection-rule {_knownDataCollectionRule} --stream-name {_knownStreamName} --log-data {_knownLogData}", true)]
    [InlineData($"--subscription {_knownSubscription} --ingestion-endpoint {_knownIngestionEndpoint} --data-collection-rule {_knownDataCollectionRule} --stream-name {_knownStreamName} --log-data {_knownLogData} --tenant {_knownTenant}", true)]
    [InlineData($"--subscription {_knownSubscription} --ingestion-endpoint {_knownIngestionEndpoint} --data-collection-rule {_knownDataCollectionRule} --stream-name {_knownStreamName}", false)] // missing log-data
    [InlineData($"--subscription {_knownSubscription} --ingestion-endpoint {_knownIngestionEndpoint} --data-collection-rule {_knownDataCollectionRule}", false)] // missing stream-name and log-data
    [InlineData($"--subscription {_knownSubscription} --ingestion-endpoint {_knownIngestionEndpoint}", false)] // missing most parameters
    [InlineData("", false)] // missing all parameters
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            var mockResult = ("Success", 1, "Successfully uploaded 1 records to stream 'Custom-MyStream'");
            _monitorService.UploadLogs(
                Arg.Any<string>(),
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
            Assert.Contains("required", response.Message.ToLower());
        }
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsIngestionResults()
    {
        // Arrange
        var mockResult = ("Success", 3, "Successfully uploaded 3 records to stream 'Custom-MyStream'");
        _monitorService.UploadLogs(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(mockResult);

        var args = _parser.Parse([
            "--subscription", _knownSubscription,
            "--ingestion-endpoint", _knownIngestionEndpoint,
            "--data-collection-rule", _knownDataCollectionRule,
            "--stream-name", _knownStreamName,
            "--log-data", _knownLogData
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        // Verify the mock was called
        await _monitorService.Received(1).UploadLogs(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_CallsServiceWithCorrectParameters()
    {
        // Arrange
        var mockResult = ("Success", 1, "Successfully uploaded 1 records to stream 'Custom-MyStream'");
        _monitorService.UploadLogs(
            _knownIngestionEndpoint,
            _knownDataCollectionRule,
            _knownStreamName,
            _knownLogData,
            _knownTenant,
            Arg.Any<RetryPolicyOptions>())
            .Returns(mockResult);

        var args = _parser.Parse([
            "--subscription", _knownSubscription,
            "--ingestion-endpoint", _knownIngestionEndpoint,
            "--data-collection-rule", _knownDataCollectionRule,
            "--stream-name", _knownStreamName,
            "--log-data", _knownLogData,
            "--tenant", _knownTenant
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.Equal(200, response.Status);
        await _monitorService.Received(1).UploadLogs(
            _knownIngestionEndpoint,
            _knownDataCollectionRule,
            _knownStreamName,
            _knownLogData,
            _knownTenant,
            Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceErrors()
    {
        // Arrange
        _monitorService.UploadLogs(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromException<(string, int, string)>(new Exception("Test ingestion error")));

        var args = _parser.Parse([
            "--subscription", _knownSubscription,
            "--ingestion-endpoint", _knownIngestionEndpoint,
            "--data-collection-rule", _knownDataCollectionRule,
            "--stream-name", _knownStreamName,
            "--log-data", _knownLogData
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Test ingestion error", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesInvalidJsonData()
    {
        // Arrange
        var invalidLogData = "invalid json data";
        var mockResult = ("Failed", 0, "Invalid JSON format: Unexpected character 'i' at position 0.");
        _monitorService.UploadLogs(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            invalidLogData,
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(mockResult);

        var args = _parser.Parse([
            "--subscription", _knownSubscription,
            "--ingestion-endpoint", _knownIngestionEndpoint,
            "--data-collection-rule", _knownDataCollectionRule,
            "--stream-name", _knownStreamName,
            "--log-data", invalidLogData
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.Equal(200, response.Status); // Service handles the error, doesn't throw
        Assert.NotNull(response.Results);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesEmptyLogData()
    {
        // Arrange
        var emptyLogData = "[]";
        var mockResult = ("Failed", 0, "Log data array is empty");
        _monitorService.UploadLogs(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            emptyLogData,
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(mockResult);

        var args = _parser.Parse([
            "--subscription", _knownSubscription,
            "--ingestion-endpoint", _knownIngestionEndpoint,
            "--data-collection-rule", _knownDataCollectionRule,
            "--stream-name", _knownStreamName,
            "--log-data", emptyLogData
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.Equal(200, response.Status); // Service handles the error, doesn't throw
        Assert.NotNull(response.Results);
    }
}
