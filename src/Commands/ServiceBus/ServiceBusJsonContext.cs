// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Commands.ServiceBus.Queue;
using AzureMcp.Commands.ServiceBus.Topic;

namespace AzureMcp.Commands.ServiceBus;

[JsonSerializable(typeof(QueuePeekCommand.QueuePeekCommandResult))]
[JsonSerializable(typeof(QueueDetailsCommand.QueueDetailsCommandResult))]
[JsonSerializable(typeof(SubscriptionPeekCommand.SubscriptionPeekCommandResult))]
[JsonSerializable(typeof(SubscriptionDetailsCommand.SubscriptionDetailsCommandResult))]
[JsonSerializable(typeof(TopicDetailsCommand.TopicDetailsCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
internal sealed partial class ServiceBusJsonContext : JsonSerializerContext
{
}
