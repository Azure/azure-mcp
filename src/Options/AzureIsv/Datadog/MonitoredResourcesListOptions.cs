// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.AzureIsv.Datadog;

public class MonitoredResourcesListOptions : SubscriptionOptions
{
    [JsonPropertyName(OptionDefinitions.Datadog.DatadogResourceParam)]
    public string? DatadogResource { get; set; }
}
