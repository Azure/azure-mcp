using System.Text.Json.Serialization;
using AzureMcp.Commands.Subscription;

namespace AzureMcp;

[JsonSerializable(typeof(SubscriptionListCommand.SubscriptionListCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class SubscriptionJsonContext : JsonSerializerContext
{

}
