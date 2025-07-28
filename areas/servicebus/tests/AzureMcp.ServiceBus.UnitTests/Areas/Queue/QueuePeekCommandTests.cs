// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Messaging.ServiceBus;
using AzureMcp.Core.Options;
using AzureMcp.ServiceBus.Commands.Queue;
using AzureMcp.ServiceBus.Services;
using AzureMcp.Core.Models.Command;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.ServiceBus.UnitTests.Areas.Queue;

public class QueuePeekCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IServiceBusService _serviceBusService;
    private readonly ILogger<QueuePeekCommand> _logger;
    private readonly QueuePeekCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    // Test constants
    private const string SubscriptionId = "sub123";
    private const string QueueName = "testQueue";
    private const string NamespaceName = "test.servicebus.windows.net";
    private const int MaxMessages = 5;

    public QueuePeekCommandTests()
    {
        _serviceBusService = Substitute.For<IServiceBusService>();
        _logger = Substitute.For<ILogger<QueuePeekCommand>>();

        var collection = new ServiceCollection().AddSingleton(_serviceBusService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsPeekedMessages()
    {
        // Arrange
        var messages = new List<ServiceBusReceivedMessage>
        {
            CreateTestMessage("message1", "First test message"),
            CreateTestMessage("message2", "Second test message")
        };

        _serviceBusService.PeekQueueMessages(
            Arg.Is(NamespaceName),
            Arg.Is(QueueName),
            Arg.Is(MaxMessages),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()
        ).Returns(messages);

        var args = _parser.Parse(["--subscription", SubscriptionId, "--namespace", NamespaceName, "--queue", QueueName, "--max-messages", MaxMessages.ToString()]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<QueuePeekResult>(json);

        Assert.NotNull(result);
        Assert.Equal(2, result.Messages.Count);
        Assert.Equal("message1", result.Messages[0].MessageId);
        Assert.Equal("message2", result.Messages[1].MessageId);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEmptyListWhenNoMessages()
    {
        // Arrange
        var messages = new List<ServiceBusReceivedMessage>();

        _serviceBusService.PeekQueueMessages(
            Arg.Is(NamespaceName),
            Arg.Is(QueueName),
            Arg.Is(MaxMessages),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()
        ).Returns(messages);

        var args = _parser.Parse(["--subscription", SubscriptionId, "--namespace", NamespaceName, "--queue", QueueName, "--max-messages", MaxMessages.ToString()]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<QueuePeekResult>(json);

        Assert.NotNull(result);
        Assert.Empty(result.Messages);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesQueueNotFound()
    {
        // Arrange
        var serviceBusException = new ServiceBusException("Queue not found", ServiceBusFailureReason.MessagingEntityNotFound);

        _serviceBusService.PeekQueueMessages(
            Arg.Is(NamespaceName),
            Arg.Is(QueueName),
            Arg.Any<int>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()
        ).ThrowsAsync(serviceBusException);

        var args = _parser.Parse(["--subscription", SubscriptionId, "--namespace", NamespaceName, "--queue", QueueName]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(404, response.Status);
        Assert.Contains("Queue not found", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesGenericException()
    {
        // Arrange
        var expectedError = "Test error";

        _serviceBusService.PeekQueueMessages(
            Arg.Is(NamespaceName),
            Arg.Is(QueueName),
            Arg.Any<int>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()
        ).ThrowsAsync(new Exception(expectedError));

        var args = _parser.Parse(["--subscription", SubscriptionId, "--namespace", NamespaceName, "--queue", QueueName]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.StartsWith(expectedError, response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_UsesDefaultMaxMessagesWhenNotSpecified()
    {
        // Arrange
        var messages = new List<ServiceBusReceivedMessage>
        {
            CreateTestMessage("message1", "Test message")
        };

        _serviceBusService.PeekQueueMessages(
            Arg.Is(NamespaceName),
            Arg.Is(QueueName),
            Arg.Is(1), // Default is 1
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()
        ).Returns(messages);

        var args = _parser.Parse(["--subscription", SubscriptionId, "--namespace", NamespaceName, "--queue", QueueName]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<QueuePeekResult>(json);

        Assert.NotNull(result);
        Assert.Single(result.Messages);
    }

    private static ServiceBusReceivedMessage CreateTestMessage(string messageId, string body)
    {
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(
            body: BinaryData.FromString(body),
            messageId: messageId,
            sequenceNumber: 1);

        return message;
    }

    private class QueuePeekResult
    {
        [JsonPropertyName("messages")]
        public List<ServiceBusReceivedMessage> Messages { get; set; } = new();
    }
}
