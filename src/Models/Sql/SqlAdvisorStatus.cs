// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Models.Sql;

/// <summary>
/// Represents the status of a SQL database advisor.
/// </summary>
public sealed record SqlAdvisorStatus
{
    /// <summary>
    /// Gets or sets the name of the advisor.
    /// </summary>
    [JsonPropertyName("advisorName")]
    public string AdvisorName { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the recommendations status of the advisor.
    /// </summary>
    [JsonPropertyName("recommendationsStatus")]
    public string RecommendationsStatus { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the auto-execute status of the advisor.
    /// </summary>
    [JsonPropertyName("autoExecuteStatus")]
    public string AutoExecuteStatus { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the number of recommended actions for this advisor.
    /// </summary>
    [JsonPropertyName("recommendedActionsCount")]
    public int RecommendedActionsCount { get; init; }

    /// <summary>
    /// Gets or sets whether this advisor has recommendations.
    /// </summary>
    [JsonPropertyName("hasRecommendations")]
    public bool HasRecommendations { get; init; }

    /// <summary>
    /// Gets or sets whether this advisor's recommendations are supported by the current implementation.
    /// </summary>
    [JsonPropertyName("isSupported")]
    public bool IsSupported { get; init; }

    /// <summary>
    /// Gets or sets additional notes about this advisor's status.
    /// </summary>
    [JsonPropertyName("notes")]
    public string Notes { get; init; } = string.Empty;
}
