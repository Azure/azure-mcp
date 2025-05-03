// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace AzureMcp.Commands.Server.Tests;

public class ServiceStartCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ServiceStartCommand _command;

    public ServiceStartCommandTests()
    {
        var services = new ServiceCollection();
        services.AddSingleton(Substitute.For<ICacheService>());
        services.AddSingleton(Substitute.For<ISubscriptionService>());
        services.AddSingleton(Substitute.For<IStorageService>());
        services.AddSingleton(Substitute.For<ICosmosService>());
        services.AddSingleton(Substitute.For<IMonitorService>());
        services.AddSingleton(Substitute.For<IResourceGroupService>());
        services.AddSingleton(Substitute.For<IAppConfigService>());
        services.AddSingleton<CommandFactory>();

        _serviceProvider = services.BuildServiceProvider();
        _command = new(_serviceProvider);
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        // Arrange & Act
        var command = new ServiceStartCommand(_serviceProvider);

        // Assert
        Assert.Equal("start", command.GetCommand().Name);
        Assert.Equal("Starts Azure MCP Server.", command.GetCommand().Description);
    }

    [Fact]
    public void GetArguments_ReturnsExpectedArguments()
    {
        // Arrange
        var command = new ServiceStartCommand(_serviceProvider);

        // Act
        var args = command.GetArguments()?.ToList();

        // Assert
        Assert.NotNull(args);
        Assert.Equal(2, args.Count);
        Assert.Contains(args, a => a.Name == ArgumentDefinitions.Service.Transport.Name);
        Assert.Contains(args, a => a.Name == ArgumentDefinitions.Service.Port.Name);
    }

    [Fact]
    public void ClearArguments_RemovesAllArguments()
    {
        // Arrange
        var command = new ServiceStartCommand(_serviceProvider);

        // Act
        command.ClearArguments();

        // Assert
        var arguments = command.GetArguments();
        Assert.NotNull(arguments);
        Assert.Empty(arguments);
    }

    [Fact]
    public void AddArgument_AddsNewArgument()
    {
        // Arrange
        var command = new ServiceStartCommand(_serviceProvider);
        var newArg = new ArgumentDefinition<string>("test", "test description");
        var arguments = command.GetArguments();

        Assert.NotNull(arguments);

        var initialCount = arguments.Count();

        // Act
        command.AddArgument(newArg);

        // Assert
        Assert.Equal(initialCount + 1, arguments.Count());
        Assert.Contains(arguments, a => a.Name == "test");
    }
}
