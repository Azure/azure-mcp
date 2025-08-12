// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.ServiceBus.UnitTests;

public class ServiceBusSetupTests
{
    private readonly ServiceBusSetup _setup;
    private readonly ILoggerFactory _loggerFactory;
    private readonly CommandGroup _rootGroup;

    public ServiceBusSetupTests()
    {
        _setup = new ServiceBusSetup();
        _loggerFactory = Substitute.For<ILoggerFactory>();
        _loggerFactory.CreateLogger<ServiceBusSetup>().Returns(Substitute.For<ILogger<ServiceBusSetup>>());
        _rootGroup = new CommandGroup("root", "Root command group");
    }

    [Fact]
    public void RegisterCommands_CreatesServiceBusGroupWithCorrectDescription()
    {
        // Act
        _setup.RegisterCommands(_rootGroup, _loggerFactory);

        // Assert
        var serviceBusGroup = _rootGroup.SubGroups.FirstOrDefault(g => g.Name == "servicebus");
        Assert.NotNull(serviceBusGroup);
        
        // Verify the enhanced description contains key elements
        Assert.Contains("messaging infrastructure", serviceBusGroup.Description);
        Assert.Contains("reliable asynchronous communication", serviceBusGroup.Description);
        Assert.Contains("enterprise application integration", serviceBusGroup.Description);
        Assert.Contains("point-to-point communication", serviceBusGroup.Description);
        Assert.Contains("publish-subscribe messaging patterns", serviceBusGroup.Description);
        Assert.Contains("decoupled application architectures", serviceBusGroup.Description);
        Assert.Contains("Do not use this tool for real-time communication", serviceBusGroup.Description);
        Assert.Contains("direct API calls, database operations", serviceBusGroup.Description);
        Assert.Contains("dead letter handling", serviceBusGroup.Description);
        Assert.Contains("enterprise integration patterns", serviceBusGroup.Description);
    }

    [Fact]
    public void RegisterCommands_CreatesExpectedSubGroups()
    {
        // Act
        _setup.RegisterCommands(_rootGroup, _loggerFactory);

        // Assert
        var serviceBusGroup = _rootGroup.SubGroups.FirstOrDefault(g => g.Name == "servicebus");
        Assert.NotNull(serviceBusGroup);

        // Verify queue subgroup exists
        var queueGroup = serviceBusGroup.SubGroups.FirstOrDefault(g => g.Name == "queue");
        Assert.NotNull(queueGroup);

        // Verify topic subgroup exists
        var topicGroup = serviceBusGroup.SubGroups.FirstOrDefault(g => g.Name == "topic");
        Assert.NotNull(topicGroup);

        // Verify subscription subgroup exists under topic
        var subscriptionGroup = topicGroup.SubGroups.FirstOrDefault(g => g.Name == "subscription");
        Assert.NotNull(subscriptionGroup);
    }

    [Fact]
    public void ConfigureServices_RegistersServiceBusService()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        _setup.ConfigureServices(services);

        // Assert
        var serviceDescriptor = services.FirstOrDefault(s => s.ServiceType.Name == "IServiceBusService");
        Assert.NotNull(serviceDescriptor);
        Assert.Equal(ServiceLifetime.Singleton, serviceDescriptor.Lifetime);
    }
}