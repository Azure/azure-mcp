// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using AzureMcp.Areas.Monitor.Commands.Metrics;
using AzureMcp.Areas.Monitor.Models;
using AzureMcp.Areas.Monitor.Services;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Areas.Monitor.UnitTests.Metrics;

public class MetricsNamespacesCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMonitorMetricsService _service;
    private readonly ILogger<MetricsNamespacesCommand> _logger;
    private readonly MetricsNamespacesCommand _command;

    public MetricsNamespacesCommandTests()
    {
        _service = Substitute.For<IMonitorMetricsService>();
        _logger = Substitute.For<ILogger<MetricsNamespacesCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_service);
        _serviceProvider = collection.BuildServiceProvider();

        _command = new(_logger);
    }

    #region Constructor and Command Setup Tests

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        // Act
        var command = _command.GetCommand();

        // Assert
        Assert.Equal("namespaces", command.Name);
        Assert.Equal("List Azure Monitor Metric Namespaces", _command.Title);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
        Assert.Contains("List available metric namespaces", command.Description);
        Assert.Contains("Required options:", command.Description);
        Assert.Contains("Optional options:", command.Description);
    }

    [Fact]
    public void GetCommand_HasCorrectOptions()
    {
        // Act
        var command = _command.GetCommand();

        // Assert
        var optionNames = command.Options.Select(o => o.Name).ToList();

        // Check for required base options
        Assert.Contains("subscription", optionNames);
        Assert.Contains("resource-name", optionNames);

        // Check for optional base options
        Assert.Contains("resource-group", optionNames);
        Assert.Contains("resource-type", optionNames);
        Assert.Contains("tenant", optionNames);

        // Check for command-specific options
        Assert.Contains("limit", optionNames);
        Assert.Contains("search-string", optionNames);
    }

    #endregion

    #region Option Binding Tests - Tested through ExecuteAsync

    [Theory]
    [InlineData("--limit 5", 5)]
    [InlineData("--limit 100", 100)]
    [InlineData("--limit 1", 1)]
    [InlineData("", 10)] // Default value
    public async Task ExecuteAsync_UsesCorrectLimit(string limitArgs, int expectedLimit)
    {
        // Arrange
        var namespaces = CreateSampleNamespaces(150); // More than any limit we'll test
        _service.ListMetricNamespacesAsync(
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(namespaces);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse($"--subscription sub1 --resource-name testResource {limitArgs}");

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        var resultJson = JsonSerializer.Serialize(response.Results);
        var resultData = JsonSerializer.Deserialize<JsonElement>(resultJson);

        Assert.True(resultData.TryGetProperty("results", out var resultsArray));
        Assert.Equal(expectedLimit, resultsArray.GetArrayLength());
    }

    [Theory]
    [InlineData("--search-string Microsoft", "Microsoft")]
    [InlineData("--search-string Storage", "Storage")]
    [InlineData("", null)]
    public async Task ExecuteAsync_PassesSearchStringToService(string searchArgs, string? expectedSearchString)
    {
        // Arrange
        var namespaces = CreateSampleNamespaces(1);
        _service.ListMetricNamespacesAsync(
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(namespaces);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse($"--subscription sub1 --resource-name testResource {searchArgs}");

        // Act
        await _command.ExecuteAsync(context, parseResult);

        // Assert
        await _service.Received(1).ListMetricNamespacesAsync(
            "sub1",
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            "testResource",
            expectedSearchString,
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>());
    }

    [Fact]
    public async Task ExecuteAsync_PassesAllParametersToService()
    {
        // Arrange
        var namespaces = CreateSampleNamespaces(1);
        _service.ListMetricNamespacesAsync(
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(namespaces);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--subscription sub1 --resource-name testResource --resource-group rg1 --resource-type Microsoft.Storage/storageAccounts --tenant tenant1");

        // Act
        await _command.ExecuteAsync(context, parseResult);

        // Assert
        await _service.Received(1).ListMetricNamespacesAsync(
            "sub1",                                    // subscription
            Arg.Is<string?>(s => s == "rg1"),         // resource group
            "Microsoft.Storage/storageAccounts",       // resource type
            "testResource",                            // resource name
            null,                                      // search string
            "tenant1",                                 // tenant
            Arg.Any<RetryPolicyOptions?>());           // retry policy
    }

    #endregion

    #region Validation Tests

    [Theory]
    [InlineData("--resource-name /subscriptions/sub/resourceGroups/rg/providers/Microsoft.Storage/storageAccounts/sa --subscription sub1", true)]
    [InlineData("--subscription sub1", false)] // Missing resource name
    [InlineData("--resource-name testResource", false)] // Missing subscription
    public async Task ExecuteAsync_ValidatesRequiredParameters(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            _service.ListMetricNamespacesAsync(
                Arg.Any<string>(),
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<string>(),
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<RetryPolicyOptions?>())
                .Returns(CreateSampleNamespaces(3));
        }

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse(args);

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(shouldSucceed ? 200 : 400, response.Status);
        if (shouldSucceed)
        {
            Assert.NotNull(response.Results);
        }
        else
        {
            Assert.Contains("required", response.Message.ToLower());
        }
    }

    #endregion

    #region Success Scenarios Tests

    [Fact]
    public async Task ExecuteAsync_ReturnsSuccessWithResults()
    {
        // Arrange
        var namespaces = CreateSampleNamespaces(3);
        _service.ListMetricNamespacesAsync(
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(namespaces);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--resource-name testResource --subscription sub1");

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        // Verify the response structure
        var resultJson = JsonSerializer.Serialize(response.Results);
        var resultData = JsonSerializer.Deserialize<JsonElement>(resultJson);

        Assert.True(resultData.TryGetProperty("results", out var resultsArray));
        Assert.Equal(3, resultsArray.GetArrayLength());

        Assert.True(resultData.TryGetProperty("status", out var statusElement));
        Assert.Contains("All 3 metric namespaces returned", statusElement.GetString());
    }

    [Fact]
    public async Task ExecuteAsync_AppliesLimitCorrectly()
    {
        // Arrange
        var namespaces = CreateSampleNamespaces(15); // More than default limit
        _service.ListMetricNamespacesAsync(
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(namespaces);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--resource-name testResource --subscription sub1 --limit 5");

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        var resultJson = JsonSerializer.Serialize(response.Results);
        var resultData = JsonSerializer.Deserialize<JsonElement>(resultJson);

        Assert.True(resultData.TryGetProperty("results", out var resultsArray));
        Assert.Equal(5, resultsArray.GetArrayLength());

        Assert.True(resultData.TryGetProperty("status", out var statusElement));
        Assert.Contains("Results truncated to 5 of 15", statusElement.GetString());
    }

    [Fact]
    public async Task ExecuteAsync_ShowsTruncationMessage()
    {
        // Arrange
        var namespaces = CreateSampleNamespaces(20);
        _service.ListMetricNamespacesAsync(
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(namespaces);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--resource-name testResource --subscription sub1"); // Default limit of 10

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);

        var resultJson = JsonSerializer.Serialize(response.Results);
        var resultData = JsonSerializer.Deserialize<JsonElement>(resultJson);

        Assert.True(resultData.TryGetProperty("status", out var statusElement));
        var statusMessage = statusElement.GetString();
        Assert.Contains("Results truncated to 10 of 20", statusMessage);
        Assert.Contains("Use --search-string to filter", statusMessage);
        Assert.Contains("increase --limit", statusMessage);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesEmptyResults()
    {
        // Arrange
        _service.ListMetricNamespacesAsync(
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(new List<MetricNamespace>());

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--resource-name testResource --subscription sub1");

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.Null(response.Results);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesNullResults()
    {
        // Arrange
        _service.ListMetricNamespacesAsync(
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(Task.FromResult((List<MetricNamespace>?)null)!);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--resource-name testResource --subscription sub1");

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.Null(response.Results);
    }

    #endregion

    #region Service Interaction Tests

    [Fact]
    public async Task ExecuteAsync_CallsServiceWithCorrectParameters()
    {
        // Arrange
        var namespaces = CreateSampleNamespaces(1);
        _service.ListMetricNamespacesAsync(
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(namespaces);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse(
            "--resource-name testResource --subscription sub1 --resource-group rg1 --resource-type Microsoft.Storage/storageAccounts --search-string test --tenant tenant1");

        // Act
        await _command.ExecuteAsync(context, parseResult);

        // Assert
        await _service.Received(1).ListMetricNamespacesAsync(
            "sub1",                                    // subscription
            Arg.Is<string?>(s => s == "rg1"),         // resource group
            "Microsoft.Storage/storageAccounts",       // resource type
            "testResource",                            // resource name
            "test",                                    // search string
            "tenant1",                                 // tenant
            Arg.Any<RetryPolicyOptions?>());           // retry policy
    }

    [Fact]
    public async Task ExecuteAsync_CallsServiceWithMinimalParameters()
    {
        // Arrange
        var namespaces = CreateSampleNamespaces(1);
        _service.ListMetricNamespacesAsync(
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(namespaces);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--resource-name testResource --subscription sub1");

        // Act
        await _command.ExecuteAsync(context, parseResult);

        // Assert
        await _service.Received(1).ListMetricNamespacesAsync(
            "sub1",          // subscription
            null,            // resource group
            null,            // resource type
            "testResource",  // resource name
            null,            // search string
            null,            // tenant
            Arg.Any<RetryPolicyOptions?>()); // retry policy
    }

    #endregion

    #region Exception Handling Tests

    [Fact]
    public async Task ExecuteAsync_HandlesServiceException()
    {
        // Arrange
        var exception = new InvalidOperationException("Service unavailable");
        _service.ListMetricNamespacesAsync(
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .ThrowsAsync(exception);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--resource-name testResource --subscription sub1");

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Service unavailable", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_LogsExceptionWithContext()
    {
        // Arrange
        var exception = new Exception("Test error");
        _service.ListMetricNamespacesAsync(
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .ThrowsAsync(exception);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--resource-name testResource --subscription sub1 --resource-group rg1");

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert - Verify exception was handled properly (we can't easily mock ILogger extensions)
        Assert.Equal(500, response.Status);
        Assert.Contains("Test error", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }

    #endregion

    #region Edge Case Tests

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task ExecuteAsync_HandlesInvalidLimitValues(int limit)
    {
        // Arrange
        var namespaces = CreateSampleNamespaces(5);
        _service.ListMetricNamespacesAsync(
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(namespaces);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse($"--resource-name testResource --subscription sub1 --limit {limit}");

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);

        // When limit is 0 or negative, Take() should return empty results
        var resultJson = JsonSerializer.Serialize(response.Results);
        var resultData = JsonSerializer.Deserialize<JsonElement>(resultJson);

        Assert.True(resultData.TryGetProperty("results", out var resultsArray));
        Assert.Equal(0, resultsArray.GetArrayLength());
    }

    [Fact]
    public async Task ExecuteAsync_HandlesExactLimitMatch()
    {
        // Arrange
        var namespaces = CreateSampleNamespaces(10); // Exactly 10 items
        _service.ListMetricNamespacesAsync(
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(namespaces);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--resource-name testResource --subscription sub1 --limit 10");

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);

        var resultJson = JsonSerializer.Serialize(response.Results);
        var resultData = JsonSerializer.Deserialize<JsonElement>(resultJson);

        Assert.True(resultData.TryGetProperty("results", out var resultsArray));
        Assert.Equal(10, resultsArray.GetArrayLength());

        Assert.True(resultData.TryGetProperty("status", out var statusElement));
        Assert.Contains("All 10 metric namespaces returned", statusElement.GetString());
        Assert.DoesNotContain("truncated", statusElement.GetString());
    }

    #endregion

    #region Helper Methods

    private static List<MetricNamespace> CreateSampleNamespaces(int count)
    {
        var namespaces = new List<MetricNamespace>();
        for (int i = 0; i < count; i++)
        {
            namespaces.Add(new MetricNamespace
            {
                Name = $"Microsoft.Storage/storageAccounts/namespace{i}",
                Type = "Microsoft.Storage",
                ClassificationType = i % 2 == 0 ? "Platform" : "Custom"
            });
        }
        return namespaces;
    }

    #endregion
}
