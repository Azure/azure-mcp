// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.CloudArchitect.Options;

/// <summary>
/// Represents the different architecture tiers.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ArchitectureTier
{
    Infrastructure,
    Platform,
    Application,
    Data,
    Security,
    Operations
}
