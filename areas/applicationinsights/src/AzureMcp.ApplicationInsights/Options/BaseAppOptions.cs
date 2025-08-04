// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Options;

namespace AzureMcp.ApplicationInsights.Options;

/// <summary>
/// Base options for all Application Insights App commands
/// </summary>
public class BaseAppOptions : SubscriptionOptions, IAppOptions
{
    /// <summary>
    /// The name of the Application Insights resource.
    /// </summary>
    [JsonPropertyName(ApplicationInsightsOptionDefinitions.ResourceNameName)]
    public string? ResourceName { get; set; }

    /// <summary>
    /// The resource ID of the Application Insights resource.
    /// </summary>
    [JsonPropertyName(ApplicationInsightsOptionDefinitions.ResourceIdName)]
    public string? ResourceId { get; set; }
}
