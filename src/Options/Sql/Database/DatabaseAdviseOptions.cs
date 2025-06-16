// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Sql.Database;

/// <summary>
/// Options for getting SQL advisor recommendations for a database.
/// </summary>
public sealed class DatabaseAdviseOptions : BaseDatabaseOptions
{
    /// <summary>
    /// The name of the table to get recommendations for.
    /// </summary>
    [JsonPropertyName(OptionDefinitions.Sql.TableName)]
    public string? TableName { get; set; }    /// <summary>
                                              /// The minimum impact threshold for recommendations.
                                              /// </summary>
    [JsonPropertyName(OptionDefinitions.Sql.MinimumImpactName)]
    public int? MinimumImpact { get; set; }

    /// <summary>
    /// The type of advisor to filter recommendations by.
    /// </summary>
    [JsonPropertyName(OptionDefinitions.Sql.AdvisorTypeName)]
    public string? AdvisorType { get; set; }
}
