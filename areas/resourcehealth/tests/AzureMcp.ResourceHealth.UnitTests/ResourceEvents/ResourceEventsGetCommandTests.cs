// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using AzureMcp.ResourceHealth.Commands.ResourceEvents;
using AzureMcp.ResourceHealth.Models;
using AzureMcp.ResourceHealth.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.ResourceHealth.UnitTests.ResourceEvents;

public class ResourceEventsGetCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IResourceHealthService _resourceHealthService;
    private readonly ILogger<ResourceEventsGetCommand> _logger;

    public ResourceEventsGetCommandTests()
    {
        _resourceHealthService = Substitute.For<IResourceHealthService>();
        _logger = Substitute.For<ILogger<ResourceEventsGetCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_resourceHealthService);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsResourceEvents_WhenEventsExist()
    {
        var resourceId = "/subscriptions/12345678-1234-1234-1234-123456789012/resourceGroups/test-rg/providers/Microsoft.Compute/virtualMachines/test-vm";
        var expectedEvents = new List<ServiceHealthEvent>
        {
            new()
            {
                Id = "/subscriptions/12345678-1234-1234-1234-123456789012/resourceGroups/test-rg/providers/Microsoft.Compute/virtualMachines/test-vm/providers/Microsoft.ResourceHealth/events/event1",
                Name = "event1",
                Title = "VM Unavailable",
                EventType = "HealthEvent",
                Status = "Resolved",
                Summary = "Virtual machine was unavailable",
                StartTime = DateTime.UtcNow.AddHours(-2),
                EndTime = DateTime.UtcNow.AddHours(-1),
                LastUpdateTime = DateTime.UtcNow.AddHours(-1)
            },
            new()
            {
                Id = "/subscriptions/12345678-1234-1234-1234-123456789012/resourceGroups/test-rg/providers/Microsoft.Compute/virtualMachines/test-vm/providers/Microsoft.ResourceHealth/events/event2",
                Name = "event2",
                Title = "Platform Maintenance",
                EventType = "PlannedMaintenance",
                Status = "Completed",
                Summary = "Scheduled platform maintenance completed",
                StartTime = DateTime.UtcNow.AddDays(-1),
                EndTime = DateTime.UtcNow.AddDays(-1).AddHours(2),
                LastUpdateTime = DateTime.UtcNow.AddDays(-1).AddHours(2)
            }
        };

        _resourceHealthService.GetResourceEventsAsync(resourceId, null, null, null, null, null, Arg.Any<RetryPolicyOptions>())
            .Returns(expectedEvents);

        var command = new ResourceEventsGetCommand(_logger);
        var args = command.GetCommand().Parse(["--subscription", "12345678-1234-1234-1234-123456789012", "--resourceId", resourceId]);
        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<ResourceEventsGetCommandResult>(json, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(result);
        Assert.Equal(2, result.Events.Count);
        Assert.Equal("HealthEvent", result.Events[0].EventType);
        Assert.Equal("PlannedMaintenance", result.Events[1].EventType);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsFilteredEvents_WhenFilterProvided()
    {
        var resourceId = "/subscriptions/12345678-1234-1234-1234-123456789012/resourceGroups/test-rg/providers/Microsoft.Compute/virtualMachines/test-vm";
        var filter = "eventType eq 'HealthEvent'";
        var expectedEvents = new List<ServiceHealthEvent>
        {
            new()
            {
                Id = "/subscriptions/12345678-1234-1234-1234-123456789012/resourceGroups/test-rg/providers/Microsoft.Compute/virtualMachines/test-vm/providers/Microsoft.ResourceHealth/events/event1",
                Name = "event1",
                Title = "VM Unavailable",
                EventType = "HealthEvent",
                Status = "Resolved",
                Summary = "Virtual machine was unavailable"
            }
        };

        _resourceHealthService.GetResourceEventsAsync(resourceId, filter, null, null, null, null, Arg.Any<RetryPolicyOptions>())
            .Returns(expectedEvents);

        var command = new ResourceEventsGetCommand(_logger);
        var args = command.GetCommand().Parse(["--subscription", "12345678-1234-1234-1234-123456789012", "--resourceId", resourceId, "--filter", filter]);
        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<ResourceEventsGetCommandResult>(json, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(result);
        Assert.Single(result.Events);
        Assert.Equal("HealthEvent", result.Events[0].EventType);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsLimitedEvents_WhenTopProvided()
    {
        var resourceId = "/subscriptions/12345678-1234-1234-1234-123456789012/resourceGroups/test-rg/providers/Microsoft.Compute/virtualMachines/test-vm";
        var top = 1;
        var expectedEvents = new List<ServiceHealthEvent>
        {
            new()
            {
                Id = "/subscriptions/12345678-1234-1234-1234-123456789012/resourceGroups/test-rg/providers/Microsoft.Compute/virtualMachines/test-vm/providers/Microsoft.ResourceHealth/events/event1",
                Name = "event1",
                Title = "VM Unavailable",
                EventType = "HealthEvent",
                Status = "Resolved",
                Summary = "Virtual machine was unavailable"
            }
        };

        _resourceHealthService.GetResourceEventsAsync(resourceId, null, null, null, top, null, Arg.Any<RetryPolicyOptions>())
            .Returns(expectedEvents);

        var command = new ResourceEventsGetCommand(_logger);
        var args = command.GetCommand().Parse(["--subscription", "12345678-1234-1234-1234-123456789012", "--resourceId", resourceId, "--top", top.ToString()]);
        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<ResourceEventsGetCommandResult>(json, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(result);
        Assert.Single(result.Events);
        Assert.Equal("HealthEvent", result.Events[0].EventType);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        var resourceId = "/subscriptions/12345678-1234-1234-1234-123456789012/resourceGroups/test-rg/providers/Microsoft.Compute/virtualMachines/test-vm";
        var expectedError = "Service error. To mitigate this issue, please refer to the troubleshooting guidelines here at https://aka.ms/azmcp/troubleshooting.";

        _resourceHealthService.GetResourceEventsAsync(resourceId, null, null, null, null, null, Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new Exception("Service error"));

        var command = new ResourceEventsGetCommand(_logger);
        var parser = new Parser(command.GetCommand());
        var args = parser.Parse(["--subscription", "12345678-1234-1234-1234-123456789012", "--resourceId", resourceId]);
        var context = new CommandContext(_serviceProvider);

        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Equal(expectedError, response.Message);
    }

    private record ResourceEventsGetCommandResult(List<ServiceHealthEvent> Events);
}
