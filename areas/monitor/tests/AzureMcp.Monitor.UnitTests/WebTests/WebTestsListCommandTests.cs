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

public class WebTestsListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMonitorWebTestService _service;
    private readonly ILogger<WebTestsListCommand> _logger;
    private readonly WebTestsListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public WebTestsListCommandTests()
    {
        _service = Substitute.For<IMonitorWebTestService>();
        _logger = Substitute.For<ILogger<WebTestsListCommand>>();

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
        Assert.Equal("list", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
        Assert.Contains("Lists all web tests", command.Description);
    }

    [Fact]
    public void Name_ReturnsCorrectValue()
    {
        Assert.Equal("list", _command.Name);
    }

    [Fact]
    public void Title_ReturnsCorrectValue()
    {
        Assert.Equal("List all web tests in a subscription or resource group", _command.Title);
    }

    [Fact]
    public void Description_ContainsRequiredInformation()
    {
        var description = _command.Description;
        Assert.Contains("subscription", description);
        Assert.Contains("resource group", description);
        Assert.Contains("web tests", description);
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

        // WebTestsListCommand specific options
        Assert.Contains("resource-group", options);

        // Verify resource group is optional
        var resourceGroupOption = command.Options.First(o => o.Name == "resource-group");
        Assert.False(resourceGroupOption.IsRequired);
    }

    #endregion

    #region Option Binding Tests

    [Fact]
    public async Task ExecuteAsync_BindsSubscriptionOnlyOptions()
    {
        // Arrange
        var args = new string[] { "--subscription", "sub1" };
        var expectedResults = new List<WebTestSummaryInfo>
        {
            new()
            {
                ResourceName = "test1",
                Location = "eastus",
                ResourceGroup = "rg1",
                Kind = "ping",
                AppInsightsComponentId = "/subscriptions/sub1/resourceGroups/rg1/providers/Microsoft.Insights/components/appinsights1"
            }
        };

        _service.ListWebTests("sub1", null, Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedResults);

        var parseResult = _parser.Parse(args);

        // Act
        await _command.ExecuteAsync(_context, parseResult);

        // Assert
        await _service.Received(1).ListWebTests("sub1", null, Arg.Any<RetryPolicyOptions?>());
    }

    [Fact]
    public async Task ExecuteAsync_BindsResourceGroupOptions()
    {
        // Arrange
        var args = new string[] { "--subscription", "sub1", "--resource-group", "rg1" };
        var expectedResults = new List<WebTestSummaryInfo>
        {
            new()
            {
                ResourceName = "test1",
                Location = "eastus",
                ResourceGroup = "rg1",
                Kind = "ping",
                AppInsightsComponentId = "/subscriptions/sub1/resourceGroups/rg1/providers/Microsoft.Insights/components/appinsights1"
            }
        };

        _service.ListWebTests("sub1", "rg1", null, Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedResults);

        var parseResult = _parser.Parse(args);

        // Act
        await _command.ExecuteAsync(_context, parseResult);

        // Assert
        await _service.Received(1).ListWebTests("sub1", "rg1", null, Arg.Any<RetryPolicyOptions?>());
    }

    #endregion

    #region ExecuteAsync Tests - Success Scenarios

    [Theory]
    [InlineData("--subscription sub1")]
    [InlineData("--subscription sub1 --resource-group rg1")]
    public async Task ExecuteAsync_ValidInput_ReturnsSuccess(string args)
    {
        // Arrange
        var expectedResults = new List<WebTestSummaryInfo>
        {
            new()
            {
                ResourceName = "test1",
                Location = "eastus",
                ResourceGroup = "rg1",
                Kind = "ping",
                AppInsightsComponentId = "/subscriptions/sub1/resourceGroups/rg1/providers/Microsoft.Insights/components/appinsights1"
            },
            new()
            {
                ResourceName = "test2",
                Location = "westus",
                ResourceGroup = "rg1",
                Kind = "multistep",
                AppInsightsComponentId = "/subscriptions/sub1/resourceGroups/rg1/providers/Microsoft.Insights/components/appinsights2"
            }
        };

        _service.ListWebTests(Arg.Any<string>(), Arg.Is<string>(rg => rg != null), Arg.Any<string?>(), Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedResults);
        _service.ListWebTests(Arg.Any<string>(), Arg.Any<string?>(), Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedResults);

        var argArray = string.IsNullOrEmpty(args) ? Array.Empty<string>() : args.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var parseResult = _parser.Parse(argArray);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        Assert.Equal("Success", response.Message);

        // Verify the actual content of the results
        var results = GetResult(response.Results);
        Assert.NotNull(results);
        Assert.Equal(2, results.Count);
        Assert.Equal("test1", results[0].ResourceName);
        Assert.Equal("test2", results[1].ResourceName);
        Assert.Equal("eastus", results[0].Location);
        Assert.Equal("westus", results[1].Location);
    }

    [Fact]
    public async Task ExecuteAsync_EmptyResults_ReturnsSuccessWithNullResults()
    {
        // Arrange
        _service.ListWebTests(Arg.Any<string>(), Arg.Any<string?>(), Arg.Any<RetryPolicyOptions?>())
            .Returns(new List<WebTestSummaryInfo>());

        var parseResult = _parser.Parse(["--subscription", "sub1"]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.Null(response.Results);
        Assert.Equal("Success", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_CallsServiceWithCorrectParameters()
    {
        // Arrange
        _service.ListWebTests(Arg.Any<string>(), Arg.Is<string>(rg => rg != null), Arg.Any<string?>(), Arg.Any<RetryPolicyOptions?>())
            .Returns(new List<WebTestSummaryInfo>());

        var parseResult = _parser.Parse(["--subscription", "sub1", "--resource-group", "rg1"]);

        // Act
        await _command.ExecuteAsync(_context, parseResult);

        // Assert
        await _service.Received(1).ListWebTests("sub1", "rg1", null, Arg.Any<RetryPolicyOptions?>());
    }

    #endregion

    #region ExecuteAsync Tests - Validation Failures

    [Theory]
    [InlineData("")]                            // Missing subscription
    [InlineData("--resource-group rg1")]       // Missing subscription
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
        _service.When(x => x.ListWebTests(
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>()))
            .Do(x => throw expectedException);

        var parseResult = _parser.Parse(["--subscription", "sub1"]);

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
        _service.When(x => x.ListWebTests(
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>()))
            .Do(x => throw expectedException);

        var parseResult = _parser.Parse(["--subscription", "sub1"]);

        // Act
        await _command.ExecuteAsync(_context, parseResult);

        // Assert
        _logger.Received(1).Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Is<object>(o => o.ToString()!.Contains("Error listing web tests")),
            expectedException,
            Arg.Any<Func<object, Exception?, string>>());
    }

    #endregion

    #region Helper Methods

    private List<WebTestSummaryInfo>? GetResult(ResponseResult? result)
    {
        if (result == null)
        {
            return null;
        }
        var json = JsonSerializer.Serialize(result);
        return JsonSerializer.Deserialize<WebTestsListCommandResult>(json)?.webTests;
    }

    private record WebTestsListCommandResult(List<WebTestSummaryInfo> webTests);

    #endregion
}
