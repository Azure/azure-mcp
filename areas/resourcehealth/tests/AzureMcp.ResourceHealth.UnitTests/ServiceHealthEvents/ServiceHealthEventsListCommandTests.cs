// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using AzureMcp.ResourceHealth.Commands.ServiceHealthEvents;
using AzureMcp.ResourceHealth.Models;
using AzureMcp.ResourceHealth.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.ResourceHealth.UnitTests.ServiceHealthEvents;

public class ServiceHealthEventsListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IResourceHealthService _resourceHealthService;
    private readonly ILogger<ServiceHealthEventsListCommand> _logger;

    public ServiceHealthEventsListCommandTests()
    {
        _resourceHealthService = Substitute.For<IResourceHealthService>();
        _logger = Substitute.For<ILogger<ServiceHealthEventsListCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_resourceHealthService);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsServiceHealthEvents_WhenEventsExist()
    {
        var subscriptionId = "12345678-1234-1234-1234-123456789012";
        var expectedEvents = new List<ServiceHealthEvent>
        {
            new()
            {
                Id = "/subscriptions/12345678-1234-1234-1234-123456789012/providers/Microsoft.ResourceHealth/events/event1",
                Name = "event1",
                Title = "Service Issue",
                EventType = "ServiceIssue",
                Status = "Active",
                Summary = "Service degradation in West US"
            },
            new()
            {
                Id = "/subscriptions/12345678-1234-1234-1234-123456789012/providers/Microsoft.ResourceHealth/events/event2",
                Name = "event2",
                Title = "Planned Maintenance",
                EventType = "PlannedMaintenance",
                Status = "Resolved",
                Summary = "Scheduled maintenance completed"
            }
        };

        _resourceHealthService.ListServiceHealthEventsAsync(subscriptionId, null, null, null, null, null, null, null, null, Arg.Any<RetryPolicyOptions>())
            .Returns(expectedEvents);

        var command = new ServiceHealthEventsListCommand(_logger);
        var args = command.GetCommand().Parse(["--subscription", subscriptionId]);
        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<ServiceHealthEventsListResult>(json, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(result);
        Assert.Equal(2, result.Events.Count);
        Assert.Equal("ServiceIssue", result.Events[0].EventType);
        Assert.Equal("PlannedMaintenance", result.Events[1].EventType);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsFilteredEvents_WhenEventTypeProvided()
    {
        var subscriptionId = "12345678-1234-1234-1234-123456789012";
        var eventType = "ServiceIssue";
        var expectedEvents = new List<ServiceHealthEvent>
        {
            new()
            {
                Id = "/subscriptions/12345678-1234-1234-1234-123456789012/providers/Microsoft.ResourceHealth/events/event1",
                Name = "event1",
                Title = "Service Issue",
                EventType = "ServiceIssue",
                Status = "Active",
                Summary = "Service degradation in West US"
            }
        };

        _resourceHealthService.ListServiceHealthEventsAsync(subscriptionId, null, eventType, null, null, null, null, null, null, Arg.Any<RetryPolicyOptions>())
            .Returns(expectedEvents);

        var command = new ServiceHealthEventsListCommand(_logger);
        var args = command.GetCommand().Parse(["--subscription", subscriptionId, "--event-type", eventType]);
        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<ServiceHealthEventsListResult>(json, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(result);
        Assert.Single(result.Events);
        Assert.Equal("ServiceIssue", result.Events[0].EventType);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        var subscriptionId = "12345678-1234-1234-1234-123456789012";
        var expectedError = "Service error. To mitigate this issue, please refer to the troubleshooting guidelines here at https://aka.ms/azmcp/troubleshooting.";

        _resourceHealthService.ListServiceHealthEventsAsync(subscriptionId, null, null, null, null, null, null, null, null, Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new Exception("Service error"));

        var command = new ServiceHealthEventsListCommand(_logger);
        var parser = new Parser(command.GetCommand());
        var args = parser.Parse(["--subscription", subscriptionId]);
        var context = new CommandContext(_serviceProvider);

        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Equal(expectedError, response.Message);
    }

    [Theory]
    [InlineData("--subscription")]
    public async Task ExecuteAsync_ReturnsError_WhenRequiredParameterIsMissing(string missingParameter)
    {
        var command = new ServiceHealthEventsListCommand(_logger);
        var args = command.GetCommand().Parse(
        [
            missingParameter == "--subscription" ? "" : "--subscription", "12345678-1234-1234-1234-123456789012",
        ]);

        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(400, response.Status);
        Assert.Equal($"Missing Required options: {missingParameter}", response.Message);
    }

    private record ServiceHealthEventsListResult(IReadOnlyList<ServiceHealthEvent> Events);
}
