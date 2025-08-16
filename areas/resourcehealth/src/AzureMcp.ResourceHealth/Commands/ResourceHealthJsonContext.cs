// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.ResourceHealth.Commands.AvailabilityStatus;

namespace AzureMcp.ResourceHealth.Commands;

[JsonSerializable(typeof(AvailabilityStatusGetCommand.AvailabilityStatusGetCommandResult))]
[JsonSerializable(typeof(AvailabilityStatusListCommand.AvailabilityStatusListCommandResult))]
[JsonSerializable(typeof(AzureMcp.ResourceHealth.Models.AvailabilityStatus), TypeInfoPropertyName = "AvailabilityStatusModel")]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault)]
internal sealed partial class ResourceHealthJsonContext : JsonSerializerContext;
