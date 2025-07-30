// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Monitor.Options.AppInsights.WebTests;

public class WebTestsGetOptions : BaseAppInsightsOptions
{
    [JsonPropertyName(MonitorOptionDefinitions.WebTestResourceName)]
    public string? ResourceName { get; set; }
}
