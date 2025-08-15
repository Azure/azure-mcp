// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.CloudArchitect.Options;

/// <summary>
/// Represents the different architecture tiers.
/// </summary>
public class ArchitectureDesignTiers
{
    public List<string> Infrastructure { get; set; } = new();

    public List<string> Platform { get; set; } = new();

    public List<string> Application { get; set; } = new();

    public List<string> Data { get; set; } = new();

    public List<string> Security { get; set; } = new();

    public List<string> Operations { get; set; } = new();
}
