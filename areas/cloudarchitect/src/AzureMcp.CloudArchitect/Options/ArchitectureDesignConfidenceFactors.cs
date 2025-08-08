// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.CloudArchitect.Options;

/// <summary>
/// Confidence factors for the architecture design.
/// </summary>
public class ArchitectureDesignConfidenceFactors
{
    [JsonPropertyName("explicitRequirementsCoverage")]
    public double ExplicitRequirementsCoverage { get; set; }

    [JsonPropertyName("implicitRequirementsCertainty")]
    public double ImplicitRequirementsCertainty { get; set; }

    [JsonPropertyName("assumptionRisk")]
    public double AssumptionRisk { get; set; }
}
