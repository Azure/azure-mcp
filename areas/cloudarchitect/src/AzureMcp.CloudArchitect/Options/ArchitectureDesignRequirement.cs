// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.CloudArchitect.Options;

/// <summary>
/// Represents a single architecture design requirement.
/// </summary>
public class ArchitectureDesignRequirement
{
    public string Category { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Source { get; set; } = string.Empty;

    public RequirementImportance Importance { get; set; }

    public double Confidence { get; set; }
}
