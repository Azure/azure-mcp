// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.CloudArchitect.Options;

/// <summary>
/// Confidence factors for the architecture design.
/// </summary>
public class ArchitectureDesignConfidenceFactors
{
    public double ExplicitRequirementsCoverage { get; set; }

    public double ImplicitRequirementsCertainty { get; set; }

    public double AssumptionRisk { get; set; }
}
