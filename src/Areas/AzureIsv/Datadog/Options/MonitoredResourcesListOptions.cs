// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Options;

namespace AzureMcp.Areas.AzureIsv.Datadog.Options;

public class MonitoredResourcesListOptions : SubscriptionOptions
{
    [JsonPropertyName(DatadogOptionDefinitions.DatadogResourceParam)]
    public string? DatadogResource { get; set; }
}
