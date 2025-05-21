// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.AzureIsv.Datadog;

public class MonitoredResourcesListArguments : SubscriptionArguments
{
    [JsonPropertyName(ArgumentDefinitions.Datadog.DatadogResourceName)]
    public string? DatadogResource { get; set; }
}
