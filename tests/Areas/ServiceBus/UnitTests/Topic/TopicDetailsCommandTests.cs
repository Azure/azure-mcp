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
using static AzureMcp.Areas.ServiceBus.Commands.Topic.TopicDetailsCommand;

namespace AzureMcp.Tests.Areas.ServiceBus.UnitTests.Topic;

[Trait("Area", "ServiceBus")]
public class TopicDetailsCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IServiceBusService _serviceBusService;
    private readonly ILogger<TopicDetailsCommand> _logger;
    private readonly TopicDetailsCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    // Test constants
    private const string SubscriptionId = "sub123";
    private const string TopicName = "testTopic";
    private const string NamespaceName = "test.servicebus.windows.net";

    public TopicDetailsCommandTests()
    {
        _serviceBusService = Substitute.For<IServiceBusService>();
        _logger = Substitute.For<ILogger<TopicDetailsCommand>>();

        var collection = new ServiceCollection().AddSingleton(_serviceBusService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsTopicDetails()
    {
        // Arrange
        var expectedDetails = new TopicDetails
        {
            Name = TopicName,
            Status = "Active",
            AccessedAt = DateTimeOffset.UtcNow.AddDays(-1),
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-30),
            DefaultMessageTimeToLive = TimeSpan.FromDays(14),
            MaxMessageSizeInKilobytes = 1024,
            SizeInBytes = 2048,
            SubscriptionCount = 3,
            EnableBatchedOperations = true,
            MaxSizeInMegabytes = 1024,
            ScheduledMessageCount = 0
        };

        _serviceBusService.GetTopicDetails(
            Arg.Is(NamespaceName),
            Arg.Is(TopicName),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()
        ).Returns(expectedDetails);

        var args = _parser.Parse(["--subscription", SubscriptionId, "--namespace", NamespaceName, "--topic-name", TopicName]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<TopicDetailsResult>(json);

        Assert.NotNull(result);
        Assert.Equal(TopicName, result.TopicDetails.Name);
        Assert.Equal(expectedDetails.Status, result.TopicDetails.Status);
        Assert.Equal(expectedDetails.SubscriptionCount, result.TopicDetails.SubscriptionCount);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesTopicNotFound()
    {
        // Arrange
        var serviceBusException = new ServiceBusException("Topic not found", ServiceBusFailureReason.MessagingEntityNotFound);

        _serviceBusService.GetTopicDetails(
            Arg.Is(NamespaceName),
            Arg.Is(TopicName),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()
        ).ThrowsAsync(serviceBusException);

        var args = _parser.Parse(["--subscription", SubscriptionId, "--namespace", NamespaceName, "--topic-name", TopicName]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(404, response.Status);
        Assert.Contains("Topic not found", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesGenericException()
    {
        // Arrange
        var expectedError = "Test error";

        _serviceBusService.GetTopicDetails(
            Arg.Is(NamespaceName),
            Arg.Is(TopicName),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()
        ).ThrowsAsync(new Exception(expectedError));

        var args = _parser.Parse(["--subscription", SubscriptionId, "--namespace", NamespaceName, "--topic-name", TopicName]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.StartsWith(expectedError, response.Message);
    }

    private class TopicDetailsResult
    {
        [JsonPropertyName("topicDetails")]
        public TopicDetails TopicDetails { get; set; } = new();
    }
}
