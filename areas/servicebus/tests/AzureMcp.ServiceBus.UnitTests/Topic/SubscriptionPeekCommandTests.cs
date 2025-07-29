// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
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

    private class ServiceBusReceivedMessageConverter : JsonConverter<ServiceBusReceivedMessage>
    {
        public override ServiceBusReceivedMessage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? messageId = null;
            string? bodyContent = null;
            long sequenceNumber = 0;

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token");
            }

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    break;

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string? propertyName = reader.GetString();
                    reader.Read();

                    switch (propertyName)
                    {
                        case "messageId" or "MessageId":
                            messageId = reader.GetString();
                            break;
                        case "body" or "Body":
                            if (reader.TokenType == JsonTokenType.String)
                            {
                                bodyContent = reader.GetString();
                            }
                            else if (reader.TokenType == JsonTokenType.StartObject)
                            {
                                // Handle BinaryData object serialization - it gets serialized as an object
                                // with properties like { "data": "base64string" } or similar
                                using var doc = JsonDocument.ParseValue(ref reader);
                                var bodyElement = doc.RootElement;

                                // BinaryData often serializes as empty object {}, so we need to handle this
                                // In this case, we can't recover the original content from JSON
                                if (bodyElement.EnumerateObject().Any())
                                {
                                    // Try different property names that BinaryData might use
                                    if (bodyElement.TryGetProperty("data", out var dataElement))
                                    {
                                        var base64String = dataElement.GetString();
                                        if (!string.IsNullOrEmpty(base64String))
                                        {
                                            try
                                            {
                                                var bytes = Convert.FromBase64String(base64String);
                                                bodyContent = System.Text.Encoding.UTF8.GetString(bytes);
                                            }
                                            catch
                                            {
                                                // If base64 decode fails, use as-is
                                                bodyContent = base64String;
                                            }
                                        }
                                    }
                                    else if (bodyElement.TryGetProperty("value", out var valueElement))
                                    {
                                        bodyContent = valueElement.GetString();
                                    }
                                    else
                                    {
                                        // Fallback - just get the raw JSON as string
                                        bodyContent = bodyElement.GetRawText();
                                    }
                                }
                                else
                                {
                                    // Empty object - BinaryData with no content info
                                    // We can't recover the original content, so use empty string
                                    bodyContent = "";
                                }
                            }
                            else if (reader.TokenType == JsonTokenType.Null)
                            {
                                bodyContent = null;
                            }
                            break;
                        case "sequenceNumber" or "SequenceNumber":
                            if (reader.TokenType == JsonTokenType.String && long.TryParse(reader.GetString(), out long seq))
                                sequenceNumber = seq;
                            else if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt64(out long seqNum))
                                sequenceNumber = seqNum;
                            break;
                        default:
                            // Skip unknown properties
                            reader.Skip();
                            break;
                    }
                }
            }

            return ServiceBusModelFactory.ServiceBusReceivedMessage(
                body: !string.IsNullOrEmpty(bodyContent)
                    ? BinaryData.FromBytes(System.Text.Encoding.UTF8.GetBytes(bodyContent))
                    : BinaryData.FromBytes(System.Text.Encoding.UTF8.GetBytes("")),
                messageId: messageId ?? "",
                sequenceNumber: sequenceNumber);
        }

        public override void Write(Utf8JsonWriter writer, ServiceBusReceivedMessage value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("messageId", value.MessageId);
            writer.WriteNumber("sequenceNumber", value.SequenceNumber);
            writer.WriteString("enqueuedTime", value.EnqueuedTime.ToString("O"));
            writer.WriteNumber("deliveryCount", value.DeliveryCount);

            // Serialize the message body as string from BinaryData
            string? bodyContent = null;

            if (value.Body != null)
            {
                try
                {
                    // First try the standard approaches
                    var bodyString = value.Body.ToString();
                    if (!string.IsNullOrEmpty(bodyString))
                    {
                        bodyContent = bodyString;
                    }
                }
                catch
                {
                    // ToString failed, try other methods
                }

                if (bodyContent == null)
                {
                    try
                    {
                        // Try ToMemory approach
                        var memory = value.Body.ToMemory();
                        var bytes = memory.ToArray();
                        if (bytes != null && bytes.Length > 0)
                        {
                            bodyContent = System.Text.Encoding.UTF8.GetString(bytes);
                        }
                    }
                    catch
                    {
                        // ToMemory failed, try reflection
                    }
                }

                if (bodyContent == null)
                {
                    try
                    {
                        // Try reflection to access private _data field
                        var binaryDataType = value.Body.GetType();
                        var dataField = binaryDataType.GetField("_data", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        if (dataField != null)
                        {
                            var data = dataField.GetValue(value.Body);
                            if (data is byte[] bytes && bytes.Length > 0)
                            {
                                bodyContent = System.Text.Encoding.UTF8.GetString(bytes);
                            }
                        }
                    }
                    catch
                    {
                        // Reflection failed
                    }
                }

                if (bodyContent != null)
                {
                    writer.WriteString("body", bodyContent);
                }
                else
                {
                    // Final fallback
                    writer.WriteString("body", "");
                }
            }
            else
            {
                writer.WriteNull("body");
            }

            // Add application properties if any
            if (value.ApplicationProperties?.Count > 0)
            {
                writer.WriteStartObject("applicationProperties");
                foreach (var prop in value.ApplicationProperties)
                {
                    writer.WritePropertyName(prop.Key);
                    JsonSerializer.Serialize(writer, prop.Value, options);
                }
                writer.WriteEndObject();
            }

            writer.WriteEndObject();
        }
    }
}
