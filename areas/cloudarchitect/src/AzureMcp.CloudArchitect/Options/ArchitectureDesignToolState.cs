// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.CloudArchitect.Options;

/// <summary>
/// The state object for the architecture design tool.
/// </summary>
public class ArchitectureDesignToolState
{
    public List<string> ArchitectureComponents { get; set; } = new();

    public ArchitectureDesignTiers ArchitectureTiers { get; set; } = new();

    public string Thought { get; set; } = string.Empty;

    public string SuggestedHint { get; set; } = string.Empty;

    public ArchitectureDesignRequirements Requirements { get; set; } = new();

    public ArchitectureDesignConfidenceFactors ConfidenceFactors { get; set; } = new();
}
