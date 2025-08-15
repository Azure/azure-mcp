// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.CloudArchitect.Commands.Design;
using AzureMcp.CloudArchitect.Models;
using AzureMcp.CloudArchitect.Options;

namespace AzureMcp.CloudArchitect;

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = true)]
[JsonSerializable(typeof(CloudArchitectResponseObject))]
[JsonSerializable(typeof(CloudArchitectDesignResponse))]
[JsonSerializable(typeof(ArchitectureDesignToolState))]
[JsonSerializable(typeof(ArchitectureDesignTiers))]
[JsonSerializable(typeof(ArchitectureDesignRequirements))]
[JsonSerializable(typeof(ArchitectureDesignRequirement))]
[JsonSerializable(typeof(ArchitectureDesignConfidenceFactors))]
[JsonSerializable(typeof(RequirementImportance))]
public partial class CloudArchitectJsonContext : JsonSerializerContext
{
}
