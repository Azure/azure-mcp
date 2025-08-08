// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.CloudArchitect.Models;

public record ArchitectureRecommendation(
    string Component,
    string Service,
    string Reasoning,
    string Configuration,
    List<string> Alternatives
);

public record SecurityConsideration(
    string Area,
    string Recommendation,
    string Implementation
);

public record CostOptimization(
    string Service,
    string Recommendation,
    string EstimatedSavings
);

public record ArchitectureDesign(
    string OverallArchitecture,
    List<ArchitectureRecommendation> Components,
    List<SecurityConsideration> Security,
    List<CostOptimization> CostOptimizations,
    string DeploymentConsiderations,
    string MonitoringStrategy,
    List<string> NextSteps
);
