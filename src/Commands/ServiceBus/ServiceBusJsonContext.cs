using System.Text.Json.Serialization;
using AzureMcp.Commands.ServiceBus.Queue;

namespace AzureMcp.Commands.ServiceBus;

[JsonSerializable(typeof(QueuePeekCommand.QueuePeekCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class ServiceBusJsonContext : JsonSerializerContext
{
}
