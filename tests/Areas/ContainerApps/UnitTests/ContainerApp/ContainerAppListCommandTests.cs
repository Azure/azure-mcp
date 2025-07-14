// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.ContainerApps.Commands.ContainerApp;
using AzureMcp.Areas.ContainerApps.Services;
using AzureMcp.Commands;
using AzureMcp.Models.Command;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Areas.ContainerApps.UnitTests.ContainerApp;

public class ContainerAppListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IContainerAppsService _containerAppsService;
    private readonly ILogger<ContainerAppListCommand> _logger;
    private readonly ContainerAppListCommand _command;

    public ContainerAppListCommandTests()
    {
        _containerAppsService = Substitute.For<IContainerAppsService>();
        _logger = Substitute.For<ILogger<ContainerAppListCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_containerAppsService);
        _serviceProvider = collection.BuildServiceProvider();

        _command = new(_logger);
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("list", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
        Assert.Contains("Container Apps", command.Description);
    }

    [Theory]
    [InlineData("--subscription test-sub", true)]
    [InlineData("--subscription test-sub --resource-group test-rg", true)]
    [InlineData("", false)]
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            _containerAppsService.ListContainerAppsAsync(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<AzureMcp.Options.RetryPolicyOptions>())
                .Returns(new List<ContainerAppInfo>
                {
                    new("test-app", "test-rg", "East US", "Running", "test-app.example.com")
                });
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
            Assert.Equal("Success", response.Message);
        }
        else
        {
            Assert.Contains("required", response.Message.ToLower());
        }
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceErrors()
    {
        // Arrange
        _containerAppsService.ListContainerAppsAsync(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<AzureMcp.Options.RetryPolicyOptions>())
            .Returns(Task.FromException<List<ContainerAppInfo>>(new Exception("Test error")));

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--subscription test-sub");

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Test error", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesEmptyResults()
    {
        // Arrange
        _containerAppsService.ListContainerAppsAsync(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<AzureMcp.Options.RetryPolicyOptions>())
            .Returns(new List<ContainerAppInfo>());

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--subscription test-sub");

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.Null(response.Results);
        Assert.Equal("Success", response.Message);
    }
}