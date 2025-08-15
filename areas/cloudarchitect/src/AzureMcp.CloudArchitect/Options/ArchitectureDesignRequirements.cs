// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.CloudArchitect.Options;

/// <summary>
/// Contains all requirements for the architecture design.
/// </summary>
public class ArchitectureDesignRequirements
{
    public List<ArchitectureDesignRequirement> Explicit { get; set; } = new();

    public List<ArchitectureDesignRequirement> Implicit { get; set; } = new();

    public List<ArchitectureDesignRequirement> Assumed { get; set; } = new();
}
