// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using AzureMcp.Core.Options;
using AzureMcp.ServiceBus.Commands.Topic;
using AzureMcp.ServiceBus.Services;
using AzureMcp.Core.Models.Command;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;
using static AzureMcp.ServiceBus.Commands.Topic.SubscriptionPeekCommand;
using AzureMcp.ServiceBus.UnitTests.Utilities.JsonConverters;

namespace AzureMcp.ServiceBus.UnitTests.Topic;

public class SubscriptionPeekCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IServiceBusService _serviceBusService;
    private readonly ILogger<SubscriptionPeekCommand> _logger;
    private readonly SubscriptionPeekCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    // Test constants
    private const string SubscriptionId = "sub123";
    private const string TopicName = "testTopic";
    private const string SubscriptionName = "testSubscription";
    private const string NamespaceName = "test.servicebus.windows.net";
    private const int MaxMessages = 5;

    public SubscriptionPeekCommandTests()
    {
        _serviceBusService = Substitute.For<IServiceBusService>();
        _logger = Substitute.For<ILogger<SubscriptionPeekCommand>>();

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

        _serviceBusService.PeekSubscriptionMessages(
            Arg.Is(NamespaceName),
            Arg.Is(TopicName),
            Arg.Is(SubscriptionName),
            Arg.Is(MaxMessages),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()
        ).Returns(messages);

        var args = _parser.Parse([
            "--subscription", SubscriptionId,
            "--namespace", NamespaceName,
            "--topic-name", TopicName,
            "--subscription-name", SubscriptionName,
            "--max-messages", MaxMessages.ToString()
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        // Extract the actual result from the ResponseResult wrapper
        var resultsType = response.Results.GetType();
        var resultField = resultsType.GetField("_result", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        object actualResult = response.Results;
        if (resultField != null)
        {
            var extractedResult = resultField.GetValue(response.Results);
            if (extractedResult != null)
            {
                actualResult = extractedResult;
            }
        }

        // Serialize and deserialize to test the body field handling
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        options.Converters.Add(new ServiceBusReceivedMessageConverter());

        var json = JsonSerializer.Serialize(actualResult, options);
        var result = JsonSerializer.Deserialize<SubscriptionPeekCommandResult>(json, options);

        Assert.NotNull(result);
        Assert.Equal(2, result.Messages.Count);
        Assert.Equal("message1", result.Messages[0].MessageId);
        Assert.Equal("message2", result.Messages[1].MessageId);

        // Verify that the body content is correctly preserved after JSON roundtrip
        Assert.NotNull(result.Messages[0].Body);
        Assert.NotNull(result.Messages[1].Body);

        // Check the body content
        try
        {
            var body1Bytes = result.Messages[0].Body.ToArray();
            var body2Bytes = result.Messages[1].Body.ToArray();

            var body1String = System.Text.Encoding.UTF8.GetString(body1Bytes);
            var body2String = System.Text.Encoding.UTF8.GetString(body2Bytes);

            Assert.Equal("First test message", body1String);
            Assert.Equal("Second test message", body2String);
        }
        catch (Exception ex)
        {
            // If this fails, we know the BinaryData is not properly constructed
            Assert.Fail($"Failed to get body content: {ex.Message}");
        }
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEmptyListWhenNoMessages()
    {
        // Arrange
        var messages = new List<ServiceBusReceivedMessage>();

        _serviceBusService.PeekSubscriptionMessages(
            Arg.Is(NamespaceName),
            Arg.Is(TopicName),
            Arg.Is(SubscriptionName),
            Arg.Is(MaxMessages),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()
        ).Returns(messages);

        var args = _parser.Parse([
            "--subscription", SubscriptionId,
            "--namespace", NamespaceName,
            "--topic-name", TopicName,
            "--subscription-name", SubscriptionName,
            "--max-messages", MaxMessages.ToString()
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        // Extract the actual result from the ResponseResult wrapper
        var resultsType = response.Results.GetType();
        var resultField = resultsType.GetField("_result", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        object actualResult = response.Results;
        if (resultField != null)
        {
            var extractedResult = resultField.GetValue(response.Results);
            if (extractedResult != null)
            {
                actualResult = extractedResult;
            }
        }

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        options.Converters.Add(new ServiceBusReceivedMessageConverter());
        var json = JsonSerializer.Serialize(actualResult, options);
        var result = JsonSerializer.Deserialize<SubscriptionPeekCommandResult>(json, options);

        Assert.NotNull(result);
        Assert.Empty(result.Messages);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesSubscriptionNotFound()
    {
        // Arrange
        var serviceBusException = new ServiceBusException("Subscription not found", ServiceBusFailureReason.MessagingEntityNotFound);

        _serviceBusService.PeekSubscriptionMessages(
            Arg.Is(NamespaceName),
            Arg.Is(TopicName),
            Arg.Is(SubscriptionName),
            Arg.Any<int>(),
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
        Assert.Contains("Subscription not found", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesGenericException()
    {
        // Arrange
        var expectedError = "Test error";

        _serviceBusService.PeekSubscriptionMessages(
            Arg.Is(NamespaceName),
            Arg.Is(TopicName),
            Arg.Is(SubscriptionName),
            Arg.Any<int>(),
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

    [Fact]
    public async Task ExecuteAsync_UsesDefaultMaxMessagesWhenNotSpecified()
    {
        // Arrange
        var messages = new List<ServiceBusReceivedMessage>
        {
            CreateTestMessage("message1", "Test message")
        };

        _serviceBusService.PeekSubscriptionMessages(
            Arg.Is(NamespaceName),
            Arg.Is(TopicName),
            Arg.Is(SubscriptionName),
            Arg.Is(1), // Default is 1
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()
        ).Returns(messages);

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

        // Extract the actual result from the ResponseResult wrapper
        var resultsType = response.Results.GetType();
        var resultField = resultsType.GetField("_result", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        object actualResult = response.Results;
        if (resultField != null)
        {
            var extractedResult = resultField.GetValue(response.Results);
            if (extractedResult != null)
            {
                actualResult = extractedResult;
            }
        }

        var options = new JsonSerializerOptions();
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.Converters.Add(new ServiceBusReceivedMessageConverter());
        var json = JsonSerializer.Serialize(actualResult, options);
        var result = JsonSerializer.Deserialize<SubscriptionPeekCommandResult>(json, options);

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
}
