// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Monitor.Options.WebTests;

public class WebTestsGetOptions : BaseMonitorOptions
{
    [JsonPropertyName(MonitorOptionDefinitions.WebTestResourceName)]
    public string? ResourceName { get; set; }
}
