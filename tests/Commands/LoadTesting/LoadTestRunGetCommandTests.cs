// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.Text.Json;
using AzureMcp.Commands.LoadTesting.LoadTestRun;
using AzureMcp.Models.Command;
using AzureMcp.Models.LoadTesting.LoadTestRun;
using AzureMcp.Options;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

public class LoadTestRunGetCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILoadTestingService _service;
    private readonly ILogger<LoadTestRunGetCommand> _logger;
    private readonly LoadTestRunGetCommand _command;

    public LoadTestRunGetCommandTests()
    {
        _service = Substitute.For<ILoadTestingService>();
        _logger = Substitute.For<ILogger<LoadTestRunGetCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_service);
        _serviceProvider = collection.BuildServiceProvider();

        _command = new(_logger);
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("get", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsLoadTestRun_WhenExists()
    {
        // Arrange
        var expected = new LoadTestRunResource { TestId = "testId1", TestRunId = "testRunId1" };
        _service.GetLoadTestRunAsync(
            Arg.Is("sub123"), Arg.Is("loadTestName"), Arg.Is("run1"), Arg.Is("resourceGroup123"), Arg.Is("tenant123"), Arg.Any<RetryPolicyOptions>())
            .Returns(expected);

        var command = new LoadTestRunGetCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", "sub123",
            "--resource-group", "resourceGroup123",
            "--load-test-name", "loadTestName",
            "--load-testrun-id", "run1",
            "--tenant", "tenant123"
        ]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<LoadTestRunGetCommandResult>(json);

        Assert.NotNull(result);
        Assert.Equal(expected.TestId, result.LoadTestRun.TestId);
        Assert.Equal(expected.TestRunId, result.LoadTestRun.TestRunId);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesBadRequestErrors()
    {
        // Arrange
        var expected = new LoadTestRunResource();
        _service.GetLoadTestRunAsync(
            Arg.Is("sub123"), Arg.Is("loadTestName"), Arg.Is("run1"), Arg.Is("resourceGroup123"), Arg.Is("tenant123"), Arg.Any<RetryPolicyOptions>())
            .Returns(expected);

        var command = new LoadTestRunGetCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", "sub123",
            "--resource-group", "resourceGroup123",
            "--load-test-name", "loadTestName",
            "--tenant", "tenant123"
        ]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.Equal(400, response.Status);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceErrors()
    {
        // Arrange
        _service.GetLoadTestRunAsync(
            Arg.Is("sub123"), Arg.Is("loadTestName"), Arg.Is("run1"), Arg.Is("resourceGroup123"), Arg.Is("tenant123"), Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromException<LoadTestRunResource>(new Exception("Test error")));

        var command = new LoadTestRunGetCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", "sub123",
            "--resource-group", "resourceGroup123",
            "--load-test-name", "loadTestName",
            "--load-testrun-id", "run1",
            "--tenant", "tenant123"
        ]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Test error", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }

    private class LoadTestRunGetCommandResult
    {
        public LoadTestRunResource LoadTestRun { get; set; } = new();
    }
}
