// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Areas.ContainerApps.Commands.App;
using AzureMcp.Areas.ContainerApps.Models;
using AzureMcp.Areas.ContainerApps.Services;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Areas.ContainerApps.UnitTests.App;

[Trait("Area", "ContainerApps")]
public class AppListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IContainerAppsService _containerAppsService;
    private readonly ILogger<AppListCommand> _logger;
    private readonly AppListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public AppListCommandTests()
    {
        _containerAppsService = Substitute.For<IContainerAppsService>();
        _logger = Substitute.For<ILogger<AppListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_containerAppsService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_WithSubscription_ReturnsContainerApps()
    {
        // Arrange
        var subscriptionId = "test-subscription";
        var expectedApps = new List<ContainerApp>
        {
            new()
            {
                Name = "test-app-1",
                Id = "/subscriptions/test-sub/resourceGroups/test-rg/providers/Microsoft.App/containerApps/test-app-1",
                Location = "eastus",
                ResourceGroup = "test-rg"
            },
            new()
            {
                Name = "test-app-2",
                Id = "/subscriptions/test-sub/resourceGroups/test-rg/providers/Microsoft.App/containerApps/test-app-2",
                Location = "eastus",
                ResourceGroup = "test-rg"
            }
        };

        _containerAppsService.ListApps(Arg.Is(subscriptionId), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedApps);

        var parseResult = _parser.Parse(["--subscription", subscriptionId]);

        // Act
        var result = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, result.Status);
        Assert.NotNull(result.Results);
        await _containerAppsService.Received(1).ListApps(subscriptionId, null, null, null, Arg.Any<RetryPolicyOptions?>());
    }

    [Fact]
    public async Task ExecuteAsync_WithResourceGroup_FiltersCorrectly()
    {
        // Arrange
        var subscriptionId = "test-subscription";
        var resourceGroup = "test-rg";
        var expectedApps = new List<ContainerApp>
        {
            new()
            {
                Name = "test-app-1",
                ResourceGroup = resourceGroup
            }
        };

        _containerAppsService.ListApps(Arg.Is(subscriptionId), Arg.Is(resourceGroup), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedApps);

        var parseResult = _parser.Parse(["--subscription", subscriptionId, "--resource-group", resourceGroup]);

        // Act
        var result = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, result.Status);
        Assert.NotNull(result.Results);
        await _containerAppsService.Received(1).ListApps(subscriptionId, resourceGroup, null, null, Arg.Any<RetryPolicyOptions?>());
    }

    [Fact]
    public async Task ExecuteAsync_WithEnvironment_FiltersCorrectly()
    {
        // Arrange
        var subscriptionId = "test-subscription";
        var environment = "test-env";
        var expectedApps = new List<ContainerApp>
        {
            new()
            {
                Name = "test-app-1",
                ManagedEnvironmentId = "/subscriptions/test-sub/resourceGroups/test-rg/providers/Microsoft.App/managedEnvironments/test-env"
            }
        };

        _containerAppsService.ListApps(Arg.Is(subscriptionId), Arg.Any<string?>(), Arg.Is(environment), Arg.Any<string?>(), Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedApps);

        var parseResult = _parser.Parse(["--subscription", subscriptionId, "--environment", environment]);

        // Act
        var result = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, result.Status);
        Assert.NotNull(result.Results);
        await _containerAppsService.Received(1).ListApps(subscriptionId, null, environment, null, Arg.Any<RetryPolicyOptions?>());
    }

    [Fact]
    public async Task ExecuteAsync_ServiceThrows_HandlesException()
    {
        // Arrange
        var subscriptionId = "test-subscription";
        var errorMessage = "Service error";

        _containerAppsService.ListApps(Arg.Any<string>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<RetryPolicyOptions?>())
            .ThrowsAsync(new InvalidOperationException(errorMessage));

        var parseResult = _parser.Parse(["--subscription", subscriptionId]);

        // Act
        var result = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, result.Status);
        Assert.Contains(errorMessage, result.Message);
    }

    [Fact]
    public async Task ExecuteAsync_EmptyResults_ReturnsSuccess()
    {
        // Arrange
        var subscriptionId = "test-subscription";
        var expectedApps = new List<ContainerApp>();

        _containerAppsService.ListApps(Arg.Any<string>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedApps);

        var parseResult = _parser.Parse(["--subscription", subscriptionId]);

        // Act
        var result = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, result.Status);
        Assert.Equal("Success", result.Message);
        Assert.Null(result.Results);
    }

    [Theory]
    [InlineData("--subscription", "test-sub")]
    [InlineData("--subscription", "test-sub", "--resource-group", "test-rg")]
    [InlineData("--subscription", "test-sub", "--environment", "test-env")]
    [InlineData("--subscription", "test-sub", "--resource-group", "test-rg", "--environment", "test-env")]
    public void Parse_ValidArguments_ParsesCorrectly(params string[] args)
    {
        // Act
        var parseResult = _parser.Parse(args);

        // Assert
        Assert.Empty(parseResult.Errors);
        Assert.True(parseResult.CommandResult.Command.Name == "list");
    }
}
