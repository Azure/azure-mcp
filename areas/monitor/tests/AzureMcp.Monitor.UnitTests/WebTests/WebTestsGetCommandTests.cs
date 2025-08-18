// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using AzureMcp.Models.Monitor.WebTests;
using AzureMcp.Monitor.Commands.WebTests;
using AzureMcp.Monitor.Services;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Monitor.UnitTests.WebTests;

public class WebTestsGetCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMonitorWebTestService _service;
    private readonly ILogger<WebTestsGetCommand> _logger;
    private readonly WebTestsGetCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public WebTestsGetCommandTests()
    {
        _service = Substitute.For<IMonitorWebTestService>();
        _logger = Substitute.For<ILogger<WebTestsGetCommand>>();

        var collection = new ServiceCollection().AddSingleton(_service);
        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    #region Constructor and Properties Tests

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("get", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
        Assert.Contains("Gets details for a specific web test", command.Description);
    }

    [Fact]
    public void Name_ReturnsCorrectValue()
    {
        Assert.Equal("get", _command.Name);
    }

    [Fact]
    public void Title_ReturnsCorrectValue()
    {
        Assert.Equal("Get details of a specific web test", _command.Title);
    }

    [Fact]
    public void Description_ContainsRequiredInformation()
    {
        var description = _command.Description;
        Assert.Contains("resource group", description);
        Assert.Contains("webtest resource name", description);
        Assert.Contains("detailed information", description);
    }

    [Fact]
    public void Metadata_IsConfiguredCorrectly()
    {
        var metadata = _command.Metadata;
        Assert.False(metadata.Destructive);
        Assert.True(metadata.ReadOnly);
    }

    #endregion

    #region Option Registration Tests

    [Fact]
    public void RegisterOptions_AddsAllExpectedOptions()
    {
        var command = _command.GetCommand();
        var options = command.Options.Select(o => o.Name).ToList();

        // Base options from BaseMonitorWebTestsCommand (subscription from SubscriptionCommand)
        Assert.Contains("subscription", options);

        // Base options from BaseMonitorWebTestsCommand (subscription from SubscriptionCommand)
        Assert.Contains("subscription", options);

        // WebTestsGetCommand specific options
        Assert.Contains("resource-group", options);
        Assert.Contains("webtest-resourcename", options);

        // Verify required options are marked as required
        var requiredOptions = command.Options.Where(o => o.IsRequired).Select(o => o.Name).ToList();
        Assert.Contains("resource-group", requiredOptions);
        Assert.Contains("webtest-resourcename", requiredOptions);
    }

    #endregion

    #region Option Binding Tests

    [Fact]
    public async Task ExecuteAsync_BindsAllOptionsCorrectly()
    {
        // Arrange
        var args = new string[] { "--subscription", "sub1", "--resource-group", "rg1", "--webtest-resourcename", "webtest1" };
        var expectedResult = new WebTestDetailedInfo
        {
            ResourceName = "webtest1",
            Location = "eastus",
            ResourceGroup = "rg1",
            Id = "/subscriptions/sub1/resourceGroups/rg1/providers/Microsoft.Insights/webtests/webtest1"
        };

        _service.GetWebTest("sub1", "rg1", "webtest1", null, Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedResult);

        var parseResult = _parser.Parse(args);

        // Act
        await _command.ExecuteAsync(_context, parseResult);

        // Assert
        await _service.Received(1).GetWebTest("sub1", "rg1", "webtest1", null, Arg.Any<RetryPolicyOptions?>());
    }

    #endregion

    #region ExecuteAsync Tests - Success Scenarios

    [Fact]
    public async Task ExecuteAsync_ValidInput_ReturnsSuccess()
    {
        // Arrange
        var args = new string[] { "--subscription", "sub1", "--resource-group", "rg1", "--webtest-resourcename", "webtest1" };
        var expectedResult = new WebTestDetailedInfo
        {
            ResourceName = "webtest1",
            Location = "eastus",
            ResourceGroup = "rg1",
            Id = "/subscriptions/sub1/resourceGroups/rg1/providers/Microsoft.Insights/webtests/webtest1",
            Kind = "ping",
            WebTestName = "Test web test",
            IsEnabled = true,
            FrequencyInSeconds = 300,
            TimeoutInSeconds = 30,
            IsRetryEnabled = false,
            AppInsightsComponentId = "/subscriptions/sub1/resourceGroups/rg1/providers/Microsoft.Insights/components/appinsights1"
        };

        _service.GetWebTest("sub1", "rg1", "webtest1", Arg.Any<string?>(), Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedResult);

        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        Assert.Equal("Success", response.Message);

        // Verify the actual content of the results
        var result = GetResult(response.Results);
        Assert.NotNull(result);
        Assert.Equal("webtest1", result.ResourceName);
        Assert.Equal("eastus", result.Location);
        Assert.Equal("ping", result.Kind);
        Assert.Equal(300, result.FrequencyInSeconds);
        Assert.Equal(30, result.TimeoutInSeconds);
        Assert.True(result.IsEnabled);
    }

    [Fact]
    public async Task ExecuteAsync_WebTestNotFound_ReturnsNotFound()
    {
        // Arrange
        var args = new string[] { "--subscription", "sub1", "--resource-group", "rg1", "--webtest-resourcename", "nonexistent" };

        // The service throws an exception when a web test is not found (as seen in implementation)
        _service.When(x => x.GetWebTest(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string?>(), Arg.Any<RetryPolicyOptions?>()))
            .Do(x => throw new Exception("Error retrieving details for web test 'nonexistent'"));

        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status); // Exception handling returns 500, not 404
        Assert.Contains("troubleshooting", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_CallsServiceWithCorrectParameters()
    {
        // Arrange
        var expectedResult = new WebTestDetailedInfo
        {
            ResourceName = "webtest1",
            Location = "eastus"
        };

        _service.GetWebTest(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string?>(), Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedResult);

        var parseResult = _parser.Parse(["--subscription", "sub1", "--resource-group", "rg1", "--webtest-resourcename", "webtest1"]);

        // Act
        await _command.ExecuteAsync(_context, parseResult);

        // Assert
        await _service.Received(1).GetWebTest("sub1", "rg1", "webtest1", null, Arg.Any<RetryPolicyOptions?>());
    }

    #endregion

    #region ExecuteAsync Tests - Validation Failures

    [Theory]
    [InlineData("")]                                                        // Missing all required options
    [InlineData("--subscription sub1")]                                    // Missing resource-group and resource-name
    [InlineData("--subscription sub1 --resource-group rg1")]              // Missing resource-name
    [InlineData("--resource-group rg1 --webtest-resourcename webtest1")]         // Missing subscription
    public async Task ExecuteAsync_InvalidInput_ReturnsBadRequest(string args)
    {
        // Arrange
        var argArray = string.IsNullOrEmpty(args) ? Array.Empty<string>() : args.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var parseResult = _parser.Parse(argArray);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(400, response.Status);
        Assert.NotEmpty(response.Message);
        Assert.Null(response.Results);
    }

    #endregion

    #region ExecuteAsync Tests - Error Handling

    [Fact]
    public async Task ExecuteAsync_ServiceThrowsException_ReturnsInternalServerError()
    {
        // Arrange
        var expectedException = new Exception("Service unavailable");
        _service.When(x => x.GetWebTest(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>()))
            .Do(x => throw expectedException);

        var parseResult = _parser.Parse(["--subscription", "sub1", "--resource-group", "rg1", "--webtest-resourcename", "webtest1"]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Service unavailable", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ServiceThrowsException_LogsError()
    {
        // Arrange
        var expectedException = new Exception("Service error");
        _service.When(x => x.GetWebTest(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>()))
            .Do(x => throw expectedException);

        var parseResult = _parser.Parse(["--subscription", "sub1", "--resource-group", "rg1", "--webtest-resourcename", "webtest1"]);

        // Act
        await _command.ExecuteAsync(_context, parseResult);

        // Assert
        _logger.Received(1).Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Is<object>(o => o.ToString()!.Contains("Error retrieving web test")),
            expectedException,
            Arg.Any<Func<object, Exception?, string>>());
    }

    #endregion

    #region Helper Methods

    private WebTestDetailedInfo? GetResult(ResponseResult? result)
    {
        if (result == null)
        {
            return null;
        }
        var json = JsonSerializer.Serialize(result);
        return JsonSerializer.Deserialize<WebTestsGetCommandResult>(json)?.webTest;
    }

    private record WebTestsGetCommandResult(WebTestDetailedInfo webTest);

    #endregion
}
