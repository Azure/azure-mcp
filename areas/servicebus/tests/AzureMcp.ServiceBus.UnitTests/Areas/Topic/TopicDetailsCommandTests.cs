// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure;
using Azure.Messaging.ServiceBus;
using AzureMcp.ServiceBus.Commands.Topic;
using AzureMcp.ServiceBus.Models;
using AzureMcp.ServiceBus.Services;
using AzureMcp.Core.Models.Command;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.ServiceBus.UnitTests.Areas.Topic;

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
            DefaultMessageTimeToLive = TimeSpan.FromDays(14),
            MaxMessageSizeInKilobytes = 1024,
            SizeInBytes = 2048,
            SubscriptionCount = 3,
            EnablePartitioning = true,
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
        // write a json convertor that extends from EntityStatus 
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
