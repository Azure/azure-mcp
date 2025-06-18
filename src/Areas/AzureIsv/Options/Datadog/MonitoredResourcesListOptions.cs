// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;
using AzureMcp.Options;

namespace AzureMcp.Areas.AzureIsv.Options.Datadog;

public class MonitoredResourcesListOptions : SubscriptionOptions
{
    [JsonPropertyName(OptionDefinitions.Datadog.DatadogResourceParam)]
    public string? DatadogResource { get; set; }
}
