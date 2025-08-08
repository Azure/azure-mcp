// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.CloudArchitect.Commands.Design;
using AzureMcp.CloudArchitect.Models;

namespace AzureMcp.CloudArchitect;

[JsonSourceGenerationOptions(WriteIndented = true, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(DesignCommandResult))]
[JsonSerializable(typeof(ArchitectureDesign))]
[JsonSerializable(typeof(ArchitectureRecommendation))]
[JsonSerializable(typeof(SecurityConsideration))]
[JsonSerializable(typeof(CostOptimization))]
public partial class CloudArchitectJsonContext : JsonSerializerContext
{
}
