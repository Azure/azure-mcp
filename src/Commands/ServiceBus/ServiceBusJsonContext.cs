using System.Text.Json.Serialization;
using AzureMcp.Commands.ServiceBus.Queue;
using AzureMcp.Commands.ServiceBus.Subscription;

namespace AzureMcp.Commands.ServiceBus;

[JsonSerializable(typeof(QueuePeekCommand.QueuePeekCommandResult))]
[JsonSerializable(typeof(SubscriptionPeekCommand.SubscriptionPeekCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class ServiceBusJsonContext : JsonSerializerContext
{
}
