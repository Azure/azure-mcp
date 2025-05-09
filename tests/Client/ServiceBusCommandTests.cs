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
        private const string QUEUE_NAME = "queue1";

        private readonly string _serviceBusNamespace;

        public ServiceBusCommandTests(McpClientFixture mcpClient, LiveTestSettingsFixture liveTestSettings, ITestOutputHelper output) : base(mcpClient, liveTestSettings, output)
        {
            _serviceBusNamespace = $"{Settings.ResourceBaseName}.servicebus.windows.net";
        }

        [Fact]
        [Trait("Category", "Live")]
        public async Task Should_peek_messages()
        {
            var numberOfMessages = 2;

            var credentials = new CustomChainedCredential(Settings.TenantId);
            await using (var client = new ServiceBusClient(_serviceBusNamespace, credentials))
            await using (var sender = client.CreateSender(QUEUE_NAME))
            {
                var batch = await sender.CreateMessageBatchAsync(TestContext.Current.CancellationToken);

                for (int i = 0; i < numberOfMessages; i++)
                {
                    Assert.True(batch.TryAddMessage(new ServiceBusMessage("Message " + i)),
                        $"Unable to add message #{i} to batch.");
                }

                await sender.SendMessagesAsync(batch, TestContext.Current.CancellationToken);
            }

            var result = await CallToolAsync(
                "azmcp-servicebus-queue-peek",
                new()
                {
                    { "subscription", Settings.SubscriptionId },
                    { ServiceBus.QueueName, QUEUE_NAME },
                    { ServiceBus.NamespaceName,  _serviceBusNamespace},
                    { ServiceBus.MaxMessagesName, numberOfMessages.ToString() }
                });

            var messages = result.AssertProperty("messages");
            Assert.Equal(JsonValueKind.Array, messages.ValueKind);
            Assert.Equal(numberOfMessages, messages.GetArrayLength());
        }
    }
}
