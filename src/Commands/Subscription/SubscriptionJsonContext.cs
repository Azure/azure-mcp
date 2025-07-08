// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.Subscription.Commands;

namespace AzureMcp;

[JsonSerializable(typeof(SubscriptionListCommand.SubscriptionListCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class SubscriptionJsonContext : JsonSerializerContext
{

}
