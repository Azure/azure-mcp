// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.ResourceHealth.Commands.AvailabilityStatus;
using AzureMcp.ResourceHealth.Commands.ResourceEvents;
using AzureMcp.ResourceHealth.Commands.ServiceHealthEvents;
using AzureMcp.ResourceHealth.Models;

namespace AzureMcp.ResourceHealth.Commands;

[JsonSerializable(typeof(AvailabilityStatusGetCommand.AvailabilityStatusGetCommandResult))]
[JsonSerializable(typeof(AvailabilityStatusListCommand.AvailabilityStatusListCommandResult))]
[JsonSerializable(typeof(ResourceEventsGetCommand.ResourceEventsGetCommandResult))]
[JsonSerializable(typeof(ServiceHealthEventsListCommand.ServiceHealthEventsListCommandResult))]
[JsonSerializable(typeof(AzureMcp.ResourceHealth.Models.AvailabilityStatus), TypeInfoPropertyName = "AvailabilityStatusModel")]
[JsonSerializable(typeof(AzureMcp.ResourceHealth.Models.ServiceHealthEvent), TypeInfoPropertyName = "ServiceHealthEventModel")]
[JsonSerializable(typeof(ServiceHealthEventsResponse))]
[JsonSerializable(typeof(ServiceHealthEventData))]
[JsonSerializable(typeof(ServiceHealthEventProperties))]
[JsonSerializable(typeof(ServiceHealthImpact))]
[JsonSerializable(typeof(ServiceHealthImpactedService))]
[JsonSerializable(typeof(ServiceHealthImpactedRegion))]
[JsonSerializable(typeof(ServiceHealthImpactedResource))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault)]
internal sealed partial class ResourceHealthJsonContext : JsonSerializerContext;
