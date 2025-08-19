// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using AzureMcp.ServiceBus.Commands.Queue;
using AzureMcp.ServiceBus.Services;
using AzureMcp.Core.Options;
using AzureMcp.Core.Models.Command;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;
using static AzureMcp.ServiceBus.Commands.Queue.QueuePeekCommand;

namespace AzureMcp.ServiceBus.UnitTests.Queue;

[Trait("Area", "ServiceBus")]
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
    public async Task ExecuteAsync_ReturnsMessages()
    {
        // Arrange
        var expectedMessages = new List<ServiceBusReceivedMessage>
        {
            ServiceBusModelFactory.ServiceBusReceivedMessage(body: new BinaryData("Message 1")),
            ServiceBusModelFactory.ServiceBusReceivedMessage(body: new BinaryData("Message 2"))
        };

        _serviceBusService.PeekQueueMessages(
            Arg.Is(NamespaceName),
            Arg.Is(QueueName),
            Arg.Is(10),
            Arg.Is(false),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()
        ).Returns(expectedMessages);

        var args = _parser.Parse([
            "--subscription", SubscriptionId,
            "--namespace", NamespaceName,
            "--queue", QueueName,
            "--max-messages", "10"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);
        Assert.NotNull(response.Results);
        
        // Verify the service was called with correct parameters
        await _serviceBusService.Received(1).PeekQueueMessages(
            NamespaceName,
            QueueName,
            10,
            false,
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsDeadLetterMessages()
    {
        // Arrange
        var expectedMessages = new List<ServiceBusReceivedMessage>
        {
            ServiceBusModelFactory.ServiceBusReceivedMessage(body: new BinaryData("Dead Letter Message 1")),
            ServiceBusModelFactory.ServiceBusReceivedMessage(body: new BinaryData("Dead Letter Message 2"))
        };

        _serviceBusService.PeekQueueMessages(
            Arg.Is(NamespaceName),
            Arg.Is(QueueName),
            Arg.Is(5),
            Arg.Is(true),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()
        ).Returns(expectedMessages);

        var args = _parser.Parse([
            "--subscription", SubscriptionId,
            "--namespace", NamespaceName,
            "--queue", QueueName,
            "--max-messages", "5",
            "--dead-letter", "true"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);
        Assert.NotNull(response.Results);
        
        // Verify the service was called with correct parameters for dead letter
        await _serviceBusService.Received(1).PeekQueueMessages(
            NamespaceName,
            QueueName,
            5,
            true,
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEmptyListWhenNoMessages()
    {
        // Arrange
        _serviceBusService.PeekQueueMessages(
            Arg.Is(NamespaceName),
            Arg.Is(QueueName),
            Arg.Is(1),
            Arg.Is(false),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()
        ).Returns(new List<ServiceBusReceivedMessage>());

        var args = _parser.Parse([
            "--subscription", SubscriptionId,
            "--namespace", NamespaceName,
            "--queue", QueueName
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var json = JsonSerializer.Serialize(response.Results, options);
        var result = JsonSerializer.Deserialize<QueuePeekCommandResult>(json, options);
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
            Arg.Any<bool>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()
        ).ThrowsAsync(serviceBusException);

        var args = _parser.Parse([
            "--subscription", SubscriptionId,
            "--namespace", NamespaceName,
            "--queue", QueueName
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

        _serviceBusService.PeekQueueMessages(
            Arg.Is(NamespaceName),
            Arg.Is(QueueName),
            Arg.Any<int>(),
            Arg.Any<bool>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()
        ).ThrowsAsync(new Exception(expectedError));

        var args = _parser.Parse([
            "--subscription", SubscriptionId,
            "--namespace", NamespaceName,
            "--queue", QueueName
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.StartsWith(expectedError, response.Message);
    }

    [Theory]
    [InlineData("--subscription sub123 --namespace test.servicebus.windows.net --queue testQueue", true)]
    [InlineData("--namespace test.servicebus.windows.net --queue testQueue", false)]  // Missing subscription
    [InlineData("--subscription sub123 --queue testQueue", false)]   // Missing namespace
    [InlineData("--subscription sub123 --namespace test.servicebus.windows.net", false)] // Missing queue
    [InlineData("", false)]  // Missing all required options
    public async Task ExecuteAsync_ValidatesRequiredParameters(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            var expectedMessages = new List<ServiceBusReceivedMessage>
            {
                ServiceBusModelFactory.ServiceBusReceivedMessage(body: new BinaryData("Message 1"))
            };

            _serviceBusService.PeekQueueMessages(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<int>(),
                Arg.Any<bool>(),
                Arg.Any<string>(),
                Arg.Any<RetryPolicyOptions>())
                .Returns(expectedMessages);
        }

        var parseResult = _parser.Parse(args.Split(' ', StringSplitOptions.RemoveEmptyEntries));

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        if (shouldSucceed)
        {
            Assert.Equal(200, response.Status);
            Assert.Equal("Success", response.Message);
        }
        else
        {
            Assert.Equal(400, response.Status);
            Assert.Contains("required", response.Message.ToLower());
        }
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    public async Task ExecuteAsync_RespectMaxMessagesParameter(int maxMessages)
    {
        // Arrange
        var expectedMessages = new List<ServiceBusReceivedMessage>();
        for (int i = 0; i < maxMessages; i++)
        {
            expectedMessages.Add(ServiceBusModelFactory.ServiceBusReceivedMessage(body: new BinaryData($"Message {i}")));
        }

        _serviceBusService.PeekQueueMessages(
            Arg.Is(NamespaceName),
            Arg.Is(QueueName),
            Arg.Is(maxMessages),
            Arg.Is(false),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()
        ).Returns(expectedMessages);

        var args = _parser.Parse([
            "--subscription", SubscriptionId,
            "--namespace", NamespaceName,
            "--queue", QueueName,
            "--max-messages", maxMessages.ToString()
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        await _serviceBusService.Received(1).PeekQueueMessages(
            NamespaceName,
            QueueName,
            maxMessages,
            false,
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_DefaultsToOneMessageWhenMaxMessagesNotSpecified()
    {
        // Arrange
        var expectedMessages = new List<ServiceBusReceivedMessage>
        {
            ServiceBusModelFactory.ServiceBusReceivedMessage(body: new BinaryData("Message 1"))
        };

        _serviceBusService.PeekQueueMessages(
            Arg.Is(NamespaceName),
            Arg.Is(QueueName),
            Arg.Is(1),
            Arg.Is(false),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()
        ).Returns(expectedMessages);

        var args = _parser.Parse([
            "--subscription", SubscriptionId,
            "--namespace", NamespaceName,
            "--queue", QueueName
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        await _serviceBusService.Received(1).PeekQueueMessages(
            NamespaceName,
            QueueName,
            1,
            false,
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>());
    }
}