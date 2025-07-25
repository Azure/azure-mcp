// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.AzureIsv.Commands.Datadog;

namespace AzureMcp.AzureIsv.Commands.Datadog;

[JsonSerializable(typeof(MonitoredResourcesListCommand.MonitoredResourcesListResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class DatadogJsonContext : JsonSerializerContext;
