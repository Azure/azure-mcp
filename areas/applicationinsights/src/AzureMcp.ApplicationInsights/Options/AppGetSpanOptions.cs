// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.ApplicationInsights.Options
{
    public class AppGetSpanOptions : BaseAppOptions
    {
        /// <summary>
        /// The start time of the investigation in ISO format (optional).
        /// </summary>
        [JsonPropertyName(ApplicationInsightsOptionDefinitions.StartTimeName)]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// The end time of the investigation in ISO format (optional).
        /// </summary>
        [JsonPropertyName(ApplicationInsightsOptionDefinitions.EndTimeName)]
        public DateTime EndTime { get; set; }

        [JsonPropertyName(ApplicationInsightsOptionDefinitions.ItemIdName)]
        public string? ItemId { get; set; }

        [JsonPropertyName(ApplicationInsightsOptionDefinitions.ItemTypeName)]
        public string? ItemType { get; set; }
    }
}
