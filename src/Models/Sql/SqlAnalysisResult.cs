// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Models.Sql;

/// <summary>
/// Represents the result of a recommendation analysis for a SQL database.
/// </summary>
public sealed record SqlAnalysisResult
{
    /// <summary>
    /// Gets or sets the database name that was analyzed.
    /// </summary>
    [JsonPropertyName("database")]
    public string Database { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the server name containing the database.
    /// </summary>
    [JsonPropertyName("server")]
    public string Server { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp when the analysis was performed.
    /// </summary>
    [JsonPropertyName("analysisTimestamp")]
    public DateTimeOffset AnalysisTimestamp { get; init; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Gets or sets whether the analysis was successful.
    /// </summary>
    [JsonPropertyName("analysisSuccessful")]
    public bool AnalysisSuccessful { get; init; } = true;

    /// <summary>
    /// Gets or sets any analysis summary message.
    /// </summary>
    [JsonPropertyName("analysisSummary")]
    public string AnalysisSummary { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the status of each advisor that was checked.
    /// </summary>
    [JsonPropertyName("advisorStatuses")]
    public List<SqlAdvisorStatus> AdvisorStatuses { get; init; } = new();

    /// <summary>
    /// Gets or sets the number of tables analyzed.
    /// </summary>
    [JsonPropertyName("tablesAnalyzed")]
    public int TablesAnalyzed { get; init; }

    /// <summary>
    /// Gets or sets the number of advisors checked.
    /// </summary>
    [JsonPropertyName("advisorsChecked")]
    public int AdvisorsChecked { get; init; }

    /// <summary>
    /// Gets or sets the list of recommendations found.
    /// </summary>
    [JsonPropertyName("recommendations")]
    public List<SqlRecommendation> Recommendations { get; init; } = new();

    /// <summary>
    /// Gets or sets the total number of recommendations found.
    /// </summary>
    [JsonPropertyName("totalRecommendations")]
    public int TotalRecommendations => Recommendations.Count;

    /// <summary>
    /// Gets or sets whether there are any recommendations.
    /// </summary>
    [JsonPropertyName("hasRecommendations")]
    public bool HasRecommendations => Recommendations.Count > 0;
}
