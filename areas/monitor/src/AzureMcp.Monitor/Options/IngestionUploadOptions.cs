// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Options;

namespace AzureMcp.Monitor.Options;

public class IngestionUploadOptions : SubscriptionOptions
{
    [JsonPropertyName(MonitorOptionDefinitions.DataCollectionRuleName)]
    public string? DataCollectionRule { get; set; }

    [JsonPropertyName(MonitorOptionDefinitions.LogDataName)]
    public string? LogData { get; set; }

    [JsonPropertyName(MonitorOptionDefinitions.StreamNameName)]
    public string? StreamName { get; set; }

    [JsonPropertyName(MonitorOptionDefinitions.IngestionEndpointName)]
    public string? IngestionEndpoint { get; set; }
}
