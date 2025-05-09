// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.Messaging.ServiceBus;
using AzureMcp.Services.Azure.Authentication;
using AzureMcp.Tests.Client.Helpers;
using Xunit;
using static AzureMcp.Models.Argument.ArgumentDefinitions;

namespace AzureMcp.Tests.Client
{
    public class ServiceBusCommandTests : CommandTestsBase,
    IClassFixture<McpClientFixture>, IClassFixture<LiveTestSettingsFixture>
    {
        private readonly string _serviceBusNamespace;

        public ServiceBusCommandTests(McpClientFixture mcpClient, LiveTestSettingsFixture liveTestSettings, ITestOutputHelper output) : base(mcpClient, liveTestSettings, output)
        {
            _serviceBusNamespace = $"{Settings.ResourceBaseName}.servicebus.windows.net";
        }

        [Fact]
        [Trait("Category", "Live")]
        public async Task Queue_peek_messages()
        {
            var queueName = "queue1";
            var numberOfMessages = 2;

            await SendTestMessages(queueName, numberOfMessages);

            var result = await CallToolAsync(
                "azmcp-servicebus-queue-peek",
                new()
                {
                    { Common.SubscriptionName, Settings.SubscriptionId },
                    { ServiceBus.QueueName, queueName },
                    { ServiceBus.NamespaceName, _serviceBusNamespace},
                    { ServiceBus.MaxMessagesName, numberOfMessages.ToString() }
                });

            var messages = result.AssertProperty("messages");
            Assert.Equal(JsonValueKind.Array, messages.ValueKind);
            Assert.Equal(numberOfMessages, messages.GetArrayLength());
        }

        [Fact]
        [Trait("Category", "Live")]
        public async Task Topic_subscription_peek_messages()
        {
            var topicName = "topic1";
            var subscription = "subscription1";
            var numberOfMessages = 2;

            await SendTestMessages(topicName, numberOfMessages);

            var result = await CallToolAsync(
                "azmcp-servicebus-topic-peek",
                new()
                {
                    { Common.SubscriptionName, Settings.SubscriptionId },
                    { ServiceBus.NamespaceName, _serviceBusNamespace},
                    { ServiceBus.TopicName, topicName },
                    { ServiceBus.SubscriptionName, subscription },
                    { ServiceBus.MaxMessagesName, numberOfMessages.ToString() }
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
    }
}
