// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.ApplicationInsights.Models;

public record AppCorrelateDataSet
{
    /// <summary>
    /// The name of the dataset
    /// </summary>
    [JsonPropertyName("table")]
    public string Table { get; set; } = string.Empty;
    /// <summary>
    /// The list of filters applied to this dataset
    /// </summary>
    [JsonPropertyName("filters")]
    public string[] Filters { get; set; } = Array.Empty<string>();
    /// <summary>
    /// The split by dimension for this dataset
    /// </summary>
    [JsonPropertyName("splitBy")]
    public string SplitBy { get; set; } = string.Empty;

    /// <summary>
    /// The aggregation function to apply to the dataset. Valid values are 'Count', 'Average' and '95thPercentile'.
    /// </summary>
    [JsonPropertyName("aggregation")]
    public string Aggregation { get; set; } = "Count";
}
