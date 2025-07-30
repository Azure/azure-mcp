// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Extension.Commands;
using AzureMcp.Extension.Models;

namespace AzureMcp;

[JsonSerializable(typeof(AzResult))]
[JsonSerializable(typeof(AzqrReportResult))]
[JsonSerializable(typeof(JsonElement))]
[JsonSerializable(typeof(List<string>))]
[JsonSerializable(typeof(GenerateAzCommandPayload))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class ExtensionJsonContext : JsonSerializerContext
{
}
