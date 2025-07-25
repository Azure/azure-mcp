// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Options;

namespace AzureMcp.AzureIsv.Options.Datadog;

public class MonitoredResourcesListOptions : SubscriptionOptions
{
    [JsonPropertyName(DatadogOptionDefinitions.DatadogResourceParam)]
    public string? DatadogResource { get; set; }
}
