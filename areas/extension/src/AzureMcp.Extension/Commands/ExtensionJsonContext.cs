// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Extension.Commands;
using Microsoft.AspNetCore.Http;

namespace AzureMcp;

[JsonSerializable(typeof(AzqrReportResult))]
[JsonSerializable(typeof(JsonElement))]
[JsonSerializable(typeof(List<string>))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
public partial class ExtensionJsonContext : JsonSerializerContext
{

}
