// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Messaging.ServiceBus;
using AzureMcp.Areas.ServiceBus.Commands.Topic;
using AzureMcp.Areas.ServiceBus.Models;
using AzureMcp.Areas.ServiceBus.Services;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;
using static AzureMcp.Areas.ServiceBus.Commands.Topic.SubscriptionDetailsCommand;

namespace AzureMcp.Tests.Areas.ServiceBus.UnitTests.Topic;

[Trait("Area", "ServiceBus")]
public class SubscriptionDetailsCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IServiceBusService _serviceBusService;
    private readonly ILogger<SubscriptionDetailsCommand> _logger;
    private readonly SubscriptionDetailsCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    // Test constants
    private const string SubscriptionId = "sub123";
    private const string TopicName = "testTopic";
    private const string SubscriptionName = "testSubscription";
    private const string NamespaceName = "test.servicebus.windows.net";

    public SubscriptionDetailsCommandTests()
    {
        _serviceBusService = Substitute.For<IServiceBusService>();
        _logger = Substitute.For<ILogger<SubscriptionDetailsCommand>>();

        var collection = new ServiceCollection().AddSingleton(_serviceBusService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new();
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsSubscriptionDetails()
    {
        // Arrange
        var expectedDetails = new SubscriptionDetails
        {
            Name = SubscriptionName,
            TopicName = TopicName,
            Status = Azure.Messaging.ServiceBus.Administration.EntityStatus.Active,
            DefaultMessageTimeToLive = TimeSpan.FromDays(14),
            LockDuration = TimeSpan.FromMinutes(1),
            MaxDeliveryCount = 10,
            ActiveMessageCount = 5,
            DeadLetterMessageCount = 0,
            TransferMessageCount = 0,
            TransferDeadLetterMessageCount = 0
        };

        _serviceBusService.GetSubscriptionDetails(
            Arg.Is(NamespaceName),
            Arg.Is(TopicName),
            Arg.Is(SubscriptionName),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()
        ).Returns(expectedDetails);

        var args = _parser.Parse([
            "--subscription", SubscriptionId,
            "--namespace", NamespaceName,
            "--topic-name", TopicName,
            "--subscription-name", SubscriptionName
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<SubscriptionDetailsResult>(json);

        Assert.NotNull(result);
        Assert.Equal(SubscriptionName, result.SubscriptionDetails.Name);
        Assert.Equal(TopicName, result.SubscriptionDetails.TopicName);
        Assert.Equal(expectedDetails.Status, result.SubscriptionDetails.Status);
        Assert.Equal(expectedDetails.ActiveMessageCount, result.SubscriptionDetails.ActiveMessageCount);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesSubscriptionNotFound()
    {
        // Arrange
        var serviceBusException = new ServiceBusException("Subscription not found", ServiceBusFailureReason.MessagingEntityNotFound);

        _serviceBusService.GetSubscriptionDetails(
            Arg.Is(NamespaceName),
            Arg.Is(TopicName),
            Arg.Is(SubscriptionName),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()
        ).ThrowsAsync(serviceBusException);

        var args = _parser.Parse([
            "--subscription", SubscriptionId,
            "--namespace", NamespaceName,
            "--topic-name", TopicName,
            "--subscription-name", SubscriptionName
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(404, response.Status);
        Assert.Contains("not found", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesGenericException()
    {
        // Arrange
        var expectedError = "Test error";

        _serviceBusService.GetSubscriptionDetails(
            Arg.Is(NamespaceName),
            Arg.Is(TopicName),
            Arg.Is(SubscriptionName),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()
        ).ThrowsAsync(new Exception(expectedError));

        var args = _parser.Parse([
            "--subscription", SubscriptionId,
            "--namespace", NamespaceName,
            "--topic-name", TopicName,
            "--subscription-name", SubscriptionName
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.StartsWith(expectedError, response.Message);
    }

    private class SubscriptionDetailsResult
    {
        [JsonPropertyName("subscriptionDetails")]
        public SubscriptionDetails SubscriptionDetails { get; set; } = new();
    }
}
