// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.CloudArchitect.Commands.Design;

namespace AzureMcp.CloudArchitect;

[JsonSourceGenerationOptions(WriteIndented = true, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(List<string>))]
public partial class CloudArchitectJsonContext : JsonSerializerContext
{
}
