// Simple test to validate the ResourceEventsGetCommand
using System.CommandLine;
using AzureMcp.Core.Models.Command;
using AzureMcp.ResourceHealth.Commands.ResourceEvents;
using AzureMcp.ResourceHealth.Models;
using AzureMcp.ResourceHealth.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.ResourceHealth.UnitTests.ResourceEvents;

public class ResourceEventsGetCommandSimpleTests
{
    [Fact]
    public void Command_ShouldHaveCorrectName()
    {
        var logger = Substitute.For<ILogger<ResourceEventsGetCommand>>();
        var command = new ResourceEventsGetCommand(logger);
        var cmd = command.GetCommand();
        
        Assert.Equal("get", cmd.Name);
    }

    [Fact]
    public void Command_ShouldHaveRequiredOptions()
    {
        var logger = Substitute.For<ILogger<ResourceEventsGetCommand>>();
        var command = new ResourceEventsGetCommand(logger);
        var cmd = command.GetCommand();
        
        var resourceIdOption = cmd.Options.FirstOrDefault(o => o.Name == "resourceId");
        Assert.NotNull(resourceIdOption);
        Assert.True(resourceIdOption.IsRequired);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnSuccess_WithValidData()
    {
        var resourceHealthService = Substitute.For<IResourceHealthService>();
        var logger = Substitute.For<ILogger<ResourceEventsGetCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(resourceHealthService);
        var serviceProvider = collection.BuildServiceProvider();

        var expectedEvents = new List<ServiceHealthEvent>
        {
            new()
            {
                Id = "test-event",
                Name = "test",
                Title = "Test Event",
                EventType = "HealthEvent",
                Status = "Active"
            }
        };

        var resourceId = "/subscriptions/12345678-1234-1234-1234-123456789012/resourceGroups/test-rg/providers/Microsoft.Compute/virtualMachines/test-vm";
        
        resourceHealthService.GetResourceEventsAsync(resourceId, Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int?>(), Arg.Any<string>(), Arg.Any<Core.Options.RetryPolicyOptions>())
            .Returns(expectedEvents);

        var command = new ResourceEventsGetCommand(logger);
        var args = command.GetCommand().Parse(["--subscription", "12345678-1234-1234-1234-123456789012", "--resourceId", resourceId]);
        var context = new CommandContext(serviceProvider);

        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);
    }
}
