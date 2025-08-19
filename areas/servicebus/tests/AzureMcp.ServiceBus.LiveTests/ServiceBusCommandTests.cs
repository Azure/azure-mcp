// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.Messaging.ServiceBus;
using AzureMcp.Core.Models.Option;
using AzureMcp.Core.Services.Azure.Authentication;
using AzureMcp.ServiceBus.Options;
using AzureMcp.Tests;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.ServiceBus.LiveTests
{
    public class ServiceBusCommandTests : CommandTestsBase, IClassFixture<LiveTestFixture>
    {
        private const string QueueName = "queue1";
        private const string TopicName = "topic1";
        private const string SubscriptionName = "subscription1";

        private readonly string _serviceBusNamespace;

        public ServiceBusCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output) : base(liveTestFixture, output)
        {
            _serviceBusNamespace = $"{Settings.ResourceBaseName}.servicebus.windows.net";
        }

        [Fact]
        public async Task Queue_peek_messages()
        {
            var numberOfMessages = 2;

            await SendTestMessages(QueueName, numberOfMessages);

            var result = await CallToolAsync(
                "azmcp_servicebus_queue_peek",
                new()
                {
                    { OptionDefinitions.Common.SubscriptionName, Settings.SubscriptionId },
                    { ServiceBusOptionDefinitions.QueueName, QueueName },
                    { ServiceBusOptionDefinitions.NamespaceName, _serviceBusNamespace},
                    { ServiceBusOptionDefinitions.MaxMessagesName, numberOfMessages.ToString() }
                });

            var messages = result.AssertProperty("messages");
            Assert.Equal(JsonValueKind.Array, messages.ValueKind);
            Assert.Equal(numberOfMessages, messages.GetArrayLength());
        }

        [Fact]
        public async Task Topic_subscription_peek_messages()
        {
            var numberOfMessages = 2;

            await SendTestMessages(TopicName, numberOfMessages);

            var result = await CallToolAsync(
                "azmcp_servicebus_topic_subscription_peek",
                new()
                {
                    { OptionDefinitions.Common.SubscriptionName, Settings.SubscriptionId },
                    { ServiceBusOptionDefinitions.NamespaceName, _serviceBusNamespace},
                    { ServiceBusOptionDefinitions.TopicName, TopicName },
                    { ServiceBusOptionDefinitions.SubscriptionName, SubscriptionName },
                    { ServiceBusOptionDefinitions.MaxMessagesName, numberOfMessages.ToString() }
                });

            var messages = result.AssertProperty("messages");
            Assert.Equal(JsonValueKind.Array, messages.ValueKind);
            Assert.Equal(numberOfMessages, messages.GetArrayLength());
        }

        [Fact]
        public async Task Queue_details()
        {
            var result = await CallToolAsync(
                "azmcp_servicebus_queue_details",
                new()
                {
                    { OptionDefinitions.Common.SubscriptionName, Settings.SubscriptionId },
                    { ServiceBusOptionDefinitions.QueueName, QueueName },
                    { ServiceBusOptionDefinitions.NamespaceName, _serviceBusNamespace},
                });

            var details = result.AssertProperty("queueDetails");
            Assert.Equal(JsonValueKind.Object, details.ValueKind);
        }

        [Fact]
        public async Task Topic_details()
        {
            var result = await CallToolAsync(
                "azmcp_servicebus_topic_details",
                new()
                {
                    { OptionDefinitions.Common.SubscriptionName, Settings.SubscriptionId },
                    { ServiceBusOptionDefinitions.TopicName, TopicName },
                    { ServiceBusOptionDefinitions.NamespaceName, _serviceBusNamespace},
                });

            var details = result.AssertProperty("topicDetails");
            Assert.Equal(JsonValueKind.Object, details.ValueKind);
        }

        [Fact]
        public async Task Subscription_details()
        {
            var result = await CallToolAsync(
                "azmcp_servicebus_topic_subscription_details",
                new()
                {
                    { OptionDefinitions.Common.SubscriptionName, Settings.SubscriptionId },
                    { ServiceBusOptionDefinitions.TopicName, TopicName },
                    { ServiceBusOptionDefinitions.SubscriptionName, SubscriptionName },
                    { ServiceBusOptionDefinitions.NamespaceName, _serviceBusNamespace},
                });

            var details = result.AssertProperty("subscriptionDetails");
            Assert.Equal(JsonValueKind.Object, details.ValueKind);
        }

        [Fact]
        public async Task Queue_peek_dead_letter_messages()
        {
            var numberOfMessages = 2;

            await SendTestMessagesToDeadLetter(QueueName, numberOfMessages);

            var result = await CallToolAsync(
                "azmcp_servicebus_queue_peek",
                new()
                {
                    { OptionDefinitions.Common.SubscriptionName, Settings.SubscriptionId },
                    { ServiceBusOptionDefinitions.QueueName, QueueName },
                    { ServiceBusOptionDefinitions.NamespaceName, _serviceBusNamespace},
                    { ServiceBusOptionDefinitions.MaxMessagesName, numberOfMessages.ToString() },
                    { ServiceBusOptionDefinitions.DeadLetterName, "true" }
                });

            var messages = result.AssertProperty("messages");
            Assert.Equal(JsonValueKind.Array, messages.ValueKind);
            Assert.Equal(numberOfMessages, messages.GetArrayLength());
        }

        [Fact]
        public async Task Topic_subscription_peek_dead_letter_messages()
        {
            var numberOfMessages = 2;

            await SendTestMessagesToDeadLetterSubscription(TopicName, SubscriptionName, numberOfMessages);

            var result = await CallToolAsync(
                "azmcp_servicebus_topic_subscription_peek",
                new()
                {
                    { OptionDefinitions.Common.SubscriptionName, Settings.SubscriptionId },
                    { ServiceBusOptionDefinitions.NamespaceName, _serviceBusNamespace},
                    { ServiceBusOptionDefinitions.TopicName, TopicName },
                    { ServiceBusOptionDefinitions.SubscriptionName, SubscriptionName },
                    { ServiceBusOptionDefinitions.MaxMessagesName, numberOfMessages.ToString() },
                    { ServiceBusOptionDefinitions.DeadLetterName, "true" }
                });

            var messages = result.AssertProperty("messages");
            Assert.Equal(JsonValueKind.Array, messages.ValueKind);
            Assert.Equal(numberOfMessages, messages.GetArrayLength());
        }

        private async Task SendTestMessages(string queueOrTopicName, int numberOfMessages)
        {
            var credentials = new CustomChainedCredential(Settings.TenantId);
            await using (var client = new ServiceBusClient(_serviceBusNamespace, credentials))
            await using (var sender = client.CreateSender(queueOrTopicName))
            {
                var batch = await sender.CreateMessageBatchAsync(TestContext.Current.CancellationToken);

                for (int i = 0; i < numberOfMessages; i++)
                {
                    Assert.True(batch.TryAddMessage(new ServiceBusMessage("Message " + i)),
                        $"Unable to add message #{i} to batch.");
                }

                await sender.SendMessagesAsync(batch, TestContext.Current.CancellationToken);
            }
        }

        private async Task SendTestMessagesToDeadLetter(string queueName, int numberOfMessages)
        {
            var credentials = new CustomChainedCredential(Settings.TenantId);
            await using (var client = new ServiceBusClient(_serviceBusNamespace, credentials))
            {
                await using (var sender = client.CreateSender(queueName))
                {
                    var batch = await sender.CreateMessageBatchAsync(TestContext.Current.CancellationToken);

                    for (int i = 0; i < numberOfMessages; i++)
                    {
                        Assert.True(batch.TryAddMessage(new ServiceBusMessage("Dead Letter Message " + i)),
                            $"Unable to add message #{i} to batch.");
                    }

                    await sender.SendMessagesAsync(batch, TestContext.Current.CancellationToken);
                }

                await using (var receiver = client.CreateReceiver(queueName))
                {
                    var messages = await receiver.ReceiveMessagesAsync(numberOfMessages, TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken);
                    
                    foreach (var message in messages)
                    {
                        await receiver.DeadLetterMessageAsync(message, "Test", "Message moved to dead letter for testing", TestContext.Current.CancellationToken);
                    }
                }
            }
        }

        private async Task SendTestMessagesToDeadLetterSubscription(string topicName, string subscriptionName, int numberOfMessages)
        {
            var credentials = new CustomChainedCredential(Settings.TenantId);
            await using (var client = new ServiceBusClient(_serviceBusNamespace, credentials))
            {
                await using (var sender = client.CreateSender(topicName))
                {
                    var batch = await sender.CreateMessageBatchAsync(TestContext.Current.CancellationToken);

                    for (int i = 0; i < numberOfMessages; i++)
                    {
                        Assert.True(batch.TryAddMessage(new ServiceBusMessage("Dead Letter Message " + i)),
                            $"Unable to add message #{i} to batch.");
                    }

                    await sender.SendMessagesAsync(batch, TestContext.Current.CancellationToken);
                }

                await using (var receiver = client.CreateReceiver(topicName, subscriptionName))
                {
                    var messages = await receiver.ReceiveMessagesAsync(numberOfMessages, TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken);
                    
                    foreach (var message in messages)
                    {
                        await receiver.DeadLetterMessageAsync(message, "Test", "Message moved to dead letter for testing", TestContext.Current.CancellationToken);
                    }
                }
            }
        }
    }
}
