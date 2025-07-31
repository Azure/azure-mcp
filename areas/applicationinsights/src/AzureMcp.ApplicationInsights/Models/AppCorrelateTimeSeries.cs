// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Helpers;

namespace AzureMcp.ApplicationInsights.Models;

public class AppCorrelateTimeSeries
{
    [JsonPropertyName("label")]
    public string Label { get; set; } = string.Empty;

    [JsonPropertyName("timeSeries")]
    [JsonConverter(typeof(RoundedDoubleArrayConverter))]
    public double[] Data { get; set; } = Array.Empty<double>();
}
