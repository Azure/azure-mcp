// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Models.Sql;

/// <summary>
/// Represents a recommendation for a SQL database from Azure SQL Database advisors.
/// </summary>
public sealed record SqlRecommendation
{
    /// <summary>
    /// Gets or sets the name of the recommendation.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the recommendation.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the estimated impact score of implementing this recommendation.
    /// </summary>
    [JsonPropertyName("impact")]
    public int Impact { get; init; }

    /// <summary>
    /// Gets or sets the table name for which this recommendation applies (if applicable).
    /// </summary>
    [JsonPropertyName("tableName")]
    public string TableName { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the SQL command to implement the recommendation (if applicable).
    /// </summary>
    [JsonPropertyName("implementationSql")]
    public string ImplementationSql { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the SQL command to revert the recommendation if it exists (if applicable).
    /// </summary>
    [JsonPropertyName("revertSql")]
    public string RevertSql { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets additional implementation details from Azure advisor.
    /// </summary>
    [JsonPropertyName("implementationDetails")]
    public string ImplementationDetails { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the expected performance improvement percentage.
    /// </summary>
    [JsonPropertyName("expectedImprovementPercent")]
    public double? ExpectedImprovementPercent { get; init; }

    /// <summary>
    /// Gets or sets the current status of this recommendation from Azure advisor.
    /// </summary>
    [JsonPropertyName("recommendationStatus")]
    public string RecommendationStatus { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of recommendation (e.g., CreateIndex, DropIndex, ForceLastGoodPlan, DbParameterization).
    /// </summary>
    [JsonPropertyName("recommendationType")]
    public string RecommendationType { get; init; } = string.Empty;
}
